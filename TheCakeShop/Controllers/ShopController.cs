using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCakeShop.Models.Data;
using TheCakeShop.Models.ViewModels.Products;
using TheCakeShop.Models.ViewModels.Shop;
using System.IO;

namespace TheCakeShop.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Pages");
        }
        public ActionResult CategoryMenuPartial()
        {
            //Display list of CategoryVM
            List<CategoryVM> categoryVMList;
            //Init the list
            using (Db db = new Db())
            {
                categoryVMList = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryVM(x)).ToList();
            }
            //Return partial with list
            return PartialView(categoryVMList);
        }

        public ActionResult CategoryMenuPartiall()
        {
            //Display list of CategoryVM
            List<CategoryVM> categoryVMList;
            //Init the list
            using (Db db = new Db())
            {
                categoryVMList = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryVM(x)).ToList();
            }
            //Return partial with list
            return PartialView(categoryVMList);
        }

        // GET: /Shop/Category/name
        public ActionResult Category(string name)
        {
            //Declare a list of ProductVM
            List<ProductVM> productVMList;

            using (Db db = new Db())
            {
                //Get Category Id
                CategoryDTO categoryDTO = db.Categories.Where(x => x.Slug == name).FirstOrDefault();
                int catId = categoryDTO.Id;
                //Init the list
                productVMList = db.Products.ToArray().Where(x => x.CategoryId == catId).Select(x => new ProductVM(x)).ToList();
                //Get Category name
                var productCat = db.Products.Where(x => x.CategoryId == catId).FirstOrDefault();
                ViewBag.CategoryName = productCat.CategoryName;
            }
            //Return view with list
            return View(productVMList);

        }

        // GET: /Shop/ProductDetails/name
        [ActionName("product-details")]
        public ActionResult ProductDetails(string name)
        {
            //Declare the VM and DTO
            ProductVM model;
            ProductDTO dto;
            //Init product Id
            int id = 0;

            using (Db db = new Db())
            {
                //Check if product exist 
                if (! db.Products.Any(x => x.Slug.Equals(name)))
                {
                    return RedirectToAction("Index", "Shop");
                }
                //Init ProductDTO
                dto = db.Products.Where(x => x.Slug == name).FirstOrDefault();
                //Get inserted id
                id = dto.Id;
                //Init model
                model = new ProductVM(dto);
            }
            //Get Gallery Images
            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs"))
                                               .Select(fn => Path.GetFileName(fn));
            //Return view with model
            return View("ProductDetails", model);
        }
    }
}