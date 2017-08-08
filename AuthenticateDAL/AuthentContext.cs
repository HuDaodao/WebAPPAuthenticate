using System.Data.Entity;

using AuthenticateDAL.Configuration;
using AuthenticateModel;

namespace AuthenticateDAL
{
    /// <summary>
    /// 上下文配置
    /// </summary>
    public class AuthentContext:DbContext
    {
        //static AuthentContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseAlways<AuthentContext>());
        //}
        public AuthentContext():base("AuthentContext") { }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<MainDictionary> MainDictionary { get; set; }
        public DbSet<Module> Module { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new DictionaryConfig());
            modelBuilder.Configurations.Add(new MainDictionaryConfig());
            modelBuilder.Configurations.Add(new ModuleConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
