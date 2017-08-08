using System.Data.Entity.ModelConfiguration;

using AuthenticateModel;

namespace AuthenticateDAL.Configuration
{
    public class RoleModuleConfig : EntityTypeConfiguration<RoleModule>
    {
        public RoleModuleConfig()
        {
            //设置主键
            HasKey(rm => rm.Id);
            //上次修改时间不能为空
            Property(rm => rm.LastChangeTime).IsRequired();
            //上次修改人不能为空
            Property(rm => rm.LastChangeUser).IsRequired();
            //设置与角色表级联删除
            HasRequired(rm=>rm.Role).WithMany(r=>r.RoleModuel).HasForeignKey(rm=>rm.RoleId).WillCascadeOnDelete(true);
            //设置与模块表级联删除
            HasRequired(rm=>rm.Module).WithMany(r=>r.RoleModule).HasForeignKey(rm=>rm.ModuleId).WillCascadeOnDelete(true);
        }
    }
}
