using System.Collections.Generic;

namespace WebAppAuthenticate.ViewModels
{
    public class JsTreeCheck
    {
        public int id { get; set; }
        /// <summary>
        /// 节点显示的名字
        /// </summary>
        public string text { get; set; }

        public State state { get; set; }

        public List<JsTreeCheck> children { get; set; }
    }

    public class State
    {
        public bool opened { get; set; } = true;
        public bool disabled { get; set; } = false;
        public bool selected { get; set; } = false;
    }
}