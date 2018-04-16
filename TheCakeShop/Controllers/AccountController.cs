using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheCakeShop.Models.Data;
using TheCakeShop.Models.ViewModels;
using TheCakeShop.Models.ViewModels.Account;

namespace TheCakeShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return Redirect("/Account/Login");
        }

        // GET: /Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            //Confirm user is not logged in

            string username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                return RedirectToAction("user-profile");
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginUserVM model)
        {
            //Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Check if the user is valid
            bool isValid = false;

            using (Db db = new Db())
            {
                if (db.Users.Any(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password)))
                {
                    isValid = true;
                }
            }

            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberME);
                return Redirect(FormsAuthentication.GetRedirectUrl(model.Username, model.RememberME));
            }
            return View();
        }

        // GET: /Account/create-account
        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        // POST: /Account/create-account
        [ActionName("create-account")]
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
            TempData["SM"] = "You are now registered and can login.";

            //Redirect

            return Redirect("~/Account/Login");
        }

        // GET: /Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Account/Login");
        }

        [Authorize]
        public ActionResult UserNavPartial()
        {
            //Get Username
            string username = User.Identity.Name;
            //Declare model
            UserNavPartialVM model;

            using (Db db = new Db())
            {
                //Get the user
                UserDTO dto = db.Users.FirstOrDefault(x => x.Username == username);
                //Build the model
                model = new UserNavPartialVM()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
            }
            //Return partial view with model
            return PartialView(model);
        }

        // GET: /Account/user-profile
        [HttpGet]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile()
        {
            //Get the username
            string username = User.Identity.Name;
            //Declare model
            UserProfileVM model;

            using (Db db = new Db())
            {
                //Get user
                UserDTO dto = db.Users.FirstOrDefault(x => x.Username == username);
                //Build the model
                model = new UserProfileVM(dto);
            }
            //Return view with model
            return View("UserProfile", model);
        }

        // POST: /Account/user-profile
        [HttpPost]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile(UserProfileVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("UserProfile", model);
            }

            // Check if passwords match if need be
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View("UserProfile", model);
                }
            }

            using (Db db = new Db())
            {
                // Get username
                string username = User.Identity.Name;

                // Make sure username is unique
                if (db.Users.Where(x => x.Id != model.Id).Any(x => x.Username == username))
                {
                    ModelState.AddModelError("", "Username " + model.Username + " already exists.");
                    model.Username = "";
                    return View("UserProfile", model);
                }

                // Edit DTO
                UserDTO dto = db.Users.Find(model.Id);

                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
                dto.EmailAddress = model.EmailAddress;
                dto.Username = model.Username;
                dto.Address = model.Address;
                dto.Number = model.Number;
                dto.DateOfBirth = model.DateOfBirth;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    dto.Password = model.Password;
                }

                // Save
                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "You have edited your profile!";

            // Redirect
            return Redirect("~/Account/user-profile");
        }

        // GET: /Account/Orders
        [Authorize(Roles = "User")]
        public ActionResult Orders()
        {
            //Init list of OrdersForUsersVM
            List<OrdersForUsersVM> ordersForUsers = new List<OrdersForUsersVM>();

            using (Db db = new Db())
            {
                //Get user id
                UserDTO user = db.Users.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
                int userId = user.Id;
                //Init list of OrderVM
                List<OrderVM> orders = db.Orders.Where(x => x.UserId == userId).ToArray().Select(x => new OrderVM(x)).ToList();
                //Loops through list of OrderVM
                foreach (var order in orders)
                {
                    //Init the product list
                    Dictionary<string, int> productsAndQty = new Dictionary<string, int>();
                    //declare total
                    decimal total = 0m;
                    //Init the list of OrderDetailsDTO
                    List<OrderDetailsDTO> orderDetailsDTO = db.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                    //Loop through list of OrderDetailsDTO
                    foreach (var orderDetails in orderDetailsDTO)
                    {
                        //get product
                        ProductDTO product = db.Products.Where(x => x.Id == orderDetails.ProductId).FirstOrDefault();

                        //Get product price
                        decimal price = product.Price;

                        //Get product name
                        string productname = product.Name;

                        //Add to product dict
                        productsAndQty.Add(productname, orderDetails.Quantity);

                        //Get total
                        total += orderDetails.Quantity * price;
                    }

                    //Add to OrdersForUsersVM 
                    ordersForUsers.Add(new OrdersForUsersVM()
                    {
                        OrderNumber = order.OrderId,
                        Total = total,
                        ProductsAndQty = productsAndQty,
                        CreatedAt = order.CreatedAt
                    });
                }
            }
            //Return view with orders
            return View(ordersForUsers);
        }
    }
}