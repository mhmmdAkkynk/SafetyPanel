using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SafetyPanel.Models;
using SafetyPanel.Models.Admin;

namespace SafetyPanel.Controllers.Admin
{
    public class AdminSettingsController : Controller
    {
        Context context = new Context();
        errorCode errorCode = new errorCode();
        List<KeyValuePair<string, string>> output_array = new List<KeyValuePair<string, string>>();

        // GET: AdminSettings
        [Authorize]
        [HttpGet]
        public ActionResult Settings()
        {
            if (Session["Id"] != null)
            {
                int Id = Convert.ToInt32(Session["Id"].ToString());
                var adminInfo = context.Admins.Where(m => m.Id == Id).ToList();
                return View(adminInfo);
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Sign_In", "SignIn");
            }
        }

        [HttpPost]
        public ActionResult AdminGeneralInfo(string UserName, HttpPostedFileBase Photo)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var id = Convert.ToInt32(Session["Id"].ToString());
            var adminGeneralInfo = context.Admins.Where(m => m.Id == id).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null && UserName != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/img/AdminPhoto/"), Path.GetFileName(Photo.FileName));
                        Photo.SaveAs(path);
                        string[] pathArray = path.Split('\\');
                        for (int i = 6; i < pathArray.Length; i++)
                        {
                            stringBuilder.Append("/");
                            stringBuilder.Append(pathArray[i]);
                        }
                        var filePath = stringBuilder.ToString();
                        adminGeneralInfo[0].UserName = UserName;
                        adminGeneralInfo[0].Photo = filePath;
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading";
                }
            }
            return Redirect("Settings");
        }

        public PartialViewResult AdminPersonalInfo()
        {
            if(Session["Id"] != null)
            {
                AdminHayat adminHayat = new AdminHayat();

                int Id = Convert.ToInt32(Session["Id"].ToString());
                var adminInfo = context.Admins.Where(m => m.Id == Id).ToList();
                string[] adminInfoArray = adminInfo[0].FullName.Split(' ');
                ViewBag.FirstName = adminInfoArray[0];
                ViewBag.LastName = adminInfoArray[1];
                var province = context.Provinces.ToList();
                var districts = context.Districts.ToList();

                adminHayat.AdminlerInformation = adminInfo;
                adminHayat.Provinces = province;
                adminHayat.Districts = districts;

                return PartialView(adminHayat);
            }
            else
            {
                Session.Abandon();
                return PartialView();
            }
        }
    }
}