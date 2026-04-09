using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Employee
{
    public int Idemployee { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Patronimyc { get; set; }

    public string Login { get; set; } = null!;

    public bool Registrator { get; set; }

    public bool Hidden { get; set; }

    public int IdemployeeStatus { get; set; }

    public TimeOnly? ArrivalTime { get; set; }

    public virtual ICollection<EmployeeFault> EmployeeFaults { get; set; } = new List<EmployeeFault>();

    public virtual ICollection<EmployeeProperty> EmployeePropertyEmployees { get; set; } = new List<EmployeeProperty>();

    public virtual ICollection<EmployeeProperty> EmployeePropertyMasterEmployees { get; set; } = new List<EmployeeProperty>();

    public virtual EmployeeStatus IdemployeeStatusNavigation { get; set; } = null!;

    public virtual ICollection<Smkdocument2Employee> Smkdocument2Employees { get; set; } = new List<Smkdocument2Employee>();

    public virtual ICollection<WorkEvent> WorkEvents { get; set; } = new List<WorkEvent>();

    public virtual ICollection<WorkSheet> WorkSheets { get; set; } = new List<WorkSheet>();
}
