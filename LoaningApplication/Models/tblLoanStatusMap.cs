using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanStatusMap : EntityTypeConfiguration<tblLoanStatusModel>
    {
        public tblLoanStatusMap()
        {
            HasKey(i => i.loanstatusID);
            ToTable("tbloanstatus");
        }
    }
}