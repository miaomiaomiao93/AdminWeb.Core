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
        List<ModuleViewModels> ListPageModules(ModuleViewModels moduleViewModels);
    }
}
