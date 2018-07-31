using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class TaskScoreModel :IModel
    {
        public Guid UserID { get; set; }

        public Guid TaskID { get; set; }

        public int Score { get; set; }

        public int ScoreType { get; set; }
    }
}
