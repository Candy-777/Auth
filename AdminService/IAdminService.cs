using Domain.Entities;

namespace AdminService
{
    public interface IAdminService
    {
    
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserByName(string username);
        Task<AppUser> GetUserById(Guid id);
        Task<bool> DeleteUserById(Guid id);
        Task<bool> DeleteUserByUserName(string username);
    }
}
