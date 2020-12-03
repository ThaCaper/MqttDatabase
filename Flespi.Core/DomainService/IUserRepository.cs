﻿using System.Collections.Generic;
using System.Dynamic;
using Flespi.Entity;

namespace Flespi.Core.DomainService
{
    public interface IUserRepository
    {
        User CreateUser(User newUser);

        List<User> GetAllUsers();

        User GetUserById(int id);

        User UpdateUser(User updatedUser);

        User DeleteUser(int id);
    }
}