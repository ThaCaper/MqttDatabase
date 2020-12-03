using System.Collections.Generic;
using Flespi.Core.DomainService;
using Flespi.Entity;
using Microsoft.EntityFrameworkCore;

namespace Flespi.Infrastructure.SQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public User CreateUser(User newUser)
        {
            _context.Users.Add(newUser).State = EntityState.Added;
            _context.SaveChanges();
            return newUser;
        }

        public List<User> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public int GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public User UpdateUser(User updatedUser)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}