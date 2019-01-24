using AdminWeb.Core.Services.BASE;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.IRepository;
using AdminWeb.Core.IServices;
using AutoMapper;
using AdminWeb.Core.Model.ViewModels;
using AdminWeb.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AdminWeb.Core.Services
{	
	/// <summary>
	/// ModuleServices
	/// </summary>	
	public class ModuleServices : BaseServices<Module>, IModuleServices
    {
	
        IModuleRepository dal;
        IMapper IMapper;
        public ModuleServices(IModuleRepository dal, IMapper IMapper)
        {
            this.dal = dal;
            base.baseDal = dal;
            this.IMapper = IMapper;
        }


        ///// <summary>
        ///// 获取菜单分页
        ///// </summary>
        ///// <param name="moduleViewModels"></param>
        ///// <returns></returns>
        //public async Task<List<ModuleViewModels>> ListPageModules(ModuleViewModels moduleViewModels)
        //{
        //    List<ModuleViewModels> viewModels = new List<ModuleViewModels>();
        //    var models = await dal.Query();
        //    var query ="";
        //    if (!string.IsNullOrEmpty(moduleViewModels.Name))
        //    {
        //        query = query + "s => s.Name==" + moduleViewModels.Name;
        //    }
        //    if (!string.IsNullOrEmpty(moduleViewModels.Action))
        //    {
        //        query = query + "&&s.Action==" + moduleViewModels.Action;

        //    }
        //    var total = moduleViewModels.TotalCount;
        //    var models1 =dal.Query(query, moduleViewModels.PageIndex, moduleViewModels.PageSize, moduleViewModels.OrderByFileds,ref total);

        //    foreach (var s in models)
        //    {
        //        viewModels.Add(IMapper.Map<ModuleViewModels>(s));
        //    }

        //    return viewModels;
        //}

    }
}
