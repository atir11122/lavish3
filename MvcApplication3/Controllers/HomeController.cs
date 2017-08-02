using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Database1Entities2 de = new Database1Entities2();

        public ActionResult Index()
        {
            //List<Product> listp = new List<Product>();
            //listp = de.Products.Select(p=>p).ToList();

            ////Customer c11 = de1.Customers.Single(c1 => c1.Email == c.Email && c1.Password == c.Password);
            //Session["Products"] = listp;
          
            //// ViewBag.Name = c11.Name;

            List<Models.Product> pd = de.Products.ToList();
           // Models.Product pd = de.Products.ToList();

            return View(pd);

        }

        public JsonResult Check()
        {
            bool result = false;
            string email = Request["remail"];
            Customer c = (Customer)de.Customers.Where(a=>a.Email == email);
            if(c!=null)
            {
                result = true;
            }
            return this.Json(result, JsonRequestBehavior.AllowGet);
        
        
        }

        public ActionResult Remail()
        {


            return View();
        }

        public ActionResult Email()
        {
            string email = Request["remail"];
            Customer c = de.Customers.Single(a => a.Email == email);
           Session["remail"] = c.Email;
           Session["pass"] = c.Password;




            return View();
        }





        public ActionResult Search()
        {

            Session["type"] = Request["search"];
           // string a = (string)Session["type"];

            return RedirectToAction("Dresses1", "Home");
        }
        public ActionResult Dresses1()
        {
            return View(de.Products.ToList());
        }

        public ActionResult Dresses(string a)
        {
           
                Session["type"] = a;

            
           
            return View(de.Products.ToList());
        }
       
        public ActionResult Jeans()
        {


            return View("Jeans", "_layout");
        }
        public ActionResult Products()
        {


            return View("Products", "_layout");
        }
        public ActionResult Salwars()
        {


            return View("Salwars", "_layout");
        }
        public ActionResult Sandals()
        {


            return View("Sandals", "_layout");
        }
        public ActionResult Sarees()
        {


            return View("Sarees", "_layout");
        }
        public ActionResult Shirts()
        {


            return View("Shirts", "_layout");
        }
        public ActionResult Skirts()
        {


            return View("Skirts", "_layout");
        }
        public ActionResult Sweaters()
        {


            return View("Sweaters", "_layout");
        }
        public ActionResult Single()
        {


            return View("Single", "_layout");
        }
        public ActionResult Mail()
        {


            return View("Mail", "_layout");
        }
        public ActionResult Faq()
        {


            return View("Faq", "_layout");
        }
     

    [HttpPost]
        public ActionResult Index(Customer c)
        {
            if (c.Name != null)
            {
                try
                {
                    de.Customers.Add(c);
                    de.SaveChanges();
                    ViewBag.Name = "You have been successfully registered.Please Log-In";

                    return View("Index");

                }
                catch
                {

                    ViewBag.Name = "Failed. Please try again in a few moments";

                    return View("Index");



                }

            }

            else
            {

                ViewBag.Name = "Failed. Please try again in a few moments";

                return View("Index");



            }

           // return View("Index", "_layout", de.Customers.ToList());
        }
        public ActionResult AboutUs()
        {
            return View("AboutUs", "_layout");
        }
      
       
      

    }
}
