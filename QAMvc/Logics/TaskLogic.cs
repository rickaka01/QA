using QA.DB;
using QA.Model;
using QAMvc.Models;
using System;
using System.Collections.Generic;

namespace QAMvc.Logics
{
    public class TaskLogic
    {
        ArticleDB db = new ArticleDB();
        TaskDB tdb = new TaskDB();
        TaskArticleDB tadb = new TaskArticleDB();
        TaskScoreDB sdb = new TaskScoreDB();

        public ArticleModel Get(string id)
        {
            var am = new ArticleModel();

            am = (ArticleModel)db.Read(id);

            return am;
        }

        public TaskModel GetByUserID(string userid)
        {
            return tdb.ReadByUserID(userid);
        }

        public string Create(string uid,string cid)
        {
            //根据cid查询一组tid,然后把tid 保存到 taskdetail
            //返回第一个tid
            return tdb.Create(uid,cid);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public TaskArticleViewModel Get(string cid, string tid, string uid, string seq)
        {
            int index = 1;

            // tid没有值的时候
            if (string.IsNullOrEmpty(tid) || tid == "undefined")
            {
                //查询当前用户有没有未完成的tid,如果有直接返回tid
                var t = GetByUserID(uid);
                if (t != null)
                {
                    tid = t.ID.ToString();
                    seq = t.Status;
                }
                else
                {
                    tid = string.Empty;
                }

                // 查询不出来,就创建
                if (string.IsNullOrEmpty(tid))
                {
                    //首先根据cid 自动生成tid
                    //调用Create方法
                    tid = Create(uid, cid);
                }
            }

            // 判断查询条件seq的值
            if (!string.IsNullOrEmpty(seq) && seq != "undefined")
            {
                index = Convert.ToInt32(seq);
                if (index <= 1)
                    index = 1;
            }

            return Get(tid, index);
        }

        /// <summary>
        /// 按指定tid获取数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public TaskArticleViewModel Get(string tid, int seq)
        {
            var am = new TaskArticleViewModel();

            //根据tid和seq获取aid
            var a = (TaskArticleModel)tadb.Read(tid, seq);
            var aid = a.ArticleID.ToString();

            //如果没有查询到结果
            if (aid == Guid.Empty.ToString())
            {
                // 返回异常信息
                am.p = a.ArticleNo;
            }
            else
            {
                //这里是获取aid
                var r = Get(aid);
                am.tid = tid;
                am.id = r.ID.ToString();
                am.p = r.Title;
                am.p1 = r.Details[0].Title;
                am.p2 = r.Details[1].Title;
                am.p3 = r.Details[2].Title;
                am.p4 = r.Details[3].Title;
                am.i = seq.ToString();
            }
            return am;
        }

        public KeyValuePair<int, int> GetScore(string uid,string tid)
        {
            //查询本次积分和总积分
            var total = sdb.ReadAll(uid);
            var  current = sdb.Read(tid);

            return new KeyValuePair<int,int>(total,current);
        }

    }
}