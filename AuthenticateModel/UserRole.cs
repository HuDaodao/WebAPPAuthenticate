using System;

namespace AuthenticateModel
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole
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
        /// 用户ID
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

    }
}
