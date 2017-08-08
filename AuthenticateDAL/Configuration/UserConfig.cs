using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            //设置主键
            HasKey(m => m.Id);
            //真名不能为空，最长为50字符
            Property(m => m.RealName).IsRequired().HasMaxLength(50);
            //用户名不能为空，最长为50字符
            Property(m => m.UserName).IsRequired().HasMaxLength(50);
            //手机不能为空，最长为50字符
            Property(m => m.MobilePhone).IsRequired().HasMaxLength(50);
            //邮箱不能为空，最长为200字符
            Property(m => m.Email).IsRequired().HasMaxLength(200);
            //办公电话最长为30字符
            Property(m => m.Phone ).HasMaxLength(30);
            //密码不能为空，最长为100字符
            Property(m => m.PassWord).IsRequired().HasMaxLength(100);
            //上次修改时间不能为空
            Property(d => d.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(d => d.LastChangeUser).IsRequired();
        }
    }
}
