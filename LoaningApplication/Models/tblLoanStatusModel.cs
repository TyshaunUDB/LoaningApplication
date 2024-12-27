using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanStatusModel
    {
        public int loanstatusID { get; set; }
        public string loanstatusName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
        public int Archived { get; set; }
    }
}