using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblPaymentModel
    {
        public int paymentID { get; set; }
        public int loanID { get; set; }
        public double paymentAmount { get; set; }
        public DateTime paymentDate { get; set; }
        public string paymentProof { get; set; } // Nullable string for payment proof
    }
}