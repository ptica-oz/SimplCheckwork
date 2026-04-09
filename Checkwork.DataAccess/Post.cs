using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Post
{
    public int PostId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeeProperty> EmployeeProperties { get; set; } = new List<EmployeeProperty>();

    public virtual ICollection<Smkdocument2DepartamentPost> Smkdocument2DepartamentPosts { get; set; } = new List<Smkdocument2DepartamentPost>();

    public virtual ICollection<Smkdocument2Post> Smkdocument2Posts { get; set; } = new List<Smkdocument2Post>();
}
