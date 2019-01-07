using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.IRepository;
using AdminWeb.Core.IServices;

namespace AdminWeb.Core.Services
{	
	/// <summary>
	/// ModuleServices
	/// </summary>	
	public class ModuleServices : BaseServices<Module>, IModuleServices
    {
	
        IModuleRepository dal;
        public ModuleServices(IModuleRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}
