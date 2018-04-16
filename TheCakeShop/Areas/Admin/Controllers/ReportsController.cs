using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCakeShop.Areas.Admin.Models.ViewModels.Shop;
using TheCakeShop.Models.Data;
using TheCakeShop.Models.ViewModels;

namespace TheCakeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        // GET: Admin/Reports
        public ActionResult AnnuallyReport()
        {
            //Init list of OrdersForAdminVM
            List<OrdersForAdmin> ordersForAdmins = new List<OrdersForAdmin>();

            using (Db db = new Db())
            {
                //Init list of OrderVM
                List<OrderVM> orders = db.Orders.ToArray().Select(x => new OrderVM(x)).ToList();
                //Loop through list of OrderVM
                foreach (var order in orders)
                {
                    //Init product dict
                    Dictionary<string, int> productAndQty = new Dictionary<string, int>();
                    //Declare total
                    decimal total = 0m;

                    //Init list of OrderDetailsDTO
                    List<OrderDetailsDTO> orderDetailsList = db.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                    //Get username 
                    UserDTO user = db.Users.Where(x => x.Id == order.UserId).FirstOrDefault();
                    string username = user.Username;
                    //Loop through list of OrderDetailsDTO
                    foreach (var orderDetails in orderDetailsList)
                    {
                        //Get product
                        ProductDTO product = db.Products.Where(x => x.Id == orderDetails.ProductId).FirstOrDefault();
                        //Get product price
                        decimal price = product.Price;
                        //Get product name
                        string productname = product.Name;
                        //Add to product dict
                        productAndQty.Add(productname, orderDetails.Quantity);
                        //Get total
                        total += orderDetails.Quantity * price;

                    }

                    ordersForAdmins.Add(new OrdersForAdmin()
                    {
                        OrderNumber = order.OrderId,
                        Username = username,
                        Total = total,
                        ProductsAndQty = productAndQty,
                        CreatedAt = order.CreatedAt

                    });
                }
            }

            //Return View
            return View(ordersForAdmins);
        }

        public ActionResult MonthlyReport()
        {
            //Init list of OrdersForAdminVM
            List<OrdersForAdmin> ordersForAdmins = new List<OrdersForAdmin>();

            using (Db db = new Db())
            {
                //Init list of OrderVM
                List<OrderVM> orders = db.Orders.ToArray().Select(x => new OrderVM(x)).ToList();
                //Loop through list of OrderVM
                foreach (var order in orders)
                {
                    //Init product dict
                    Dictionary<string, int> productAndQty = new Dictionary<string, int>();
                    //Declare total
                    decimal total = 0m;

                    //Init list of OrderDetailsDTO
                    List<OrderDetailsDTO> orderDetailsList = db.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                    //Get username 
                    UserDTO user = db.Users.Where(x => x.Id == order.UserId).FirstOrDefault();
                    string username = user.Username;
                    //Loop through list of OrderDetailsDTO
                    foreach (var orderDetails in orderDetailsList)
                    {
                        //Get product
                        ProductDTO product = db.Products.Where(x => x.Id == orderDetails.ProductId).FirstOrDefault();
                        //Get product price
                        decimal price = product.Price;
                        //Get product name
                        string productname = product.Name;
                        //Add to product dict
                        productAndQty.Add(productname, orderDetails.Quantity);
                        //Get total
                        total += orderDetails.Quantity * price;

                    }

                    ordersForAdmins.Add(new OrdersForAdmin()
                    {
                        OrderNumber = order.OrderId,
                        Username = username,
                        Total = total,
                        ProductsAndQty = productAndQty,
                        CreatedAt = order.CreatedAt

                    });
                }
            }

            //Return View
            return View(ordersForAdmins);
        }
    }
}