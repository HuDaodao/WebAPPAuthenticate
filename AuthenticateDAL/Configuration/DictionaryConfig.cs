using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    /// <summary>
    /// 数据字典配置
    /// </summary>
    public class DictionaryConfig:EntityTypeConfiguration<Dictionary>
    {
        public DictionaryConfig()
        {
            //设置主键
            HasKey(d => d.Id);
            //中文名不能为空，最长50字符
            Property(d => d.ChineseName).IsRequired().HasMaxLength(50);
            //英文名不能为空，最长50字符
            Property(d => d.EnglishName).IsRequired().HasMaxLength(50);
            //排序不能为空
            Property(d => d.Order).IsRequired();
            //上次修改时间不能为空
            Property(d => d.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(d => d.LastChangeUser).IsRequired();
        }
    }
}
