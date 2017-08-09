using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

using AuthenticateDAL;
using AuthenticateModel;
using System;

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
        /// 返回子模块
        /// </summary>
        /// <param name="parentId">模块父ID</param>
        /// <returns>模块列表</returns>
        public List<Module> GetSonModule(int parentId)
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.Module.Where(m=>m.ParentId==parentId).ToList();
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

        /// <summary>
        /// 保存选择的角色
        /// </summary>
        /// <param name="roleIds">角色数组</param>
        /// <param name="moduleId">当前模块ID</param>
        /// <param name="changeUser">修改人ID</param>
        public void SaveModuleRoles(List<int> roleIds, int moduleId, int changeUser)
        {
            using (AuthentContext db = new AuthentContext())
            {
                //先要删除这个模块的角色
                var roles = db.RoleModule.Where(rm => rm.ModuleId == moduleId && roleIds.Contains(rm.RoleId));
                db.RoleModule.RemoveRange(roles);

                ////判断他是不是子模块
                //Module currentModule = db.Module.Where(module => module.Id == moduleId).FirstOrDefault();
                //var a = currentModule.ParentId;

                //再重新保存
                List<RoleModule> roleModels = new List<RoleModule>();
                foreach (var roleId in roleIds)
                {
                    RoleModule roleModel = new RoleModule();
                    roleModel.ModuleId = moduleId;
                    roleModel.RoleId = roleId;
                    roleModel.LastChangeTime = DateTime.Now;
                    roleModel.LastChangeUser = changeUser;
                    roleModels.Add(roleModel);
                }
                db.RoleModule.AddRange(roleModels);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取一个模块的所有角色Id
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns>模块角色Id列表</returns>
        public List<int> GetModuleRole(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                var moduleRoles = db.RoleModule.Where(ur => ur.ModuleId == id).ToList();
                List<int> roles = new List<int>();
                foreach (var moduleRole in moduleRoles)
                {
                    roles.Add(moduleRole.RoleId);
                }
                return roles;
            }
        }

    }
}
