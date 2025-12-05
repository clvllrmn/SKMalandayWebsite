using Newtonsoft.Json;
using SKMalandayWebsite.Models;
using SKMalandayWebsite.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SKMalandayWebsite.Controllers
{
    public class SKMalandayController : Controller
    {

        #region View Actions

        public ActionResult RegistrationPage() => View();

        public ActionResult AboutPage() => View();

        public ActionResult ProjectPage() => View();

        public ActionResult ContactPage() => View();

        public ActionResult CouncilPage() => View();

        public ActionResult RolePage() => View();

        public ActionResult HomePage() => View();

        public ActionResult DashboardPage() => View();

        #endregion


        #region Login

        public ActionResult LoginPage(string Username, string Password)
        {
            try
            {
                if (Username == "admin" && Password == "admin123")
                {
                    Session["UserFname"] = "Kathryn";
                    Session["UserId"] = 0;

                    return RedirectToAction("DashboardPage", "SKMalanday");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        #endregion

        public JsonResult GetUsers()
        {
            using (var db = new SKContext())
            {
                var list = db.tbl_users
                             .OrderByDescending(u => u.createdAt)
                             .Select(u => new
                             {
                                 u.userID,
                                 u.fullName,
                                 birthday = u.birthDate,
                                 u.gender,
                                 u.email,
                                 u.address,
                                 u.contactNo,
                                 u.interest
                             })
                             .ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult VerifyConnection(UserInformation userinfo)
        {
            try
            {
                if (userinfo == null)
                {
                    return Json(new { success = false, message = "Invalid payload received." });
                }

                if (string.IsNullOrWhiteSpace(userinfo.fullName))
                {
                    return Json(new { success = false, message = "Full Name is required." });
                }

                if (!userinfo.birthDate.HasValue)
                {
                    return Json(new { success = false, message = "Birthdate is required." });
                }

                using (var db = new SKContext())
                {
                    if (!db.Database.Exists())
                    {
                        return Json(new { success = false, message = "Database connection failed. Check connection string 'sk_db'." });
                    }

                    var newUser = new tbl_usersModel
                    {
                        fullName = userinfo.fullName,
                        email = userinfo.email,
                        address = userinfo.address,
                        contactNo = userinfo.contactNo,
                        birthDate = userinfo.birthDate.Value,
                        gender = userinfo.gender,
                        interest = userinfo.interest,
                        createdAt = DateTime.Now,
                        updatedAt = DateTime.Now
                    };

                    db.tbl_users.Add(newUser);
                    db.SaveChanges();

                    return Json(new { success = true, message = "User registered successfully." });
                }
            }
            catch (Exception ex)
            {
                string err = ex.InnerException?.Message ?? ex.Message;

                return Json(new { success = false, message = err });
            }
        }


        [HttpGet]
        public JsonResult GetInterestChartData()
        {
            try
            {
                using (var db = new SKContext())
                {
                    var dataGroup = db.tbl_users
                                      .GroupBy(u => u.interest)
                                      .Select(g => new
                                      {
                                          InterestId = g.Key,
                                          Count = g.Count()
                                      })
                                      .ToList();

                    var model = new ChartModel();
                    List<string> labels = new List<string>();
                    List<int> counts = new List<int>();

                    foreach (var item in dataGroup)
                    {
                        
                        string name = GetInterestName(item.InterestId.ToString());

                        labels.Add(name);
                        counts.Add(item.Count);
                    }

                    model.ChartLabels = labels;
                    model.ChartData = new List<List<int>>();
                    model.ChartData.Add(counts); 

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       


        private string GetInterestName(string id)
        {
            if (string.IsNullOrEmpty(id)) return "Unspecified";

            switch (id.Trim())
            {
                case "1": return "Leadership";
                case "2": return "Sports";
                case "8": return "Arts";
                case "9": return "Education";
                case "10": return "Mental Health";
                case "4": return "Gender Equality";
                case "5": return "Environment";
                case "6": return "Entrepreneurship";
                case "7": return "Digital Literacy";
                default: return "Other (" + id + ")";
            }
        }

    }
}
