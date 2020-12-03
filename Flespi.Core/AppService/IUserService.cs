using System.Collections.Generic;
using Flespi.Entity;

namespace Flespi.Core.AppService
{
    public interface IUserService
    {
        User CreateUser(User newUser);

        List<User> GetAllUsers();

        User GetUserById(int id);

        User UpdateUser(User updatedUser);

        User DeleteUser(int id);
    }
}