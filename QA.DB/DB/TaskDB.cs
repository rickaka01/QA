using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utitlity;

namespace QA.DB
{
    public class TaskDB : BaseDB
    {
        ArticleDB adb = new ArticleDB();
        TaskArticleDB tadb = new TaskArticleDB();

        private string userSql = "select * from ttask where cUserID='{0}' and cStatus<={1} limit 1";

        public TaskDB()
        {
            this.name = "ttask";
            this.cSql = @"insert into ttask(cID,cCatalogID,cUserID,cNo) values('{0}','{1}','{2}','{3}')";
            this.rSql = @"select * from ttask where cid='{0}'";
            this.uSql = "update ttask set cStatus='{1}' where cID='{0}'";
        }

        public string GetUpdateSQL()
        {
            return this.uSql;
        }

        public string Create(string uid,string cid)
        {
            List<string> sqls = new List<string>();

            var tid = string.Empty;

            tid = Guid.NewGuid().ToString();
            var no = new NumberGenerator().Next();

            // 开始创建数据
            // 首先调用article表,随机获取5个
            var list = adb.Rand(cid).Cast<ArticleSingleModel>();

            // 然后生成task和taskarticle
            var sql1 = string.Format(cSql, tid, cid, uid, no);
            sqls.Add(sql1);

            int i=0;
            foreach (var item in list)
            {
                i++;
                var sql2 = string.Format(tadb.GetCreateSQL(), Guid.NewGuid(),tid,i, item.ID, item.No);
                sqls.Add(sql2);
            }

            var result =utility.Exec(sqls.ToArray());
            if (result.Item1)
                return tid;
            else
                return null;
        }

        public bool Update(string id, string seq)
        {
            var sql = string.Format(uSql, id, seq + 1);
            var result = utility.Exec(sql);

            return result.Item1;
        }

        private IModel Read(Dictionary<string, object> r)
        {
            var m = new TaskModel();
            var type = typeof(TaskModel);
            var guid = new Guid(r["cID"].ToString());
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.CatalogID = new Guid(r["cCatalogID"].ToString());
            m.No = r["cNo"].ToString();
            m.UserID = Guid.Parse(r["cUserID"].ToString());
            m.Status = r["cStatus"].ToString();
            return m;
        }

        public TaskModel ReadByUserID(string uid)
        {
            var sql = string.Format(userSql,uid,Common.Seed);       
            var r = utility.Get(name, sql);
            try
            {
                var m = Read(r);

                return (TaskModel)m;
            }
            catch
            {
                return null;
            }
        }
    }
}
