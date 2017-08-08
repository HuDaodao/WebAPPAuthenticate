using System;

namespace AuthenticateModel
{
    /// <summary>
    /// 数据字典主表
    /// </summary>
    public class MainDictionary
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 上次修改人
        /// </summary>
        public int LastChangeUser { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastChangeTime { get; set; }
    }
}
