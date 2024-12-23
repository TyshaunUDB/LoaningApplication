using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblAccountStatusModel
    {
        public int statusID { get; set; }
        public string statusName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}