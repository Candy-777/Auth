using Domain.Entities;
using MyEntitiesService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEntitiesService
{
    public interface IMyEntityService
    {
        public Task<MyEntity> AddMyEntityAsync(CreateEntityDto createEntityDto);
        public Task<bool> DeleteMyEntityAsync (DeleteEntityDto deleteEntityDto);
        public Task<IEnumerable<MyEntity>> GetMyEntitiesForUserAsync(Guid userId);
        public Task<bool> DeleteAllMyEntitiesAsync(Guid userId);

    }
}
