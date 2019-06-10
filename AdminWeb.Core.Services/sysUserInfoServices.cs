using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.IServices;
using AdminWeb.Core.IRepository;
using System.Threading.Tasks;
using System.Linq;
using AdminWeb.Core.Model;
using AdminWeb.Core.Model.ViewModels;
using System.Collections.Generic;
using SqlSugar;
using AutoMapper;
using System.IO;

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
        IMapper IMapper;

        public sysUserInfoServices(IsysUserInfoRepository dal, IUserRoleServices userRoleServices, IRoleRepository roleRepository, IMapper IMapper)
        {
            this.dal = dal;
            this.userRoleServices = userRoleServices;
            this.roleRepository = roleRepository;
            this.IMapper = IMapper;
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
        /// 获取用户的角色
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
            var user = (await dal.Query(a => a.uLoginName == loginName && a.uLoginPWD == loginPWD&&a.uStatus==1)).FirstOrDefault();
            return user != null ? user : null;
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public sysUserInfo SysUserInfo(int uid)
        {
            return baseDal.GetSimpleClient().GetSimpleClient<sysUserInfo>().GetById(uid);
        }

        /// <summary>
        /// 根据ID获取用户信息(sysUserInfoViewModels)
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<sysUserInfoViewModels> GetSysUserInfo(int uid)
        {
            var sysUser = await baseDal.QueryByID(uid);
            //sysUser.uHeaderImgUrl = Path.Combine(Directory.GetCurrentDirectory() , sysUser.uHeaderImgUrl);
            return IMapper.Map<sysUserInfoViewModels>(sysUser);
        }


        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="sysUserInfoViewModels"></param>
        /// <returns></returns>
        public TableModel<sysUserInfoViewModels> ListPagesysUserInfos(sysUserInfoViewModels sysUserInfoViewModels)
        {
            List<sysUserInfoViewModels> viewModels = new List<sysUserInfoViewModels>();

            var total = sysUserInfoViewModels.TotalCount;
            var orderByFileds = !string.IsNullOrEmpty(sysUserInfoViewModels.OrderByFileds) ? "" : sysUserInfoViewModels.OrderByFileds;

            //动态拼接拉姆达
            var query = Expressionable.Create<sysUserInfo>().AndIF(!string.IsNullOrEmpty(sysUserInfoViewModels.uRealName), s => s.uRealName == sysUserInfoViewModels.uRealName).ToExpression();


            var models = dal.Query(query, sysUserInfoViewModels.PageIndex, sysUserInfoViewModels.PageSize, sysUserInfoViewModels.OrderByFileds, ref total);

            //var models2 = dal.GetSimpleClient()
            //                .Queryable<Module, ModulePermission>((ml, mp) => new object[] { JoinType.Left, ml.Id == mp.ModuleId })
            //                .WhereIF(!string.IsNullOrEmpty(moduleViewModels.Name), (ml, mp) => ml.Name == moduleViewModels.Name)
            //                .Select((ml, mp) =>new ModulePermission {CreateBy=mp.CreateBy,CreateTime=mp.CreateTime })
            //                .ToPageList(moduleViewModels.PageIndex, moduleViewModels.PageSize, ref total);

            foreach (var s in models)
            {
                //s.uHeaderImgUrl = Path.Combine(Directory.GetCurrentDirectory(), s.uHeaderImgUrl);
                viewModels.Add(IMapper.Map<sysUserInfoViewModels>(s));
            }

            return new TableModel<sysUserInfoViewModels>() { Code = 1, Count = total, Data = viewModels, Msg = "success" };
        }

        /// <summary>
        /// 添加单个用户
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>

        public async Task<bool> AddSysUserInfo(sysUserInfoViewModels sysUserInfoViewModels)
        {
            //转换model
            var module = IMapper.Map<sysUserInfo>(sysUserInfoViewModels);
            return await Add(module) > 0;
        }


        /// <summary>
        /// 更新Model
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        public async Task<bool> UpdateSysUserInfo(sysUserInfoViewModels sysUserInfoViewModels)
        {
            //转换model
            var module = IMapper.Map<sysUserInfo>(sysUserInfoViewModels);
            //忽略更新的字段
            List<string> lstIgnoreColumns = new List<string>()
            {
                "CreateTime"
            };
            return await Update(module, null, lstIgnoreColumns, "");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSysUserInfo(int Id)
        {
            return await DeleteById(Id);
        }
    }
}

//----------sysUserInfo结束----------
