using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class OffWorkSheet
{
    public int IdoffWorkSheet { get; set; }

    public int IdworkSheet { get; set; }

    public int IdoffType { get; set; }

    public double? Value { get; set; }

    public bool ByHand { get; set; }

    public virtual OffType IdoffTypeNavigation { get; set; } = null!;

    public virtual WorkSheet IdworkSheetNavigation { get; set; } = null!;
}
