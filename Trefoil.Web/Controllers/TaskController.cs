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
    public class TaskController : Controller
    {
        private readonly ITrefoilService _trefoilService;
        private readonly List<BusinessFunctionModel> _businessfunctions;
        private readonly List<UserModel> _internalusers;
        private readonly List<OrganisationModel> _organisations;
        private readonly List<PriorityTypeModel> _prioritytypes;
        private readonly List<ProjectModel> _projects;
        private readonly List<StateTypeModel> _statetypes;
        private readonly List<StatusTypeModel> _statustypes;
        private readonly List<UserModel> _users;

        private string history = String.Empty;

        public TaskController(ITrefoilService trefoilService)
        {
            _trefoilService = trefoilService;

            _businessfunctions = _trefoilService.GetBusinessFunctionDtoList().MapObjects<BusinessFunctionDto, BusinessFunctionModel>().ToList();
            _internalusers = _trefoilService.GetUserDtoListByInternal().MapObjects<UserDto, UserModel>().ToList();
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
            var model = new TaskIndexModel
            {
                Tasks = _trefoilService.GetTaskDtoList().MapObjects<TaskDto, TaskModel>().OrderBy(t => t.scheduledend).ToList()
            };
            model.Tasks.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects);
            model.NewTask.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects, _organisations);

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? tid)
        {
            if (!tid.HasValue)
            {
                return RedirectToAction("Index", "Task");
            }

            try
            {
                var model = new TaskDetailsModel
                {
                    Task = _trefoilService.GetTaskDtoByTaskId((int)tid).MapObject<TaskDto, TaskModel>(),
                    NewTask = _trefoilService.GetTaskDtoByTaskId((int)tid).MapObject<TaskDto, TaskModel>(),
                    Notes = _trefoilService.GetNoteDtoListByTaskId((int)tid).MapObjects<NoteDto, NoteModel>().ToList(),
                    AssignedUsers = _trefoilService.GetUserDtoListByTaskId((int)tid).MapObjects<UserDto, UserModel>().ToList(),
                };
                model.Task.TaskLookup(_prioritytypes, _statetypes, _statustypes, _users, _projects, _organisations);
                model.NewTask.TaskLookup(_prioritytypes, _statetypes, _statustypes, _internalusers, _projects, _organisations);
                model.NewNote.taskid = tid;
                if (model.Task.ownerid != null)
                {
                    model.Owner = _trefoilService.GetUserDtoByUserId((int)model.Task.ownerid).MapObject<UserDto, UserModel>();
                }
                model.Owner.UserBusinessFunction(_businessfunctions);

                return View(model);
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Task");
            }
        }

        [HttpPost]
        public ActionResult Details(TaskModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Task");
            }

            int? taskid = model.taskid;
            if (model.taskid == 0)
            {
                _trefoilService.AddTask(model.MapObject<TaskModel, TaskDto>());
            }
            else
            {
                // TODO: update modifiedby.
                _trefoilService.UpdateTaskByTaskId((int)taskid, null, model.MapObject<TaskModel, TaskDto>());
            }

            return RedirectToAction("Index", "Task");
        }

        [HttpPost]
        public  ActionResult EditTask(TaskModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Task");
            }

            try
            {
                int taskid = model.taskid;

                if (taskid == 0)
                {
                    taskid = _trefoilService.AddTask(model.MapObject<TaskModel, TaskDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateTaskByTaskId(taskid, null, model.MapObject<TaskModel, TaskDto>());
                }

                return RedirectToAction("Details", "Task", new { tid = taskid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Task");
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
                int? taskid = model.taskid;

                if (noteid == 0)
                {
                    // TODO: implement createdby
                    _trefoilService.AddNoteByTaskId((int)taskid, null, model.MapObject<NoteModel, NoteDto>());
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.UpdateNoteByNoteId(noteid, null, model.MapObject<NoteModel, NoteDto>());
                }

                return RedirectToAction("Details", "Task", new { tid = model.taskid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Task");
            }
        }

        [HttpPost]
        public ActionResult DeleteTask(int id)
        {
            _trefoilService.DeleteTaskByTaskId(id);

            return RedirectToAction("Index", "Task");
        }

        [HttpPost]
        public ActionResult AssignTask(TaskModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return RedirectToAction("Index", "Task");
            }

            try
            {
                int taskid = model.taskid;

                if (taskid == 0)
                {
                    return RedirectToAction("Index", "Task");
                }
                else
                {
                    // TODO: implement modifiedby
                    _trefoilService.AddTaskAssignment(taskid, (int)model.ownerid);
                }

                return RedirectToAction("Details", "Task", new { tid = taskid });
            }
            catch (Exception ex)
            {
                // TODO: output exception.
                return RedirectToAction("Index", "Task");
            }
        }
    }
}
