using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class UserModel : IModel
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", ID, LoginName, Password, CreateDate, CreateUser);
        }
    }
}
