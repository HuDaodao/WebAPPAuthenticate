using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class ModuleFormViewModel
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

        [DisplayName("URL")]
        [MaxLength(100, ErrorMessage = "不得超过100个字")]
        public string Url { get; set; }

        [Required]
        [DisplayName("图标")]
        [MaxLength(100, ErrorMessage = "不得超过100个字")]
        public string Icon { get; set; }

        [Required]
        [DisplayName("排序")]
        public int Order { get; set; }

        [DisplayName("是否启用")]
        public bool Status { get; set; }

        [DisplayName("是否显示")]
        public bool IsDisplay { get; set; }

        [DisplayName("导航图片")]
        [MaxLength(300, ErrorMessage = "不得超过300个字")]
        public string NavigatePicture { get; set; }

        public string Error { get; set; }
    }
}