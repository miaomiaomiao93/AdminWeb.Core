using AdminWeb.Core.IServices.BASE;
using AdminWeb.Core.Model;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminWeb.Core.IServices
{	
	/// <summary>
	/// ModuleServices
	/// </summary>	
    public interface IModuleServices :IBaseServices<Module>
	{
        /// <summary>
        /// 获取单个菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ModuleViewModels> GetModule(int Id);
        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        TableModel<ModuleViewModels> ListPageModules(ModuleViewModels moduleViewModels);
        /// <summary>
        /// 获取当前可用的菜单路由
        /// </summary>
        /// <returns></returns>
        List<ModuleViewModels> ListModules();
        /// <summary>
        /// 添加新的菜单
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>

        Task<bool> AddModule(ModuleViewModels moduleViewModels);
        /// <summary>
        /// 更新当前菜单
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        Task<bool> UpdateModule(ModuleViewModels moduleViewModels);
        /// <summary>
        /// 作废菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteModule(int Id);

    }
}
