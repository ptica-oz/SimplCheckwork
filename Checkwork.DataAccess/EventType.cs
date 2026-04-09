using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class EventType
{
    public int IdeventType { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<WorkEvent> WorkEvents { get; set; } = new List<WorkEvent>();
}
