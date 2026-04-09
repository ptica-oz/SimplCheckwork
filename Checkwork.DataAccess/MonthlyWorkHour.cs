using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class MonthlyWorkHour
{
    public int Id { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Hours { get; set; }
}
