using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? ParentLocationId { get; set; }

    public virtual ICollection<EmployeeProperty> EmployeeProperties { get; set; } = new List<EmployeeProperty>();
}
