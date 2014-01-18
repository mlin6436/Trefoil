using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Helper;
using Trefoil.Library;

namespace Trefoil.Web.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            businessvalueid = 1;
            organisationid = 1;
            prioritytypeid = 1;
            scheduledend = DateTime.UtcNow;
            scheduledstart = DateTime.UtcNow;
            statetypeid = 1;
            statustypeid = 1;
            ownerid = 1;
        }

        [DisplayName("Actual Duration (mins)")]
        public int? actualdurationminutes { get; set; }

        [DisplayName("Actual End Date")]
        public DateTime? actualend { get; set; }

        [DisplayName("Actual Start Date")]
        public DateTime? actualstart { get; set; }

        [DisplayName("Budget")]
        public double? budget { get; set; }

        [DisplayName("Business Value")]
        public int businessvalueid { get; set; }

        [DisplayName("Cancelled Reason")]
        [StringLength(200)]
        public string cancelledreason { get; set; }

        [DisplayName("Main Contact")]
        public int? contactid { get; set; }

        public int? createdby { get; set; }

        [DisplayName("Created On")]
        public DateTime createdon { get; set; }

        [DisplayName("Description")]
        [StringLength(2000)]
        public string description { get; set; }

        [DisplayName("Is Billed")]
        public bool isbilled { get; set; }

        public bool isdisabled { get; set; }

        public int? modifiedby { get; set; }

        [DisplayName("Modified On")]
        public DateTime? modifiedon { get; set; }

        [DisplayName("Client")]
        public int? organisationid { get; set; }

        [DisplayName("Project Owner")]
        public int? ownerid { get; set; }

        [DisplayName("Project")]
        public int projectid { get; set; }

        [DisplayName("Project Priority")]
        public int prioritytypeid { get; set; }

        [DisplayName("Rejected Reason")]
        [StringLength(200)]
        public string rejectedreason { get; set; }

        [DisplayName("Scheduled Duration (mins)")]
        public int? scheduleddurationminutes { get; set; }

        [DisplayName("Scheduled End")]
        public DateTime? scheduledend { get; set; }

        [DisplayName("Scheduled Start")]
        public DateTime? scheduledstart { get; set; }

        [DisplayName("Project State")]
        public int statetypeid { get; set; }

        [DisplayName("Project Status")]
        public int statustypeid { get; set; }

        [DisplayName("Project Subject")]
        [Required(ErrorMessage="Project subject is required.")]
        [StringLength(200)]
        public string subject { get; set; }

        [DisplayName("Total Cost")]
        public double? totalcost { get; set; }

        [DisplayName("Total Charge")]
        public double? totalcharge { get; set; }




        public double actualdurationhours
        {
            get
            {
                if (actualdurationminutes.HasValue)
                {
                    return Math.Round((double)actualdurationminutes / 60, 2);
                }

                return 0;
            }
        }

        public double progress
        {
            get
            {
                if (scheduleddurationminutes.HasValue)
                {
                    return (actualdurationminutes.HasValue ? (double)actualdurationminutes : 0) / (double)scheduleddurationminutes;
                }

                return 0;
            }
        }

        public double scheduleddurationhours
        {
            get
            {
                if (scheduleddurationminutes.HasValue)
                {
                    return Math.Round((double)scheduleddurationminutes / 60, 2);
                }

                return 0;
            }
        }

        public IEnumerable<SelectListItem> BusinessValueCode { get; set; }

        public string businessvaluename { get; set; }

        public IEnumerable<SelectListItem> Organisations { get; set; }

        public string organisationname { get; set; }

        public IEnumerable<SelectListItem> PriorityTypeCode { get; set; }

        public string prioritytypename { get; set; }

        public IEnumerable<SelectListItem> StateTypeCode { get; set; }

        public string statetypename { get; set; }

        public IEnumerable<SelectListItem> StatusTypeCode { get; set; }

        public string statustypename { get; set; }
        
        public IEnumerable<SelectListItem> Users { get; set; }

        public string ownername { get; set; }
    }

    public class ProjectIndexModel
    {        
        public List<ProjectModel> Projects { get; set; }
        public ProjectModel NewProject { get; set; }

        public ProjectIndexModel()
        {
            Projects = new List<ProjectModel>();
            NewProject = new ProjectModel();
        }
    }

    public class ProjectDetailsModel
    {
        public ProjectModel Project { get; set; }
        public List<NoteModel> Notes { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public NoteModel NewNote { get; set; }
        public TaskModel NewTask { get; set; }
        public UserModel Owner { get; set; }
        public UserModel Contact { get; set; }

        public ProjectDetailsModel()
        {
            Project = new ProjectModel();
            Notes = new List<NoteModel>();
            Tasks = new List<TaskModel>();
            NewNote = new NoteModel();
            NewTask = new TaskModel();
            Owner = new UserModel();
            Contact = new UserModel();
        }
    }
}