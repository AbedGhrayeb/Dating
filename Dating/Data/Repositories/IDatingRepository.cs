using Dating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Data.Repositories
{
    public interface IDatingRepository
    {
        void Add<T> (T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<AppUser> Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUser(string id);
    }
}
