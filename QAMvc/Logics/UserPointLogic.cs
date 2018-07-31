using QA.DB;
using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAMvc.Logics
{
    public class UserPointLogic
    {
        UserPointDB udb = new UserPointDB();

        public bool CreatePoint(Guid userId, int p)
        {
            var model = new UserPointModel { UserID = userId, Point = p };

            return udb.Create(model);
        }

        public int GetPoint(string uid, string tid)
        {
            //查询本次积分和总积分
            var total = udb.ReadAll(uid);

            return total;
        }
    }
}