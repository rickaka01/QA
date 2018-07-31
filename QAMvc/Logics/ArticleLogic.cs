using QAMvc.Models;
using Utility.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QA.DB;
using QA.Model;

namespace QAMvc.Logics
{
    public class ArticleLogic
    {
        ArticleDB db = new ArticleDB();
        ArticleAnswerDB db3 = new ArticleAnswerDB();

        public bool Create(ArticleViewModel m)
        {
            var details = new List<ArticleDetailModel>();
            details.Add(new ArticleDetailModel { AnswerNo ="A", Title = m.p1  });
            details.Add(new ArticleDetailModel { AnswerNo = "B", Title = m.p2 });
            details.Add(new ArticleDetailModel { AnswerNo = "C", Title = m.p3 });
            details.Add(new ArticleDetailModel { AnswerNo = "D", Title = m.p4 });

            ArticleModel d = new ArticleModel
            {
                CatalogID = new Guid(m.id),
                Title = m.p,
                Details = details.ToArray(),
                Answer = new ArticleAnswerModel { cNo = m.p5 },
            };

            return db.Create(d);
        }

        public ArticleModel Get(string articleID)
        {
            return (ArticleModel)db.Read(articleID);
        }

        public bool Check(TaskViewModel m)
        {
            var r = (ArticleAnswerModel)db3.Read(m.p);
            return r.cNo == m.p1;
        }
    }
}