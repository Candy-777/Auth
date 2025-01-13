using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        // пока нигде не используется
        Task<TEntity?> GetById (Guid Id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Create (TEntity entity);
        Task<TEntity> Update (TEntity entity);
        Task<bool> Delete (Guid Id);
    }
}
