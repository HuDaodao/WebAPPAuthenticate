using System;
using System.Collections.Generic;

namespace AuthenticateModel
{
    /// <summary>
    /// 模块表
    /// </summary>
    public class Module
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 访问路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 导航图片
        /// </summary>
        public string NavigatePicture { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 上次修改人
        /// </summary>
        public int LastChangeUser { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastChangeTime { get; set; }

        /// <summary>
        /// 角色模块表ID
        /// </summary>
        public virtual int? RoleModuleId { get; set; }

        /// <summary>
        /// 角色模块集合
        /// </summary>
        public virtual ICollection<RoleModule> RoleModule { get; set; }
    }
}
