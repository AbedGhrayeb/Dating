
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        private int pageSize=10;
        
        public int PageSize
        {
            get { return pageSize; }
                  //ex:        60       50         take(50)
                  //            30      50                      take(30)
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string UserId { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;
        public string OrderBy { get; set; }
    }
}
