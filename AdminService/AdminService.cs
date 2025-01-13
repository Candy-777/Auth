using CustomExceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminService
{
    public class AdminService:IAdminService
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            var result = await _userManager.Users.Include(u => u.MyEntities).ToListAsync();
            return result;
        }

        public async Task<AppUser> GetUserByName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or empty");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw  UserNotFoundException.ForUserName(username);
            }
            return user;
        }

        public async Task<AppUser> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw UserNotFoundException.ForUserId(id);
            }
            return user;
        }
        public async Task<bool> DeleteUserById(Guid id) 
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)            {
                throw  UserNotFoundException.ForUserId(id); 
            }
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                throw AdminDeletionException.ForAdminUser(user.UserName);
            }
            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw UserNotFoundException.ForUserName(username);
            }
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                throw AdminDeletionException.ForAdminUser(username);
            }
            await _userManager.DeleteAsync(user);
            return true;
        }

    }
}
