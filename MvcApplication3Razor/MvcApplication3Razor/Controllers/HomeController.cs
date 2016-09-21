using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3Razor.Models;

namespace MvcApplication3Razor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            ViewBag.MyText = "Este texto vem do controller";            

            return View(); 
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult IndexRazor()
        {
            return View();
        }


        [HttpPost]
        public ActionResult IndexRazor(testeRazor model, string returnUrl)
        {
            for (int i = 0; i < 3; i++)
            {
                model.UserName = "Teste UserName";
                model.Password = "Teste Password";
            }            

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
