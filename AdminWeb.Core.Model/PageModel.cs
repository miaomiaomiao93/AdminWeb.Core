using System;
using System.Collections.Generic;
using System.Text;

namespace AdminWeb.Core.Model
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PageModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public string OrderByFileds { get; set; }

    }
}
