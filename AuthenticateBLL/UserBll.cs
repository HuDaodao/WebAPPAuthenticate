using AuthenticateModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticateDAL;

namespace AuthenticateBLL
{
    public class UserBll
    {
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="realName">真名</param>
        /// <param name="pageStar">开始条数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sortCol">排序字段</param>
        /// <param name="sortDir">排序方式</param>
        /// <param name="total">总共用户条数</param>
        /// <returns>用户列表</returns>
        public List<User> GetPageUsers(string realName,string userName,int pageStar,int pageSize,int sortCol,string sortDir, out int total)
        {
            using (AuthentContext db = new AuthentContext())
            {
                IQueryable<User> users = db.User;
                if (!string.IsNullOrEmpty(realName))
                {
                    users = users.Where(user => user.RealName.Contains(realName));
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    users = users.Where(user => user.UserName.Contains(userName));
                }
                total = db.User.Count();
                if (sortCol == 1)
                {
                    if (sortDir == "asc")
                    {
                        users = users.OrderBy(u => u.RealName);
                    }
                    else
                    {
                        users = users.OrderByDescending(u => u.RealName);
                    }
                }
                else
                {
                    users = users.OrderByDescending(u => u.Id);
                }
                return users.Skip(pageStar).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="userIn">用户实体</param>
        public void SaveUser(User userIn)
        {
            userIn.PassWord= Base64(userIn.PassWord);

            using (AuthentContext db = new AuthentContext())
            {
                if (userIn.Id == 0)
                {
                    db.User.Add(userIn);
                }
                else
                {
                    User user=new User();
                    user = db.User.Where(u => u.Id == userIn.Id).FirstOrDefault();
                    user.UserName = userIn.UserName;
                    user.Id = userIn.Id;
                    user.PassWord = userIn.PassWord;
                    user.Phone = userIn.Phone;
                    user.Age = userIn.Age;
                    user.Email = userIn.Email;
                    user.MobilePhone = userIn.MobilePhone;
                    user.RealName = userIn.RealName;
                    user.Sex = userIn.Sex;
                    user.LastChangeTime = userIn.LastChangeTime;
                    user.LastChangeUser = userIn.LastChangeUser;
                    user.Status = userIn.Status;
                    user.UserName = userIn.UserName;
                    user.UserName = userIn.UserName;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 通过Id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.User.Where(u => u.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 返回所有的用户
        /// </summary>
        /// <returns>用户列表</returns>
        public List<User> GetAllUser()
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.User.ToList();
            }
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                User user = db.User.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    db.User.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// BASE64 加密
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns></returns>
        public string Base64(string value)
        {
            var btArray = Encoding.UTF8.GetBytes(value);
            var result = Convert.ToBase64String(btArray, 0, btArray.Length);
            return result;
        }

        /// <summary>
        /// 检查用户名是否可(true:是，false:否)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public bool CheckUserName(string userName, int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                var user = db.User.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    return user.Id == id;
                }
                return true;
            }
        }

        /// <summary>
        /// 保存选择的角色
        /// </summary>
        /// <param name="roleIds">角色数组</param>
        /// <param name="userId">用户ID</param>
        /// <param name="changeUser">修改人ID</param>
        public void SaveUserRoles(List<int> roleIds,int userId,int changeUser)
        {
            using (AuthentContext db = new AuthentContext())
            {
                //先要删除这个用户的角色
                var roles = db.UserRole.Where(ur => ur.UserId == userId && roleIds.Contains(ur.RoleId));
                db.UserRole.RemoveRange(roles);

                //再重新保存
                List<UserRole> userRoles=new List<UserRole>();
                foreach (var roleId in roleIds)
                {
                    UserRole ur = new UserRole();
                    ur.UserId = userId;
                    ur.RoleId = roleId;
                    ur.LastChangeTime=DateTime.Now;
                    ur.LastChangeUser =changeUser;
                    userRoles.Add(ur);
                }
                db.UserRole.AddRange(userRoles);

                db.SaveChanges();
            }
        }
    }
}
