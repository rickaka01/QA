using QA.Model;
using System;
using System.Reflection;
using Utility.Crypto;
using System.Linq;

namespace QA.DB
{
    public class UserDB : BaseDB
    {
        public UserDB()
        {
            this.cSql = "INSERT INTO tusers (cId,cLoginName,cPassword,cCreateDate,cCreateUser) VALUES('{0}','{1}','{2}','{3}','{4}')";

            this.rSql = "select * from tusers where cloginname='{0}'";

            this.uSql = "";

            this.dSql = "";
        }

        public override bool Create(IModel m)
        {
            var user = (UserModel)m;

            var sql = string.Format(cSql, Guid.NewGuid(), user.LoginName, MD5Crypto.Encode(user.Password), DateTime.Now, "sys");

            var result = utility.Exec(sql);

            return result.Item1;
        }

        public override IModel Read(string loginname)
        {
            UserModel m = new UserModel();
            var sql = string.Format(rSql, loginname);

            var result = utility.List("tusers", sql);

            var r = result.FirstOrDefault();

            if (r != null)
            {
                m.LoginName = r["cLoginName"].ToString();

                var type = typeof(UserModel);

                var guid = new Guid(r["cId"].ToString());
                PropertyInfo pi = type.BaseType.GetProperty("ID");
                pi.SetValue(m, guid);

                m.Password = r["cPassword"].ToString();

                var date = Convert.ToDateTime(r["cCreateDate"]);
                pi = type.BaseType.GetProperty("CreateDate");
                pi.SetValue(m, date);

                m.CreateUser = r["cCreateUser"].ToString();
            }
            return m;
        }
    }
}
