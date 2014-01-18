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
    [AuthorisedUser(Roles = "Administrator,Account,Internal,Client")]
    public class ContactController : Controller
    {
        private readonly ITrefoilService _trefoilService;

        public ContactController(ITrefoilService trefoilService)
        {
            _trefoilService = trefoilService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var models = new List<UserOrganisationModel>();

            var userDtos = _trefoilService.GetUserDtoList();

            foreach (var userDto in userDtos)
            {
                models.Add(new UserOrganisationModel()
                {
                    UserModel = userDto.MapObject<UserDto, UserModel>(),
                    OrganisationModel = userDto.organisationid.HasValue ? _trefoilService.GetOrganisationDtoByOrganisationId((int)userDto.organisationid).MapObject<OrganisationDto, OrganisationModel>() : null,
                });
            }

            return View(models);
        }

        [HttpGet]
        public ActionResult Details(int? systemuserid)
        {
            if (!systemuserid.HasValue)
            {
                return RedirectToAction("Index", "Contact");
            }

            var model = _trefoilService.GetUserDtoByUserId((int)systemuserid).MapObject<UserDto, UserModel>();

            return View(model);
        }
    }
}
