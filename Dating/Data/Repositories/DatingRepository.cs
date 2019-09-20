using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dating.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dating.Data.Repositories
{
    public class DatingRepository : IDatingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DatingRepository(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            var users = await _context.AppUsers
                .Include(p=>p.Photos)
                .ToListAsync();
            return users;
        }

        public async Task<AppUser> GetUser(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<AppUser> Update(AppUser user)
        {
            var entity = _context.AppUsers.Attach(user);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}
