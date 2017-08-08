﻿using System;
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
    public class ModuleController : Controller
    {
        [HasUserFilter]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回字典JSon数据
        /// </summary>
        /// <param name="id">上级Id</param>
        /// <returns></returns>
        public ActionResult TreeList(string id)
        {
            DictionaryBll bll=new DictionaryBll();
            var dictionary = bll.GetAllDiction();
            List<JsTreeModel>jsTrees=new List<JsTreeModel>();
            foreach (var diction in dictionary)
            {
                JsTreeModel jsTree=new JsTreeModel();
                jsTree.id = diction.Id;
                jsTree.children = false;
                jsTree.text = diction.ChineseName;
                jsTree.parent = "#";
                jsTrees.Add(jsTree);
            }
            return Json(jsTrees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 数据字典主表的编辑
        /// </summary>
        /// <param name="id">数据字典主键</param>
        /// <returns>编辑部分视图</returns>
        public ActionResult MainDictionDetail(int id = 0)
        {
            MainDictionFormViewModel mainDictionForm = new MainDictionFormViewModel();
            if (id != 0)
            {
                DictionaryBll bll = new DictionaryBll();
                MainDictionary mainDic =  bll.GetMainDictioById(id);
                if (mainDic != null)
                {
                    mainDictionForm.Id = mainDic.Id;
                    mainDictionForm.ChineseName = mainDic.ChineseName;
                    mainDictionForm.EnglishName = mainDic.EnglishName;
                    mainDictionForm.Description = mainDic.Description;
                    mainDictionForm.ReadOnly = mainDic.ReadOnly;
                }
                else
                {
                    return PartialView("MainDictionCreatAndEdit", mainDictionForm);
                }
            }
            return PartialView("MainDictionCreatAndEdit", mainDictionForm);
        }

        /// <summary>
        /// 保存数据字典主表
        /// </summary>
        /// <param name="dictionView"></param>
        /// <returns></returns>
        public ActionResult SaveMain(MainDictionFormViewModel dictionView)
        {
            if (ModelState.IsValid)
            {
                MainDictionary mainDictionary = new MainDictionary();
                mainDictionary.Id = dictionView.Id ?? 0;
                mainDictionary.ChineseName = dictionView.ChineseName;
                mainDictionary.Description = dictionView.Description;
                mainDictionary.EnglishName = dictionView.EnglishName;
                mainDictionary.LastChangeTime = DateTime.Now;
                mainDictionary.LastChangeUser = int.Parse(Session["UserId"].ToString());
                mainDictionary.ReadOnly = dictionView.ReadOnly;
                DictionaryBll bll = new DictionaryBll();
                try
                {
                    bll.SaveMainDiction(mainDictionary);
                }
                catch (Exception e)
                {
                    dictionView.Error = "保存出错,请重试";
                    return Json(dictionView.Error, JsonRequestBehavior.AllowGet);
                }
                return Json("access", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dictionView.Error = "模型验证未通过";
                return Json(dictionView.Error, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除数据字典主表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteMain(int? id)
        {
            string result = "ok";
            if (string.IsNullOrEmpty(id.ToString()))
            {
                result = null;
            }
            else
            {
                DictionaryBll bll = new DictionaryBll();
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