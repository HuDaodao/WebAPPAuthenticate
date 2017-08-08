using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class UserFormViewModel
    {
        public int? Id { get; set; }

        [DisplayName("性别")]
        public bool Sex { get; set; }

        [DisplayName ("手机")]
        [StringLength(50, ErrorMessage = "不得超过50个字")]
        [Required(ErrorMessage = "手机号必填")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "真名必填")]
        [StringLength(50, ErrorMessage = "不得超过50个字")]
        [DisplayName("真名")]
        public string RealName { get; set; }

        [Required(ErrorMessage = "用户名必填")]
        [MaxLength(50,ErrorMessage = "不得超过50个字")]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        [Range(0,120,ErrorMessage = "年龄必须在15~110之间")]
        [DisplayName("年龄")]
        public int Age { get; set; }

        [MaxLength(30, ErrorMessage = "不得超过30个字")]
        [DisplayName("工作电话")]
        public string Phone { get; set; }

        [MaxLength(200, ErrorMessage = "不得超过200个字")]
        [DisplayName("邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码必填")]
        [MaxLength(100, ErrorMessage = "不得超过100个字")]
        [DisplayName("密码")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "是否启用必填")]
        [DisplayName("是否启用")]
        public bool Status { get; set; }

        public string Error { get; set; }

        public virtual int? UserRoleId { get; set; }
    }
}