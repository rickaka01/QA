﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Model
{
    public class UserPointModel : IModel
    {
        public Guid UserID { get; set; }

        public decimal Point { get; set; }
    }
}
