using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Code { get; set; }
        public string  LastChangeUser { get; set; }
        public string LastChangeTime { get; set; }
        public string Description { get; set; }

    }
}