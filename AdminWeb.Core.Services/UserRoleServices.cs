using AdminWeb.Core.IServices;
using AdminWeb.Core.FrameWork.IRepository;
using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.Model.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using SqlSugar;

namespace AdminWeb.Core.Services
{
    /// <summary>
    /// UserRoleServices
    /// </summary>	
    public class UserRoleServices : BaseServices<UserRole>, IUserRoleServices
    {

        IUserRoleRepository dal;
        public UserRoleServices(IUserRoleRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        public async Task<UserRole> SaveUserRole(int uid, int rid)
        {

            UserRole model = new UserRole();
            var userList = await dal.Query(a => a.UserId == uid && a.RoleId == rid);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                UserRole userRole = new UserRole() { UserId = uid, RoleId = rid };
                var id = await dal.Add(userRole);
                model = await dal.QueryByID(id);
            }

            return model;

        }

        /// <summary>
        /// 获取当前用户的角色
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<string> ListUserRoles(int uid)
        {
            var userRoles = dal.GetSimpleClient()
                            .Queryable<UserRole, Role>((ur, rl) => new object[] { JoinType.Left, ur.UserId == rl.Id })
                            .Where((ur, rl) => ur.UserId == uid)
                            .Select((ur, rl) => new Role { Name = rl.Name })
                            .ToList();
            return userRoles.Select(s => s.Name).ToList();
        }
    }
}
