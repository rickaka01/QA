using QA;
using QA.Model;
using Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QA.DB;
using Utility.Crypto;

namespace QAMvc.Logics
{
    public class LoginLogic
    {
        UserDB db = new UserDB();
        UserPointLogic ulogic = new UserPointLogic();
        UserPointDB udb = new UserPointDB();

        /// <summary>
        /// 校验帐号和密码
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>UserID/SessionID</returns>
        public KeyValuePair<string,string> Check(string p1,string p2)
        {
            var r = (UserModel)db.Read(p1);
            if (r.LoginName == p1 && r.Password == MD5Crypto.Encode(p2))
            {
                if (!udb.Exists()) ulogic.CreatePoint(r.ID, 1);
                return new KeyValuePair<string, string>(r.ID.ToString(), Guid.NewGuid().ToString());
            }
            else
                return new KeyValuePair<string, string>();
        }

        public string Reg(string u, string p)
        {
            var m = new UserModel { LoginName = u.Trim(), Password = p };
            var r = (UserModel)db.Read(u);
            
            // 校验不能重复
            if (r.LoginName!=u.Trim())
            {
                if (db.Create(m))
                {
                    return string.Empty;
                }
                else
                {
                    return db.ErrorMessage;
                }
            }
            else
            {
                //如果重复
                return string.Format("{0}已存在!",u);
            }
        }
    }
}