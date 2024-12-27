using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LoaningContext : DbContext
    {
        static LoaningContext()
        {
            Database.SetInitializer<LoaningContext>(null);
        }

        public LoaningContext() : base("Name=loandb") {}

        public virtual DbSet<tblAccountModel> tbaccount { get; set; }
        public virtual DbSet<tblAccountStatusModel> tbaccountstatus { get; set; }
        public virtual DbSet<tblLoanDisbursementModel> tbloandisbursement { get; set; }
        public virtual DbSet<tblLoanModel> tbloan { get; set; }
        public virtual DbSet<tblLoanStatusModel> tbloanstatus { get; set; }
        public virtual DbSet<tblLogsModel> tblogs { get; set; }
        public virtual DbSet<tblPaymentModel> tbpayment { get; set; }
        public virtual DbSet<tblRolesModel> tbroles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tblAccountMap());
            modelBuilder.Configurations.Add(new tblAccountStatusMap());
            modelBuilder.Configurations.Add(new tblLoanDisbursementMap());
            modelBuilder.Configurations.Add(new tblLoanMap());
            modelBuilder.Configurations.Add(new tblLoanStatusMap());
            modelBuilder.Configurations.Add(new tblLogsMap());
            modelBuilder.Configurations.Add(new tblPaymentMap());
            modelBuilder.Configurations.Add(new tblRolesMap());
        }
    }
}