using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SafetyPanel.Models;
using SafetyPanel.Models.Admin;

namespace SafetyPanel.Controllers.Login
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        Context context = new Context();
        List<KeyValuePair<string, string>> output_array = new List<KeyValuePair<string, string>>();
        errorCode error = new errorCode();

        [HttpGet]
        public ActionResult Sign_Up()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sign_Up(Adminler adminler, string Password_Repeat)
        {
            var checkSignUp = context.Admins.Where(m => m.Email == adminler.Email).ToList();
            var crypto = new SimpleCrypto.PBKDF2();

            adminler.AccountVerify = 0;

            if (adminler.FullName != null && adminler.Email!=null && adminler.Password!=null && Password_Repeat!= "")
            {
                if (checkSignUp.Count == 0)
                {
                    if (adminler.Password == Password_Repeat)
                    {
                        var encrypedPassword = crypto.Compute(adminler.Password);
                        adminler.Password = encrypedPassword;
                        adminler.Salt = crypto.Salt;
                        context.Admins.Add(adminler);
                        context.SaveChanges();
                        ActivationCodeSend(adminler.Email);
                        output_array = error.errorCodesValue(0, 0, 0);
                    }
                    else
                    {
                        output_array = error.errorCodesValue(1, 1, 1);
                    }
                }
                else
                {
                    output_array = error.errorCodesValue(1, 1, 2);
                }
            }
            else
            {
                output_array = error.errorCodesValue(1, 1, 3);
            }          

            return Json(output_array, JsonRequestBehavior.AllowGet);
        }

        public void ActivationCodeSend(string email)
        {
            var chechEmail = context.Admins.Where(m => m.Email == email).ToList();
            string verifyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/SignUp/AccountVerify?Id=" + chechEmail[0].Id;
            var body = "<p>Değerli {0}; </p><p>Üyelik işleminiz başarı ile gerçekleşmiştir. Lütfen aşağıdaki bağlantıya tıklayarak üyeliğiniz aktifleştirin.</p><h4>Bağlantı adresi;</h4></p><a href='"+verifyUrl+ "'>" + verifyUrl + "</a>";

            var message = new MailMessage();
            message.To.Add(new MailAddress(chechEmail[0].Email));  // replace with valid value 
            message.From = new MailAddress("mhmmd_akkynk@hotmail.com");  // replace with valid value
            message.Subject = "Üyeliğinizi Aktifleştirin";
            message.Body = string.Format(body, chechEmail[0].FullName);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "mhmmd_akkynk@hotmail.com",  // replace with valid value
                    Password = "24051997Ma*"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        public ActionResult AccountVerify(int Id)
        {
            var chechEmail = context.Admins.Where(m => m.Id == Id).ToList();
            if(chechEmail.Count != 0)
            {
                chechEmail[0].AccountVerify = 1;   //Hesabı aktif et
                context.SaveChanges();
            }
            else
            {
                output_array = error.errorCodesValue(1, 1, 12);
                return Json(output_array, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Sign_In", "SignIn");
        }
    }
}