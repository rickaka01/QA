using QAMvc.Logics;
using QAMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QAMvc.Controllers
{
    public class TaskController : ApiController
    {
        ArticleLogic alogic = new ArticleLogic();
        TaskLogic tlogic = new TaskLogic();
        TaskDetailLogic dloginc = new TaskDetailLogic();

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpPost]
        public ArticleViewModel Get(string cid, string tid, string seq)
        {
            // 获取用户信息
            IEnumerable<string> oo;
            Request.Headers.TryGetValues("userid", out oo);
            string uid = string.Empty;
            if (oo != null)
            {
                var d = oo.ToList();
                uid = d.FirstOrDefault();
            }
            else
            {
                return null;
            }

            // 返回数据
            return tlogic.Get(cid, tid, uid, seq);
        }

        /// <summary>
        /// 根据来源的数据提交信息并返回结果数据
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Do(TaskViewModel m)
        {
            IEnumerable<string> oo;
            Request.Headers.TryGetValues("userid", out oo);
            string uid = string.Empty;
            if (oo != null)
            {
                var d = oo.ToList();
                uid = d.FirstOrDefault();
            }
            else
            {
                return false;
            }

            //记录数据
            return dloginc.Create(m,Guid.Parse(uid));
        }

        [HttpPost]
        public KeyValuePair<int, int> Score(string tid)
        {
            IEnumerable<string> oo;
            Request.Headers.TryGetValues("userid", out oo);
            string uid = string.Empty;
            if (oo != null)
            {
                var d = oo.ToList();
                uid = d.FirstOrDefault();
                return tlogic.GetScore(uid, tid);
            }
            else
            {
                return new KeyValuePair<int,int>(-1,-1);
            }
        }
    }
}
