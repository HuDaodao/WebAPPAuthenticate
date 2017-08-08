using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

using AuthenticateDAL;
using AuthenticateModel;

namespace AuthenticateBLL
{
    public class ModuleBll
    {
        /// <summary>
        /// 返回模块数据
        /// </summary>
        /// <returns>模块List</returns>
        public List<Module> GetAllModule()
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.Module.ToList();
            }
        }

        /// <summary>
        /// 保存模块
        /// </summary>
        /// <param name="module">模块实体</param>
        public void SaveModule(Module module)
        {
            using (AuthentContext db = new AuthentContext())
            {
                if (module.Id == 0)
                {
                    db.Module.Add(module);
                }
                else
                {
                    Module mod;
                    mod = db.Module.Where(m => m.Id == module.Id).FirstOrDefault();
                    if (mod != null)
                    {
                        mod.ChineseName = module.ChineseName;
                        mod.Description = module.Description;
                        mod.EnglishName = module.EnglishName;
                        mod.LastChangeTime = module.LastChangeTime;
                        mod.LastChangeUser = module.LastChangeUser;
                        mod.NavigatePicture = module.NavigatePicture;
                        mod.IsDisplay = module.IsDisplay;
                        mod.Status = module.Status;
                        mod.Icon = module.Icon;
                        mod.Order = module.Order;
                        mod.Url = module.Url;
                        mod.ParentId = module.ParentId;
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 通过id获取一条模块信息
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns></returns>
        public Module GetModuleById(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.Module.Where(m => m.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 删除一个模块信息
        /// </summary>
        /// <param name="id">模块ID</param>
        public void DeleteMain(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                Module mainDic = db.Module.Where(m=>m.Id==id).FirstOrDefault();
                db.Module.Remove(mainDic);
                db.SaveChanges();
            }
        }
    }
}
