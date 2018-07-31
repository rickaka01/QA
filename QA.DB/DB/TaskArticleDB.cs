using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QA.DB
{
    public class TaskArticleDB : BaseDB
    {
        public TaskArticleDB()
        {
            this.name = "ttaskArticle";
            this.cSql = "insert ttaskArticle(cID,cTaskID,cSeq,cArticleID,cArticleNo) values('{0}','{1}',{2},'{3}','{4}')";
            this.rSql = "select * from ttaskArticle where cTaskID='{0}' and cSeq={1}";
        }

        public string GetCreateSQL()
        {
            return this.cSql;
        }

        private IModel Read(Dictionary<string, object> r)
        {
            var m = new TaskArticleModel();
            var guid = new Guid(r["cID"].ToString());
            var type = typeof(TaskArticleModel);
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.Seq = Convert.ToInt32(r["cSeq"]);
            m.TaskID = Guid.Parse(r["cTaskID"].ToString());
            m.ArticleID = Guid.Parse(r["cArticleID"].ToString());
            m.ArticleNo = r["cArticleNo"].ToString();
            return m;
        }

        public IModel Read(string tid, int seq)
        {
            var sql = string.Format(rSql,tid,seq);
            var result = utility.List(name, sql);
            var r = result.FirstOrDefault();
            return Read(r);
        }
    }
}
