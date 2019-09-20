using Dating.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Data
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;

        public Seed(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void SeedUser()
        {
            var usersData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<AppUser>>(usersData);
           
            foreach (var user in users)
            {

                _userManager.CreateAsync(user, "Secure.123");
            }
            _context.SaveChanges();
        }
    }
}
