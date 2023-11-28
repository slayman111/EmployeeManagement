using System.Collections.Generic;

namespace EmployeeManagement.Model.Entity;

public class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int SpecializationId { get; set; }

    public string? InsuranceNumber { get; set; }

    public string? MedBookNumber { get; set; }

    public string? EmploymentBookNumber { get; set; }

    public decimal Salary { get; set; }

    public int CreatedById { get; set; }

    public virtual Admin CreatedBy { get; set; } = null!;

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual ICollection<WorkingShift> WorkingShifts { get; set; } = new List<WorkingShift>();
}
