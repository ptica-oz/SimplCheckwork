namespace SimplCheckwork.Components
{
    using Microsoft.AspNetCore.Mvc;
    using SimplCheckwork.Models.Employees;

    public class EmployeeTable : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(EmployeeTableViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
