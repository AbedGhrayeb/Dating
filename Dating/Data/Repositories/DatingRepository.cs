using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dating.Helpers;
using Dating.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dating.Data.Repositories
{
    [Authorize]
    public class DatingRepository : IDatingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DatingRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
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

        public async Task<PagedList<AppUser>> GetUsers(UserParams userParams)
        {
            var users = _context.AppUsers.Include(p => p.Photos)
                .OrderBy(u=>u.LastActive)
                .AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId && u.Gender == userParams.Gender);
            if (userParams.MinAge != 18 ||userParams.MaxAge!=99)
            {
                users = users.Where(u => u.DateOfBirth.CalculatAge() >= userParams.MinAge
                  && u.DateOfBirth.CalculatAge() <= userParams.MaxAge);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

             return await PagedList<AppUser>.CreateAsync(users,userParams.pageNumber,userParams.PageSize);

            
        }

        public async Task<AppUser> GetUser(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(string userId)
        {
            return await _context.Photos.Include(u => u.AppUser)
                .Where(u=>u.AppUserId==userId)
                .ToListAsync();
        }
        public void AddPhotos(Photo photo)
        {
            _context.Photos.Add(photo);
            _context.SaveChanges();
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
