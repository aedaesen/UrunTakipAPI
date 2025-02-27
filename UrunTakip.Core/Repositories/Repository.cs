using Fluent.Infrastructure.FluentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrunTakip.Core.Entities;

namespace UrunTakip.Core.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context, DbSet<T> ts)
    {
        _context = context;
        _dbSet = ts;
    }

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
    public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
    public async Task Add(T entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
    public async Task Update(T entity) { _dbSet.Update(entity); await _context.SaveChangesAsync(); }
    public async Task Delete(int id)
    {
        var entity = await GetById(id);
        if (entity != null) { _dbSet.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public IQueryable<Product> GetAllQueryable()
    {
        return (IQueryable<Product>)_dbSet.AsQueryable();
    }

}
