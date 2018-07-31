using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class IModel
    {
        public Guid ID { get; private set; }

        public DateTime CreateDate { get; private set; }

        public string CreateUser { get; set; }
    }
}
