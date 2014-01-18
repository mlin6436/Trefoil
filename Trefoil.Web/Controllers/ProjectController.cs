using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Trefoil.Helper;
using Trefoil.Library;
using Trefoil.Web.Filters;
using Trefoil.Web.Helpers;
using Trefoil.Web.Models;

namespace Trefoil.Web.Controllers
{
    [AuthorisedUser(Roles = "Administrator,Account,Internal")]
    public class ProjectController : Controller
    {
        private readonly ITrefoilService _trefoilService;
        private readonly List<BusinessFunctionModel> _businessfunctions;
        private readonly List<BusinessValueModel> _businessvalues;
        private readonly List<OrganisationModel> _organisations;
        private readonly List<PriorityTypeModel> _prioritytypes;
        private readonly List<ProjectModel> _projects;
        private readonly List<StateTypeModel> _statetypes;
        private readonly List<StatusTypeModel> _statustypes;
        private readonly List<UserModel> _users;

        public ProjectController(ITrefoilService trefoilService)
        {
            _trefoilService = trefoilService;

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
            var model = new ProjectIndexModel
            {
                Projects = _trefoilService.GetProjectDtoList().MapObjects<ProjectDto, ProjectModel>().OrderBy(p => p.scheduledend).ToList()
            };
            model.Projects.ProjectLookup(_businessvalues, _organisations, _users, _prioritytypes, _statetypes, _statustypes);
            model.NewProject.ProjectLookup(_businessvalues, _organisations, _users, _prioritytypes, _statetypes, _statustypes);

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? pid, int? tab)
        {
            if (tab.HasValue)
            {
                ViewBag.TabIndex = tab;
            }

            if (!pid.HasValue)
            {
                return RedirectToAction("Index", "Project");
            }

            try
            {
                var model = new ProjectDetailsModel
                {
                    Project = _trefoilService.GetProjectDtoByProjectId((int)pid).MapObject<ProjectDto, ProjectModel>(),
                    Notes = _trefoilService.GetNoteDtoListByProjectId((int)pid).MapObjects<NoteDto, NoteModel>().ToList(),
                    Tasks = _trefoilService.GetTaskDtoListByProjectId((int)pid).MapObjects<TaskDto, TaskModel>().OrderBy(t => t.scheduledend).ToList(),
                };

                model.Project.ProjectLookup(_businessvalues, _organisations, _users, _prioritytypes, _statetypes, _statustypes);
                model.Tasks.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects);
                model.NewNote.projectid = pid;
                model.NewTask.projectid = pid;
                model.NewTask.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects, _organisations);
                if (model.Project.ownerid != null)
                {
                    model.Owner = _trefoilService.GetUserDtoByUserId((int) model.Project.ownerid).MapObject<UserDto, UserModel>();
                }
                model.Owner.UserBusinessFunction(_businessfunctions);
                if (model.Project.contactid != null)
                {
                    model.Contact = _trefoilService.GetUserDtoByUserId((int) model.Project.contactid).MapObject<UserDto, UserModel>();
                }
                model.Contact.UserBusinessFunction(_businessfunctions);

                return View(model);
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Project");
            }
        }

        [HttpPost]
        public ActionResult EditProject(ProjectModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Project");
            }

            try
            {
                int projectid = model.projectid;

                if (projectid == 0)
                {
                    projectid = _trefoilService.AddProject(model.MapObject<ProjectModel, ProjectDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateProjectByProjectId((int)projectid, null, model.MapObject<ProjectModel, ProjectDto>());
                }

                return RedirectToAction("Details", "Project", new { pid = projectid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Project");
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
                int? projectid = model.projectid;

                if (noteid == 0)
                {
                    // TODO: implement createdby
                    _trefoilService.AddNoteByProjectId((int)projectid, null, model.MapObject<NoteModel, NoteDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateNoteByNoteId(noteid, null, model.MapObject<NoteModel, NoteDto>());
                }

                return RedirectToAction("Details", "Project", new { pid = model.projectid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Project");
            }
        }

        [HttpPost]
        public ActionResult DeleteProject(int id)
        {
            _trefoilService.DeleteProjectByProjectId(id);

            return RedirectToAction("Index", "Project");
        }        
    }
}