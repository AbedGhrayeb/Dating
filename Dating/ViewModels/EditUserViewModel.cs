using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.ViewModels
{
    public class EditUserViewModel
    {
     
        public string Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }

    }
}
