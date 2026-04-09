using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Smkdocument2DepartamentPost
{
    public string Smkdocument2DepartamentPostId { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public int PostId { get; set; }

    public int DepartamentId { get; set; }

    public virtual Departament Departament { get; set; } = null!;

    public virtual Smkdocument Document { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
