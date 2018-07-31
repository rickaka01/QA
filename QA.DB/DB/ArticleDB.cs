using Utility.DB;
using QA.Model;
using Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QA.DB
{
    public class ArticleDB : BaseDB
    {
        ArticleDetailDB detaildb = new ArticleDetailDB();
        ArticleAnswerDB answerdb = new ArticleAnswerDB();

        private string randSql = "select * from tarticle where cCatalogID='{0}' order by rand() limit {1}";

        public ArticleDB()
        {
            this.name = "tarticle";
            this.cSql = "insert into tarticle(cID,cCatalogID,cNo,cTitle) values ('{0}','{1}','{2}','{3}')";
            this.rSql = "select * from tarticle where cID='{0}'";
        }

        public override bool Create(IModel m)
        {
            List<string> sqls = new List<string>();

            var guid =Guid.NewGuid();

            var article = (ArticleModel)m;

            var no = new NumberGenerator().Next();

            // 生成主表
            var sql1 = string.Format(cSql,guid , article.CatalogID, no ,article.Title);

            sqls.Add(sql1);

            // 生成明细表
            var dSql = new ArticleDetailDB().GetCreateSQL();
            foreach (var item in article.Details)
            {
                var sql2 = string.Format(dSql,Guid.NewGuid(),guid,item.AnswerNo,item.Title);
                sqls.Add(sql2);
            }
            
            // 生成子表
            var aSql = new ArticleAnswerDB().GetCreateSQL();
            var sql3 = string.Format(aSql, Guid.NewGuid(), guid, article.Answer.cNo);
            sqls.Add(sql3);

            // 保存数据
            var result = utility.Exec(sqls.ToArray());

            return result.Item1;
        }

        public IModel Read(Dictionary<string,object> r)
        {
            var m = new ArticleSingleModel();
            var type = typeof(ArticleSingleModel);
            var guid = new Guid(r["cID"].ToString());
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.CatalogID = new Guid(r["cCatalogID"].ToString());
            m.No = r["cNo"].ToString();
            m.Title = r["cTitle"].ToString();
            return m;
        }

        public override IModel Read(string param)
        {
            var m = new ArticleModel();
            var sql = string.Format(rSql, param);

            var result = utility.List(name, sql);

            var r = result.FirstOrDefault();

            //开始赋值
            var type = typeof(ArticleModel);
            var guid = new Guid(r["cID"].ToString());
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.CatalogID = new Guid(r["cCatalogID"].ToString());
            m.No = r["cNo"].ToString();
            m.Title = r["cTitle"].ToString();

            m.Details = detaildb.List(param).Cast<ArticleDetailModel>().OrderBy(x=>x.AnswerNo).ToArray();
            m.Answer = (ArticleAnswerModel)answerdb.Read(param);

            return m;
        }

        public List<IModel> Rand(string cid)
        {
            var sql = string.Format(randSql, cid, Common.Seed);
            var list = new List<IModel>();
            var l = utility.List(name, sql);

            //开始赋值
            foreach (var r in l)
            {
               list.Add(this.Read(r));
            }

            return list;
        }
    }
}
