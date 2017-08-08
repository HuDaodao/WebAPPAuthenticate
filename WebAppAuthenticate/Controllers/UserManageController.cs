using System;
using AuthenticateBLL;
using AuthenticateModel;
using System.Collections.Generic;
using System.Web.Mvc;

using WebAppAuthenticate.ViewModels;
using Newtonsoft.Json;
using WebAppAuthenticate.Filters;

namespace WebAppAuthenticate.Controllers
{
    //[HasUserFilter]
    public class UserManageController : Controller
    {
        /// <summary>
        /// 返回用户管理页面
        /// </summary>
        /// <returns>用户管理界面</returns>
        public ActionResult Index()
        {
            return View(new UserFormViewModel());
        }

        /// <summary>
        /// 返回用户DataTable数据
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns>用户DataTable数据</returns>
        public ActionResult DataList(SearchBaseModel searchBase)
        {
            TableJsonData<UserViewModel> jsonData=new TableJsonData<UserViewModel>();
            int total;
            var list = GetUserViewModel(searchBase, out total);

            jsonData.aaData = list;
            jsonData.draw = searchBase.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            return Json(jsonData);
        }

        /// <summary>
        /// 查询用户List
        /// </summary>
        /// <param name="total">总数</param>
        /// <param name="search">查询条件</param>
        /// <returns>用户列表</returns>
        public List<UserViewModel> GetUserViewModel(SearchBaseModel search, out int total)
        {
            UserBll userBll = new UserBll();
            List<User> userList = userBll.GetPageUsers(search.RealName, search.UserName, search.PageStart, search.PageSize, search.SortCol,search.SortDir,out total);
           
            List<UserViewModel> userView = new List<UserViewModel>();
            //将数据库查出的List<User>转为List<UserViewModel>视图模型
            foreach (var user in userList)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    Status = user.Status,
                    MobilePhont = user.MobilePhone,
                    Email = user.Email,
                    Phone = user.Phone,
                };
                userView.Add(userViewModel);
            }
            return userView;
        }

        /// <summary>
        /// 返回编辑页部分视图
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns>部分视图</returns>
        public ActionResult UserDetail(int id = 0)
        {
            UserFormViewModel userForm = new UserFormViewModel();
            if (id != 0)
            {
                UserBll bll = new UserBll();
                User user = bll.GetUserById(id);
                if (user != null)
                {
                    userForm.UserName = user.UserName;
                    userForm.Age = user.Age;
                    userForm.Email = user.Email;
                    userForm.PassWord = user.PassWord;
                    userForm.Sex = user.Sex;
                    userForm.Status = user.Status;
                    userForm.Id = user.Id;
                    LoginBll loginBll = new LoginBll();
                    string pass = loginBll.UnBase64(user.PassWord);
                    userForm.PassWord = pass;
                    userForm.RealName = user.RealName;
                    userForm.MobilePhone = user.MobilePhone;
                    userForm.UserRoleId = user.UserRoleId;
                }
                else
                {
                    return PartialView("CreatAndEdit", userForm);
                }
            }
            return PartialView("CreatAndEdit", userForm);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="userFormViewModel">用户视图模型</param>
        /// <returns>保存结果</returns>
        public ActionResult Save(UserFormViewModel userFormViewModel)
        {
            UserBll bll=new UserBll();
            User user=new User();
            if (ModelState.IsValid)
            {
                user.UserName = userFormViewModel.UserName;
                user.Id = userFormViewModel.Id==null?0:int.Parse(userFormViewModel.Id.ToString());
                user.PassWord = userFormViewModel.PassWord;
                user.Phone = userFormViewModel.Phone;
                user.Age = userFormViewModel.Age;
                user.Email = userFormViewModel.Email;
                user.MobilePhone = userFormViewModel.MobilePhone;
                user.RealName = userFormViewModel.RealName;
                user.Sex = userFormViewModel.Sex;
                user.LastChangeTime = DateTime.Now;
                user.LastChangeUser = int.Parse(Session["UserId"].ToString());
                user.Status = userFormViewModel.Status;
                user.UserName = userFormViewModel.UserName;
                user.UserName = userFormViewModel.UserName;
                try
                {
                    bll.SaveUser(user);
                }
                catch (Exception e)
                {
                    userFormViewModel.Error = "保存出错,请重试";
                    return Json(userFormViewModel.Error, JsonRequestBehavior.AllowGet);
                }
                return Json("access", JsonRequestBehavior.AllowGet);
            }
            else
            {
                userFormViewModel.Error = "模型验证未通过";
                return Json(userFormViewModel.Error, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>删除结果</returns>
        public ActionResult Delete(int? id)
        {
            string result = "ok";
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                UserBll userBll = new UserBll();
                try
                {
                    userBll.Delete(int.Parse(id.ToString()));
                }
                catch (Exception e)
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return Json(result);
        }

        /// <summary>
        /// 检查重名检查用户名是否可用
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="id">用户id</param>
        /// <returns>true:是，false:否</returns>
        public JsonResult CheckUserName(string userName, int id)
        {
            UserBll bll=new UserBll();
            var result = bll.CheckUserName(userName, id);
            return Json(result);
        }

        /// <summary>
        /// 获取角色jstree数据
        /// </summary>
        /// <returns>json格式的JStree</returns>
        public ActionResult RoleTreeList()
        {
            RoleBll bll = new RoleBll();
            var roles = bll.GetAllRole();
            RoleJsTree  jst=new RoleJsTree();
            jst.id = 0;
            jst.text = "全选";
            List<RoleJsTree> children = new List<RoleJsTree>();
            foreach (var role in roles)
            {
                RoleJsTree child = new RoleJsTree()
                {
                    id = role.Id,
                    text = role.RoleName,
                };
                children.Add(child);
            }
            jst.children = children;
            return Json(jst, JsonRequestBehavior.AllowGet);
        }
    }
}