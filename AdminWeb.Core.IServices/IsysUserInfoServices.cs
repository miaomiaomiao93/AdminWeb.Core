    

using AdminWeb.Core.IServices.BASE;
using AdminWeb.Core.Model.Models;
using System.Threading.Tasks;

namespace AdminWeb.Core.IServices
{	
	/// <summary>
	/// sysUserInfoServices
	/// </summary>	
    public interface IsysUserInfoServices :IBaseServices<sysUserInfo>
	{
        Task<sysUserInfo> SaveUserInfo(string loginName, string loginPWD);
        Task<string> GetUserRoleNameStr(string loginName, string loginPWD);
        Task<sysUserInfo> CheckUserInfo(string loginName, string loginPWD);

        sysUserInfo SysUserInfo(int uid);

    }
}
