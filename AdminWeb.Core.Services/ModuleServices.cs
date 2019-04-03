using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.IRepository;
using AdminWeb.Core.IServices;
using AutoMapper;
using AdminWeb.Core.Model.ViewModels;
using AdminWeb.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using SqlSugar;
using AdminWeb.Core.FrameWork.IRepository;

namespace AdminWeb.Core.Services
{
    /// <summary>
    /// ModuleServices
    /// </summary>	
    public class ModuleServices : BaseServices<Module>, IModuleServices
    {

        IModuleRepository dal;
        IUserRoleServices userRoleServices;
        IMapper IMapper;
        List<string> UserRoles;
        public ModuleServices(IModuleRepository dal, IMapper IMapper, IUserRoleServices userRoleServices)
        {
            this.dal = dal;
            base.baseDal = dal;
            this.IMapper = IMapper;
            this.userRoleServices = userRoleServices;
        }


        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        public TableModel<ModuleViewModels> ListPageModules(ModuleViewModels moduleViewModels)
        {
            List<ModuleViewModels> viewModels = new List<ModuleViewModels>();

            var total = moduleViewModels.TotalCount;
            var orderByFileds = !string.IsNullOrEmpty(moduleViewModels.OrderByFileds) ? "" : moduleViewModels.OrderByFileds;

            //动态拼接拉姆达
            var query = Expressionable.Create<Module>().AndIF(!string.IsNullOrEmpty(moduleViewModels.Name), s => s.Name == moduleViewModels.Name).ToExpression();


            var models = dal.Query(query, moduleViewModels.PageIndex, moduleViewModels.PageSize, moduleViewModels.OrderByFileds, ref total);

            //var models2 = dal.GetSimpleClient()
            //                .Queryable<Module, ModulePermission>((ml, mp) => new object[] { JoinType.Left, ml.Id == mp.ModuleId })
            //                .WhereIF(!string.IsNullOrEmpty(moduleViewModels.Name), (ml, mp) => ml.Name == moduleViewModels.Name)
            //                .Select((ml, mp) =>new ModulePermission {CreateBy=mp.CreateBy,CreateTime=mp.CreateTime })
            //                .ToPageList(moduleViewModels.PageIndex, moduleViewModels.PageSize, ref total);

            foreach (var s in models)
            {
                viewModels.Add(IMapper.Map<ModuleViewModels>(s));
            }

            return new TableModel<ModuleViewModels>() { Code = 1, Count = total, Data = viewModels, Msg = "success" };
        }

        /// <summary>
        /// 获取单个菜单信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ModuleViewModels> GetModule(int Id)
        {
            var module = await QueryByID(Id);
            var moduleViewModel = IMapper.Map<ModuleViewModels>(module);

            moduleViewModel.Meta.Icon = moduleViewModel.Icon;
            moduleViewModel.Meta.Title = moduleViewModel.Title;
            moduleViewModel.Meta.Role = userRoleServices.ListUserRoles(Id);

            return moduleViewModel;
        }

        /// <summary>
        /// 添加单个菜单
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>

        public async Task<bool> AddModule(ModuleViewModels moduleViewModels)
        {
            //转换model
            var module = IMapper.Map<Module>(moduleViewModels);
            return await Add(module) > 0;
        }


        /// <summary>
        /// 更新Model
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        public async Task<bool> UpdateModule(ModuleViewModels moduleViewModels)
        {
            //转换model
            var module = IMapper.Map<Module>(moduleViewModels);
            //忽略更新的字段
            List<string> lstIgnoreColumns = new List<string>()
            {
                "CreateId","CreateBy","CreateTime"
            };
            return await Update(module, null, lstIgnoreColumns, "");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteModule(int Id)
        {
            return await DeleteById(Id);
        }

        /// <summary>
        /// 获取当前菜单树(客户机递归当前菜单)
        /// </summary>
        /// <returns></returns>
        public List<ModuleViewModels> ListClientModules()
        {
            var modules = dal.GetSimpleClient().GetSimpleClient<Module>().GetList(s => s.IsDeleted == false);
            List<ModuleViewModels> viewModels = new List<ModuleViewModels>();
            foreach (var t in modules)
            {
                viewModels.Add(IMapper.Map<ModuleViewModels>(t));
            }
            foreach(var s in viewModels)
            {
                s.Meta.Role = new List<string>() { "Admin" };
            }
            return viewModels;
        }


        /// <summary>
        /// 获取当前菜单树(服务器递归当前菜单)
        /// </summary>
        /// <returns></returns>
        public List<ModuleViewModels> ListServerModules()
        {
            var modules = dal.GetSimpleClient().GetSimpleClient<Module>().GetList(s => s.IsDeleted == false);
            List<ModuleViewModels> viewModels = new List<ModuleViewModels>();
            foreach (var t in modules)
            {
                viewModels.Add(IMapper.Map<ModuleViewModels>(t));
            }
            return GetMenuTrees(viewModels);
        }
        /// <summary>
        /// 读取菜单树
        /// </summary>
        /// <returns></returns>
        public List<ModuleViewModels> GetMenuTrees(List<ModuleViewModels> moduleViewModels)
        {
            //顶级父级菜单
            List<ModuleViewModels> modulePartentModels = new List<ModuleViewModels>();

            //循环出顶级父级菜单
            foreach (var t in moduleViewModels)
            {
                if (t.ParentId == 0)
                {
                    modulePartentModels.Add(t);
                    t.Meta.Icon = t.Icon;
                    t.Meta.Title = t.Title;
                    t.Meta.Role = UserRoles;
                }
            }
            //清楚顶级父级菜单
            moduleViewModels.RemoveAll(s => s.ParentId == 0);
            modulePartentModels = BubbleSort(modulePartentModels);
            //一级菜单的二级菜单
            foreach (var t in modulePartentModels)
            {
                t.Children = GetChildrenViewModels(t.Id, moduleViewModels);
            }
            return modulePartentModels;
        }

        /// <summary>
        /// 递归获取当前的父级的子菜单
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        public List<ModuleViewModels> GetChildrenViewModels(int Id, List<ModuleViewModels> moduleViewModels)
        {

            List<ModuleViewModels> childrenViewModels = new List<ModuleViewModels>();
            foreach (var s in moduleViewModels)
            {
                if (s.ParentId == Id)
                {
                    childrenViewModels.Add(s);
                    s.Meta.Icon = s.Icon;
                    s.Meta.Title = s.Title;
                    s.Meta.Role = UserRoles;
                }
            }
            foreach(var a in childrenViewModels)
            {
                moduleViewModels.Remove(a);
            }
            foreach (var t in childrenViewModels)
            {
                //递归
                t.Children= GetChildrenViewModels(t.Id, moduleViewModels);
            }
            if (childrenViewModels.Count == 0)
            {
                return null;
            }

            return BubbleSort(childrenViewModels);
        }


        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="weighFields"></param>
        /// <returns></returns>
        public List<ModuleViewModels> BubbleSort(List<ModuleViewModels> moduleViewModels)
        {
            var len = moduleViewModels.Count;
            for (var i = 0; i < len - 1; i++)
            {
                for (var j = 0; j < len - 1 - i; j++)
                {

                    if (moduleViewModels[j].OrderSort > moduleViewModels[j + 1].OrderSort)
                    {        // 相邻元素两两对比
                        var temp = moduleViewModels[j + 1];
                        // 元素交换
                        moduleViewModels[j + 1] = moduleViewModels[j];
                        moduleViewModels[j] = temp;
                    }

                }
            }
            return moduleViewModels;
        }

    }
}
