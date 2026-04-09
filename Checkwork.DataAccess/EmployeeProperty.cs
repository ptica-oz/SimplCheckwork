using System;
using System.Collections.Generic;

namespace Checkwork.DataAccess;

public partial class EmployeeProperty
{
    public int EmployeePropertiesId { get; set; }

    public int EmployeeId { get; set; }

    public int? DepartamentId { get; set; }

    public int? PersonelNumber { get; set; }

    public byte[]? Picture { get; set; }

    public int? PostId { get; set; }

    public int? MasterEmployeeId { get; set; }

    public int? LocationId { get; set; }

    public int? CategoryId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? MobilePhoneNumber { get; set; }

    public string? Skype { get; set; }

    public string? Email { get; set; }

    public DateOnly? BirthDate { get; set; }

    public DateOnly? SatrtDate { get; set; }

    public string? Education { get; set; }

    public string? Passport { get; set; }

    public decimal? Pnumber { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Departament? Departament { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual Employee? MasterEmployee { get; set; }

    public virtual Post? Post { get; set; }
}
