using System.Xml.Linq;

namespace WebApp.Models
{
    public static class DepartmentsRepository
    {
        private static List<Department> _departments = new List<Department>
        {
            new Department(1, "Sales", "Sales Department"),
            new Department(2, "Engineering", "Engineering Department"),
            new Department(3, "QA", "Quanlity Assurance")
        };

        public static List<Department> GetDepartments(string? filter = null)
        {
            if (string.IsNullOrWhiteSpace(filter)) return _departments;

            return _departments.Where(x => x.Name is not null && x.Name.ToLower().Contains(filter.ToLower())).ToList();
        }

        public static Department? GetDepartmentById(int id)
        {
            return _departments.FirstOrDefault(x => x.Id == id);
        }

        public static void AddDepartment(Department? Department)
        {
            if (Department is not null)
            {
                int maxId = _departments.Max(x => x.Id);
                Department.Id = maxId + 1;
                _departments.Add(Department);
            }
        }

        public static bool UpdateDepartment(Department? Department)
        {
            if (Department is not null)
            {
                var emp = _departments.FirstOrDefault(x => x.Id == Department.Id);
                if (emp is not null)
                {
                    emp.Name = Department.Name;
                    emp.Description = Department.Description;                   

                    return true;
                }
            }

            return false;
        }

        public static bool DeleteDepartment(Department? Department)
        {
            if (Department is not null)
            {
                _departments.Remove(Department);
                return true;
            }

            return false;
        }
    }
}
