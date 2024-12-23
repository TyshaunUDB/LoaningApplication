using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblRolesMap : EntityTypeConfiguration<tblRolesModel>
    {
        public tblRolesMap()
        {
            HasKey(i => i.roleID);
            ToTable("tbroles");
        }
    }
}