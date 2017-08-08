using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    /// <summary>
    /// 角色属性配置
    /// </summary>
    public class RoleConfig:EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            //设置主键
            HasKey(r => r.Id);
            //角色名不能为空，最长为50字符
            Property(r => r.RoleName).IsRequired().HasMaxLength(50);
            //Code不能为空，最长为100字符
            Property(r => r.Code).IsRequired().HasMaxLength(100);
            //上次修改时间不能为空
            Property(r => r.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(r => r.LastChangeUser).IsRequired();
        }
    }
}
