using System.Collections.Generic;

namespace WebAppAuthenticate.ViewModels
{
    public class RoleJsTree
    {
        public int id { get; set; }
        /// <summary>
        /// 节点显示的名字
        /// </summary>
        public string text { get; set; }

        public List<RoleJsTree> children { get; set; }
    }
}