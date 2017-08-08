using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAuthenticate.ViewModels
{
    public class JsTreeModel
    {
        public int id { get; set; }
        /// <summary>
        /// 节点显示的名字
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// parentID（前台一级parentid为#）
        /// </summary>
        public string parent { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool children { get; set; }
    }
}