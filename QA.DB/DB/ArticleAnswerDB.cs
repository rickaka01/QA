using QA.Model;
using System;
using System.Linq;
using System.Reflection;

namespace QA.DB
{
    public class ArticleAnswerDB : BaseDB
    {
        public ArticleAnswerDB()
        {
            this.name = "tarticleanswer";
            this.cSql = "INSERT INTO tarticleanswer(cID,cArticleID,cNo) values('{0}','{1}','{2}')";
            this.rSql = "select * from tarticleanswer where cArticleID='{0}'";
        }

        public string GetCreateSQL()
        {
            return this.cSql;
        }

        public override IModel Read(string articleID)
        {
            var m = new ArticleAnswerModel();
            var sql = string.Format(rSql, articleID);

            var result = utility.List(name, sql);

            var r = result.FirstOrDefault();


            var type = typeof(ArticleAnswerModel);

            var guid = new Guid(r["cID"].ToString());
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.cNo = r["cNo"].ToString();


            return m;
        } 
    }
}
