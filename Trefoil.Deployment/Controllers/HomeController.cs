using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Entity;
using Trefoil.Library;
using Trefoil.Service;

namespace Trefoil.Deployment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string message = string.Empty;

            try
            {
                TrefoilEntity entity = new TrefoilEntity();
                entity.DropAndCreate(ConfigurationManager.AppSettings["DataSource"],
                    ConfigurationManager.AppSettings["DatabaseName"],
                    ConfigurationManager.AppSettings["UserId"],
                    ConfigurationManager.AppSettings["Password"]);

                ITrefoilService service = new TrefoilService(entity);
                message = "Done!";
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
            }

            //service.AddLog(new LogDto() { Level = "A", Description = "TEST", Application = "APP" });
            ViewBag.Message = message;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
