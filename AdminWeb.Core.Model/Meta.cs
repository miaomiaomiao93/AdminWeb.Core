using System;
using System.Collections.Generic;

namespace AdminWeb.Core.Model
{
    /// <summary>
    /// 元数据信息
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// 菜单标题（显示的文字）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 使用的图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 当前菜单可访问的角色
        /// </summary>
        public List<string> Role { get; set; }
    }
}
