using EmployeeManagement.Model.Entity;

namespace EmployeeManagement.Core;

public static class CurrentUser
{
    public static Admin User { get; set; }

    static CurrentUser()
    {
        User = default!;
    }

    public static void Reset() => User = default!;
}
