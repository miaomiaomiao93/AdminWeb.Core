using AutoMapper;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWeb.Core.AutoMapper
{
    /// <summary>
    /// Mapper映射配置文件
    /// </summary>
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BlogArticle, BlogViewModels>();
            CreateMap<BlogViewModels,BlogArticle>();

            CreateMap<Module, ModuleViewModels>();
            CreateMap<ModuleViewModels, Module>();
        }
    }
}
