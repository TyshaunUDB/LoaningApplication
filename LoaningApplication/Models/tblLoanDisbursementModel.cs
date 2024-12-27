using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanDisbursementModel
    {
        public int disbursementID { get; set; }
        public int loanID { get; set; }
        public DateTime disbursementDate { get; set; }
        public double disbursedAmount { get; set; }
        public string disbursementMethod { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}