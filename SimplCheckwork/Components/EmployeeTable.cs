namespace SimplCheckwork.Components
{
    using System.Text;
    using Checkwork.BusinessComponents.BusinessComponents;
    using Checkwork.BusinessComponents.Helpers;
    using Checkwork.DataAccess.Dto;
    using Microsoft.AspNetCore.Mvc;
    using SimplCheckwork.Models.Employees;

    public class EmployeeTable : ViewComponent
    {
        public IViewComponentResult Invoke(EmployeeTableViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
