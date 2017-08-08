using System.Web.Mvc;
using AuthenticateBLL;
using WebAppAuthenticate.ViewModels;
using AuthenticateModel;

namespace WebAppAuthenticate.Controllers
{
    public class LogInController : Controller
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            LoginViewModel logModel=new LoginViewModel();
            return View(logModel);
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userLogin">登陆的用户</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn(LoginViewModel userLogin)
        {
            Session["HasUser"] = false;
            if (ModelState.IsValid)
            {
                LoginBll bll = new LoginBll();

                User user = new User();
                user.UserName = userLogin.UserName;
                user.PassWord = userLogin.PassWord;

                User isValiduser = bll.IsValid(user);
                if (isValiduser!=null)
                {
                    Session["HasUser"] = true;
                    Session["UserId"] = isValiduser.Id;
                    Session["UserName"] = userLogin.UserName;
                    return RedirectToAction("Index", "UserManage");
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid EmployeeName or PassWord");
                    userLogin.ErrorMsg = "用户名或密码错误";
                    return View(userLogin);
                }
            }
            else
            {
                userLogin.ErrorMsg = "模型未通过验证";
                return View(userLogin);
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session["HasUser"] = false;
            return RedirectToAction("LogIn");
        }
    }
}