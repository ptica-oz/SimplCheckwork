namespace SimplCheckwork.BusinessComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Checkwork.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using SimplCheckwork.BusinessComponents.Dto;
    using SimplCheckwork.Enums;
    using SimplCheckwork.Helpers;

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

        private const string GetDataByPrevEventQuery = @"SELECT * dbo.WorkEvent WHERE (PrevEvent = '{0}')";

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

                var now = DateTime.Now;
                if (endDate == DateTime.Now.Date) 
                {
                    endDate = endDate.AddHours(now.Hour);
                    endDate = endDate.AddMinutes(now.Minute);
                }
                else
                {
                    endDate = endDate.AddHours(24);
                }

                var query = string.Format(WorkEventByEmployeeAndDateIntervalQuery, employeeId, 
                    startDate.ToString("o").Substring(0, 19), endDate.ToString("o").Substring(0, 19));
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

                    if (eventType?.Code == EventTypeCodes.GoAway)
                    {
                        newItem.CanDelete = true;
                    }
                    else if(eventType?.Code == EventTypeCodes.Arrive)
                    {
                        newItem.CanDelete = GetDataByPrevEvent(workEvent.IdworkEvent).Count() == 0;
                    }
                }
                else if(workEvent.PrevEvent == null)
                {
                    newItem.CanDelete = true;
                    var date = $"C {workEvent.DateEvent?.GetFormattedDate()}";
                    var workEventByParentList = GetDataByPrevEvent(workEvent.IdworkEvent);
                    if (workEventByParentList.Count() == 1)
                    {
                        date += $" до {workEventByParentList.First().DateEvent?.GetFormattedDate()}";
                    }
                }

                result.Add(newItem);
            }

            return result;
        }

        private static IEnumerable<WorkEvent> GetDataByPrevEvent(int prevEventId)
        {
            IEnumerable<WorkEvent> result = new List<WorkEvent>();

            using (var entities = new CheckworkCurContext())
            {
                var query = string.Format(GetDataByPrevEventQuery, prevEventId);
                var formattableQuery = FormattableStringFactory.Create(query);
                result = entities.Database.SqlQuery<WorkEvent>(formattableQuery).ToList();
            }

            return result;
        }
    }
}
