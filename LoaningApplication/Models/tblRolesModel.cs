using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoaningApplication.Models
{
    public class tblRolesModel
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}