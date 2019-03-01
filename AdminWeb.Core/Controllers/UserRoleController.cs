using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWeb.Core.AuthHelper;
using AdminWeb.Core.BasicData;
using AdminWeb.Core.IServices;
using AdminWeb.Core.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Core.Controllers
{
    /// <summary>
    /// 用户权限控制器所有接口
    /// </summary>
    [Authorize(Policy = "Admin")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleController : Controller
    {
        IsysUserInfoServices sysUserInfoServices;
        IUserRoleServices userRoleServices;
        IRoleServices roleServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleServices"></param>
        public UserRoleController(IsysUserInfoServices sysUserInfoServices, IUserRoleServices userRoleServices, IRoleServices roleServices)
        {
            this.sysUserInfoServices = sysUserInfoServices;
            this.userRoleServices = userRoleServices;
            this.roleServices = roleServices;
        }



        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPWD"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddUser(string loginName, string loginPWD)
        {
            var model = await sysUserInfoServices.SaveUserInfo(loginName, loginPWD);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 新建Role
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> AddRole([FromBody]RoleViewModel roleViewModel)
        {
            var model = await roleServices.SaveRole(roleViewModel);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 新建用户角色关系
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddUserRole(int uid, int rid)
        {
            var model = await userRoleServices.SaveUserRole(uid, rid);
            return Ok(new
            {
                success = true,
                data = model
            });
        }
        /// <summary>
        /// 获取当前的用户的角色以及用户的信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles(int uid)
        {
            var roles =userRoleServices.ListUserRoles(uid);
            var user = await sysUserInfoServices.QueryByID(uid);
            return Ok(new
            {
                success = true,
                roles = roles,
                name= user.uLoginName,
                avatar=user.uHeaderImgUrl
            });
        }
    }
}
