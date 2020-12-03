using System.Collections.Generic;
using Flespi.Core.DomainService;
using Flespi.Entity;

namespace Flespi.Core.AppService.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User CreateUser(User newUser)
        {
            return _userRepo.CreateUser(newUser);
        }

        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);
        }

        public User UpdateUser(User updatedUser)
        {
            return _userRepo.UpdateUser(updatedUser);
        }

        public User DeleteUser(int id)
        {
            return _userRepo.DeleteUser(id);
        }
    }
}