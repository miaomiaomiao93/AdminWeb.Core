using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.IServices;
using AdminWeb.Core.IRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using AdminWeb.Core.Common;

namespace AdminWeb.Core.Services
{
    /// <summary>
    /// RoleModulePermissionServices 应用服务
    /// </summary>	
    public class RoleModulePermissionServices : BaseServices<RoleModulePermission>, IRoleModulePermissionServices
    {

        IRoleModulePermissionRepository dal;
        IModuleRepository moduleRepository;
        IRoleRepository roleRepository;

        // 将多个仓储接口注入
        public RoleModulePermissionServices(IRoleModulePermissionRepository dal, IModuleRepository moduleRepository, IRoleRepository roleRepository)
        {
            this.dal = dal;
            this.moduleRepository = moduleRepository;
            this.roleRepository = roleRepository;
            base.baseDal = dal;
        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = await dal.Query(a => a.IsDeleted == false);
            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = await roleRepository.QueryByID(item.RoleId);
                    item.Module = await moduleRepository.QueryByID(item.ModuleId);
                }

            }
            return roleModulePermissions;
        }

    }
}
