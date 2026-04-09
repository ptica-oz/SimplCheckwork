using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Smkdocument2Post
{
    public string Smkdocument2PostId { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public int PostId { get; set; }

    public virtual Smkdocument Document { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
