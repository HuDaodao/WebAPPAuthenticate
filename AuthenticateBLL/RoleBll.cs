using AuthenticateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticateDAL;

namespace AuthenticateBLL
{
    public class RoleBll
    {
        /// <summary>
        /// 返回所有的角色
        /// </summary>
        /// <returns>角色列表</returns>
        public List<Role> GetAllRole()
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.Role.ToList();
            }
        }

        /// <summary>
        /// 获取一个角色的所有用户Id
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色的用户Id列表</returns>
        public List<int> GetRoleUser(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                var roleUsers = db.UserRole.Where(ur => ur.RoleId == id).ToList();
                List<int> users = new List<int>();
                foreach (var roleUser in roleUsers)
                {
                    users.Add(roleUser.UserId);
                }
                return users;
            }
        }

        /// <summary>
        /// 获取一个角色的所有模块Id
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>角色的模块Id列表</returns>
        public List<int> GetRoleModule(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                var roleModules = db.RoleModule.Where(ur => ur.RoleId == id).ToList();
                List<int> modules = new List<int>();
                foreach (var roleModule in roleModules)
                {
                    modules.Add(roleModule.ModuleId);
                }
                return modules;
            }
        }


        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="name">角色名</param>
        /// <param name="pageStar">开始条数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sortCol">排序字段</param>
        /// <param name="sortDir">排序方式</param>
        /// <param name="total">总共角色条数</param>
        /// <returns>角色列表</returns>
        public List<Role> GetPageRoles(string name, int pageStar, int pageSize, int sortCol, string sortDir, out int total)
        {
            using (AuthentContext db = new AuthentContext())
            {
                IQueryable<Role> roles = db.Role;
                if (!string.IsNullOrEmpty(name))
                {
                    roles = roles.Where(role => role.RoleName.Contains(name));
                }
                total = db.Role.Count();
                if (sortCol == 0)
                {
                    roles = sortDir == "asc" ? roles.OrderBy(u => u.RoleName) : roles.OrderByDescending(u => u.RoleName);
                }
                else
                {
                    roles = roles.OrderByDescending(u => u.Id);
                }
                return roles.Skip(pageStar).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="roleIn">角色实体</param>
        public void SaveRole(Role roleIn)
        {
            using (AuthentContext db = new AuthentContext())
            {
                if (roleIn.Id == 0)
                {
                    db.Role.Add(roleIn);
                }
                else
                {
                    Role role = new Role();
                    role = db.Role.Where(u => u.Id == roleIn.Id).FirstOrDefault();
                    if (role != null)
                    {
                        role.RoleName= roleIn.RoleName;
                        role.Description = roleIn.Description;
                        role.Code = roleIn.Code;
                        role.LastChangeTime = roleIn.LastChangeTime;
                        role.LastChangeUser = roleIn.LastChangeUser;
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 通过Id获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleById(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.Role.Where(u => u.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                Role role = db.Role.Where(u => u.Id == id).FirstOrDefault();
                if (role != null)
                {
                    db.Role.Remove(role);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 保存选择的用户
        /// </summary>
        /// <param name="userIds">用户Id数组</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="changeUser">修改人ID</param>
        public void SaveRoleUsers(List<int> userIds, int roleId, int changeUser)
        {
            using (AuthentContext db = new AuthentContext())
            {
                //先要删除这个角色的用户
                var users = db.UserRole.Where(ur => ur.RoleId == roleId && userIds.Contains(ur.UserId));
                db.UserRole.RemoveRange(users);

                //再重新保存
                List<UserRole> roleUsers = new List<UserRole>();
                foreach (var userId in userIds)
                {
                    UserRole userRole = new UserRole();
                    userRole.UserId = userId;
                    userRole.RoleId = roleId;
                    userRole.LastChangeTime = DateTime.Now;
                    userRole.LastChangeUser = changeUser;
                    roleUsers.Add(userRole);
                }
                db.UserRole.AddRange(roleUsers);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 检查角色名是否可(true:是，false:否)【重新判断逻辑！！！】
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        public bool CheckRoleName(string roleName, int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                var role = db.Role.FirstOrDefault(u => u.RoleName == roleName);
                if (role != null)
                {
                    return role.Id == id;
                }
                return true;
            }
        }
    }
}
