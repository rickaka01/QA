using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public  class TaskDetailModel : IModel
    {
        public Guid TaskID { get; set; }

        public Guid ArticleID { get; set; }

        public string ArticleNo { get; set; }

        public Guid DetailID { get; set; }

        public string DetailNo { get; set; }

        public string AnswerNo { get; set; }
    }
}
