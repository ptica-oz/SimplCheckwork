namespace SimplCheckwork.Models.Jornal
{
    internal class JournalModel
    {
        public int SelectedEmployeeId { get; set; }

        public EmployeeItem? SelectedEmployee { get; set; }

        public IEnumerable<EmployeeItem> Employees { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<DateTime> Dates { get; set; } = new List<DateTime>();

        public bool IsDatesGenerated { get; set; } = false;
    }

    internal class EmployeeItem 
    {
        public EmployeeItem(int employeeId, string signature)
        {
            EmployeeId = employeeId;
            Signature = signature;
        }

        public int EmployeeId { get; set; }

        public string Signature { get; set; }
    }
}
