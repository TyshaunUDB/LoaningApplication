using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLogsModel
    {
        public int actionID { get; set; }
        public int? accountID { get; set; } 
        public string actionDesc { get; set; }
        public DateTime actionDate { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}