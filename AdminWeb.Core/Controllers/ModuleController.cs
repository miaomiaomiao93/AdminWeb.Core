using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWeb.Core.IServices;
using AdminWeb.Core.Model;
using AdminWeb.Core.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Core.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [Route("api/Module")]
    [ApiController]
    public class ModuleController : Controller
    {
        IModuleServices moduleServices;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="moduleServices"></param>
        public ModuleController(IModuleServices moduleServices)
        {
            this.moduleServices = moduleServices;
        }

        /// <summary>
        /// 获取单个菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("Get")]
        [HttpGet]
        public async Task<object> Get(int Id)
        {
            var model =await moduleServices.QueryByID(Id);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        [Route("ListPage")]
        [HttpPost]
        public  IActionResult ListPage([FromBody] ModuleViewModels moduleViewModels)
        {
            var models = moduleServices.ListPageModules(moduleViewModels);
            return Ok(new TableModel<ModuleViewModels>() { Code = 1, Count = moduleViewModels.TotalCount, Data = models, Msg = "success" });
        }
    }
}