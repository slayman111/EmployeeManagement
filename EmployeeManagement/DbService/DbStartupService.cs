using System.Threading.Tasks;
using EmployeeManagement.Model.Entity;
using EmployeeManagement.Model.Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EmployeeManagement.DbService;

internal static class DbStartupService
{
    public static async Task EnsureCreatedAsync()
    {
        var context = new EmployeeDbContext();
        if (await context.Database.EnsureCreatedAsync()) await InitTestData(context);

        var databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
        await databaseCreator.CreateTablesAsync();
    }

    private static async Task InitTestData(DbContext context)
    {
        var userService = new CrudDbService<int, Admin>(context);
        var specialization = new CrudDbService<int, Specialization>(context);

        await userService.CreateAsync(new Admin
        {
            Login = "log",
            Password = "pass"
        });

        await specialization.CreateAsync(new Specialization
        {
            Name = "Официант"
        });

        await specialization.CreateAsync(new Specialization
        {
            Name = "Бармен"
        });

        await specialization.CreateAsync(new Specialization
        {
            Name = "Повар"
        });
    }
}
