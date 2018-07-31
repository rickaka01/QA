
using QAMvc.Logics;
using QAMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QAMvc.Controllers
{
    public class ArticleController : ApiController
    {
        ArticleLogic logic;

        public ArticleController()
        {
            logic = new ArticleLogic();
        }

        [HttpPost]
        public bool Create(ArticleViewModel m)
        {
            if (m != null)
            {
                return logic.Create(m);
            }
            return false;
        }
    }
}
