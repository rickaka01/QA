using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DB
{
    /// <summary>
    /// 每天首次登录获得1个积分
    /// </summary>
    public class UserPointDB : BaseDB
    {
        public string existsSql = "select 1 as c from tUserPoint where fCreateDate >='{0}'";
        private string aSql = "select sum(cPoint) as cPoint from tUserPoint where cUserID='{0}'";

        public UserPointDB()
        {
            this.name = "tUserPoint";
            this.cSql = "insert into tUserPoint(cID,cUserID,cPoint,cCreateDate,cCreateUser) values('{0}','{1}',{2},'{3}','{4}')";
        }

        public override bool Create(IModel m1)
        {
            var m = (UserPointModel)m1;

            var sql = string.Format(cSql, Guid.NewGuid(),m.UserID,m.Point,DateTime.Now,Common.DefaultName);

            var result =  utility.Exec(sql);

            this.ErrorMessage = result.Item2;

            return result.Item1;
        }

        public bool Exists()
        {
            var sql = string.Format(existsSql, DateTime.Now.ToString("yyyy-MM-dd"));
            var o = utility.GetScalar(sql);
            if (o != null && o.Item2 == "1")
                return true;
            else
                return false;
        }


        public int ReadAll(string uid)
        {
            int score = 0;
            var sql = string.Format(aSql, uid);
            var r = utility.Get(name, sql);
            if (r != null)
            {
                score = Convert.ToInt32(r["cPoint"]);
            }
            return score;
        }
    }
}
