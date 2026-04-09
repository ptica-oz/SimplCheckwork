using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class Smkdocument2Employee
{
    public string Smkdocument2EmployeeId { get; set; } = null!;

    public int EmployeeId { get; set; }

    public string DocumentId { get; set; } = null!;

    public DateTime FamiliarizeDate { get; set; }

    public virtual Smkdocument Document { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
