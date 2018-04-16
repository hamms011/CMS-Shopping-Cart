using System;
using System.Collections.Generic;
using PagedList;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCakeShop.Models.Data;
using TheCakeShop.Models.ViewModels.Account;

namespace TheCakeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index(int? page)
        {
            //Declare list of PageVM
            List<UserVM> usersList;

            using (Db db = new Db())
            {
                //Init the Database
                usersList = db.Users.ToArray().OrderBy(x => x.Id).Select(x => new UserVM(x)).ToList();
            }

            //Return view with list
            return View(usersList);
        }

        // GET: Admin/Users/CreateAccount
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        // POST: Admin/Users/CreateAccount
        [HttpPost]
        public ActionResult CreateAccount(UserVM model)
        {
            //Check model state
            if (!ModelState.IsValid)
            {
                return View("CreateAccount", model);
            }
            //Check if passwords match
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View("CreateAccount", model);
            }

            using (Db db = new Db())
            {
                //Make sure username is unique
                if (db.Users.Any(x => x.Username.Equals(model.Username)))
                {
                    ModelState.AddModelError("", "Username " + model.Username + " is taken.");
                    model.Username = "";
                    return View("CreateAccount", model);
                }
                //Create UserDTO
                UserDTO userDTO = new UserDTO()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Number = model.Number,
                    EmailAddress = model.EmailAddress,
                    Username = model.Username,
                    Password = model.Password,
                    DateOfBirth = model.DateOfBirth
                };
                //Add the DTO
                db.Users.Add(userDTO);
                //Save
                db.SaveChanges();
                //Add to userRolesDTO
                int id = userDTO.Id;

                UserRoleDTO userRoleDTO = new UserRoleDTO()
                {
                    UserId = id,
                    RoleId = 2
                };

                db.UserRoles.Add(userRoleDTO);
                db.SaveChanges();
            }
            //Create a tempdata message
            TempData["SM"] = "User is now registered.";

            //Redirect

            return RedirectToAction("CreateAccount");
        }

        // GET: Admin/Pages/DeletePage/id
        public ActionResult DeleteAccount(int id)
        {
            using (Db db = new Db())
            {
                //Get the page
                UserDTO dto = db.Users.Find(id);
                //Remove the page
                db.Users.Remove(dto);
                //Save
                db.SaveChanges();
            }
            //TempData Message
            TempData["DM"] = "User has been deleted.";

            //Redirect
            return RedirectToAction("Index");
        }

    }
}