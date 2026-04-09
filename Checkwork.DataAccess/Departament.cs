using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Departament
{
    public int DepartamentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? ParentDepartamentId { get; set; }

    public int WorkType { get; set; }

    public virtual ICollection<EmployeeProperty> EmployeeProperties { get; set; } = new List<EmployeeProperty>();

    public virtual ICollection<Departament> InverseParentDepartament { get; set; } = new List<Departament>();

    public virtual Departament? ParentDepartament { get; set; }

    public virtual ICollection<Smkdocument2DepartamentPost> Smkdocument2DepartamentPosts { get; set; } = new List<Smkdocument2DepartamentPost>();
}
