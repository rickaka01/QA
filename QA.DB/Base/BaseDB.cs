using QA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.DB;

namespace QA.DB
{
    public class BaseDB
    {
        protected string name;
        protected string cSql;
        protected string rSql;
        protected string uSql;
        protected string dSql;

        protected MySQLDBUtility utility = new MySQLDBUtility();

        public string ErrorMessage { get; set; }

        public virtual bool Create(IModel m)
        {
            return false;
        }

        public virtual IModel Read(string param)
        {
            throw new Exception();
        }

        public virtual List<IModel> List()
        {
            return new List<IModel>();
        }

        public virtual List<IModel> List(string param)
        {
            return new List<IModel>();
        }

        public virtual bool Update()
        {
            return false;
        }

        public virtual bool Delete()
        {
            return false;
        }
    }
}
