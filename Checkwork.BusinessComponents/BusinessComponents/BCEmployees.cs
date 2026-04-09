namespace Checkwork.BusinessComponents.BusinessComponents
{
    using System;
    using System.Runtime.CompilerServices;
    using Checkwork.BusinessComponents.Dto;
    using Checkwork.DataAccess;
    using Checkwork.DataAccess.Dto;
    using Checkwork.DataAccess.Extentions;
    using Microsoft.EntityFrameworkCore;

    public class BCEmployees
    {
        private const string SqlGetEmployeeStatuses = @"
        with WE as
        (
        select * from WorkEvent 
        where DateEvent >= '{1}'
        )

        select Employee.IDEmployee IDEmployee, 

        IIF(CONVERT(VARCHAR(5),cast(p.BirthDate as date), 104) =
        CONVERT(VARCHAR(5),cast('2017-09-01 13:00:00' as date), 104), 1, 0) IsBirthDate

		, coalesce(d.Name,'') DepartamentName
		, coalesce(l.Name,'') RoomName

        , IIF(LastEventTime is NULL, NULL, (select e.Name from EventType e where T.IDEventType = e.IDEventType)) EventName
        , IIF(LastEventTime is NULL, NULL, (select e.Code from EventType e where T.IDEventType = e.IDEventType)) EventCode

        , LastEventTime

        , IIF(LastEventTime is NULL, NULL, (select o.Name from OffType o where T.IDOffType = o.IDOffType)) OffTypeName
        , IIF(LastEventTime is NULL, NULL, (select o.Code from OffType o where T.IDOffType = o.IDOffType)) OffTypeCode 
 

        , IIF(LastEventTime is NULL, NULL, T.RegEmployee) RegEmployee
        , IIF(LastEventTime is NULL, NULL, T.Note) Note
        , IIF(LastEventTime is NULL, 0, T.Remote) Remote

        , FirstArriveTime

	        from Employee left join EmployeeProperties p on Employee.IDEmployee = p.EmployeeId
			left join Departament d on p.DepartamentId = d.DepartamentId
			left join [Location] l on p.LocationId = l.LocationId

	        CROSS APPLY

	        (select A.IDEmployee, A.IDEventType, 

	        IIF(IDEventType > 2, 

		        'c ' +
		        (select top 1 CONVERT(VARCHAR(10),w2.DateEvent, 104) + ' ' + CONVERT(VARCHAR(5),w2.DateEvent, 108) from WE w2 
		        where w2.IDEmployee = Employee.IDEmployee and w2.IDOffType = A.IDOFFType 
		        and IIF(A.IDOFFType = 1, IIF(w2.Note = A.Note, 1, 0), 1) = 1
		        and w2.DateEvent > 

                (select coalesce((select top 1 DateEvent  from (select * from WE where IDEmployee = Employee.IDEmployee and DateEvent <= A.DateEvent) f
			    where IIF(IDOffType is null or IDOffType != w2.IDOffType, 1, 0) = 1 
			    order by DateEvent desc), (select min(DateEvent) from WE where IDEmployee = Employee.IDEmployee and month(DateEvent) <= month(A.DateEvent))))

		        order by w2.DateEvent asc)
		        + ' по ' +
		        (select top 1 CONVERT(VARCHAR(10),w3.DateEvent, 104) + ' ' + CONVERT(VARCHAR(5),w3.DateEvent, 108) from WE w3 
		        where w3.IDEmployee = Employee.IDEmployee and w3.IDOffType = A.IDOFFType 
		        and IIF(A.IDOFFType = 1, IIF((w3.Note is NULL and A.Note is null) or w3.Note = A.Note, 1, 0), 1) = 1 and w3.DateEvent > A.DateEvent
		        order by w3.DateEvent desc
		        )

		        , IIF(A.DateEvent >= cast('{0}' as date), CONVERT(VARCHAR(5),A.DateEvent, 108), NULL)
	        ) LastEventTime, 

	        A.IDOffType,
	        A.RegEmployee,
	        A.Note,
	        A.Remote,

	        (select top 1 CONVERT(VARCHAR(5), w.DateEvent, 108)
	        from WE w 
	        where w.IDEmployee = Employee.IDEmployee and w.IDEventType = 1
		        and w.DateEvent < '{0}' and w.DateEvent >= cast('{0}' as date) 
	        order by w.DateEvent asc) FirstArriveTime

	        from  
		        (select top 1 w.*
		        from WE w 
		        where w.IDEmployee = Employee.IDEmployee and w.DateEvent < '{0}'
		        order by w.DateEvent desc) A

	        ) T

        where Employee.Hidden = 0";

        public static IEnumerable<EmployeeStatusView> GetEmployeeStatuses(DateTime date)
        {
            IEnumerable<EmployeeStatusView> result = new List<EmployeeStatusView>();

            using (var entities = new CheckworkCurContext())
            {
                var begin = new DateTime(date.Year, date.Month, 1).AddMonths(-6);
                var query = string.Format(SqlGetEmployeeStatuses, date.ToString("o").Substring(0, 19), begin.ToString("o").Substring(0, 19));
                var formattableQuery = FormattableStringFactory.Create(query);                
                result = entities.Database.SqlQuery<EmployeeStatusView>(formattableQuery).ToList();
            }

            return result;
        }

        public static IEnumerable<EmployeeDto> GetEmployees()
        {
            using (var entities = new CheckworkCurContext())
            {
                var list = new List<EmployeeDto>();
                foreach (var employee in entities.Employees)
                {
                    list.Add(EmployeeExtension.Convert(employee));
                }
                return list;
            }
        }
    }
}
