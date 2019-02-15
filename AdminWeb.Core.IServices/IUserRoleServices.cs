using AdminWeb.Core.IServices.BASE;
using AdminWeb.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminWeb.Core.IServices
{	
	/// <summary>
	/// UserRoleServices
	/// </summary>	
    public interface IUserRoleServices :IBaseServices<UserRole>
	{

        Task<UserRole> SaveUserRole(int uid, int rid);

        List<string> ListUserRoles(int uid);
    }
}

