using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Trefoil.Helper;
using Trefoil.Library;
using Trefoil.Web.Filters;
using Trefoil.Web.Models;

namespace Trefoil.Web.Controllers
{
    [AuthorisedUser(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ITrefoilService _trefoilService;

        public AdminController(ITrefoilService trefoilService)
        {
            this._trefoilService = trefoilService;
        }

        public ActionResult Index(string expandMode)
        {
            return View();
        }

        public ActionResult BusinessStatus()
        {
            List<BusinessStatusModel> model = _trefoilService.GetBusinessStatusDtoList().MapObjects<BusinessStatusDto, BusinessStatusModel>().ToList();

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBusinessStatus(int? id, BusinessStatusModel model)
        {
            if (id.HasValue)
            {
                _trefoilService.UpdateBusinessStatusByBusinessStatusId((int)id, model.MapObject<BusinessStatusModel, BusinessStatusDto>());
            }

            return RedirectToAction("BusinessStatus");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertBusinessStatus(BusinessStatusModel model)
        {
            if (ModelState.IsValid)
            {
                _trefoilService.AddBusinessStatus(model.MapObject<BusinessStatusModel, BusinessStatusDto>());
            }

            return RedirectToAction("BusinessStatus");
        }

        public ActionResult BusinessType()
        {
            List<BusinessTypeModel> model = _trefoilService.GetBusinessTypeDtoList().MapObjects<BusinessTypeDto, BusinessTypeModel>().ToList();

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBusinessType(int? id, BusinessTypeModel model)
        {
            if (id.HasValue)
            {
                _trefoilService.UpdateBusinessTypeByBusinessTypeId((int)id, model.MapObject<BusinessTypeModel, BusinessTypeDto>());
            }

            return RedirectToAction("BusinessType");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertBusinessType(BusinessTypeModel model)
        {
            if (ModelState.IsValid)
            {
                _trefoilService.AddBusinessType(model.MapObject<BusinessTypeModel, BusinessTypeDto>());
            }

            return RedirectToAction("BusinessType");
        }

        public ActionResult BusinessFunction()
        {
            List<BusinessFunctionModel> model = _trefoilService.GetBusinessFunctionDtoList().MapObjects<BusinessFunctionDto, BusinessFunctionModel>().ToList();

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBusinessFunction(int? id, BusinessFunctionModel model)
        {
            if (id.HasValue)
            {
                _trefoilService.UpdateBusinessFunctionByBusinessFunctionId((int)id, model.MapObject<BusinessFunctionModel, BusinessFunctionDto>());
            }

            return RedirectToAction("BusinessFunction");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertBusinessFunction(BusinessFunctionModel model)
        {
            if (ModelState.IsValid)
            {
                _trefoilService.AddBusinessFunction(model.MapObject<BusinessFunctionModel, BusinessFunctionDto>());
            }

            return RedirectToAction("BusinessFunction");
        }
    }
}
