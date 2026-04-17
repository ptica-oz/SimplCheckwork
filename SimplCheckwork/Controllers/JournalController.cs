namespace SimplCheckwork.Controllers
{
    using Checkwork.BusinessComponents.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using SimplCheckwork.BusinessComponents;
    using SimplCheckwork.Models.Jornal;

    public class JournalController : Controller
    {
        public IActionResult Index(string employeeId)
        {
            var employees = BCEmployees.GetEmployees();
            var employeesSignatures = employees
                .Select(r => new EmployeeItem(r.IDEmployee, StringHelper.CombinSignature(r.Surname, r.Name, r.Patronimyc))).
                OrderBy(r => r.Signature);
            var model = new JournalModel()
            {
                Employees = employeesSignatures,
                SelectedEmployeeId = int.Parse(employeeId)
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult GetJournalByEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return Content("<div class='error'>Дата начала не может быть позже даты окончания</div>");
            }

            var dates = BCJournal.GetReport(employeeId, startDate, endDate);
            return PartialView("_ReportTable", dates);
        }         
    }
}
