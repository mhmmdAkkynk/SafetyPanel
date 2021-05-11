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
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        Context context = new Context();
        List<KeyValuePair<string, string>> output_array = new List<KeyValuePair<string, string>>();
        errorCode errorCode = new errorCode();

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ResetPassword(Adminler adminler)
        {
            var chechEmail = context.Admins.Where(m => m.Email == adminler.Email).ToList();            

            if(adminler.Email != null)
            {
                if (chechEmail.Count != 0)
                {
                    Guid randomKey = Guid.NewGuid();
                    chechEmail[0].Reset_Password_Code = randomKey.ToString().Substring(0, 6).ToUpper();
                    var date = DateTime.Now.ToString("dd MMMM yyyy, dddd");
                    var hour = DateTime.Now.ToString("HH:mm");
                    context.SaveChanges();

                    var body = "<p>Değerli {0}; </p><p>Şifre değiştirme talebiniz {1} {2} tarihinde başarıyla alınmıştır.</p><h4>Doğrulama kodu; <span class='badge badge-secondary' style='color:#0E37E8'>{3}</span></h4></p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(chechEmail[0].Email));  // replace with valid value 
                    message.From = new MailAddress("mhmmd_akkynk@hotmail.com");  // replace with valid value
                    message.Subject = "Şifre Değiştirme İsteği";
                    message.Body = string.Format(body, chechEmail[0].FullName, date, hour, chechEmail[0].Reset_Password_Code);
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
                        output_array = errorCode.errorCodesValue(0, 0, 7);
                    }
                }
                else
                {
                    output_array = errorCode.errorCodesValue(1, 1, 14);
                }
            }
            else
            {
                output_array = errorCode.errorCodesValue(1, 1, 3);
            }
            return Json(output_array, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckResetPassword(Adminler adminler)
        {
            var chechResetPassword = context.Admins.Where(m => m.Reset_Password_Code == adminler.Reset_Password_Code).ToList();
            if(chechResetPassword.Count != 0)
            {
                output_array = errorCode.errorCodesValue(0, 0, 8);
                output_array.Add(new KeyValuePair<string, string>("Id", chechResetPassword[0].Id.ToString()));
            }
            else
            {
                output_array = errorCode.errorCodesValue(1, 1, 9);
                output_array.Add(new KeyValuePair<string, string>("Id", ""));
            }
            return Json(output_array, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ResetPasswordConfirm(string Id)
        {
            var id = Convert.ToInt32(Id.ToString());
            var admin = context.Admins.Where(m => m.Id == id).ToList();
            return View(admin);
        }

        [HttpPost]
        public ActionResult ResetPasswordConfirm(string currentPassword,string Password_Repeat, Adminler adminler)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            if (currentPassword != null && adminler.Password != null && Password_Repeat != "")
            {
                if(adminler.Password == Password_Repeat)
                {
                    var chechCurrentPass = context.Admins.Where(m => m.Id == adminler.Id).ToList();
                    var password = crypto.Compute(currentPassword, chechCurrentPass[0].Salt);

                    if (chechCurrentPass.Count != 0)
                    {
                        if(chechCurrentPass[0].Password == password)
                        {
                            var encrypedPassword = crypto.Compute(adminler.Password);
                            chechCurrentPass[0].Password = encrypedPassword;
                            chechCurrentPass[0].Salt = crypto.Salt;
                            context.SaveChanges();
                            output_array = errorCode.errorCodesValue(0, 0, 11);
                        }
                        else
                        {
                            output_array = errorCode.errorCodesValue(1, 1, 10);
                        }
                    }
                    else
                    {
                        output_array = errorCode.errorCodesValue(1, 1, 15);
                    }
                }
                else
                {
                    output_array = errorCode.errorCodesValue(1, 1, 1);
                }
                
            }
            else
            {
                output_array = errorCode.errorCodesValue(1, 1, 3);
            }
            return Json(output_array, JsonRequestBehavior.AllowGet);
        }
    }
}