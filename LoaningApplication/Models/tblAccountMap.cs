using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblAccountMap : EntityTypeConfiguration<tblAccountModel>
    {
        public tblAccountMap() 
        {
            HasKey(i => i.accountID);
            ToTable("tbaccount");
        }
    }
}