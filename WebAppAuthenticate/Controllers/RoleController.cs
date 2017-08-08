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
    public class RoleController : Controller
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
        /// 获取角色jstree数据
        /// </summary>
        /// <returns>json格式的JStree</returns>
        public ActionResult UserTreeList()
        {
            UserBll bll = new UserBll();
            var roles = bll.GetAllUser();
            syncJsTree jst = new syncJsTree();
            jst.id = 0;
            jst.text = "全选";
            List<syncJsTree> children = new List<syncJsTree>();
            foreach (var role in roles)
            {
                syncJsTree child = new syncJsTree()
                {
                    id = role.Id,
                    text = role.UserName,
                };
                children.Add(child);
            }
            jst.children = children;
            return Json(jst, JsonRequestBehavior.AllowGet);
        }
    }
}