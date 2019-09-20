using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Helpers
{
    public static class Extensions
    {
        public static int CalculatAge(this DateTime dateTime)
        {
             //ex: 19           2019           -   2000
            var age = DateTime.Today.Year - dateTime.Year;
            //ex:    2000+19          >   2019
            if (dateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}
