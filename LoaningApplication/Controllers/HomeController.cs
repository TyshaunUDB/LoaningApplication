using Google.Protobuf.Collections;
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
        public ActionResult Loans()
        {
            return View();
        }
        public int logIn(string loginEmail, string loginPass)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccount.Where(x => x.emailAddress == loginEmail && x.password == loginPass && x.statusID == 1).FirstOrDefault();
                if (exists == null)
                {
                    return 0;
                }
                else if (exists.roleID == 1)
                {
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

        public int emailExist(string email)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccount.Where(x => x.emailAddress == email).FirstOrDefault();
                if (exists == null)
                {
                    return 0;
                }
                else
                {
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
                if (checkActive == null)
                {
                    var checkPending = db.tbloan.Where(y => y.accountID == accID && y.statusID == 3).FirstOrDefault();
                    if (checkPending == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
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
                    var accID = db.tbaccount.FirstOrDefault(s => s.emailAddress == email).accountID;
                    var loggedinLoan = db.tbloan.Where(x => x.statusID == 3 || x.statusID == 1).Where(y => y.accountID == accID).Select(a => new
                    {
                        a.loanID,
                        Pending = a.paymentMonth * a.loanTerm - a.amountPaid,
                        a.loanAmount,
                        a.paymentMonth,
                        a.dueDate,
                        a.amountPaid
                    }).ToList();

                    return Json(new { success = true, data = loggedinLoan }, JsonRequestBehavior.AllowGet);
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
                var PaymentMonth = (LoanAmount * 0.03 * Math.Pow(1 + 0.03, LoanMonths)) /
                                (Math.Pow(1 + 0.03, LoanMonths) - 1);
                var addLoanApplication = new tblLoanModel
                {
                    accountID = accID,
                    statusID = 3,
                    loanAmount = LoanAmount,
                    loanTerm = LoanMonths,
                    paymentMonth = PaymentMonth,
                    startDate = DateTime.Now,
                    dueDate = DateTime.Now.AddMonths(1),
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
        public JsonResult getLoans()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var loanList = db.tbloan.Select(a => new
                    {
                        a.loanID,
                        a.statusID,
                        Applicant = db.tbaccount.FirstOrDefault(y => y.accountID == a.accountID).emailAddress,
                        StatusName = db.tbloanstatus.FirstOrDefault(x => x.loanstatusID == a.statusID).loanstatusName,
                        a.loanAmount,
                        a.loanTerm,
                        a.startDate,
                        a.dueDate,
                        a.amountPaid,
                        a.GovtIDPic,
                        a.CompIDPic,
                        a.payslipPic,
                        a.tinSSS,
                        a.updateAt,
                        a.createAt,
                    }).ToList();

                    return Json(new { success = true, data = loanList }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public int paymentCheck(String email, int PaymentLoanID)
        {
            using (var db = new LoaningContext())
            {
                var accID = db.tbaccount.FirstOrDefault(s => s.emailAddress == email).accountID;
                var LoanDate = db.tbloan.FirstOrDefault(s => s.loanID == PaymentLoanID).dueDate;
                var PreviousLoanDate = LoanDate.AddMonths(-1);
                var checkPending = db.tbloan.Where(x => x.accountID == accID && x.statusID == 3).FirstOrDefault();
                var checkPayment = db.tbpayment.Where(y => y.loanID == PaymentLoanID && y.paymentDate >= PreviousLoanDate).FirstOrDefault();
                if (checkPayment != null)
                {
                    return 1;
                }
                else if (checkPending != null)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }
        public void payment(String email, int LoanID, HttpPostedFileBase PaymentProof)
        {
            string uploadsFolder = Server.MapPath("~/Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (PaymentProof != null)
            {
                string PaymentFilePath = Path.Combine(uploadsFolder, Path.GetFileName(PaymentProof.FileName));
                PaymentProof.SaveAs(PaymentFilePath);
            }

            using (var db = new LoaningContext())
            {
                var Amount = db.tbloan.FirstOrDefault(s => s.loanID == LoanID).paymentMonth;

                var addPayment = new tblPaymentModel
                {
                    loanID = LoanID,
                    paymentAmount = Amount,
                    paymentDate = DateTime.Now,
                    paymentProof = PaymentProof != null ? "/Images/" + Path.GetFileName(PaymentProof.FileName) : null
                };
                db.tbpayment.Add(addPayment);
                db.SaveChanges();

                var exists = db.tbloan.Where(x => x.loanID == LoanID).FirstOrDefault();
                if (exists != null)
                {
                    exists.amountPaid = exists.amountPaid + Amount;
                    db.SaveChanges();
                }
            }
        }

        public void updateLoan(int EditLoanID, int EditStatusID)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbloan.Where(x => x.loanID == EditLoanID).FirstOrDefault();
                if (exists != null)
                {
                    exists.statusID = 1;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public JsonResult getStatuses()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var statusList = db.tbloanstatus.Select(a => new
                    {
                        a.loanstatusID,
                        a.loanstatusName
                    }).ToList();

                    return Json(new { success = true, data = statusList }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AccountStatus()
        {
            return View();
        }
        public ActionResult Roles()
        {
            return View();
        }
        public ActionResult Disbursement()
        {
            return View();
        }
        public ActionResult LoanStatus()
        {
            return View();
        }
        public ActionResult Logs()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAccstatus()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var accountstatus = db.tbaccountstatus.Select(a => new
                    {
                        statusID = a.statusID,
                        a.statusName,
                        a.updateAt,
                        a.createAt,
                    }).ToList();

                    return Json(new { success = true, data = accountstatus }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void updateAccStat(int editstatusID, String editstatusName)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccountstatus.Where(x => x.statusID == editstatusID).FirstOrDefault();
                if (exists != null)
                {
                    exists.statusName = editstatusName;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
        public void deleteAccStat(int deleteAccStatusID)
        {
            using (var db = new LoaningContext())
            {
                var exists = db.tbaccountstatus.Where(x => x.statusID == deleteAccStatusID).FirstOrDefault();
                if (exists != null)
                {
                    exists.statusID = 2;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        [HttpGet]
        public JsonResult GetRoles()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var roles = db.tbroles.Select(a => new
                    {
                        roleID = a.roleID,
                        a.roleName,
                        a.updateAt,
                        a.createAt,
                    }).ToList();

                    return Json(new { success = true, data = roles }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetDisbursement()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var disbursement = db.tbloandisbursement.Select(a => new
                    {
                        disbursementID = a.disbursementID,
                        a.loanID,
                        a.disbursementDate,
                        a.disbursedAmount,
                        a.disbursementMethod,
                        a.updateAt,
                        a.createAt,
                    }).ToList();

                    return Json(new { success = true, data = disbursement }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetLoanStatus()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var loanstatus = db.tbloanstatus.Select(a => new
                    {
                        loanstatusID = a.loanstatusID,
                        a.loanstatusName,
                        a.updateAt,
                        a.createAt,
                    }).ToList();

                    return Json(new { success = true, data = loanstatus }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetLogs()
        {
            try
            {
                using (var db = new LoaningContext())
                {
                    var logs = db.tblogs.Select(a => new
                    {
                        actionID = a.actionID,
                        accountID = db.tbaccount.FirstOrDefault(y => y.accountID == a.accountID).emailAddress,
                        a.actionDesc,
                        a.actionDate
                    }).ToList();

                    return Json(new { success = true, data = logs }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}