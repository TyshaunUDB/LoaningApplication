using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblPaymentMap : EntityTypeConfiguration<tblPaymentModel>
    {
        public tblPaymentMap()
        {
            HasKey(i => i.paymentID);
            ToTable("tbpayment");
        }
    }
}