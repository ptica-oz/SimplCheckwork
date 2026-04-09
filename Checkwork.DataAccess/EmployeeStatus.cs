using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class EmployeeStatus
{
    public int IdemployeeStatus { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
