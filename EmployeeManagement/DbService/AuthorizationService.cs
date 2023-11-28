using System.Threading.Tasks;
using EmployeeManagement.DbService.Exception;
using EmployeeManagement.Model.Entity;
using EmployeeManagement.Model.Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DbService;

internal static class AuthorizationService
{
    public static async Task<Admin> LoginAsync(string? login, string? password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            throw new EmptyCredentialsException();

        await using var context = new EmployeeDbContext();

        var user = await context.Admins.FirstOrDefaultAsync(u =>
            u.Login.Equals(login) && u.Password.Equals(password));

        if (user is null) throw new UserNotFoundException();

        return user;
    }
}
