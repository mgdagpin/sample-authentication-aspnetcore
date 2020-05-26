using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Authentication_ASP.Net_Core.Common
{
    public class AppUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
