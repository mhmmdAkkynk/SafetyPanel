using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyPanel.Models;
using SafetyPanel.Models.Admin;

namespace SafetyPanel.Controllers.Login
{
    public class HomeController : Controller
    {
        // GET: Home
        Context context = new Context();

        [Authorize]
        public ActionResult Home_Page()
        {
            var email = (string)Session["Email"];
            var accountInfo = context.Admins.Where(m => m.Email == email).ToList();
            return View(accountInfo);
        }
    }
}