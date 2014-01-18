using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Trefoil.Entity;
using Trefoil.Helper;
using Trefoil.Library;

namespace Trefoil.Service
{
    public class TrefoilService : ITrefoilService
    {
        private readonly TrefoilEntity _trefoilEntity;

        public TrefoilService(TrefoilEntity trefoilEntity)
        {
            _trefoilEntity = trefoilEntity;
        }

        public void AddBusinessFunction(BusinessFunctionDto dto)
        {
            if (dto.businessfunctionid == 0 || _trefoilEntity.BusinessFunction.Any(b => b.businessfunctionid == dto.businessfunctionid))
            {
                dto.businessfunctionid = _trefoilEntity.BusinessFunction.Max(b => b.businessfunctionid) + 1;
            }
            dto.createdon = DateTime.UtcNow;
            _trefoilEntity.BusinessFunction.Add(dto.MapObject<BusinessFunctionDto, BusinessFunction>());
            _trefoilEntity.SaveChanges();
        }

        public void AddBusinessStatus(BusinessStatusDto dto)
        {
            if (dto.businessstatusid == 0 || _trefoilEntity.BusinessStatus.Any(b => b.businessstatusid == dto.businessstatusid))
            {
                dto.businessstatusid = _trefoilEntity.BusinessStatus.Max(b => b.businessstatusid) + 1;
            }
            dto.createdon = DateTime.UtcNow;
            _trefoilEntity.BusinessStatus.Add(dto.MapObject<BusinessStatusDto, BusinessStatus>());
            _trefoilEntity.SaveChanges();
        }

        public void AddBusinessType(BusinessTypeDto dto)
        {
            if (dto.businesstypeid == 0 || _trefoilEntity.BusinessType.Any(b => b.businesstypeid == dto.businesstypeid))
            {
                dto.businesstypeid = _trefoilEntity.BusinessType.Max(b => b.businesstypeid) + 1;
            }
            dto.createdon = DateTime.UtcNow;
            _trefoilEntity.BusinessType.Add(dto.MapObject<BusinessTypeDto, BusinessType>());
            _trefoilEntity.SaveChanges();
        }

        public int AddNoteByOrganisationId(int organisationid, int? createdby, NoteDto dto)
        {
            var note = dto.MapObject<NoteDto, Note>();

            note.description = dto.description;
            note.createdby = dto.createdby;
            note.createdon = DateTime.UtcNow;

            _trefoilEntity.Note.Add(note);
            _trefoilEntity.SaveChanges();

            var organisation = _trefoilEntity.Organisation.SingleOrDefault(o => o.organisationid == organisationid);
            organisation.Notes = new Collection<Note>();
            organisation.Notes.Add(note);
            _trefoilEntity.SaveChanges();

            return note.noteid;
        }

        public int AddNoteByProjectId(int projectid, int? createdby, NoteDto dto)
        {
            var note = dto.MapObject<NoteDto, Note>();

            note.description = dto.description;
            note.createdby = dto.createdby;
            note.createdon = DateTime.UtcNow;

            _trefoilEntity.Note.Add(note);
            _trefoilEntity.SaveChanges();

            var project = _trefoilEntity.Project.SingleOrDefault(p => p.projectid == projectid);
            project.Notes = new Collection<Note>();
            project.Notes.Add(note);
            _trefoilEntity.SaveChanges();

            return note.noteid;
        }

        public int AddNoteByTaskId(int taskid, int? createdby, NoteDto dto)
        {
            var note = dto.MapObject<NoteDto, Note>();

            note.description = dto.description;
            note.createdby = dto.createdby;
            note.createdon = DateTime.UtcNow;

            _trefoilEntity.Note.Add(note);
            _trefoilEntity.SaveChanges();

            var task = _trefoilEntity.Task.SingleOrDefault(t => t.taskid == taskid);
            task.Notes = new Collection<Note>();
            task.Notes.Add(note);
            _trefoilEntity.SaveChanges();

            return note.noteid;
        }

        public int AddOrganisation(OrganisationDto dto)
        {
            var organisation = dto.MapObject<OrganisationDto, Organisation>();
            organisation.createdon = DateTime.UtcNow;

            _trefoilEntity.Organisation.Add(organisation);
            _trefoilEntity.SaveChanges();

            return organisation.organisationid;
        }

        public int AddProject(ProjectDto dto)
        {
            var project = dto.MapObject<ProjectDto, Project>();
            project.createdon = DateTime.UtcNow;
            project.isbilled = false;

            _trefoilEntity.Project.Add(project);
            _trefoilEntity.SaveChanges();

            return project.projectid;
        }

        public int AddTask(TaskDto dto)
        {
            var task = dto.MapObject<TaskDto, Task>();
            task.createdon = DateTime.UtcNow;

            _trefoilEntity.Task.Add(task);
            _trefoilEntity.SaveChanges();

            return task.taskid;
        }

        public void AddTaskAssignment(int taskid, int userid)
        {
            var user = _trefoilEntity.User.SingleOrDefault(u => u.systemuserid == userid);
            var task = _trefoilEntity.Task.SingleOrDefault(t => t.taskid == taskid);
            task.Users = new Collection<User>();
            task.Users.Add(user);
            
            _trefoilEntity.SaveChanges();
        }

        public int AddUser(int organisationid, UserDto dto)
        {
            var user = dto.MapObject<UserDto, User>();
            user.createdby = 1; // TODO:
            user.createdon = DateTime.UtcNow;
            user.fullname = (user.firstname + " " + user.lastname);
            user.organisationid = organisationid;
            user.parentsystemuserid = 1; // TODO:
            user.roletypeid = 1; // TODO:

            _trefoilEntity.User.Add(user);
            _trefoilEntity.SaveChanges();

            return user.systemuserid;
        }

        public void DeleteOrganisationByOrganisationId(int organisationid)
        {
            var organisation = _trefoilEntity.Organisation.FirstOrDefault(o => o.organisationid == organisationid);
            if (organisation != null)
            {
                organisation.isdisabled = true;
            }
            var projects = _trefoilEntity.Project.Where(p => p.organisationid == organisationid).ToList();
            projects.ForEach(p => p.isdisabled = true);
            var projectsid = projects.Select(p => p.projectid).ToList();
            var tasks = _trefoilEntity.Task.Where(t => projectsid.Contains((int)t.projectid)).ToList();
            tasks.ForEach(t => t.isdisabled = true);
            _trefoilEntity.SaveChanges();
        }

        public void DeleteProjectByProjectId(int projectid)
        {
            var project = _trefoilEntity.Project.FirstOrDefault(p => p.projectid == projectid);
            if (project != null)
            {
                project.isdisabled = true;
            }
            var tasks = _trefoilEntity.Task.Where(t => t.projectid == projectid).ToList();
            tasks.ForEach(t => t.isdisabled = true);
            _trefoilEntity.SaveChanges();
        }

        public void DeleteTaskByTaskId(int taskid)
        {
            var task = _trefoilEntity.Task.FirstOrDefault(t => t.taskid == taskid);
            if (task != null)
            {
                task.isdisabled = true;
            }
            _trefoilEntity.SaveChanges();
        }

        public IEnumerable<BusinessFunctionDto> GetBusinessFunctionDtoList()
        {
            return _trefoilEntity.BusinessFunction.MapObjects<BusinessFunction, BusinessFunctionDto>();
        }

        public IEnumerable<BusinessStatusDto> GetBusinessStatusDtoList()
        {
            return _trefoilEntity.BusinessStatus.MapObjects<BusinessStatus, BusinessStatusDto>();
        }

        public IEnumerable<BusinessTypeDto> GetBusinessTypeDtoList()
        {
            return _trefoilEntity.BusinessType.MapObjects<BusinessType, BusinessTypeDto>();
        }

        public IEnumerable<BusinessValueDto> GetBusinessValueDtoList()
        {
            return _trefoilEntity.BusinessValue.MapObjects<BusinessValue, BusinessValueDto>();
        }

        public IEnumerable<NoteDto> GetNoteDtoListByOrganisationId(int organisationid)
        {
            return _trefoilEntity.Note.Where(n => n.Organisations.Any(o => o.organisationid == organisationid)).MapObjects<Note, NoteDto>();
        }

        public IEnumerable<NoteDto> GetNoteDtoListByProjectId(int projectid)
        {
            return _trefoilEntity.Note.Where(n => n.Projects.Any(p => p.projectid == projectid)).MapObjects<Note, NoteDto>();
        }

        public IEnumerable<NoteDto> GetNoteDtoListByTaskId(int taskid)
        {
            return _trefoilEntity.Note.Where(n => n.Tasks.Any(t => t.taskid == taskid)).MapObjects<Note, NoteDto>();
        }

        public OrganisationDto GetOrganisationDtoByOrganisationId(int organisationid)
        { 
            return _trefoilEntity.Organisation.SingleOrDefault(o => o.organisationid == organisationid && o.isdisabled == false).MapObject<Organisation, OrganisationDto>();
        }

        public IEnumerable<OrganisationDto> GetOrganisationDtoList()
        {
            return _trefoilEntity.Organisation.Where(o => o.isdisabled == false).MapObjects<Organisation, OrganisationDto>();
        }

        public IEnumerable<PriorityTypeDto> GetPriorityTypeDtoList()
        {
            return _trefoilEntity.PriorityType.MapObjects<PriorityType, PriorityTypeDto>();
        }
        
        public ProjectDto GetProjectDtoByProjectId(int projectid)
        {
            return _trefoilEntity.Project.SingleOrDefault(p => p.projectid == projectid && p.isdisabled == false).MapObject<Project, ProjectDto>();
        }

        public IEnumerable<ProjectDto> GetProjectDtoList()
        {
            return _trefoilEntity.Project.Where(p => p.isdisabled == false).MapObjects<Project, ProjectDto>();
        }

        public IEnumerable<ProjectDto> GetProjectDtoListByOrganisationId(int organisationid)
        {
            return _trefoilEntity.Project.Where(p => p.organisationid == organisationid).MapObjects<Project, ProjectDto>();
        }

        public IEnumerable<StateTypeDto> GetStateTypeDtoList()
        {
            return _trefoilEntity.StateType.MapObjects<StateType, StateTypeDto>();
        }

        public IEnumerable<StatusTypeDto> GetStatusTypeDtoList()
        {
            return _trefoilEntity.StatusType.MapObjects<StatusType, StatusTypeDto>();
        }

        public TaskDto GetTaskDtoByTaskId(int taskid)
        {
            return _trefoilEntity.Task.SingleOrDefault(t => t.taskid == taskid && t.isdisabled == false).MapObject<Task, TaskDto>();
        }

        public IEnumerable<TaskDto> GetTaskDtoList()
        {
            return _trefoilEntity.Task.Where(t => t.isdisabled == false).MapObjects<Task, TaskDto>();
        }

        public IEnumerable<TaskDto> GetTaskDtoListByOrganisationId(int organisationid)
        {
            return _trefoilEntity.Task.Where(t => t.organisationid == organisationid && t.isdisabled == false).MapObjects<Task, TaskDto>();
        }

        public IEnumerable<TaskDto> GetTaskDtoListByProjectId(int projectid)
        {
            return _trefoilEntity.Task.Where(t => t.projectid == projectid && t.isdisabled == false).MapObjects<Task, TaskDto>();
        }

        public UserDto GetUserDtoByEmail(string emailaddress)
        {
            return _trefoilEntity.User.SingleOrDefault(u => u.emailaddress.Equals(emailaddress) && u.isdisabled == false).MapObject<User, UserDto>();
        }

        public UserDto GetUserDtoByUserId(int userid)
        {
            return _trefoilEntity.User.SingleOrDefault(u => u.systemuserid == userid && u.isdisabled == false).MapObject<User, UserDto>();
        }

        public IEnumerable<UserDto> GetUserDtoList()
        {
            return _trefoilEntity.User.Where(u => u.isdisabled == false).MapObjects<User, UserDto>();
        }

        public IEnumerable<UserDto> GetUserDtoListByInternal()
        {
            return _trefoilEntity.User.Where(u => u.isinternal == true && u.isdisabled == false).MapObjects<User, UserDto>();
        }

        public IEnumerable<UserDto> GetUserDtoListByOrganisationId(int organisationid)
        {
            return _trefoilEntity.User.Where(u => u.organisationid == organisationid && u.isdisabled == false).MapObjects<User, UserDto>();
        }

        public IEnumerable<UserDto> GetUserDtoListByTaskId(int taskid)
        {
            return _trefoilEntity.User.Where(u => u.Tasks.Any(t => t.taskid == taskid) && u.isdisabled == false).MapObjects<User, UserDto>();
        }

        public void UpdateBusinessFunctionByBusinessFunctionId(int businessfunctionid, BusinessFunctionDto dto)
        {
            var businessfunction = _trefoilEntity.BusinessFunction.SingleOrDefault(b => b.businessfunctionid == businessfunctionid);
            if (businessfunction != null)
            {
                businessfunction.name = dto.name;
                businessfunction.isdisabled = dto.isdisabled;
                businessfunction.modifiedon = DateTime.UtcNow;
            }
            _trefoilEntity.SaveChanges();
        }

        public void UpdateBusinessStatusByBusinessStatusId(int businessstatusid, BusinessStatusDto dto)
        {
            var businessstatus = _trefoilEntity.BusinessStatus.SingleOrDefault(b => b.businessstatusid == businessstatusid);
            if (businessstatus != null)
            {
                businessstatus.name = dto.name;
                businessstatus.isdisabled = dto.isdisabled;
                businessstatus.modifiedon = DateTime.UtcNow;
            }
            _trefoilEntity.SaveChanges();
        }

        public void UpdateBusinessTypeByBusinessTypeId(int businesstypeid, BusinessTypeDto dto)
        {
            var businesstype = _trefoilEntity.BusinessType.SingleOrDefault(b => b.businesstypeid == businesstypeid);
            if (businesstype != null)
            {
                businesstype.name = dto.name;
                businesstype.isdisabled = dto.isdisabled;

                businesstype.modifiedon = DateTime.UtcNow;
            }
            _trefoilEntity.SaveChanges();
        }

        public void UpdateNoteByNoteId(int noteid, int? modifiedby, NoteDto dto)
        {
            var note = _trefoilEntity.Note.SingleOrDefault(n => n.noteid == noteid);

            if (note != null)
            {
                note.description = dto.description;

                note.modifiedby = modifiedby;
                note.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }
        
        public void UpdateOrganisationByOrganisationId(int organisationid, int? modifiedby, OrganisationDto dto)
        {
            var organisation = _trefoilEntity.Organisation.SingleOrDefault(o => o.organisationid == organisationid);

            if (organisation != null)
            {
                organisation.address1_city = dto.address1_city;
                organisation.address1_country = dto.address1_country;
                organisation.address1_county = dto.address1_county;
                organisation.address1_fax = dto.address1_fax;
                organisation.address1_line1 = dto.address1_line1;
                organisation.address1_line2 = dto.address1_line2;
                organisation.address1_line3 = dto.address1_line3;
                organisation.address1_name = dto.address1_name;
                organisation.address1_postalcode = dto.address1_postalcode;
                organisation.address1_postofficebox = dto.address1_postofficebox;
                organisation.address1_telephone1 = dto.address1_telephone1;
                organisation.address1_telephone2 = dto.address1_telephone2;
                organisation.address1_telephone3 = dto.address1_telephone3;
                organisation.businessstatusid = dto.businessstatusid;
                organisation.businesstypeid = dto.businesstypeid;
                organisation.name = dto.name;
                organisation.social_facebook = dto.social_facebook;
                organisation.social_linkedin = dto.social_linkedin;
                organisation.social_twitter = dto.social_twitter;
                organisation.social_vimeo = dto.social_vimeo;
                organisation.social_youtube = dto.social_youtube;
                organisation.website = dto.website;

                organisation.modifiedby = modifiedby;
                organisation.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }

        // TODO: not used.
        public void UpdateProjectActualTimeSpentByProjectId(int projectid, int? modifiedby, DateTime? actualstart, DateTime? actualend, int? actualdurationminutes)
        {
            var project = _trefoilEntity.Project.SingleOrDefault(p => p.projectid == projectid);

            if (project != null)
            {
                project.actualdurationminutes = actualdurationminutes;
                project.actualend = actualend;
                project.actualstart = actualstart;

                project.modifiedby = modifiedby;
                project.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }

        public void UpdateProjectByProjectId(int projectid, int? modifiedby, ProjectDto dto)
        {
            var project = _trefoilEntity.Project.SingleOrDefault(p => p.projectid == projectid);

            if (project != null)
            {
                project.budget = dto.budget;
                project.businessvalueid = dto.businessvalueid;
                project.contactid = dto.contactid;
                project.description = dto.description;
                project.isbilled = dto.isbilled;
                project.organisationid = dto.organisationid;
                project.ownerid = dto.ownerid;
                project.prioritytypeid = dto.prioritytypeid;
                project.scheduleddurationminutes = dto.scheduleddurationminutes;
                project.scheduledend = dto.scheduledend;
                project.scheduledstart = dto.scheduledstart;
                project.statetypeid = dto.statetypeid;
                project.statustypeid = dto.statustypeid;
                project.subject = dto.subject;
                project.totalcharge = dto.totalcharge;
                project.totalcost = dto.totalcost;

                project.modifiedby = modifiedby;
                project.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }

        // TODO: not used.
        public void UpdateProjectStatusAsCancelledByProjectId(int projectid, int? modifiedby, string cancelledreason)
        {
            var project = _trefoilEntity.Project.SingleOrDefault(p => p.projectid == projectid);

            if (project != null)
            {
                project.cancelledreason = cancelledreason;
                project.statetypeid = _trefoilEntity.StatusType.SingleOrDefault(s => s.name.Equals("Cancelled")).statustypeid;

                project.modifiedby = modifiedby;
                project.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }

        // TODO: not used.
        public void UpdateProjectStatusAsRejectedByProjectId(int projectid, int? modifiedby, string rejectedreason)
        {
            var project = _trefoilEntity.Project.SingleOrDefault(p => p.projectid == projectid);

            if (project != null)
            {
                project.rejectedreason = rejectedreason;
                project.statetypeid = _trefoilEntity.StatusType.SingleOrDefault(s => s.name.Equals("Rejected")).statustypeid;

                project.modifiedby = modifiedby;
                project.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }

        // TODO: not used.
        public void UpdateTaskActualTimeSpentByTaskId(int taskid, int? modifiedby, DateTime? actualstart, DateTime? actualend, int? actualdurationminutes)
        {
            var task = _trefoilEntity.Task.SingleOrDefault(t => t.taskid == taskid);

            if (task != null)
            {
                task.actualdurationminutes = actualdurationminutes;
                task.actualend = actualend;
                task.actualstart = actualstart;

                task.modifiedon = DateTime.UtcNow;
                task.modifiedby = modifiedby;
            }

            _trefoilEntity.SaveChanges();
        }

        public void UpdateTaskByTaskId(int taskid, int? modifiedby, TaskDto dto)
        {
            var task = _trefoilEntity.Task.SingleOrDefault(t => t.taskid == taskid);

            if (task != null)
            {
                task.description = dto.description;
                task.ownerid = dto.ownerid;
                task.prioritytypeid = dto.prioritytypeid;
                task.projectid = dto.projectid;
                task.scheduleddurationminutes = dto.scheduleddurationminutes;
                task.scheduledend = dto.scheduledend;
                task.scheduledstart = dto.scheduledstart;
                task.statetypeid = dto.statetypeid;
                task.statustypeid = dto.statustypeid;
                task.subject = dto.subject;

                task.modifiedon = DateTime.UtcNow;
                task.modifiedby = modifiedby;
            }

            _trefoilEntity.SaveChanges();
        }

        public void UpdateUserByUserId(int systemuserid, int? modifiedby, UserDto dto)
        {
            var user = _trefoilEntity.User.SingleOrDefault(u => u.systemuserid == systemuserid);

            if (user != null)
            {
                user.address1_city = dto.address1_city;
                user.address1_country = dto.address1_country;
                user.address1_county = dto.address1_county;
                user.address1_fax = dto.address1_fax;
                user.address1_line1 = dto.address1_line1;
                user.address1_line2 = dto.address1_line2;
                user.address1_line3 = dto.address1_line3;
                user.address1_name = dto.address1_name;
                user.address1_postalcode = dto.address1_postalcode;
                user.address1_postofficebox = dto.address1_postofficebox;
                user.address1_telephone1 = dto.address1_telephone1;
                user.address1_telephone2 = dto.address1_telephone2;
                user.address1_telephone3 = dto.address1_telephone3;
                user.businessfunctionid = dto.businessfunctionid;
                user.emailaddress = dto.emailaddress;
                user.firstname = dto.firstname;
                user.fullname = (dto.firstname + " " + dto.lastname);
                user.jobtitle = dto.jobtitle;
                user.lastname = dto.lastname;
                user.middlename = dto.middlename;
                user.mobilephone = dto.mobilephone;
                user.nickname = dto.nickname;
                user.social_facebook = dto.social_facebook;
                user.social_linkedin = dto.social_linkedin;
                user.social_twitter = dto.social_twitter;
                user.social_vimeo = dto.social_vimeo;
                user.social_youtube = dto.social_youtube;
                user.title = dto.title;
                user.website = dto.website;
                user.isinternal = dto.isinternal;

                user.modifiedby = modifiedby;
                user.modifiedon = DateTime.UtcNow;
            }

            _trefoilEntity.SaveChanges();
        }

        public bool ValidateLogin(UserDto dto)
        {
            var user = _trefoilEntity.User.FirstOrDefault(u => u.emailaddress == dto.emailaddress);

            if (user != null)
            {
                if (user.password.Equals(dto.password))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
