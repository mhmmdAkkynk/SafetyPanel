using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SafetyPanel.Models;
using SafetyPanel.Models.Admin;

namespace SafetyPanel.Controllers.Login
{
    public class SignInController : Controller
    {
        // GET: SignIn
        Context context = new Context();
        errorCode errorCode = new errorCode();
        List<KeyValuePair<string, string>> output_array = new List<KeyValuePair<string, string>>();

        [HttpGet]
        public ActionResult Sign_In()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Sign_In(Adminler adminler)
        {   
            var crypto = new SimpleCrypto.PBKDF2();
            var chechSignIn = context.Admins.Where(m => m.Email == adminler.Email).ToList();

            if (adminler.Email != null && adminler.Password!= null)
            {
                if (chechSignIn.Count != 0)
                {
                    var password = crypto.Compute(adminler.Password, chechSignIn[0].Salt);    //posttan gelen password değeri ile database de kayıtlı olan salt değeri karşılaştırılır.

                    if(chechSignIn[0].Password == password)
                    {
                        if (chechSignIn[0].AccountVerify != 0)
                        {
                            FormsAuthentication.SetAuthCookie(chechSignIn[0].Email, false);
                            Session["Id"] = chechSignIn[0].Id;
                            Session["Email"] = chechSignIn[0].Email;
                            Session["AdminName"] = chechSignIn[0].FullName;
                            Session["Photo"] = chechSignIn[0].Photo;

                            output_array = errorCode.errorCodesValue(0, 0, 4);
                            output_array.Add(new KeyValuePair<string, string>("account", "True"));
                        }
                        else
                        {
                            output_array = errorCode.errorCodesValue(0, 0, 13);
                            output_array.Add(new KeyValuePair<string, string>("account", "False"));
                        }
                    }
                    else
                    {
                        output_array = errorCode.errorCodesValue(1, 1, 5);
                        output_array.Add(new KeyValuePair<string, string>("account", ""));
                    }
                }
                else
                {
                    output_array = errorCode.errorCodesValue(1, 1, 5);
                    output_array.Add(new KeyValuePair<string, string>("account", ""));
                }
            }
            else
            {
                output_array = errorCode.errorCodesValue(1, 1, 3);
                output_array.Add(new KeyValuePair<string, string>("account", ""));
            }

            return Json(output_array,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Sign_In","SignIn");
        }
    }
}