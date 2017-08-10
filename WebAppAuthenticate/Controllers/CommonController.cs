using System.Collections.Generic;
using System.Web.Mvc;

using AuthenticateModel;
using AuthenticateBLL;
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
        /// <paramref name="ids">模块id列表</paramref>
        /// <returns>用户树数据</returns>
        public JsTreeCheck JsTreeAllModulewhithHead(List<int> ids)
        {
            ModuleBll bll = new ModuleBll();
            JsTreeCheck jst = new JsTreeCheck();
            jst.id = 0;
            jst.text = "所有模块";
            var childrenModule = bll.GetSonModule(0);
            jst.children = JsTreeAllModule(childrenModule,ids);
            return jst;
        }

        /// <summary>
        /// 递归拼出模块树
        /// </summary>
        /// <param name="modules">模块列表</param>
        /// <param name="ids">模块id列表</param>
        /// <returns>模块列表</returns>
        public List<JsTreeCheck> JsTreeAllModule(List<Module> modules,List<int>ids)
        {
            ModuleBll bll=new ModuleBll();
            List<JsTreeCheck> jsTreeList = new List<JsTreeCheck>();
            foreach (var module in modules)
            {
                State childState = new State();
                if (ids != null)
                {
                    if (ids.Contains(module.Id))
                    {
                        childState.selected = true;
                    }
                }
                JsTreeCheck jsTree = new JsTreeCheck
                {
                    id = module.Id,
                    text = module.ChineseName,
                    state =childState
                };
                var childrenModel = bll.GetSonModule(module.Id);
                if (childrenModel.Count != 0)
                {
                      jsTree.children=JsTreeAllModule(childrenModel,ids);
                }
                jsTreeList .Add(jsTree);
            }
            return jsTreeList;
        }


    }
}