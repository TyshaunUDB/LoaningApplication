using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanMap : EntityTypeConfiguration<tblLoanModel>
    {
        public tblLoanMap()
        {
            HasKey(i => i.loanID);
            ToTable("tbloan");
        }
    }
}