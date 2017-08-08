using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppAuthenticate.ViewModels
{
    public class DictionaryFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "中文名必填")]
        [MaxLength(50, ErrorMessage = "不得超过50个字")]
        [DisplayName("中文名")]
        public string ChineseName { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

        [Required(ErrorMessage = "英文名必填")]
        [MaxLength(50, ErrorMessage = "不得超过50个字")]
        [DisplayName("英文名")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [DisplayName("排序")]
        public int Order { get; set; }

        [DisplayName("是否启用")]
        public bool Status { get; set; }

        [DisplayName("所属数据字典")]
        public int MainDictionaryId { get; set; }

        public string Error { get; set; }
    }
}