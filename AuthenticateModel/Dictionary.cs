using System;

namespace AuthenticateModel
{
    /// <summary>
    /// 数据字典从表
    /// </summary>
    public class Dictionary
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
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 上次修改人
        /// </summary>
        public int LastChangeUser { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastChangeTime { get; set; }

        /// <summary>
        /// 数据字典主表ID
        /// </summary>
        public virtual int MainDictionaryId { get; set; }

        /// <summary>
        /// 数据字典主表
        /// </summary>
        public virtual MainDictionary MainDictionary { get; set; }
    }
}
