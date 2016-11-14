using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.Models.Libs
{
    public class DatabaseSettings
    {
        public RoleSettings Roles { get; set; }
    }

    public class RoleSettings
    {
        public string UserRole { get; set; }
        public string AdminRole { get; set; }
    }
}
