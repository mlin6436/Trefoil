using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Helper;
using Trefoil.Library;
using Trefoil.Web.Filters;
using Trefoil.Web.Models;

namespace Trefoil.Web.Controllers
{
    [AuthorisedUser(Roles = "Administrator,Account,Internal")]
    public class DashboardController : Controller
    {
        private ITrefoilService _trefoilService;

        public DashboardController(ITrefoilService trefoilService)
        {
            _trefoilService = trefoilService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
