using QA.Model;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace QA.DB
{
    public class ArticleDetailDB : BaseDB
    {
        public ArticleDetailDB()
        {
            this.name = "tarticledetail";
            this.cSql = "INSERT INTO tarticledetail(cID,cArticleID,cAnswerNo,cTitle) values ('{0}','{1}','{2}','{3}')";
            this.rSql = "select * from tarticledetail where cArticleID='{0}'";
        }

        public string GetCreateSQL()
        {
            return this.cSql;
        }

        private IModel Read(Dictionary<string, object> r)
        {
            var m = new ArticleDetailModel();

            // 开始赋值
            var type = typeof(ArticleDetailModel);
            var guid = new Guid(r["cID"].ToString());
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.ArticleID = new Guid(r["cArticleID"].ToString());
            m.AnswerNo = r["cAnswerNo"].ToString();
            m.Title = r["cTitle"].ToString();

            return m;
        }

        public override IModel Read(string id)
        {
            var sql = string.Format(rSql, id);

            var l = utility.Get(name, sql);

            return Read(l);
        }

        public IModel Read(string aid, string no)
        {
            var sql = string.Format(rSql, aid,no);

            var l = utility.Get(name, sql);

            return Read(l);
        }

        public override List<IModel> List(string id)
        {
            var list = new List<IModel>();
            var sql = string.Format(rSql,id);

            var l= utility.List(name, sql);

            foreach (var r in l)
            {
                var m = Read(r);
                list.Add(m);
            }

            return list;
        }
    }
}
