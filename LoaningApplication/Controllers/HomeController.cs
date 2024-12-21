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
        public ActionResult OTP()
        {
            return View();
        }
    }
}