using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Core.Controllers
{
    public class sysUserInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}