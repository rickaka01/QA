using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAMvc.Models
{
    public class ArticleViewModel
    {
        public string id { get; set; }
        public string p { get; set; }
        public string p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public string p4 { get; set; }
        public string p5 { get; set; }
    }

    public class TaskArticleViewModel : ArticleViewModel
    {
        public string tid { get; set; }
        public string i { get; set; }
    }
}