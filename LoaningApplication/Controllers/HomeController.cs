using LoaningApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoaningApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LandingPage()
        {
            return View();
        }
        public ActionResult UserLandingPage()
        {
            return View();
        }
        public ActionResult LoanApplicationPage()
        {
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult RegistrationPage()
        {
            return View();
        }
        public ActionResult Accounts()
        {
            return View();
        }
        public int logIn(string loginEmail, string loginPass) { 
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccount.Where(x => x.emailAddress == loginEmail && x.password == loginPass && x.statusID == 1).FirstOrDefault();
                if (exists == null)
                {
                    return 0;
                }
                else if (exists.roleID == 1) {
                    return 1;
                }
                else if (exists.roleID == 2)
                {
                    return 2;
                }
                else if (exists.roleID == 3)
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int emailExist(string email) {
            using (var db = new LoaningContext()) { 
                var exists = db.tbaccount.Where(x => x.emailAddress == email).FirstOrDefault();
                if (exists == null)
                {
                    return 0;
                }
                else {
                    return 1;
                }
            }
        }

        public void regUser(String fName, String mName, String lName, String email, String phone, DateTime birthDate, String address, String pass)
        {
            using (var db = new LoaningContext())
            {
                var addUser = new tblAccountModel
                {
                    roleID = 1,
                    statusID = 1,
                    firstName = fName,
                    middleName = mName,
                    lastName = lName,
                    emailAddress = email,
                    phoneNumber = phone,
                    birthDate = birthDate,
                    Address = address,
                    password = pass,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                db.tbaccount.Add(addUser);
                db.SaveChanges();
            }
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var accounts = db.tbaccount.Select(a => new
                    {
                        accountID = a.accountID,
                        a.firstName,
                        a.middleName,
                        a.lastName,
                        a.emailAddress,
                        a.phoneNumber,
                        a.birthDate,
                        a.Address,
                        RoleName = db.tbroles.FirstOrDefault(r => r.roleID == a.roleID).roleName,
                        StatusName = db.tbaccountstatus.FirstOrDefault(s => s.statusID == a.statusID).statusName,
                        a.updateAt,
                        a.createAt,
                    }).ToList();

                    return Json(new { success = true, data = accounts }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void updateAcc(int editID, String editFName, String editMName, String editLName, String editPhone, DateTime editBDay, String editAdress)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccount.Where(x => x.accountID == editID).FirstOrDefault();
                if (exists != null)
                {
                    exists.firstName = editFName;
                    exists.middleName = editMName;
                    exists.lastName = editLName;
                    exists.phoneNumber = editPhone;
                    exists.birthDate = editBDay;
                    exists.Address = editAdress;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
    }
}