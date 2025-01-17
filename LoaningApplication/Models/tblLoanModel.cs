﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanModel
    {
        public int loanID { get; set; }
        public int accountID { get; set; }
        public int statusID { get; set; }
        public double loanAmount { get; set; }
        public int loanTerm { get; set; }
        public double paymentMonth { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public double amountPaid { get; set; }
        public string GovtIDPic { get; set; }
        public string payslipPic { get; set; }
        public string CompIDPic { get; set; }
        public string tinSSS { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}