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
        Task<ModuleViewModels> GetModule(int Id);

        List<ModuleViewModels> ListPageModules(ModuleViewModels moduleViewModels);

        Task<bool> AddModule(ModuleViewModels moduleViewModels);


        Task<bool> UpdateModule(ModuleViewModels moduleViewModels);

        Task<bool> DeleteModule(int Id);

    }
}
