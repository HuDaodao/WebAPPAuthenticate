using System.Collections.Generic;
using System.Collections;
using Microsoft.Owin.Security.Provider;

namespace WebAppAuthenticate.ViewModels
{
    /// <summary>
    /// DataTable返回模型
    /// </summary>
    public class TableJsonData<T>
    {
        public int draw { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public int iTotalRecords { get; set; }
        /// <summary>
        /// 结果总数
        /// </summary>
        public int iTotalDisplayRecords { get; set; }
        /// <summary>
        /// 分页查询的结果
        /// </summary>
        public List<T> aaData { get; set; }
    }
}