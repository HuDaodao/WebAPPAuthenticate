using System;
using AuthenticateBLL;
using AuthenticateModel;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

using WebAppAuthenticate.ViewModels;

namespace WebAppAuthenticate.Controllers
{
    //[HasUserFilter]
    public class RoleController : CommonController
    {
        /// <summary>
        /// 返回角色管理页面
        /// </summary>
        /// <returns>角色管理界面</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回角色DataTable数据
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns>角色DataTable数据</returns>
        public ActionResult DataList(SearchBaseModel searchBase)
        {
            TableJsonData<RoleViewModel> jsonData=new TableJsonData<RoleViewModel>();
            int total;
            var list = GetRoleViewModel(searchBase, out total);

            jsonData.aaData = list;
            jsonData.draw = searchBase.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            return Json(jsonData);
        }

        /// <summary>
        /// 查询角色List
        /// </summary>
        /// <param name="total">总数</param>
        /// <param name="search">查询条件</param>
        /// <returns>角色列表</returns>
        public List<RoleViewModel> GetRoleViewModel(SearchBaseModel search, out int total)
        {
            RoleBll roleBll = new RoleBll();
            List<Role> roleList = roleBll.GetPageRoles(search.RoleName, search.PageStart, search.PageSize, search.SortCol,search.SortDir,out total);
           
            List<RoleViewModel> roleView = new List<RoleViewModel>();
            UserBll uBll = new UserBll();
            //将数据库查出的List<Role>转为List<RoleViewModel>视图模型
            foreach (var role in roleList)
            {
                RoleViewModel roleViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Code = role.Code,
                    LastChangeTime = role.LastChangeTime.ToString(CultureInfo.InvariantCulture),
                    LastChangeUser = uBll.GetUserById(role.LastChangeUser).UserName,
                    Description = role.Description
                };
                roleView.Add(roleViewModel);
            }
            return roleView;
        }

        /// <summary>
        /// 返回编辑页部分视图
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>部分视图</returns>
        public ActionResult RoleDetail(int id = 0)
        {
            RoleFormViewModel roleForm = new RoleFormViewModel();
            if (id != 0)
            {
                RoleBll bll = new RoleBll();
                Role role = bll.GetRoleById(id);
                if (role != null)
                {
                    roleForm.RoleName = role.RoleName;
                    roleForm.Id = role.Id;
                    roleForm.Description = role.Description;
                    roleForm.Code = role.Code;
                }
                else
                {
                    return PartialView("CreatAndEdit", roleForm);
                }
            }
            return PartialView("CreatAndEdit", roleForm);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="roleFormViewModel">角色视图模型</param>
        /// <returns>保存结果</returns>
        public ActionResult Save(RoleFormViewModel roleFormViewModel)
        {
            RoleBll bll=new RoleBll();
            Role role=new Role();
            string error;
            if (ModelState.IsValid)
            {
                role.RoleName = roleFormViewModel.RoleName;
                role.Id = roleFormViewModel.Id==null?0:int.Parse(roleFormViewModel.Id.ToString());
                role.Code = roleFormViewModel.Code;
                role.Description  = roleFormViewModel.Description;
                role.LastChangeTime = DateTime.Now;
                role.LastChangeUser = int.Parse(Session["UserId"].ToString());
                try
                {
                    bll.SaveRole(role);
                }
                catch (Exception e)
                {
                    error  = "保存出错,请重试";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                return Json("access", JsonRequestBehavior.AllowGet);
            }
            else
            {
                error = "模型验证未通过";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>删除结果</returns>
        public ActionResult Delete(int? id)
        {
            string result = "ok";
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                RoleBll roleBll = new RoleBll();
                try
                {
                    roleBll.Delete(int.Parse(id.ToString()));
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
        /// 检查重名检查角色名是否可用【重新判断逻辑！！】
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="id">角色ID</param>
        /// <returns>true:是，false:否</returns>
        public JsonResult CheckRoleName(string roleName, int id)
        {
            RoleBll bll=new RoleBll();
            var result = bll.CheckRoleName(roleName, id);
            return Json(result);
        }

        /// <summary>
        /// 获取用户jsTree数据
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>json格式的JStree</returns>
        public ActionResult UserTreeList(int id=0)
        {
            RoleBll bll = new RoleBll();
            var roles = bll.GetRoleUser(id);
            var jsTree = JsTreeUserWithCheck(roles);
            return Json(jsTree, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///保存选择的用户
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveChooseUsers(string ids, int roleId = 0)
        {
            if (roleId == 0)
            {
                return Json("请选择一个角色");
            }
            RoleBll bll = new RoleBll();
            string[] userIds = ids.Split(',');
            List<int> intIds = new List<int>();
            foreach (var userId in userIds)
            {
                int intNum;
                if (int.TryParse(userId, out intNum))
                {
                    intIds.Add(intNum);
                }
            }
            try
            {
                bll.SaveRoleUsers(intIds, roleId, int.Parse(Session["UserId"].ToString()));
            }
            catch (Exception e)
            {
                return Json("保存出错", JsonRequestBehavior.AllowGet);
            }
            return Json("access", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取模块树数据
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns>json格式的JStree</returns>
        public ActionResult ModuleTreeList(int id = 0)
        {
            RoleBll bll = new RoleBll();
            var roles = bll.GetRoleModule(id);
            var jsTree = JsTreeUserWithCheck(roles);
            return Json(jsTree, JsonRequestBehavior.AllowGet);
        }
    }
}