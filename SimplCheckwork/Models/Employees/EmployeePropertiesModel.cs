namespace SimplCheckwork.Models.Employees
{
    public class EmployeePropertiesModel
    {
        public int EmployeeId { get; set; }

        public string? Surname { get; set; }

        public string? Name { get; set; }

        public string? Patronymic { get; set; }

        public bool Hidden { get; set; }
    }
}
