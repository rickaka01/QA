using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class ArticleAnswerModel : IModel
    {
        public Guid cArticleID { get; set; }

        public string cNo { get; set; }
    }
}
