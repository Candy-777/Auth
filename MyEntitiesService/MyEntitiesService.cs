using CustomExceptions;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MyEntitiesService.DTOs;

namespace MyEntitiesService
{
    public class MyEntitiesService : IMyEntityService
    {
        private readonly IMyEntityRepository _myEntityRepository;
        private readonly UserManager<AppUser> _userManager;
        public MyEntitiesService(IMyEntityRepository myEntityRepository, UserManager<AppUser> userManager)
        {            
            _myEntityRepository = myEntityRepository;
            _userManager = userManager;
        }

        public async Task<MyEntity> AddMyEntityAsync(CreateEntityDto createEntityDto)
        {
            var user = await _userManager.FindByIdAsync(createEntityDto.UserId.ToString());

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var newEntity = new MyEntity
            {
                Name = createEntityDto.Name,
                Description = createEntityDto.Description,
                YouTubeUrl = createEntityDto.YouTubeUrl,
                UserId = createEntityDto.UserId,
                User = user
            };
            
            if (user.MyEntities == null)
            {
                user.MyEntities = new List<MyEntity>();
            }
            await _myEntityRepository.Create(newEntity);

            // а нужно ли то что ниже если и так работает?

            //user.MyEntities.Add(newEntity);
            //await _userManager.UpdateAsync(user);  

            return newEntity;
        }

        public async Task<IEnumerable<MyEntity>> GetMyEntitiesForUserAsync(Guid userId)
        {
            return await _myEntityRepository.GetAllForUser(userId);
        }

        public async Task<bool> DeleteAllMyEntitiesAsync(Guid userId)
        {
           return await _myEntityRepository.DeleteAllForUser(userId);
        }

        public async Task<bool> DeleteMyEntityAsync(DeleteEntityDto deleteEntityDto)
        {
            return await _myEntityRepository.Delete(deleteEntityDto.UserId, deleteEntityDto.EntityId);
        }

    }
}
