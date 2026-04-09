using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class WorkSheet
{
    public int IdworkSheet { get; set; }

    public int Idemployee { get; set; }

    public DateTime Date { get; set; }

    public double? WorkTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public bool ByHand { get; set; }

    public virtual Employee IdemployeeNavigation { get; set; } = null!;

    public virtual ICollection<OffWorkSheet> OffWorkSheets { get; set; } = new List<OffWorkSheet>();
}
