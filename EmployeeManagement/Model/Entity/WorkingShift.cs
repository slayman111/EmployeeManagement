using System;

namespace EmployeeManagement.Model.Entity;

public class WorkingShift
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public short Hours { get; set; }

    public string? Note { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
