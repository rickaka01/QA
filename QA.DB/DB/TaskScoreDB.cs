using QA.Model;
using System;

namespace QA.DB
{
    public class TaskScoreDB : BaseDB
    {
        private string aSql = "select sum(cScore) as cScore from tTaskScore where cUserID='{0}'";
        private string bSql = "select cScore from tTaskScore where cTaskID='{0}'";

        public TaskScoreDB()
        {
            this.name = "tTaskScore";
            this.cSql = "insert into tTaskScore(cID,cUserID,cTaskID,cScore,cType) values('{0}','{1}','{2}',{3},{4})";
            this.rSql = "select * from tTaskScore where cID = '{0}'";
        }

        public string GetCreateSQL()
        {
            return cSql;
        }

        public override bool Create(IModel m1)
        {
            var m = (TaskScoreModel)m1;
            var sql = string.Format(cSql, Guid.NewGuid(), m.UserID, m.TaskID, m.Score, m.ScoreType);
            var result = utility.Exec(sql);
            return result.Item1;
        }

        public int Read(string tid)
        {
            int score = 0;
            var sql = string.Format(bSql, tid);
            var r = utility.Get(name, sql);
            if (r != null)
            {
                score = Convert.ToInt32(r["cScore"]);
            }
            return score;
        }

        public int ReadAll(string uid)
        {
            int score = 0;
            var sql = string.Format(aSql, uid);
            var r = utility.Get(name, sql);
            if (r != null)
            {
                score = Convert.ToInt32(r["cScore"]);
            }
            return score;
        }

    }
}
