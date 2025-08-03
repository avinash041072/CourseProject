using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Views.Shared.Components.DepartmentList
{
    [ViewComponent]
    public class DepartmentListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string? filter)
        {
            var departments = DepartmentsRepository.GetDepartments(filter);
            return View(departments);
        }


    }
}
