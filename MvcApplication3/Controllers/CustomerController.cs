using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;
using System.Data.Entity;


namespace MvcApplication3.Controllers
{
   
    public class CustomerController : Controller
    {
        Database1Entities2 de1 = new Database1Entities2();
        //
        // GET: /Customer/

        public ActionResult IndexC()

        {
            //SendEMail("", "", "");
            Session["Products"] = de1.Products.ToList();

            return View(de1.Products.ToList());
        }
        public ActionResult Confirm()
        {
         
            return View();
        }
        public ActionResult Description(int id)
        {

           // var p1 = de1.Products.Find(id);
            Session["pid"] = id;
           

            return View(de1.Products.ToList());
        }
        public int isExisting(int id) {
            int id2 = (Int32)Session["Id"];
           // Cart c1 = de1.Carts.Single(a => a.CID == id2 && a.PID == id);
            if( (de1.Carts.Where(a => a.CID == id2).Where(a=>a.PID == id))!= null)
            {
                try
                {
                    Cart c1 = (de1.Carts.Where(a => a.CID == id2).Where(a => a.PID == id)).Single();
                    return c1.Id;

                }
                catch {
                    return -1;
                
                }

               
            }
            else
            {

                return -1;
            
            
            }
        
        
        
        
        }
            
        public ActionResult AddCart(int id)
        {
            Session["cart"] = "cart";
            if (Session["Id"] != null)
            {
                int id3 = isExisting(id);
                if (id3== -1)
                {
                    Product p11 = de1.Products.Find(id);
                    
                    Cart c11 = new Cart();
                    c11.CID = (Int32)Session["Id"];
                    c11.PID = p11.Id;
                    c11.Quantity = 1;
                    c11.STotal = p11.Price * c11.Quantity;
                    de1.Carts.Add(c11);
                    de1.SaveChanges();
                    var v = de1.Carts.Where(a => a.CID == c11.CID).ToList();
                    return RedirectToAction("IndexC");




                }
                else
                {
                    Cart c22 = de1.Carts.Find(id3);
                    c22.Quantity = c22.Quantity + 1;
                    Product p22 = de1.Products.Find(c22.PID);
                    c22.STotal = c22.STotal + p22.Price;
                    de1.Entry(c22).State = System.Data.EntityState.Modified;
                    de1.SaveChanges();





                    return RedirectToAction("IndexC");


                
                
                }


                 
                
                
                
     
               
              

           }
            else
            {
                Session["ErrorCart"] = "Please Log in first to continue.. ";

                return RedirectToAction("Index", "Home");


            }
        }

        public ActionResult Checkout()
        {

          

            return View(de1.Carts.ToList());
        }

        public ActionResult Order()
        {
            string p = null;
            if (Session["cart"] != null)
            {
                var v = de1.Carts.ToList();
                foreach (var item in v)
                {
                    if (item.CID == (Int32)Session["Id"])
                    {
                        p = p + "," + item.PID + "*" + item.Quantity;



                    }




                }
                string email = Request["confirm_email"];
                string name = Request["confirm_name"];
                string address = Request["confirm_address"];
                string phone = Request["confirm_phone"];
                Order o = new Order();
                o.AProducts = p;
                o.CID = (Int32)Session["Id"];
                o.Address = address;
                o.Mobile = phone;
                o.Email = email;
                o.Name = name;
                o.Total = (Int32)Session["Total"];
                de1.Orders.Add(o);

                var ab = de1.Carts.Where(a => a.Id == o.CID);
                //for (int j = 0; j < i.Count; j++) {

                //    de1.Carts.Remove(i[j]);

                //}

                foreach (var t in ab)
                {

                    de1.Carts.Remove(t);


                }




                de1.SaveChanges();





                return RedirectToAction("IndexC", "Customer");

            }
            else { 
                Session["ordermsg"] = "Can not place the order";
                return RedirectToAction("IndexC", "Customer");
            
            }

        }


        public ActionResult Remove(int id)
        {

           Cart c =  de1.Carts.Find(id);
           de1.Carts.Remove(c);
           de1.SaveChanges();

           return RedirectToAction("Checkout", "Customer");
        }
    
       

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult IndexC(Customer c)
        {
            try
            {
                if (c.Email == "admin@lavish.com" && c.Password == "admin") {

                    return RedirectToAction("Index", "Admin");
                
                
                }
                else
                {
                    Customer c11 = de1.Customers.Single(c1 => c1.Email == c.Email && c1.Password == c.Password);
                    Session["Name"] = c11.Name;
                    Session["Email"] = c11.Email;
                    Session["Address"] = c11.Address;
                    Session["Password"] = c11.Password;
                    Session["Id"] = c11.Id;
                    // ViewBag.Name = c11.Name;

                    // return View(from customer in de1.Customers.Take(1) select customer);
                    return View(de1.Products.ToList()); 



                }
                
            }
            catch {


                ViewBag.Name = "Failed";
              //  return View("Index","Home");
                return RedirectToAction("index","Home");
               
                //return View();
            
            }
           
            //var movies = from m in de1.Customers
            //             select m;
           

           // return View(c11.Name);


        }

        public ActionResult Logout()
        {
            Session.Clear();


            return RedirectToAction("Index", "Home");
        }
        public ActionResult Search()
        {

           Session["type"]= Request["search"];


            return RedirectToAction("Dresses1","Customer");
        }
        public ActionResult Dresses1()
        {
            return View(de1.Products.ToList());
        }
        public ActionResult Dresses()
        {


            return View("Dresses", "_clayout");
        }
        public ActionResult AboutUs()
        {


            return View("AboutUs", "_clayout");
        }
        public ActionResult Faq()
        {


            return View("Faq", "_clayout");
        }
        //public ActionResult Checkout()
        //{


        //    return View("Checkout", "_clayout");
        //}

        public ActionResult UpdateAccount()
        {


            return View("UpdateAccount", "_clayout");
        }
        [HttpPost]
        public ActionResult Update()
        {


            Customer c1 = de1.Customers.Find(Session["Id"]);
            c1.Name = Request["name"];
            c1.Email = Request["email"];
            c1.Password = Request["password"];
            de1.Entry(c1).State = System.Data.EntityState.Modified;
            de1.SaveChanges();

            return RedirectToAction("IndexC", "Customer");



        }

      
        public ActionResult Update1(Customer c)
        {
           // return View(c.Email);
           

           // Customer c11 = de1.Customers.Single(c1 => c1.Email == c.Email);

            de1.Entry(c).State = System.Data.EntityState.Modified;
            de1.SaveChanges();
            
            return RedirectToAction("IndexC");


        }
        public ActionResult Mail()
        {


            return View("Mail", "_clayout");
        }
        public ActionResult Jeans()
        {


            return View("Jeans", "_clayout");
        }
        public ActionResult Products()
        {


            return View("Products", "_clayout");
        }
        public ActionResult Salwars()
        {


            return View("Salwars", "_clayout");
        }
        public ActionResult Sandals()
        {


            return View("Sandals", "_clayout");
        }
        public ActionResult Sarees()
        {


            return View("Sarees", "_clayout");
        }
        public ActionResult Shirts()
        {


            return View("Shirts", "_clayout");
        }
        public ActionResult Skirts()
        {


            return View("Skirts", "_clayout");
        }
        public ActionResult Sweaters()
        {


            return View("Sweaters", "_clayout");
        }
        public ActionResult Single()
        {


            return View("Single", "_clayout");
        }

    }
}
