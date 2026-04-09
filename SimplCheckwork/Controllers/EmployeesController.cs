namespace SimplCheckwork.Controllers
{
    using Checkwork.BusinessComponents.BusinessComponents;
    using Checkwork.BusinessComponents.Helpers;
    using Checkwork.DataAccess.Dto;
    using Microsoft.AspNetCore.Mvc;
    using SimplCheckwork.Models.Employees;

    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            var result = new List<EmployeeModel>();

            var employeesStatuses = BCEmployees.GetEmployeeStatuses(DateTime.Now);
            var employeeStatusEvents = employeesStatuses.ToDictionary(k => k.IDEmployee, k => k);

            var tmp = GetEmployeeList().OrderBy(r => r.Surname + r.Name + r.Patronimyc);
            foreach (var employee in tmp)
            {
                var employeeStatusEvent = employeeStatusEvents[employee.IDEmployee];

                var employeeViewModel = new EmployeeModel();
                employeeViewModel.Signature = StringHelper.CombinSignature(employee.Surname, employee.Name, employee.Patronimyc);

                employeeViewModel.Status = employeeStatusEvent.EventName ?? string.Empty;
                employeeViewModel.Time = employeeStatusEvent.LastEventTime ?? string.Empty;
                employeeViewModel.RegEmployee = employeeStatusEvent.RegEmployee ?? string.Empty;
                employeeViewModel.DayOffType = employeeStatusEvent.OffTypeName ?? string.Empty;
                employeeViewModel.Note = employeeStatusEvent.Note ?? string.Empty;
                employeeViewModel.FirstArrival = employeeStatusEvent.FirstArriveTime ?? string.Empty;

                result.Add(employeeViewModel);
            }

            return View(result);
        }

        private IEnumerable<EmployeeDto> GetEmployeeList()
        {
            var list = BCEmployees.GetEmployees().ToList();
            return list.Where(r => !r.Hidden).ToList();
        }
    }
}
