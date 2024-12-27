using LoaningApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

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
                    var accounts = db.tbaccount.Where(x => x.statusID != 2).Select(a => new
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

        public void deleteAcc(int deleteAccID)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccount.Where(x => x.accountID == deleteAccID).FirstOrDefault();
                if (exists != null)
                {
                    exists.statusID = 2;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public int loanExist(String email)
        {
            using (var db = new LoaningContext())
            {
                var accID = db.tbaccount.FirstOrDefault(s => s.emailAddress == email).accountID;
                var checkActive = db.tbloan.Where(x => x.accountID == accID && x.statusID == 1).FirstOrDefault();
                var checkPending = db.tbloan.Where(y => y.accountID == accID && y.statusID == 3).FirstOrDefault();
                if (checkActive == null || checkPending == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        [HttpGet]
        public JsonResult loggedinData(String email)
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var loggedInAcc = db.tbaccount.Where(x => x.emailAddress == email).Select(a => new
                    {
                        a.firstName,
                        a.middleName,
                        a.lastName,
                        a.emailAddress,
                        a.phoneNumber,
                        a.birthDate,
                        a.Address
                    }).ToList();

                    return Json(new { success = true, data = loggedInAcc }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult loanInfo(String email)
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var loggedInAcc = db.tbaccount.Where(x => x.emailAddress == email).Select(a => new
                    {
                        a.firstName,
                        a.middleName,
                        a.lastName,
                        a.emailAddress,
                        a.phoneNumber,
                        a.birthDate,
                        a.Address
                    }).ToList();

                    return Json(new { success = true, data = loggedInAcc }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void loanApply(string email, int LoanAmount, int LoanMonths, HttpPostedFileBase GovID, HttpPostedFileBase CompanyID, HttpPostedFileBase Payslip, HttpPostedFileBase SSSTin)
        {
            string uploadsFolder = Server.MapPath("~/Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (GovID != null)
            {
                string govIDFilePath = Path.Combine(uploadsFolder, Path.GetFileName(GovID.FileName));
                GovID.SaveAs(govIDFilePath);
            }

            if (CompanyID != null)
            {
                string compIDFilePath = Path.Combine(uploadsFolder, Path.GetFileName(CompanyID.FileName));
                CompanyID.SaveAs(compIDFilePath);
            }

            if (Payslip != null)
            {
                string paySlipFilePath = Path.Combine(uploadsFolder, Path.GetFileName(Payslip.FileName));
                Payslip.SaveAs(paySlipFilePath);
            }

            if (SSSTin != null)
            {
                string sssTinFilePath = Path.Combine(uploadsFolder, Path.GetFileName(SSSTin.FileName));
                SSSTin.SaveAs(sssTinFilePath);
            }

            using (var db = new LoaningContext())
            {
                var accID = db.tbaccount.FirstOrDefault(s => s.emailAddress == email).accountID;
                var addLoanApplication = new tblLoanModel
                {
                    accountID = accID,
                    statusID = 3,
                    loanAmount = LoanAmount,
                    loanTerm = LoanMonths,
                    startDate = DateTime.Now,
                    dueDate = DateTime.Now,
                    GovtIDPic = GovID != null ? "/Images/" + Path.GetFileName(GovID.FileName) : null,
                    CompIDPic = CompanyID != null ? "/Images/" + Path.GetFileName(CompanyID.FileName) : null,
                    payslipPic = Payslip != null ? "/Images/" + Path.GetFileName(Payslip.FileName) : null,
                    tinSSS = SSSTin != null ? "/Images/" + Path.GetFileName(SSSTin.FileName) : null,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                db.tbloan.Add(addLoanApplication);
                db.SaveChanges();
            }
        }
    }
}