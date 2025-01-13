using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMyEntityRepository 
    {

        public Task<IEnumerable<MyEntity>> GetAllForUser(Guid userId);
        public Task<IEnumerable<MyEntity>> GetAll();
        public  Task<MyEntity> Create(MyEntity entity);
        public Task<bool> Delete(Guid userId, Guid entityId);
        public Task<bool> DeleteAllForUser(Guid userId);
        public Task Save();
    }
}
