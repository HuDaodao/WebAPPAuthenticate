using System;
using System.Collections;
using System.Collections.Generic;

namespace AuthenticateModel
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string Description { get; set; }

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
        public int? RoleModuleId { get; set; }

        /// <summary>
        /// 角色模块集合
        /// </summary>
        public virtual ICollection<RoleModule> RoleModuel { get; set; }

        /// <summary>
        /// 用户角色集合
        /// </summary>
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
