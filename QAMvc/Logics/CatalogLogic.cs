using QA.DB;
using QA.Model;
using QAMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAMvc.Logics
{
    public class CatalogLogic
    {
        CatalogDB db = new CatalogDB();

        public List<CatalogModel> List()
        {
            var list = db.List().Cast<CatalogModel>();
            return list.ToList();
        }

        public bool Create(CatalogViewModel vm)
        {
            var m = new CatalogModel { No = vm.No, Name = vm.Name };

            return db.Create(m);
        }
    }
}