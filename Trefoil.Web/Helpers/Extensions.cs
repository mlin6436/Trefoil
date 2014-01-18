using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Web.Models;

namespace Trefoil.Web.Helpers
{
    public static class Extensions
    {
        #region Cookie

        public static void CreateUserCookie(this Controller controller, LoginModel model)
        {
            HttpCookie cookie = new HttpCookie("user", model.emailaddress);

            if (model.rememberme)
            {
                cookie.Expires = DateTime.UtcNow.AddYears(100);
            }
            else
            {
                cookie.Expires = DateTime.UtcNow.AddHours(1);
            }

            controller.Response.Cookies.Add(cookie);
        }

        public static void RemoveUserCookie(this Controller controller)
        {
            if (controller.Request.Cookies.AllKeys.Contains("user"))
            {
                HttpCookie cookie = controller.HttpContext.Request.Cookies["user"];
                cookie.Expires = DateTime.UtcNow.AddDays(-1);
                controller.Response.Cookies.Add(cookie);
            }
        }

        #endregion

        #region Login Session

        public static void CreateUserCredential(this LoginModel model)
        {
            if (model != null)
            {
                System.Web.HttpContext.Current.Session["UserCredential"] = model;
            }
        }

        public static LoginModel GetUserCredential()
        {
            return (LoginModel)System.Web.HttpContext.Current.Session["UserCredential"] ?? null;
        }

        public static void RemoveUserCredential()
        {
            System.Web.HttpContext.Current.Session.Remove("UserCredential");
        }

        #endregion

        #region Organisation Lookup

        public static OrganisationModel OrganisationLookup(this OrganisationModel model, List<BusinessStatusModel> businessStatusLookup, List<BusinessTypeModel> businessTypeLookup)
        {
            model.OrganisationBusinessStatus(businessStatusLookup);
            model.OrganisationBusinessType(businessTypeLookup);

            return model;
        }

        public static List<OrganisationModel> OrganisationLookup(this List<OrganisationModel> models, List<BusinessStatusModel> businessStatusLookup, List<BusinessTypeModel> businessTypeLookup)
        {
            foreach (var model in models)
            {
                model.OrganisationLookup(businessStatusLookup, businessTypeLookup);
            }

            return models;
        }

        public static OrganisationModel OrganisationBusinessStatus(this OrganisationModel model, List<BusinessStatusModel> lookup)
        {
            model.BusinessStatusCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.businessstatusid == b.businessstatusid),
                Text = b.name,
                Value = b.businessstatusid.ToString(),
            });
            model.businessstatusname = lookup.SingleOrDefault(b => b.businessstatusid == model.businessstatusid).name;

            return model;
        }

        public static List<OrganisationModel> OrganisationBusinessStatus(this List<OrganisationModel> models, List<BusinessStatusModel> lookup)
        {
            foreach (var model in models)
            {
                model.OrganisationBusinessStatus(lookup);
            }

            return models;
        }

        public static OrganisationModel OrganisationBusinessType(this OrganisationModel model, List<BusinessTypeModel> lookup)
        {
            model.BusinessTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.businesstypeid == b.businesstypeid),
                Text = b.name,
                Value = b.businesstypeid.ToString(),
            });
            model.businesstypename = lookup.SingleOrDefault(b => b.businesstypeid == model.businesstypeid).name;

            return model;
        }

        public static List<OrganisationModel> OrganisationBusinessType(this List<OrganisationModel> models, List<BusinessTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.OrganisationBusinessType(lookup);
            }

            return models;
        }

        #endregion

        #region Task Lookup

        public static TaskModel TaskLookup(this TaskModel model, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup, List<UserModel> userLookup)
        {
            model.TaskOwner(userLookup);
            model.TaskPriorityType(priorityTypeLookup);
            model.TaskStateType(stateTypeLookup);
            model.TaskStatusType(statusTypeLookup);

            return model;
        }

        public static List<TaskModel> TaskLookup(this List<TaskModel> models, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup, List<UserModel> userLookup)
        {
            foreach (var model in models)
            {
                model.TaskLookup(priorityTypeLookup, stateTypeLookup, statusTypeLookup, userLookup);
            }

            return models;
        }

        public static TaskModel TaskLookup(this TaskModel model, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup, List<UserModel> userLookup, List<ProjectModel> projectLookup)
        {
            model.TaskOwner(userLookup);
            model.TaskPriorityType(priorityTypeLookup);
            model.TaskStateType(stateTypeLookup);
            model.TaskStatusType(statusTypeLookup);
            model.TaskProject(projectLookup);

            return model;
        }

        public static List<TaskModel> TaskLookup(this List<TaskModel> models, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup, List<UserModel> userLookup, List<ProjectModel> projectLookup)
        {
            foreach (var model in models)
            {
                model.TaskLookup(priorityTypeLookup, stateTypeLookup, statusTypeLookup, userLookup, projectLookup);
            }

            return models;
        }

        public static TaskModel TaskLookup(this TaskModel model, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup, List<UserModel> userLookup, List<ProjectModel> projectLookup, List<OrganisationModel> organisationLookup)
        {
            model.TaskOwner(userLookup);
            model.TaskPriorityType(priorityTypeLookup);
            model.TaskStateType(stateTypeLookup);
            model.TaskStatusType(statusTypeLookup);
            model.TaskProject(projectLookup);
            model.TaskOrganisation(organisationLookup);

            return model;
        }

        public static List<TaskModel> TaskLookup(this List<TaskModel> models, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup, List<UserModel> userLookup, List<ProjectModel> projectLookup, List<OrganisationModel> organisationLookup)
        {
            foreach (var model in models)
            {
                model.TaskLookup(priorityTypeLookup, stateTypeLookup, statusTypeLookup, userLookup, projectLookup, organisationLookup);
            }

            return models;
        }

        public static TaskModel TaskOrganisation(this TaskModel model, List<OrganisationModel> lookup)
        {
            model.Organisations = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.organisationid == b.organisationid),
                Text = b.name,
                Value = b.organisationid.ToString(),
            });
            model.organisationname = lookup.SingleOrDefault(b => b.organisationid == model.organisationid).name;

            return model;
        }

        public static List<TaskModel> TaskOrganisation(this List<TaskModel> models, List<OrganisationModel> lookup)
        {
            foreach (var model in models)
            {
                model.TaskOrganisation(lookup);
            }

            return models;
        }

        public static TaskModel TaskPriorityType(this TaskModel model, List<PriorityTypeModel> lookup)
        {
            model.PriorityTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.prioritytypeid == b.prioritytypeid),
                Text = b.name,
                Value = b.prioritytypeid.ToString(),
            });
            model.prioritytypename = lookup.SingleOrDefault(b => b.prioritytypeid == model.prioritytypeid).name;

            return model;
        }

        public static List<TaskModel> TaskPriorityType(this List<TaskModel> models, List<PriorityTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.TaskPriorityType(lookup);
            }

            return models;
        }

        public static TaskModel TaskProject(this TaskModel model, List<ProjectModel> lookup)
        {
            model.Projects = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.projectid == b.projectid),
                Text = b.subject,
                Value = b.projectid.ToString(),
            });
            model.projectname = lookup.SingleOrDefault(b => b.projectid == model.projectid).subject;

            return model;
        }

        public static List<TaskModel> TaskProject(this List<TaskModel> models, List<ProjectModel> lookup)
        {
            foreach (var model in models)
            {
                model.TaskProject(lookup);
            }

            return models;
        }

        public static TaskModel TaskStateType(this TaskModel model, List<StateTypeModel> lookup)
        {
            model.StateTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.statetypeid == b.statetypeid),
                Text = b.name,
                Value = b.statetypeid.ToString(),
            });
            model.statetypename = lookup.SingleOrDefault(b => b.statetypeid == model.statetypeid).name;

            return model;
        }

        public static List<TaskModel> TaskStateType(this List<TaskModel> models, List<StateTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.TaskStateType(lookup);
            }

            return models;
        }

        public static TaskModel TaskStatusType(this TaskModel model, List<StatusTypeModel> lookup)
        {
            model.StatusTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.statustypeid == b.statustypeid),
                Text = b.name,
                Value = b.statustypeid.ToString(),
            });
            model.statustypename = lookup.SingleOrDefault(b => b.statustypeid == model.statustypeid).name;

            return model;
        }

        public static List<TaskModel> TaskStatusType(this List<TaskModel> models, List<StatusTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.TaskStatusType(lookup);
            }

            return models;
        }

        public static TaskModel TaskOwner(this TaskModel model, List<UserModel> lookup)
        {
            model.Owner = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.ownerid == b.systemuserid),
                Text = b.fullname,
                Value = b.systemuserid.ToString(),
            });
            model.ownername = lookup.SingleOrDefault(b => b.systemuserid == model.ownerid).fullname;

            return model;
        }

        public static List<TaskModel> TaskOwner(this List<TaskModel> models, List<UserModel> lookup)
        {
            foreach (var model in models)
            {
                model.TaskOwner(lookup);
            }

            return models;
        }

        #endregion

        #region Project Lookup

        public static ProjectModel ProjectLookup(this ProjectModel model, List<BusinessValueModel> businessValueLookup, List<OrganisationModel> organisationLookup, List<UserModel> userLookup, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup)
        {
            model.ProjectBusinessValue(businessValueLookup);
            model.ProjectOrganisation(organisationLookup);
            model.ProjectOwner(userLookup);
            model.ProjectPriorityType(priorityTypeLookup);
            model.ProjectStateType(stateTypeLookup);
            model.ProjectStatusType(statusTypeLookup);

            return model;
        }

        public static List<ProjectModel> ProjectLookup(this List<ProjectModel> models, List<BusinessValueModel> businessValueLookup, List<OrganisationModel> organisationLookup, List<UserModel> userLookup, List<PriorityTypeModel> priorityTypeLookup, List<StateTypeModel> stateTypeLookup, List<StatusTypeModel> statusTypeLookup)
        {
            foreach (var model in models)
            {
                model.ProjectLookup(businessValueLookup, organisationLookup, userLookup, priorityTypeLookup, stateTypeLookup, statusTypeLookup);
            }

            return models;
        }

        public static ProjectModel ProjectBusinessValue(this ProjectModel model, List<BusinessValueModel> lookup)
        {
            model.BusinessValueCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.businessvalueid == b.businessvalueid),
                Text = b.name,
                Value = b.businessvalueid.ToString(),
            });
            model.businessvaluename = lookup.SingleOrDefault(b => b.businessvalueid == model.businessvalueid).name;

            return model;
        }

        public static List<ProjectModel> ProjectBusinessValue(this List<ProjectModel> models, List<BusinessValueModel> lookup)
        {
            foreach (var model in models)
            {
                model.ProjectBusinessValue(lookup);
            }

            return models;
        }

        public static ProjectModel ProjectOrganisation(this ProjectModel model, List<OrganisationModel> lookup)
        {
            model.Organisations = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.organisationid == b.organisationid),
                Text = b.name,
                Value = b.organisationid.ToString(),
            });
            model.organisationname = lookup.SingleOrDefault(b => b.organisationid == model.organisationid).name;

            return model;
        }

        public static List<ProjectModel> ProjectOrganisation(this List<ProjectModel> models, List<OrganisationModel> lookup)
        {
            foreach (var model in models)
            {
                model.ProjectOrganisation(lookup);
            }

            return models;
        }

        public static ProjectModel ProjectOwner(this ProjectModel model, List<UserModel> lookup)
        {
            model.Users = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.ownerid == b.systemuserid),
                Text = b.fullname,
                Value = b.systemuserid.ToString(),
            });
            model.ownername = lookup.SingleOrDefault(b => b.systemuserid == model.ownerid).fullname;

            return model;
        }

        public static List<ProjectModel> ProjectOwner(this List<ProjectModel> models, List<UserModel> lookup)
        {
            foreach (var model in models)
            {
                model.ProjectOwner(lookup);
            }

            return models;
        }
        
        public static ProjectModel ProjectPriorityType(this ProjectModel model, List<PriorityTypeModel> lookup)
        {
            model.PriorityTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.prioritytypeid == b.prioritytypeid),
                Text = b.name,
                Value = b.prioritytypeid.ToString(),
            });
            model.prioritytypename = lookup.SingleOrDefault(b => b.prioritytypeid == model.prioritytypeid).name;

            return model;
        }

        public static List<ProjectModel> ProjectPriorityType(this List<ProjectModel> models, List<PriorityTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.ProjectPriorityType(lookup);
            }

            return models;
        }

        public static ProjectModel ProjectStateType(this ProjectModel model, List<StateTypeModel> lookup)
        {
            model.StateTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.statetypeid == b.statetypeid),
                Text = b.name,
                Value = b.statetypeid.ToString(),
            });
            model.statetypename= lookup.SingleOrDefault(b => b.statetypeid == model.statetypeid).name;

            return model;
        }

        public static List<ProjectModel> ProjectStateType(this List<ProjectModel> models, List<StateTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.ProjectStateType(lookup);
            }

            return models;
        }

        public static ProjectModel ProjectStatusType(this ProjectModel model, List<StatusTypeModel> lookup)
        {
            model.StatusTypeCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.statustypeid == b.statustypeid),
                Text = b.name,
                Value = b.statustypeid.ToString(),
            });
            model.statustypename = lookup.SingleOrDefault(b => b.statustypeid == model.statustypeid).name;

            return model;
        }

        public static List<ProjectModel> ProjectStatusType(this List<ProjectModel> models, List<StatusTypeModel> lookup)
        {
            foreach (var model in models)
            {
                model.ProjectStatusType(lookup);
            }

            return models;
        }

        #endregion

        #region User Lookup

        public static UserModel UserBusinessFunction(this UserModel model, List<BusinessFunctionModel> lookup)
        {
            model.BusinessFuntionCode = lookup.Select(b => new SelectListItem()
            {
                Selected = (model.businessfunctionid == b.businessfunctionid),
                Text = b.name,
                Value = b.businessfunctionid.ToString(),
            });
            model.businessfunctionname = lookup.SingleOrDefault(b => b.businessfunctionid == model.businessfunctionid).name;

            return model;
        }

        public static List<UserModel> UserBusinessFunction(this List<UserModel> models, List<BusinessFunctionModel> lookup)
        {
            foreach (var model in models)
            {
                model.UserBusinessFunction(lookup);
            }

            return models;
        }

        #endregion
    }
}