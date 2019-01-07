using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminWeb.Core.IRepository;
using AdminWeb.Core.IServices;
using AdminWeb.Core.Model.Models;
using AdminWeb.Core.Services.BASE;

namespace AdminWeb.Core.Services
{
    public class TopicServices: BaseServices<Topic>, ITopicServices
    {

        ITopicRepository dal;
        public TopicServices(ITopicRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }


    }
}
