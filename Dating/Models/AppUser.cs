using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Models
{
 
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
            Photos = new Collection<Photo>();
        }
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        [PersonalData]
        public string Country { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        [PersonalData]
        public string KnownAs { get; set; }
        [PersonalData]
        public DateTime Created { get; set; }
        [PersonalData]
        public DateTime LastActive { get; set; }
        [PersonalData]
        public string Introduction { get; set; }
        [PersonalData]
        public string LookingFor { get; set; }
        [PersonalData]
        public string Interests { get; set; }
        [PersonalData]
        public ICollection<Photo> Photos { get; set; } 

    }
}
