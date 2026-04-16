namespace SimplCheckwork.Models.Employees
{
    internal class EmployeeModel
    {
        public EmployeeModel() { }

        public EmployeeModel(string signature, string status, string time, string dayOffType,
            string registrered, string note, string firstArrival)
        {
            Signature = signature;
            Status = status;
            Time = time;
            DayOffType = dayOffType;
            RegEmployee = registrered;
            Note = note;
            FirstArrival = firstArrival;
        }

        public int EmployeeId { get; set; }

        public string Signature{ get; set; }

        public string Status { get; set; }

        public string Time { get; set; }

        public string DayOffType { get; set; }

        public string RegEmployee { get; set; }

        public string Note { get; set; }

        public string FirstArrival { get; set; }
    }
}
