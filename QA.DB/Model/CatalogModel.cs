using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
     public class CatalogModel :IModel
    {
         public string No { get; set; }

         public string Name { get; set; }

         public override string ToString()
         {
             return string.Format("{0},{1},{2}",ID,No,Name);
         }
    }
}
