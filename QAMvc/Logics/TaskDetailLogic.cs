using QA.DB;
using QA.Model;
using QAMvc.Models;
using System;
using System.Linq;

namespace QAMvc.Logics
{
    public class TaskDetailLogic
    {
        TaskArticleDB tadb = new TaskArticleDB();
        ArticleDetailDB addb = new ArticleDetailDB();
        ArticleAnswerDB andb = new ArticleAnswerDB();
        TaskDetailDB db = new TaskDetailDB();
        TaskScoreDB sdb = new TaskScoreDB();
        UserPointLogic uplogic = new UserPointLogic();
        public bool Create(TaskViewModel vm,Guid userId)
        {
            //往taskdetail插入数据
            //把TaskViewModel转换成TaskDetialModel
            var m = new TaskDetailModel { TaskID = Guid.Parse(vm.p), DetailNo = vm.p2, };
            var m1 = new TaskScoreModel { TaskID = m.TaskID, Score = 0 };
            var tid = vm.p;
            var seq = Convert.ToInt32(vm.p1);

            //查询其他数据:
            //根据tid和seq获取tArticleID
            var task = (TaskArticleModel)tadb.Read(tid, seq);

            //根据tArticleID获取tArticleNo
            var taid = task.ArticleID;
            var tano = task.ArticleNo;
            m.ArticleID = taid;
            m.ArticleNo = tano;

            //根据tArticleID 和 DetailNo 获取 tDetailID
            var articleDetail = (ArticleDetailModel)addb.Read(taid.ToString(), m.DetailNo);
            m.DetailID = articleDetail.ID;

            //根据tArticleID获取AnswerNo
            var answer = (ArticleAnswerModel)andb.Read(taid.ToString());
            m.AnswerNo = answer.cNo;
            db.Create(m, vm.p1);
            if (vm.p1 == "5")
            {
                //这里要计算一下m1的Score
                var list = db.List(tid).Cast<TaskDetailModel>();
                var score = list.Where(x => x.DetailNo == x.AnswerNo).Count();
                m1.UserID = userId;
                m1.Score = score;
                sdb.Create(m1);

                int p = score;
                if (score == 5) p++;

                uplogic.CreatePoint(userId, p);
            }

            //如果是最后一个,还要往taskscore插入数据
            return true;
        }
    }
}