using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class ArticleModel : IModel
    {       
        public Guid CatalogID { get; set; }
        public string No { get; set; }
        public string Title { get; set; }
        public ArticleDetailModel[] Details { get; set; }
        public ArticleAnswerModel Answer { get; set; }
    }

    public class ArticleSingleModel : IModel
    {
        public Guid CatalogID { get; set; }
        public string No { get; set; }
        public string Title { get; set; }
    }
}
