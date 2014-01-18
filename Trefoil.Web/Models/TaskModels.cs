using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trefoil.Web.Models
{
    public class TaskModel
    {
        public TaskModel()
        {
            ownerid = 1;
            organisationid = 1;
            projectid = 1;
            prioritytypeid = 1;
            statetypeid = 1;
            statustypeid = 1;
        }

        [DisplayName("Actual Duration in Minutes")]
        public int? actualdurationminutes { get; set; }

        [DisplayName("Actual End Date")]
        public DateTime? actualend { get; set; }

        [DisplayName("Actual Start Date")]
        public DateTime? actualstart { get; set; }

        public int? createdby { get; set; }

        [DisplayName("Created On")]
        public DateTime createdon { get; set; }

        [DisplayName("Description")]
        [StringLength(2000)]
        public string description { get; set; }

        public bool isdisabled { get; set; }

        public int? modifiedby { get; set; }

        [DisplayName("Modified On")]
        public DateTime? modifiedon { get; set; }

        [DisplayName("Client")]
        public int? organisationid { get; set; }

        [DisplayName("Task Owner")]
        public int? ownerid { get; set; }

        [DisplayName("Task Priority")]
        public int prioritytypeid { get; set; }

        [DisplayName("Project")]
        public int? projectid { get; set; }

        [DisplayName("Scheduled Duration (min)")]
        public int? scheduleddurationminutes { get; set; }

        [DisplayName("Scheduled End")]
        public DateTime? scheduledend { get; set; }

        [DisplayName("Scheduled Start")]
        public DateTime? scheduledstart { get; set; }

        [DisplayName("Task State")]
        public int statetypeid { get; set; }

        [DisplayName("Task Status")]
        public int statustypeid { get; set; }

        [DisplayName("Task Subject")]
        [StringLength(200)]
        public string subject { get; set; }

        [DisplayName("Task")]
        [Key]
        public int taskid { get; set; }





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

        public IEnumerable<SelectListItem> Organisations { get; set; }

        public string organisationname { get; set; }
        
        public IEnumerable<SelectListItem> PriorityTypeCode { get; set; }

        public string prioritytypename { get; set; }

        public IEnumerable<SelectListItem> Projects { get; set; }

        public string projectname { get; set; }

        public IEnumerable<SelectListItem> StateTypeCode { get; set; }

        public string statetypename { get; set; }

        public IEnumerable<SelectListItem> StatusTypeCode { get; set; }

        public string statustypename { get; set; }

        public IEnumerable<SelectListItem> Owner { get; set; }

        public string ownername { get; set; }
    }

    public class TaskIndexModel
    {
        public List<TaskModel> Tasks { get; set; }
        public TaskModel NewTask { get; set; }

        public TaskIndexModel()
        {
            Tasks = new List<TaskModel>();
            NewTask = new TaskModel();
        }
    }

    public class TaskDetailsModel 
    { 
        public TaskModel Task { get; set; }
        public TaskModel NewTask { get; set; }
        public List<NoteModel> Notes { get; set; }
        public UserModel Owner { get; set; }
        public List<UserModel> AssignedUsers { get; set; }
        public NoteModel NewNote { get; set; }

        public TaskDetailsModel()
        {
            Task = new TaskModel();
            NewTask = new TaskModel();
            Notes = new List<NoteModel>();
            Owner = new UserModel();
            AssignedUsers = new List<UserModel>();
            NewNote = new NoteModel();
        }
    }
}