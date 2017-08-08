using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhont { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
    }
}