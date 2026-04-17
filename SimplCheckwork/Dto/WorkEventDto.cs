namespace SimplCheckwork.Dto
{
    internal class WorkEventDto
    {
        public int IdworkEvent { get; set; }

        public int Idemployee { get; set; }

        public int? IdeventType { get; set; }

        public DateTime? DateEvent { get; set; }

        public int? IdoffType { get; set; }

        public string? Note { get; set; }

        public string RegEmployee { get; set; } = null!;

        public DateTime AutomatedDate { get; set; }

        public int? PrevEvent { get; set; }

        public int? JoinWithGoaway { get; set; }

        public bool Remote { get; set; }
    }
}
