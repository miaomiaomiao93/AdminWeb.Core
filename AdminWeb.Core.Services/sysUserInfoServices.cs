using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.IServices;
using AdminWeb.Core.IRepository;
using System.Threading.Tasks;
using System.Linq;

namespace AdminWeb.Core.FrameWork.Services
{
    /// <summary>
    /// sysUserInfoServices
    /// </summary>	
    public class sysUserInfoServices : BaseServices<sysUserInfo>, IsysUserInfoServices
    {

        IsysUserInfoRepository dal;
        IUserRoleServices userRoleServices;
        IRoleRepository roleRepository;
        public sysUserInfoServices(IsysUserInfoRepository dal, IUserRoleServices userRoleServices, IRoleRepository roleRepository)
        {
            this.dal = dal;
            this.userRoleServices = userRoleServices;
            this.roleRepository = roleRepository;
            base.baseDal = dal;
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPWD"></param>
        /// <returns></returns>
        public async Task<sysUserInfo> SaveUserInfo(string loginName, string loginPWD)
        {
            sysUserInfo sysUserInfo = new sysUserInfo();
            sysUserInfo.uLoginName = loginName;
            sysUserInfo.uLoginPWD = loginPWD;
            sysUserInfo model = new sysUserInfo();
            var userList = await dal.Query(a => a.uLoginName == sysUserInfo.uLoginName && a.uLoginPWD == sysUserInfo.uLoginPWD);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await dal.Add(sysUserInfo);
                model = await dal.QueryByID(id);
            }

            return model;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPWD"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleNameStr(string loginName, string loginPWD)
        {
            string roleName = "";
            var user = (await dal.Query(a => a.uLoginName == loginName && a.uLoginPWD == loginPWD)).FirstOrDefault();
            if (user != null)
            {
                var userRoles = await userRoleServices.Query(ur => ur.UserId == user.uID);
                if (userRoles.Count > 0)
                {
                    var roles = await roleRepository.QueryByIDs(userRoles.Select(ur => ur.RoleId.ObjToString()).ToArray());

                    roleName = string.Join(',', roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }

        /// <summary>
        /// 验证是否能登陆
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPWD"></param>
        /// <returns></returns>
        public async Task<sysUserInfo> CheckUserInfo(string loginName, string loginPWD)
        {
            var user = (await dal.Query(a => a.uLoginName == loginName && a.uLoginPWD == loginPWD)).FirstOrDefault();
            return user != null ? user : null;
        }
    }
}

//----------sysUserInfo结束----------
