using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    public class MainDictionaryConfig:EntityTypeConfiguration<MainDictionary>
    {
        public MainDictionaryConfig()
        {
            //设置主键
            HasKey(m => m.Id);
            //中文名不能为空，最长50字符
            Property(m => m.ChineseName).IsRequired().HasMaxLength(50);
            //英文名不能为空，最长50字符
            Property(m => m.EnglishName).IsRequired().HasMaxLength(50);
            //上次修改时间不能为空
            Property(m => m.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(m => m.LastChangeUser).IsRequired();
        }
    }
}
