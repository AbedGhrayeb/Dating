using Dating.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeAgo;

namespace Dating.ViewModels
{
    public class UserDetailesViewModel

    {
        public UserDetailesViewModel()
        {
            LastSeen = LastActive.TimeAgo();
        }
        public string LastSeen { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public int Age { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string Gender { get; set; }
        
        public string KnownAs { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; } 
        
        public string Introduction { get; set; }
        
        public string LookingFor { get; set; }
        
        public string Interests { get; set; }
        public string PhotoUrl { get; set; }

     
        public ICollection<PhotosListViewModel> Photos { get; set; }
    }
}
