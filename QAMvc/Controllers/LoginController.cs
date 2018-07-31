using QAMvc.Logics;
using QAMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QAMvc.Controllers
{
    public class LoginController : ApiController
    {
        LoginLogic logic = new LoginLogic();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="p1">帐号</param>
        /// <param name="p2">密码</param>
        /// <returns></returns>
        public KeyValuePair<string,string> Login(string p1, string p2)
        {
            return logic.Check(p1, p2);
        }

        public string Reg(UserViewModel v)
        {
            var r = logic.Reg(v.u,v.p);

            return r;
        }
    }
}
