using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using CarsManagement.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Sakura.AspNetCore;

namespace CarsManagement.Controllers
{
    public class AdminPanelController : Controller
    {
        #region Constructr

        IHostingEnvironment _env = null;
        CarsManagementContext _context = null;
        int cat;
        public int? CatId { get; private set; }

        public AdminPanelController(IHostingEnvironment env, CarsManagementContext context)
        {
            _env = env;
            _context = context;
        }
        #endregion

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.CreatedDate = System.DateTime.Now.ToString();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            ViewBag.owner = "Owner";
            ViewData["ModifiedBy"] = HttpContext.Session.GetString("name");
            ViewBag.ModifiedDate = System.DateTime.Now.ToString();
            ViewBag.CatList = _context.Categry.ToList<Categry>();
            if (ViewData["SessionName"] == null)
            {
                return RedirectToAction("LogIn");
            }
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            User test = _context.User.Where(e => e.UserName == user.UserName).FirstOrDefault();
            if (test != null)
            {
                ModelState.AddModelError("", "User Name is alerdy exist! Try this one..");
                Random rand = new Random();
                int value = rand.Next(1000);
                string randGenrator = value.ToString("000");
                string hintId = user.UserName + randGenrator;
                User hintname = _context.User.Where(e => e.UserName == hintId).FirstOrDefault();
                if (hintname == null)
                {
                    ViewBag.hintName = hintId;
                }
            }
            else
            {
                _context.User.Add(user);
                _context.SaveChanges();
                ViewData["SessionName"] = HttpContext.Session.GetString("name");
                ViewBag.message = "Your account is created!";
            }
            ViewBag.CatList = _context.Categry.ToList<Categry>();

            return View();

        }
        #endregion

        #region LogIn
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(User u)
        {
            User query = _context.User.Where(e => e.UserName == u.UserName && e.Password == u.Password).FirstOrDefault();
            if (query != null)
            {
                HttpContext.Session.SetString("name", query.UserName);
                HttpContext.Session.SetString("type", query.Type);

                return RedirectToAction("HomeProducts");
            }
            else
            {
                ModelState.AddModelError("", "Invalid user name or password");
            }
            return View();
        }
        #endregion

        #region Home
        [HttpGet]
        public IActionResult Home()
        {


            ViewBag.CreatedDate = System.DateTime.Now.ToString();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");

            var SessionName = HttpContext.Session.GetString("name");
            ViewData["Count"] = _context.Categry.Where(e => e.CreatedBy == SessionName).ToList<Categry>().Count;

            ViewBag.owner = "Owner";
            var type = ViewData["SessionType"];
            ViewBag.type = type;
            if (ViewData["SessionName"] == null)
            {
                return RedirectToAction("LogIn");
            }


            IList<Categry> ILS = _context.Categry.ToList<Categry>();
            return View(ILS);

        }
        [HttpPost]
        public IActionResult Home(Products products)
        {

            return View();
        }
        #endregion

        #region Categry
        [HttpGet]
        public IActionResult AddCategry()
        {
            ViewBag.CreatedDate = System.DateTime.Now.ToString();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            ViewBag.owner = "Owner";
            ViewBag.CatList = _context.Categry.ToList<Categry>();
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }
            return View();

        }
        [HttpPost]
        public IActionResult AddCategry(Categry categry)
        {
            ViewBag.CatList = _context.Categry.ToList<Categry>();
            _context.Categry.Add(categry);
            _context.SaveChanges();
            ViewBag.message = "Data send successfully";
            return View();
        }
        #endregion

        #region Product
        [HttpGet]
        public IActionResult AddProduct(int cat)
        {
            ViewBag.CreatedDate = System.DateTime.Now.ToString();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            ViewBag.owner = "Owner";
            IList<Categry> SelectCategry = _context.Categry.ToList<Categry>();
            ViewBag.SelectCategry = SelectCategry;

            IList<SubCategry> SelectSubCategry = _context.SubCategry/*.Where(e=>e.CatId==cat)*/.ToList<SubCategry>();
            ViewBag.SelectSubCategry = SelectSubCategry;

            ViewBag.CatList = _context.Categry.ToList<Categry>();
            ViewBag.SubCatList = _context.SubCategry.ToList<SubCategry>();

            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }

            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Products products)
        {

            foreach (var file in Request.Form.Files)
            {
                string name = file.Name;
                string ext = System.IO.Path.GetExtension(file.FileName);
                string filename = DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss") + ext;
                string path = "";
                if (name.Equals("Image"))
                {
                    products.ProductImage = filename;
                    path = _env.WebRootPath + "/Data/Images/" + filename;
                }
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }

            //products.CatId = CatId;
            _context.Products.Add(products);
            _context.SaveChanges();
            ViewBag.message = "Data send successfully";
            ViewBag.CatList = _context.Categry.ToList<Categry>();
            return RedirectToAction("Home");
        }
        #endregion

        #region SubCategry
        [HttpGet]
        public IActionResult AddSubCategry()
        {
            ViewBag.CreatedDate = System.DateTime.Now.ToString();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            ViewBag.owner = "Owner";
            IList<Categry> SelectCategry = _context.Categry.ToList<Categry>();
            ViewBag.SelectCategry = SelectCategry;
            ViewBag.CatList = _context.Categry.ToList<Categry>();
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }

            return View();
        }
        [HttpPost]
        public IActionResult AddSubCategry(SubCategry subcategry)
        {
            _context.SubCategry.Add(subcategry);
            _context.SaveChanges();
            ViewBag.message = "Data send successfully";
            ViewBag.CatList = _context.Categry.ToList<Categry>();
            return RedirectToAction("Home");
        }
        #endregion

        #region SubcatList
        [HttpGet]
        public IActionResult SubCatList()
        {
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }
            ViewBag.owner = "Owner";
            IList<SubCategry> SubCatList = _context.SubCategry.ToList<SubCategry>();
            return View(SubCatList);
        }
        [HttpPost]
        public IActionResult SubCatList(SubCategry SubCatList)
        {
            return View();
        }
        #endregion

        #region ShowProducts
        [HttpGet]
        public IActionResult ShowProducts(Products products, int cat, int SubCatId)
        {


            ViewBag.CatList = _context.Categry.ToList();
            ViewBag.SubCatList = _context.SubCategry.Where(e => e.CatId == cat).ToList<SubCategry>();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewBag.owner = "Owner";
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }
            if (cat != null && SubCatId == 0)
            {
                IList<Products> link = _context.Products.Where(e => e.CatId == cat).ToList<Products>();

                return View(link);

            }

            else if (SubCatId != null & SubCatId != 0)
            {
                IList<Products> SubCatList = _context.Products.Where(e => e.SubCatId == SubCatId).ToList<Products>();
                return View(SubCatList);
            }



            return View();
        }
        [HttpPost]
        public IActionResult ShowProducts()
        {

            return View();
        }
        #endregion

        #region HomeProducts
        [HttpGet]
        public IActionResult HomeProducts(Products products, int Page = 1)
        {
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewBag.owner = "Owner";
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }

            ViewBag.CatList = _context.Categry.ToList<Categry>();
            IPagedList<Products> HPILS = _context.Products.ToPagedList<Products>(2, Page);
            return View(HPILS);

        }
        [HttpPost]
        public IActionResult HomeProducts()
        {
            return View();
        }
        #endregion

        #region ProductDetails
        [HttpGet]
        public IActionResult ProductDetails(Products prod, int Id)
        {
            ViewBag.owner = "Owner";
            ViewBag.CatList = _context.Categry.ToList();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            ViewData["Owner"] = "Owner";
            var type = ViewData["SessionType"];
            ViewData["Type"] = type;
            if (ViewData["SessionType"] == null)
            {
                return RedirectToAction("Login");
            }
            IList<Products> ILS = _context.Products.Where(e => e.Id == Id).ToList<Products>();
            return View(ILS);
        }
        [HttpPost]
        public IActionResult ProductDetails()
        {
            return View();
        }
        #endregion

        #region UpdateProduct
        [HttpGet]
        public IActionResult UpdateProduct(int? Id)
        {
            ViewBag.owner = "Owner";
            ViewBag.ModifiedDate = DateTime.Now.ToString();
            ViewData["SessionName"] = HttpContext.Session.GetString("name");
            ViewData["SessionType"] = HttpContext.Session.GetString("type");
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }
            Products products = _context.Products.Where(m => m.Id == Id).FirstOrDefault();
            return View(products);
        }
        [HttpPost]
        public IActionResult UpdateProduct([Bind(include: "Id,CatId,SubCatId,ProductName,ProductDescription,ProductPrice,ProductImage")]Products products)
        {
            try
            {
                _context.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("HomeProducts");
            }
            catch
            {

            }
            return View();
        }
        #endregion

        #region DeleteProductByHttpRequestAndAjax
        [HttpGet]
        public string DeleteProduct(int Id)
        {
            Products products = new Products();
            products.Id = Id;
            try
            {
                _context.Products.Remove(products);
                _context.SaveChanges();
                return "DONE";
            }
            catch (Exception ex)
            {

            }
            //return RedirectToAction("ProductsList");
            return "Error";
        }
        [HttpPost]
        public IActionResult DeleteProduct()
        {
            return View();
        }
        #endregion

        #region SearchProductByusingAjax
        public string SearchProductByAjax(int ProductId)
        {

            string Data = "";
            IList<Products> products = _context.Products.Where(m => m.CatId == ProductId).ToList<Products>();

            Data += "<table class='table table-striped'>";
            foreach (Products item in products)
            {
                Data += "<tr>";

                Data += "<td>";
                Data += item.ProductName;
                Data += "</td>";

                Data += "<td>";
                Data += item.ProductPrice;
                Data += "</td>";

                Data += "<td>";
                Data += "<img src=/Data/Images/'" + item.ProductImage + "'/>";
                Data += "</td>";

                Data += "</tr>";
            }
            Data += "</table>";

            return Data;
        }
        #endregion

        #region ProductList
        [HttpGet]
        public IActionResult ProductsList(int Page = 1)
        {
            IList<Products> ILSProsucts = _context.Products.ToList<Products>();
            return View(ILSProsucts);
            //IPagedList<Products> IPGL = _context.Products.ToPagedList<Products>(2, Page);
            //    return View(IPGL);
        }
        [HttpPost]
        public IActionResult ProductsList(Products products)
        {
            return View();
        }
        #endregion

        #region Logout
        [HttpGet]
        public IActionResult LogOut()
        {
            if (ViewData["SessionName"] == null)
            {
                return View("LogIn");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }
        #endregion

        #region Search
        [HttpGet]
        public IActionResult Searchproducts(Categry categry)
        {
            ViewBag.CatList = _context.Categry.ToList<Categry>();

            return View();
        }
        [HttpPost]
        public IActionResult Searchproducts(Products products)
        {
            ViewBag.CatList = _context.Categry.ToList<Categry>();

            Products search = _context.Products.FirstOrDefault(e => e.ProductName == products.ProductName);
            if (search != null)
            {
                IList<Products> ILS = _context.Products.ToList<Products>();
                //ViewData["ProductsImage"] = search.ProductImage;
                //ViewData["ProductsName"] = search.ProductName;
                //ViewData["ProductsPrice"] = search.ProductPrice;
                return View(ILS);
            }
            else
            {
                ModelState.AddModelError("", "Sorry");
            }
            return View();
        }
        #endregion

        #region AllinOne
        public ActionResult AllinOne()
        {
            var model = new AllinOne
            {
                categry = _context.Categry.ToList(),
                products = _context.Products.ToList()
            };

            return View(model);
        }

        #endregion

        #region ProductGallery
        [HttpGet]
        public ActionResult ProductGallery(Products products)
        {
            IList<Products> ILS = _context.Products.ToList<Products>();
            return View(ILS);
        }
        [HttpPost]
        public ActionResult ProductGallery()
        {
            return View();
        }

        #endregion

        #region ProductGalleryDetails
        public ActionResult ProductGalleryDetails(int? Id)
        {
            if (Id != null)
            {
                Products products = _context.Products.Where(e => e.Id == Id).SingleOrDefault<Products>();

                return View(products);
            }
            return View();

        }

        #endregion


    }

}
