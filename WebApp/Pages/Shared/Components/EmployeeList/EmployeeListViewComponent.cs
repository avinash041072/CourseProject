using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Pages.Shared.Components.EmployeeList
{
    public class EmployeeListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string? filter, int? departmentId)
        {
            return View(EmployeesRepository.GetEmployees(filter, departmentId));
        }
    }
}
