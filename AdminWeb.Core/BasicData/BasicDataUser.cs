using AdminWeb.Core.AuthHelper;
using AdminWeb.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWeb.Core.BasicData
{
    /// <summary>
    /// 当前用户登录信息
    /// </summary>
    public class BasicDataUser
    {
        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// 当前用户Id
        /// </summary>
        public static int UserId { get; set; }
    }
}
