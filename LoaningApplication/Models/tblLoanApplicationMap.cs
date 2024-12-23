using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLoanApplicationMap : EntityTypeConfiguration<tblLoanApplicationModel>
    {
        public tblLoanApplicationMap()
        {
            HasKey(i => i.applicationID);
            ToTable("tbloanapplications");
        }
    }
}