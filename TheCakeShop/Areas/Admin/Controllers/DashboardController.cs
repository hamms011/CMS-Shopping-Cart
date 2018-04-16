using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCakeShop.Models.Data;
using TheCakeShop.Models.ViewModels;
using TheCakeShop.Models.ViewModels.Shop;

namespace TheCakeShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {

            using (Db db = new Db())
            {
                //Counting Main
                ViewBag.CountPages = db.Pages.SqlQuery("SELECT * FROM TBLPAGES").Count();
                ViewBag.CountSideBar = db.Sidebar.SqlQuery("SELECT * FROM TBLSIDEBAR").Count();
                ViewBag.CountCategories = db.Categories.SqlQuery("SELECT * FROM TBLCATEGORIES").Count();
                ViewBag.CountProducts = db.Products.SqlQuery("SELECT * FROM TBLPRODUCTS").Count();
                ViewBag.CountUsers = db.Users.SqlQuery("SELECT * FROM TBLUSERS").Count();

                //Counting Products
                ViewBag.CountCupCakes = db.Products.SqlQuery("SELECT * FROM TBLPRODUCTS WHERE CATEGORYNAME='Cupcakes' ").Count();
                ViewBag.CountBirthdayCakes = db.Products.SqlQuery("SELECT * FROM TBLPRODUCTS WHERE CATEGORYNAME='Birthday Cakes' ").Count();
                ViewBag.CountWeddingCakes = db.Products.SqlQuery("SELECT * FROM TBLPRODUCTS WHERE CATEGORYNAME='Wedding Cakes' ").Count();
                ViewBag.CountAnniversaryCakes = db.Products.SqlQuery("SELECT * FROM TBLPRODUCTS WHERE CATEGORYNAME='Anniversary Cakes' ").Count();


            }
            
            return View();
        }
    }
}