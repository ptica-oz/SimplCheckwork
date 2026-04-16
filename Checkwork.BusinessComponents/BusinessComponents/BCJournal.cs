namespace Checkwork.BusinessComponents.BusinessComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using Checkwork.BusinessComponents.Dto;
    using Checkwork.BusinessComponents.Enums;
    using Checkwork.BusinessComponents.Helpers;
    using Checkwork.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class BCJournal
    {
        private const string WorkEventByEmployeeAndDateIntervalQuery = @"SELECT * FROM WorkEvent AS we1 
WHERE IDEmployee = '{0}'
AND ( DateEvent BETWEEN '{1}' AND '{2}'
	OR ( PrevEvent IS NULL
		AND ('{1}' BETWEEN DateEvent 
			AND ( SELECT ISNULL(we2.DateEvent, '{1}') FROM dbo.WorkEvent AS we2 
				WHERE (we2.PrevEvent = we1.IDWorkEvent) 
			)
		)
	)
 )
 ORDER BY DateEvent, PrevEvent DESC";

        public static IEnumerable<WorkEvent> GetWorkEventByEmployeeAndDateInterval(int employeeId, DateTime startDate, DateTime endDate)
        {
            IEnumerable<WorkEvent> result = new List<WorkEvent>();

            using (var entities = new CheckworkCurContext())
            {
                var query = string.Format(WorkEventByEmployeeAndDateIntervalQuery, employeeId, startDate.ToString("o").Substring(0, 19), endDate.ToString("o").Substring(0, 19));
                var formattableQuery = FormattableStringFactory.Create(query);
                result = entities.Database.SqlQuery<WorkEvent>(formattableQuery).ToList();
            }

            return result;
        }

        public static List<JournalResultModel> GetReport(int employeeId, DateTime startDate, DateTime endDate)
        {
            var result = new List<JournalResultModel>();
            var workEvents = new List<WorkEvent>();
            var eventTypes = new List<EventType>();
            var offTypes = new List<OffType>();

            using (var entities = new CheckworkCurContext())
            {
                eventTypes = entities.EventTypes.ToList();
                offTypes = entities.OffTypes.ToList();

                var query = string.Format(WorkEventByEmployeeAndDateIntervalQuery, employeeId, startDate.ToString("o").Substring(0, 19), endDate.ToString("o").Substring(0, 19));
                var formattableQuery = FormattableStringFactory.Create(query);
                workEvents = entities.Database.SqlQuery<WorkEvent>(formattableQuery).ToList();
            }

            foreach (var workEvent in workEvents)
            {
                var eventType = eventTypes.FirstOrDefault(r => r.IdeventType == workEvent.IdeventType);
                var offType = offTypes.FirstOrDefault(r => r.IdoffType == workEvent.IdoffType);

                var newItem = new JournalResultModel();
                newItem.EventName = eventType?.Name;
                newItem.OffType = offType?.Name;
                newItem.Note = workEvent.Note;
                newItem.RegEmployee = workEvent.RegEmployee;
                newItem.RegDateTime = workEvent.AutomatedDate.GetFormattedDate();

                if (eventType?.Code != EventTypeCodes.Absence) 
                {
                    newItem.DateTime = workEvent.DateEvent?.GetFormattedDate();
                }

                result.Add(newItem);
            }

            return result;

        }

    }
}
