using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Smkdocument
{
    public string DocumentId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Comment { get; set; }

    public string Location { get; set; } = null!;

    public DateTime PostDate { get; set; }

    public virtual ICollection<Smkdocument2DepartamentPost> Smkdocument2DepartamentPosts { get; set; } = new List<Smkdocument2DepartamentPost>();

    public virtual ICollection<Smkdocument2Employee> Smkdocument2Employees { get; set; } = new List<Smkdocument2Employee>();

    public virtual ICollection<Smkdocument2Post> Smkdocument2Posts { get; set; } = new List<Smkdocument2Post>();
}
