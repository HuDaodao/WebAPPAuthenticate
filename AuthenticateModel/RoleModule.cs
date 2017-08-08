using System;

namespace AuthenticateModel
{
    /// <summary>
    /// 角色模块表
    /// </summary>
    public class RoleModule
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastChangeTime { get; set; }

        /// <summary>
        /// 上次修改人
        /// </summary>
        public int LastChangeUser { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual int ModuleId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public virtual Module Module { get; set; }

    }
}
