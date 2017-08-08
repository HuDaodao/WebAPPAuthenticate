using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        public string ErrorMsg { get; set; }
    }
}