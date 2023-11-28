using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.DbService.Exception;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DbService;

internal class CrudDbService<TId, TEntity>(DbContext context) : IDisposable where TEntity : class
{
    public async Task<List<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetAsync(TId id) => await FindByIdAsync(id);

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var result = context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        context.Set<TEntity>().Remove(await FindByIdAsync(id));
        await context.SaveChangesAsync();
    }

    private async Task<TEntity> FindByIdAsync(TId id) =>
        await context.FindAsync<TEntity>(id) ?? throw new EntityNotFound();

    public void Dispose() => context.Dispose();
}
