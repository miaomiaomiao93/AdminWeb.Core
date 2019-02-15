using AdminWeb.Core.IServices.BASE;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminWeb.Core.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface IRoleServices :IBaseServices<Role>
	{
        Task<Role> SaveRole(RoleViewModel roleViewModel);


    }
}
