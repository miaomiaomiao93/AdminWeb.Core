using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminWeb.Core.IRepository;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Repository.Base;

namespace AdminWeb.Core.Repository
{
    public class BlogArticleRepository: BaseRepository<BlogArticle>, IBlogArticleRepository
    {
    }
}
