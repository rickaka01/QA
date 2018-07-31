using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QA.DB
{
    public class CatalogDB : BaseDB
    {
        public string aSql = "select * from tcatalog";

        public CatalogDB()
        {
            this.name = "tcatalog";
            this.cSql = "INSERT INTO tcatalog (cId,cNo,cName) VALUES('{0}','{1}','{2}')";
            this.rSql = "select * from tcatalog where cno='{0}'";
            this.uSql = "";
            this.dSql = "";
        }

        public override bool Create(IModel m)
        {
            var catalog = (CatalogModel)m;

            var sql = string.Format(cSql, Guid.NewGuid(), catalog.Name, catalog.Name);

            var result = utility.Exec(sql);

            return result.Item1;
        }

        private IModel Read(Dictionary<string, object> r)
        {
            var m = new CatalogModel();
            var guid = new Guid(r["cID"].ToString());
            var type = typeof(UserModel);
            PropertyInfo pi = type.BaseType.GetProperty("ID");
            pi.SetValue(m, guid);

            m.No = r["cNo"].ToString();
            m.Name = r["cName"].ToString();

            /*
            var date = Convert.ToDateTime(r["cCreateDate"]);
            pi = type.BaseType.GetProperty("CreateDate");
            pi.SetValue(m, date);

            m.CreateUser = r["cCreateUser"].ToString();
            */
            return m;
        }

        public override IModel Read(string catalog)
        {
            var sql = string.Format(rSql, catalog);
            var result = utility.List(name, sql);
            var r = result.FirstOrDefault();
            return Read(r);
        }

        public override List<IModel> List()
        {
            var l = utility.List(name, aSql);

            var list = new List<IModel>();
            if (l != null)
            {
                foreach (var r in l)
                {
                    list.Add(this.Read(r));
                }
            }
            return list;
        }
    }
}
