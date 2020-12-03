using System.Collections.Generic;
using System.Linq;
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
            return _context.Users.AsNoTracking().ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(User updatedUser)
        {
            _context.Attach(updatedUser).State = EntityState.Modified;
            _context.Update(updatedUser);
            _context.SaveChanges();
            return updatedUser;
        }

        public User DeleteUser(int id)
        {
            var userToDelete = _context.Remove(new User {Id = id}).Entity;
            _context.SaveChanges();
            return userToDelete;
        }
    }
}