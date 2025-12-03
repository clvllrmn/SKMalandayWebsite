using SKMalandayWebsite.Models;
using SKMalandayWebsite.Models.Context;
using System;
using System.Data.Entity; // Required for Entity Framework operations
using System.Linq;
using System.Web.Mvc;

namespace SKMalandayWebsite.Controllers
{
    public class SKMalandayController : Controller
    {
        // ... (View Action Methods omitted for brevity) ...

        #region View Actions
        public ActionResult RegistrationPage() { return View(); }
        public ActionResult AboutPage() { return View(); }
        public ActionResult ProjectPage() { return View(); }
        public ActionResult ContactPage() { return View(); }
        public ActionResult CouncilPage() { return View(); }
        public ActionResult RolePage() { return View(); }
        public ActionResult HomePage() { return View(); }
        #endregion

        JsonResult VerifyConnection(UserInformation userinfo) { try { if (string.IsNullOrEmpty(userinfo.contactNo)) { return Json(new { success = false, message = "Error: Contact Number is required." }, JsonRequestBehavior.AllowGet); } if (userinfo.birthDate == null) { return Json(new { success = false, message = "Error: Birthdate is required." }, JsonRequestBehavior.AllowGet); } using (var db = new SKContext()) { var newUser = new tbl_usersModel() { fullName = userinfo.fullName, email = userinfo.email, address = userinfo.address, contactNo = userinfo.contactNo, birthDate = userinfo.birthDate.Value, gender = userinfo.gender, interest = userinfo.interest, createdAt = DateTime.Now, updatedAt = DateTime.Now, }; db.tbl_users.Add(newUser); db.SaveChanges(); return Json(new { success = true, message = "User registered successfully." }, JsonRequestBehavior.AllowGet); } } catch (Exception ex) { string errorMessage = ex.InnerException?.Message ?? ex.Message; return Json(new { success = false, message = $"Error: {errorMessage}" }, JsonRequestBehavior.AllowGet); } }

        // 2. READ API Endpoint (Aligned with getUsersService)
        [HttpGet]
        public JsonResult GetUsers()
        {
            try
            {
                using (var db = new SKContext())
                {
                    // Select only the necessary fields to match the UserInformation model
                    var users = db.tbl_users
                        .Select(u => new UserInformation
                        {
                            userID = u.userID,
                            fullName = u.fullName,
                            birthDate = u.birthDate,
                            gender = u.gender,
                            address = u.address,
                            contactNo = u.contactNo,
                            email = u.email,
                            interest = u.interest
                        })
                        .ToList();

                    return Json(new { success = true, data = users }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Json(new { success = false, message = $"Error fetching data. Details: {errorMessage}" }, JsonRequestBehavior.AllowGet);
            }
        }

        // 3. DELETE API Endpoint (Aligned with deleteUserService)
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                using (var db = new SKContext())
                {
                    var userToDelete = db.tbl_users.Find(id);

                    if (userToDelete == null)
                    {
                        return Json(new { success = false, message = $"Error: User with ID {id} not found." }, JsonRequestBehavior.AllowGet);
                    }

                    db.tbl_users.Remove(userToDelete);
                    db.SaveChanges();

                    return Json(new { success = true, message = "User deleted successfully." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Json(new { success = false, message = $"Error deleting user. Details: {errorMessage}" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}