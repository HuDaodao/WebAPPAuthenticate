using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class DictionTableViewModel
    {
        public int? Id { get; set; }

        public string ChineseName { get; set; }

        public string Description { get; set; }

        public string EnglishName { get; set; }

        public int Order { get; set; }

        public string Status { get; set; }

        public string LastChangeUser { get; set; }

        public string LastChangeTime { get; set; }
    }
}