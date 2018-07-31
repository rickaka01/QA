using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class ArticleDetailModel : IModel
    {
        public Guid ArticleID { get; set; }

        public string AnswerNo { get; set; }

        public string Title { get; set; }
    }
}
