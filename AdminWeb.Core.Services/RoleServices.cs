using AdminWeb.Core.IServices;
using AdminWeb.Core.IRepository;
using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.Model.Models;
using System.Threading.Tasks;
using System.Linq;
using AdminWeb.Core.Model.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace AdminWeb.Core.Services
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
	public class RoleServices : BaseServices<Role>, IRoleServices
    {
	
        IRoleRepository dal;
        IMapper IMapper;
        public RoleServices(IRoleRepository dal, IMapper IMapper)
        {
            this.dal = dal;
            base.baseDal = dal;
            this.IMapper = IMapper;
        }

        /// <summary>
        /// ±£´æ½ÇÉ«
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<Role> SaveRole(RoleViewModel roleViewModel)
        {
            Role role = IMapper.Map<Role>(roleViewModel);
            Role model = new Role();
            var userList = await dal.Query(a => a.Name == role.Name && a.Enabled);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await dal.Add(role);
                model = await dal.QueryByID(id);
            }
            return model;
        }
    }
}
