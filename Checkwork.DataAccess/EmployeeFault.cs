using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class EmployeeFault
{
    public int IdemployeeFault { get; set; }

    public int Idemployee { get; set; }

    public DateTime Date { get; set; }

    public virtual Employee IdemployeeNavigation { get; set; } = null!;
}
