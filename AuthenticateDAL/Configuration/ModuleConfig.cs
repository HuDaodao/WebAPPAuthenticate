using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    public class ModuleConfig : EntityTypeConfiguration<Module>
    {
        public ModuleConfig()
        {
            //设置主键
            HasKey(m => m.Id);
            //中文名不能为空，最长50字符
            Property(m => m.ChineseName).IsRequired().HasMaxLength(50);
            //英文名不能为空，最长50字符
            Property(m => m.EnglishName).IsRequired().HasMaxLength(50);
            //排序不能为空
            Property(m => m.Order).IsRequired();
            //图标不能为空
            Property(m => m.Icon).IsRequired().HasMaxLength(100);
            //导航图片最长100字符
            Property(m => m.NavigatePicture).HasMaxLength(300);
            //上次修改时间不能为空
            Property(m => m.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(m => m.LastChangeUser).IsRequired();
            //URL不能超过100字符
            Property(m => m.Url).HasMaxLength(100);
        }
    }
}
