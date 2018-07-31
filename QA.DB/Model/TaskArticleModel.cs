using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class TaskArticleModel : IModel
    {
        public Guid TaskID { get; set; }

        public int Seq { get; set; }

        public Guid ArticleID { get; set; }

        public string ArticleNo { get; set; }
    }
}
