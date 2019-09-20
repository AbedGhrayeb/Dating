using System;
using System.Collections.Generic;
using System.Text;
using Dating.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dating.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
