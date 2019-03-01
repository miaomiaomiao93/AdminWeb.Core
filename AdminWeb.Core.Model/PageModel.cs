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

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 5;

        public int TotalCount { get; set; } = 0;

        public string OrderByFileds { get; set; }

    }
}
