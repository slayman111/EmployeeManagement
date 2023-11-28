using System.Collections.Generic;

namespace EmployeeManagement.Model.Entity;

public class Specialization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
