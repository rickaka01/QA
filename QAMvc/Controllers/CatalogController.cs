using QAMvc.Logics;
using QAMvc.Models;
using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace QAMvc.Controllers
{
    public class CatalogController : ApiController
    {
        CatalogLogic logic = new CatalogLogic();

        [HttpPost]
        public List<CatalogViewModel> List()
        {
            var list = new List<CatalogViewModel>();

            var source = logic.List().Cast<CatalogModel>();

            foreach (var item in source)
            {
                list.Add(new CatalogViewModel { ID = item.ID.ToString(), Name = item.Name, No = item.No });
            }

            return list;
        }

        [HttpPost]
        public bool Create(CatalogViewModel vm)
        {
            return logic.Create(vm);
        }
    }
}
