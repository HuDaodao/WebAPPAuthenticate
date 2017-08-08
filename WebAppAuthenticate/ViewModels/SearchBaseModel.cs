namespace WebAppAuthenticate.ViewModels
{
    /// <summary>
    /// DataTable查询模型
    /// </summary>
    public class SearchBaseModel
    {
        public int PageStart { get; set; }
        public int PageSize { get; set; }
        public int Draw { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortCol { get; set; }
        /// <summary>
        /// 排序顺序
        /// </summary>
        public string SortDir { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public string RealName { get; set; }
        /// <summary>
        /// 所属数据字典主表ID
        /// </summary>
        public int MainDicId { get; set; }
        
        public string RoleName { get; set; }
    }
}