namespace Checkwork.DataAccess.Dto
{
    using System;

    public sealed class EmployeeDto
    {
        public int IDEmployee { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronimyc { get; set; }

        public string Login { get; set; }

        public bool Registrator { get; set; }

        public bool Hidden { get; set; }

        public int IDEmployeeStatus { get; set; }

        public TimeOnly ArrivalTime { get; set; }

        public string JiraLogin { get; set; }
    }
}
