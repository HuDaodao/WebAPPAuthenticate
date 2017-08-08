using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class MainDictionFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "中文名必填")]
        [MaxLength(50, ErrorMessage = "不得超过50个字")]
        [DisplayName("中文名")]
        public string ChineseName { get; set; }

        [Required(ErrorMessage = "英文名必填")]
        [MaxLength(50, ErrorMessage = "不得超过50个字")]
        [DisplayName("英文名")]
        public string EnglishName { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("是否只读")]
         public bool ReadOnly { get; set; }

        public string Error { get; set; }
    }
}