using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class WorkEvent
{
    public int IdworkEvent { get; set; }

    public int Idemployee { get; set; }

    public int? IdeventType { get; set; }

    public DateTime? DateEvent { get; set; }

    public int? IdoffType { get; set; }

    public string? Note { get; set; }

    public string RegEmployee { get; set; } = null!;

    public DateTime AutomatedDate { get; set; }

    public int? PrevEvent { get; set; }

    public int? JoinWithGoaway { get; set; }

    public bool Remote { get; set; }

    public virtual Employee IdemployeeNavigation { get; set; } = null!;

    public virtual EventType? IdeventTypeNavigation { get; set; }

    public virtual OffType? IdoffTypeNavigation { get; set; }

    public virtual ICollection<WorkEvent> InverseJoinWithGoawayNavigation { get; set; } = new List<WorkEvent>();

    public virtual ICollection<WorkEvent> InversePrevEventNavigation { get; set; } = new List<WorkEvent>();

    public virtual WorkEvent? JoinWithGoawayNavigation { get; set; }

    public virtual WorkEvent? PrevEventNavigation { get; set; }
}
