using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblAccountModel
    {
        public int accountID { get; set; }
        public int roleID { get; set; }
        public int statusID { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public DateTime birthDate { get; set; }
        public string Address { get; set; }
        public string password { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}