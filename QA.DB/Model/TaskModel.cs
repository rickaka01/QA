using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class TaskModel : IModel
    {
        public Guid CatalogID{get;set;}

        public Guid UserID{get;set;}

        public string No { get; set; }

        public string Status { get; set; }
    }
}
