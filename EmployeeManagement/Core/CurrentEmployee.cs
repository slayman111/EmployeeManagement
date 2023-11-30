using EmployeeManagement.Model.Entity;

namespace EmployeeManagement.Core;

public static class CurrentEmployee
{
    public static Employee? Employee { get; set; }

    static CurrentEmployee()
    {
        Employee = default!;
    }

    public static void Reset() => Employee = default!;
}
