using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblLogsMap : EntityTypeConfiguration<tblLogsModel>
    {
        public tblLogsMap()
        {
            HasKey(i => i.actionID);
            ToTable("tblogs");
        }
    }
}