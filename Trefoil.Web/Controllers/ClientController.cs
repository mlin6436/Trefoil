using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Helper;
using Trefoil.Library;
using Trefoil.Web.Filters;
using Trefoil.Web.Helpers;
using Trefoil.Web.Models;

namespace Trefoil.Web.Controllers
{
    [AuthorisedUser(Roles = "Administrator,Account,Internal")]
    public class ClientController : Controller
    {
        private readonly ITrefoilService _trefoilService;
        private readonly List<BusinessFunctionModel> _businessfunctions;
        private readonly List<BusinessStatusModel> _businessstatus;
        private readonly List<BusinessTypeModel> _businesstypes;
        private readonly List<BusinessValueModel> _businessvalues;
        private readonly List<OrganisationModel> _organisations;
        private readonly List<PriorityTypeModel> _prioritytypes;
        private readonly List<ProjectModel> _projects;
        private readonly List<StateTypeModel> _statetypes;
        private readonly List<StatusTypeModel> _statustypes;
        private readonly List<UserModel> _users;

        public ClientController(ITrefoilService trefoilService)
        {
            _trefoilService = trefoilService;

            _businessfunctions = _trefoilService.GetBusinessFunctionDtoList().MapObjects<BusinessFunctionDto, BusinessFunctionModel>().ToList();
            _businessstatus = _trefoilService.GetBusinessStatusDtoList().MapObjects<BusinessStatusDto, BusinessStatusModel>().ToList();
            _businesstypes = _trefoilService.GetBusinessTypeDtoList().MapObjects<BusinessTypeDto, BusinessTypeModel>().ToList();
            _businessfunctions = _trefoilService.GetBusinessFunctionDtoList().MapObjects<BusinessFunctionDto, BusinessFunctionModel>().ToList();
            _businessvalues = _trefoilService.GetBusinessValueDtoList().MapObjects<BusinessValueDto, BusinessValueModel>().ToList();
            _organisations = _trefoilService.GetOrganisationDtoList().MapObjects<OrganisationDto, OrganisationModel>().ToList();
            _prioritytypes = _trefoilService.GetPriorityTypeDtoList().MapObjects<PriorityTypeDto, PriorityTypeModel>().ToList();
            _projects = _trefoilService.GetProjectDtoList().MapObjects<ProjectDto, ProjectModel>().ToList();
            _statetypes = _trefoilService.GetStateTypeDtoList().MapObjects<StateTypeDto, StateTypeModel>().ToList();
            _statustypes = _trefoilService.GetStatusTypeDtoList().MapObjects<StatusTypeDto, StatusTypeModel>().ToList();
            _users = _trefoilService.GetUserDtoList().MapObjects<UserDto, UserModel>().ToList();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new OrganisationIndexModel
            {
                Organisations = _trefoilService.GetOrganisationDtoList().MapObjects<OrganisationDto, OrganisationModel>().ToList()
            };
            model.Organisations.OrganisationLookup(_businessstatus, _businesstypes);
            model.NewOrganisation.OrganisationLookup(_businessstatus, _businesstypes);

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? cid, int? tab)
        {
            if (tab.HasValue)
            {
                ViewBag.TabIndex = tab;
            }

            if (!cid.HasValue)
            {
                return RedirectToAction("Index", "Client");
            }

            var model = new OrganisationDetailsModel
            {
                Organisation = _trefoilService.GetOrganisationDtoByOrganisationId((int)cid).MapObject<OrganisationDto, OrganisationModel>(),
                Notes = _trefoilService.GetNoteDtoListByOrganisationId((int)cid).MapObjects<NoteDto, NoteModel>().ToList(),
                Projects = _trefoilService.GetProjectDtoListByOrganisationId((int)cid).MapObjects<ProjectDto, ProjectModel>().ToList(),
                Tasks = _trefoilService.GetTaskDtoListByOrganisationId((int)cid).MapObjects<TaskDto, TaskModel>().ToList(),
                Contacts = _trefoilService.GetUserDtoListByOrganisationId((int)cid).MapObjects<UserDto, UserModel>().ToList(),
            };
            model.Organisation.OrganisationLookup(_businessstatus, _businesstypes);
            model.Projects.ProjectLookup(_businessvalues, _organisations, _users, _prioritytypes, _statetypes, _statustypes);
            model.Tasks.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects, _organisations);
            model.Contacts.UserBusinessFunction(_businessfunctions);
            model.NewNote.organisationid = cid;
            model.NewProject.organisationid = cid;
            model.NewProject.ProjectLookup(_businessvalues, _organisations, _users, _prioritytypes, _statetypes, _statustypes);
            model.NewTask.organisationid = cid;
            model.NewTask.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects, _organisations);
            model.NewContact.organisationid = cid;
            model.NewContact.UserBusinessFunction(_businessfunctions);

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            _trefoilService.DeleteOrganisationByOrganisationId(id);

            return RedirectToAction("Index", "Client");
        }

        [HttpPost]
        public ActionResult EditClient(OrganisationModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Client");
            }

            try
            {
                int? organisationid = model.organisationid;
                if (organisationid == 0)
                {
                    organisationid = _trefoilService.AddOrganisation(model.MapObject<OrganisationModel, OrganisationDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateOrganisationByOrganisationId(model.organisationid, null, model.MapObject<OrganisationModel, OrganisationDto>());
                }

                return RedirectToAction("Details", "Client", new { cid = organisationid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Client");
            }
        }

        [HttpPost]
        public ActionResult EditContact(UserModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Client");
            }

            try
            {
                int userid = model.systemuserid;
                int? organisationid = model.organisationid;

                if (userid == 0)
                {
                    _trefoilService.AddUser((int)organisationid, model.MapObject<UserModel, UserDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateUserByUserId(userid, null, model.MapObject<UserModel, UserDto>());
                }

                return RedirectToAction("Details", "Client", new { cid = model.organisationid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Client");
            }
        }

        [HttpPost]
        public ActionResult EditNote(NoteModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Client");
            }

            try
            {
                int noteid = model.noteid;
                int? organisationid = model.organisationid;

                if (noteid == 0)
                {
                    // TODO: implement createdby
                    _trefoilService.AddNoteByOrganisationId((int)organisationid, null, model.MapObject<NoteModel, NoteDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateNoteByNoteId(noteid, null, model.MapObject<NoteModel, NoteDto>());
                }

                return RedirectToAction("Details", "Client", new { cid = model.organisationid });
            }
            catch (Exception ex)
            { 
                // TODO: output exception.
                return RedirectToAction("Index", "Client");
            }
        }
    }
}
