using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticateDAL;
using AuthenticateModel;

namespace AuthenticateBLL
{
    public class LoginBll
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userLogin">登陆的用户</param>
        /// <returns></returns>
        public User IsValid(User userLogin)
        {
            if(string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.PassWord))
            {
                return null;
            }
            else
            {
                using (AuthentContext db = new AuthentContext())
                {
                    int userCount = db.User.Count();
                    //如果用户表没有用户，则新增一名用户
                    if (userCount == 0)
                    {
                        db.User.Add(new User
                        {
                            UserName = "admin",
                            PassWord = Base64("admin"),
                            RealName = "admin",
                            MobilePhone = "13243211111",
                            Email = "123@qq.com",
                            LastChangeUser = 0,
                            LastChangeTime = DateTime.Now,
                        });
                        db.SaveChanges();
                    }
                    string base64Pass = Base64(userLogin.PassWord);
                    User user = db.User.Where(u => u.UserName == userLogin.UserName && u.PassWord == base64Pass)
                                        .FirstOrDefault();
                    return user;
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
        /// BASE64 解密
        /// </summary>
        /// <param name="value">待解密字段</param>
        /// <returns></returns>
        public string UnBase64(string value)
        {
            var btArray = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(btArray);
        }

    }
}
