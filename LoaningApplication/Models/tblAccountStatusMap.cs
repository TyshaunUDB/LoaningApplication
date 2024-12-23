using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblAccountStatusMap : EntityTypeConfiguration<tblAccountStatusModel>
    {
        public tblAccountStatusMap()
        {
            HasKey(i => i.statusID);
            ToTable("tbaccountstatus");
        }
    }
}