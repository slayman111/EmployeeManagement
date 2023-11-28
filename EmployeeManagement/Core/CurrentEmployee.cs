using EmployeeManagement.Model.Entity;

namespace EmployeeManagement.Core;

public class CurrentEmployee
{
    public static Employee? Employee { get; set; }

    static CurrentEmployee()
    {
        Employee = default!;
    }

    public static void Reset() => Employee = default!;
}
