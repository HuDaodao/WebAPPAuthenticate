using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class RoleFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "角色名必填")]
        [StringLength(50, ErrorMessage = "不得超过50个字")]
        [DisplayName("角色名")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "编码必填")]
        [StringLength(50, ErrorMessage = "不得超过100个字")]
        [DisplayName("编码")]
        public string Code { get; set; }


        [DisplayName("描述")]
        public string Description { get; set; }
    }
}