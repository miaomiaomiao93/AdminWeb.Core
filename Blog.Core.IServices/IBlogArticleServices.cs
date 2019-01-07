using AdminWeb.Core.IServices.BASE;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWeb.Core.IServices
{
    public interface IBlogArticleServices :IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> getBlogs();
        Task<BlogViewModels> getBlogDetails(int id);

    }

}
