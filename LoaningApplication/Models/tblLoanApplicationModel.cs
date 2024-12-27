using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanApplicationModel
    {
        public int applicationID { get; set; }
        public int accountID { get; set; }
        public decimal loanAmount { get; set; }
        public DateTime dateApplied { get; set; }
        public string statusName { get; set; }
        public string GovtIDPic { get; set; }
        public string payslipPic { get; set; }
        public string CompIDPic { get; set; }
        public string TIN_SSSPic { get; set; } 
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}