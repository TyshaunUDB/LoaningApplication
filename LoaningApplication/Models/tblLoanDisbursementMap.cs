using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanDisbursementMap : EntityTypeConfiguration<tblLoanDisbursementModel>
    {
        public tblLoanDisbursementMap()
        {
            HasKey(i => i.disbursementID);
            ToTable("tbloandisbursement");
        }
    }
}