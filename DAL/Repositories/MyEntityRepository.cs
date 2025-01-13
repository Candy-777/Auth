using CustomExceptions;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class MyEntityRepository : IMyEntityRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<MyEntity> _dbSet;

        public MyEntityRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<MyEntity>();
        }
        public async Task<MyEntity> Create(MyEntity entity)
        {
            _ = _dbSet.AddAsync(entity);
            await Save();
            return entity;
        }

        public async Task<bool> Delete(Guid userId,Guid Id)
        {
            var entity = await _dbSet.Where(x=> x.Id == Id && x.UserId == Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new NotFoundException($"Entity not found or not owned by this user");
            }
            _dbSet.Remove(entity);
            await Save();
            return true;
        }

        public async Task<IEnumerable<MyEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MyEntity>> GetAllForUser(Guid userId)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAllForUser(Guid userId)
        {
            // стоит ли так делать ?
            await _dbSet.Where(x=>x.UserId == userId).ExecuteDeleteAsync();
            return true;
        }

    }
}
