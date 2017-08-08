using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticateBLL;
using WebAppAuthenticate.ViewModels;
using AuthenticateModel;
using WebAppAuthenticate.Filters;

namespace WebAppAuthenticate.Controllers
{
   [HasUserFilter]
    public class ModuleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回模块JSon数据
        /// </summary>
        /// <param name="id">上级Id</param>
        /// <returns></returns>
        public ActionResult TreeList(string id)
        {
            ModuleBll bll=new ModuleBll();
            var modules = bll.GetAllModule();
            List<JsTreeModel>jsTrees=new List<JsTreeModel>();
            foreach (var module in modules)
            {
                JsTreeModel jsTree=new JsTreeModel();
                jsTree.id = module.Id;
                jsTree.children = false;
                jsTree.text = module.ChineseName;
                jsTree.parent = "#";
                jsTrees.Add(jsTree);
            }
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
            string result = "ok";
            if (string.IsNullOrEmpty(id.ToString()))
            {
                result = null;
            }
            else
            {
                ModuleBll bll = new ModuleBll();
                try
                {
                    bll.DeleteMain(int.Parse(id.ToString()));
                }
                catch (Exception e)
                {
                    result = null;
                }
            }
            return Json(result);
        }
    }
}