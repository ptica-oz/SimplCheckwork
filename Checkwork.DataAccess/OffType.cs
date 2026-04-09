using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class OffType
{
    public int IdoffType { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public virtual ICollection<OffWorkSheet> OffWorkSheets { get; set; } = new List<OffWorkSheet>();

    public virtual ICollection<WorkEvent> WorkEvents { get; set; } = new List<WorkEvent>();
}
