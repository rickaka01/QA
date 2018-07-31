using QA.Model;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace QA.DB
{
    public class TaskDetailDB :BaseDB
    {
        TaskDB db = new TaskDB();

        public TaskDetailDB()
        {
            this.name = "tTaskDetail";
            this.cSql = @"insert into tTaskDetail(cID,cTaskID,cArticleID,cArticleNo,cDetailID,cDetailNo,cAnswerNo)
                                           values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            this.rSql = "select * from tTaskDetail where cTaskID = '{0}'";
        }

        public override bool Create(IModel m1)
        {
            var m = (TaskDetailModel)m1;
            var sql = string.Format(cSql,Guid.NewGuid(),m.TaskID,m.ArticleID,m.ArticleNo,m.DetailID,m.DetailNo,m.AnswerNo);
            var result = utility.Exec(sql);

            return result.Item1;
        }

        public bool Create(IModel m1,string seq)
        {
            var m = (TaskDetailModel)m1;
            var sql1 = string.Format(cSql, Guid.NewGuid(), m.TaskID, m.ArticleID, m.ArticleNo, m.DetailID, m.DetailNo, m.AnswerNo);
            var sql2 = string.Format(db.GetUpdateSQL(),m.TaskID,seq+1);
            var result = utility.Exec(new[]{sql1,sql2});

            return result.Item1;
        }

        public bool Create(TaskDetailModel m,TaskScoreModel m1)
        {
            var x = new TaskScoreDB().GetCreateSQL();

            var sql1 = string.Format(cSql, Guid.NewGuid(), m.TaskID, m.ArticleID, m.ArticleNo, m.DetailID, m.DetailNo, m.AnswerNo);
            var sql2 = string.Format(x, Guid.NewGuid(), m1.TaskID, m1.Score);
            var sql3 = string.Format(db.GetUpdateSQL(), m.TaskID, Common.Seed);
            var sqls = new List<string>();
            sqls.Add(sql1);
            sqls.Add(sql2);
            sqls.Add(sql3);

            var result = utility.Exec(sqls.ToArray());

            return result.Item1;
        }

        private IModel Read(Dictionary<string, object> r)
        {
            var m = new TaskDetailModel();
            var type = typeof(TaskDetailModel);
            var guid = new Guid(r["cID"].ToString());
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.TaskID = new Guid(r["cTaskID"].ToString());
            m.ArticleID = new Guid(r["cArticleID"].ToString());
            m.ArticleNo = r["cArticleNo"].ToString();
            m.DetailID = Guid.Parse(r["cDetailID"].ToString());
            m.DetailNo = r["cDetailNo"].ToString();
            m.AnswerNo = r["cAnswerNo"].ToString();
            return m;
        }

        public override IModel Read(string param)
        {
            return base.Read(param);
        }

        public override List<IModel> List(string param)
        {
            var sql = string.Format(rSql, param);
            var l = utility.List(name, sql);

            var list = new List<IModel>();

            foreach (var r in l)
            {
                list.Add(Read(r));
            }

            return list;
        }
    }
}
