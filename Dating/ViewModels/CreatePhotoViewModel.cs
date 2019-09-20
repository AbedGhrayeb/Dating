using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.ViewModels
{
    public class CreatePhotoViewModel
    {
        public CreatePhotoViewModel()
        {
            DateAdded = DateTime.Now;
        }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public IFormFile File { get; set; }
    }
}
