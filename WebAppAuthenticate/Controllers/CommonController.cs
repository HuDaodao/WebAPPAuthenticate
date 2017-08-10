using AuthenticateBLL;
using AuthenticateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAuthenticate.ViewModels;

namespace WebAppAuthenticate.Controllers
{
    public class CommonController : Controller
    {
        /// <summary>
        /// 角色树数据绑定
        /// </summary>
        /// <param name="idList">当前模块所有的角色ID</param>
        /// <returns>角色树数据</returns>
        public JsTreeCheck JsTreeRoleWithCheck(List<int> idList)
        {
            RoleBll bll = new RoleBll();
            var roles = bll.GetAllRole();
            JsTreeCheck jst = new JsTreeCheck();
            jst.id = 0;
            jst.text = "全选";

            List<JsTreeCheck> children = new List<JsTreeCheck>();
            foreach (var role in roles)
            {
                State childState = new State();
                if (idList.Contains(role.Id))
                {
                    childState.selected = true;
                }
                JsTreeCheck child = new JsTreeCheck()
                {
                    id = role.Id,
                    text = role.RoleName,
                    state = childState
                };
                children.Add(child);
            }
            jst.children = children;
            return jst;
        }

        /// <summary>
        /// 用户树数据绑定
        /// </summary>
        /// <param name="idList">当前角色所有的用户ID</param>
        /// <returns>用户树数据</returns>
        public JsTreeCheck JsTreeUserWithCheck(List<int> idList)
        {
            UserBll bll = new UserBll();
            var users = bll.GetAllUser();
            JsTreeCheck jst = new JsTreeCheck();
            jst.id = 0;
            jst.text = "全选";

            List<JsTreeCheck> children = new List<JsTreeCheck>();
            foreach (var user in users)
            {
                State childState = new State();
                if (idList.Contains(user.Id))
                {
                    childState.selected = true;
                }
                JsTreeCheck child = new JsTreeCheck()
                {
                    id = user.Id,
                    text = user.UserName,
                    state = childState
                };
                children.Add(child);
            }
            jst.children = children;
            return jst;
        }

        /// <summary>
        /// 模块树数据绑定
        /// </summary>
        /// <returns>用户树数据</returns>
        public JsTreeCheck JsTreeAllModulewhithHead()
        {
            ModuleBll bll = new ModuleBll();
            JsTreeCheck jst = new JsTreeCheck();
            jst.id = 0;
            jst.text = "所有模块";
            var childrenModule = bll.GetSonModule(0);
            jst.children = JsTreeAllModule(childrenModule);
            return jst;
        }

        /// <summary>
        /// 递归拼出模块树
        /// </summary>
        /// <param name="modules">模块列表</param>
        /// <returns>模块列表</returns>
        public List<JsTreeCheck> JsTreeAllModule(List<Module> modules)
        {
            ModuleBll bll=new ModuleBll();
            List<JsTreeCheck> jsTreeList = new List<JsTreeCheck>();
            foreach (var module in modules)
            {
                JsTreeCheck  jsTree=new JsTreeCheck();
                jsTree.id = module.Id;
                jsTree.text = module.ChineseName;
                var childrenModel = bll.GetSonModule(module.Id);
                if (childrenModel.Count != 0)
                {
                      jsTree.children=JsTreeAllModule(childrenModel);
                }
                jsTreeList .Add(jsTree);
            }
            return jsTreeList;
        }
    }
}