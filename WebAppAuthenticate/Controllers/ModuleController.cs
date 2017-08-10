using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AuthenticateBLL;
using WebAppAuthenticate.ViewModels;
using AuthenticateModel;
using WebAppAuthenticate.Filters;

namespace WebAppAuthenticate.Controllers
{
   [HasUserFilter]
    public class ModuleController : CommonController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回模块jsTree数据
        /// </summary>
        /// <param name="id">上级Id</param>
        /// <returns></returns>
        public ActionResult TreeList(string id)
        {
            ModuleBll bll=new ModuleBll();
            var jsTrees = JsTreeAllModulewhithHead(null);
            return Json(jsTrees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 模块的编辑
        /// </summary>
        /// <param name="id">模块主键</param>
        /// <returns>编辑部分视图</returns>
        public ActionResult ModuleDetail(int id = 0)
        {
            ModuleFormViewModel moduleForm = new ModuleFormViewModel();
            if (id != 0)
            {
                ModuleBll bll = new ModuleBll();
                Module module =  bll.GetModuleById(id);
                if (module != null)
                {
                    moduleForm.Id = module.Id;
                    moduleForm.ChineseName = module.ChineseName;
                    moduleForm.EnglishName = module.EnglishName;
                    moduleForm.Url = module.Url;
                    moduleForm.Icon = module.Icon;
                    moduleForm.Order = module.Order;
                    moduleForm.Status = module.Status;
                    moduleForm.IsDisplay = module.IsDisplay;
                    moduleForm.NavigatePicture = module.NavigatePicture;
                    moduleForm.Description = module.Description;
                }
                else
                {
                    return PartialView("ModuleEdit", moduleForm);
                }
            }
            return PartialView("ModuleEdit", moduleForm);
        }

        /// <summary>
        /// 保存模块
        /// </summary>
        /// <param name="moduleView">模块实体视图模型</param>
        /// <returns>保存结果</returns>
        public ActionResult Save(ModuleFormViewModel moduleView)
        {
            if (ModelState.IsValid)
            {
                Module module = new Module();
                module.Id = moduleView.Id ?? 0;
                module.ChineseName = moduleView.ChineseName;
                module.ParentId = moduleView.ParentId??0;
                module.Description = moduleView.Description;
                module.EnglishName = moduleView.EnglishName;
                module.LastChangeTime = DateTime.Now;
                module.LastChangeUser = int.Parse(Session["UserId"].ToString());
                module.Url = moduleView.Url;
                module.Icon = moduleView.Icon;
                module.Order = moduleView.Order;
                module.Status = moduleView.Status;
                module.IsDisplay = moduleView.IsDisplay;
                module.NavigatePicture = moduleView.NavigatePicture;

                ModuleBll bll = new ModuleBll();
                try
                {
                    bll.SaveModule(module);
                }
                catch (Exception e)
                {
                    moduleView.Error= "保存出错,请重试";
                    return Json(moduleView.Error,JsonRequestBehavior.AllowGet);
                }
                return Json("access", JsonRequestBehavior.AllowGet);
            }
            else
            {
                moduleView.Error = "模型验证未通过";
                return Json(moduleView .Error, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns>删除结果</returns>
        public ActionResult Delete(int? id)
        {
            string result = "accesss";
            if (string.IsNullOrEmpty(id.ToString()))
            {
                result = "请选择一个模块";
            }
            else
            {
                ModuleBll bll = new ModuleBll();
                int parentId = int.Parse(id.ToString());
                int sonCount = bll.GetSonModule(parentId).Count;
                if (sonCount != 0)
                {
                    result = "有子模块不能删除";
                }
                else
                {
                    try
                    {
                        bll.DeleteMain(int.Parse(id.ToString()));
                    }
                    catch (Exception e)
                    {
                        result = "删除出错！";
                    }
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 获取角色jstree数据
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns>jstree数据</returns>
        public ActionResult RoleTreeList(int id = 0)
        {
            ModuleBll bll=new ModuleBll();
            List<int> getRoles = bll.GetModuleRole(id);
            var jsTree=JsTreeRoleWithCheck(getRoles);
            return Json(jsTree, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///保存选择的角色
        /// </summary>
        /// <returns>保存结果</returns>
        public ActionResult SaveChooseRoles(string ids, int moduleId = 0)
        {
            if (moduleId == 0)
            {
                return Json("请选择一个模块");
            }
            ModuleBll bll = new ModuleBll();
            string[] roleIds = ids.Split(',');
            List<int> intIds = new List<int>();
            foreach (var roleId in roleIds)
            {
                int intNum;
                if (int.TryParse(roleId, out intNum))
                {
                    intIds.Add(intNum);
                }
            }
            try
            {
                bll.SaveModuleRoles(intIds, moduleId, int.Parse(Session["UserId"].ToString()));
            }
            catch (Exception e)
            {
                return Json("保存出错", JsonRequestBehavior.AllowGet);
            }
            return Json("access", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 检查重名 检查模块名是否可用
        /// </summary>
        /// <param name="moduleName">用户名</param>
        /// <param name="id">用户id</param>
        /// <returns>true:是，false:否</returns>
        public JsonResult CheckModuleName(string moduleName, int id)
        {
            ModuleBll bll = new ModuleBll();
            var result = bll.CheckModuleName(moduleName, id);
            return Json(result);
        }
    }
}