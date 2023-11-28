using System.Collections.Generic;

namespace EmployeeManagement.Model.Entity;

public class Admin
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
