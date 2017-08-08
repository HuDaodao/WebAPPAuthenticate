using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfig()
        {
            //设置主键
            HasKey(m => m.Id);
            //上次修改时间不能为空
            Property(d => d.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(d => d.LastChangeUser).IsRequired();
            //设置与角色表级联删除
            HasRequired(ur=>ur.Role).WithMany(r=>r.UserRole).HasForeignKey(m=>m.RoleId).WillCascadeOnDelete(true);
            //设置与用户表级联删除
            HasRequired(ur=>ur.User).WithMany(u=>u.UserRole).HasForeignKey(m=>m.UserId).WillCascadeOnDelete(true);
        }
    }
}
