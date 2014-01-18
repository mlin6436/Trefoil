using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trefoil.Helper;

namespace Trefoil.Web.Models
{
    public class OrganisationModel
    {
        public OrganisationModel()
        {
            businessstatusid = 1;
            businesstypeid = 1;
            organisationid = 1;
        }

        [DisplayName("City")]
        [StringLength(50)]
        public string address1_city { get; set; }

        [DisplayName("Country")]
        [StringLength(50)]
        public string address1_country { get; set; }

        [DisplayName("County")]
        [StringLength(50)]
        public string address1_county { get; set; }

        [DisplayName("Fax")]
        [StringLength(50)]
        public string address1_fax { get; set; }

        [DisplayName("Address Line 1")]
        [StringLength(50)]
        public string address1_line1 { get; set; }

        [DisplayName("Address Line 2")]
        [StringLength(50)]
        public string address1_line2 { get; set; }

        [DisplayName("Address Line 3")]
        [StringLength(50)]
        public string address1_line3 { get; set; }

        [DisplayName("Address Name")]
        [StringLength(100)]
        public string address1_name { get; set; }

        [DisplayName("Post Code")]
        [StringLength(20)]
        public string address1_postalcode { get; set; }

        [DisplayName("Post Office Box")]
        [StringLength(20)]
        public string address1_postofficebox { get; set; }

        [DisplayName("Telephone 1")]
        [StringLength(50)]
        public string address1_telephone1 { get; set; }

        [DisplayName("Telephone 2")]
        [StringLength(50)]
        public string address1_telephone2 { get; set; }

        [DisplayName("Telephone 3")]
        [StringLength(50)]
        public string address1_telephone3 { get; set; }

        [DisplayName("Business Type")]
        public int businesstypeid { get; set; }

        [DisplayName("Business Status")]
        public int businessstatusid { get; set; }

        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        public bool isdisabled { get; set; }

        [DisplayName("Client Name")]
        [Required(ErrorMessage = "Client name required!")]
        [StringLength(100, ErrorMessage = "Client name must be less than 100 characters!")]
        public string name { get; set; }

        public int organisationid { get; set; }

        [DisplayName("Facebook")]
        [StringLength(100)]
        public string social_facebook { get; set; }

        [DisplayName("LinkedIn")]
        [StringLength(100)]
        public string social_linkedin { get; set; }

        [DisplayName("Twitter")]
        [StringLength(100)]
        public string social_twitter { get; set; }

        [DisplayName("Vimeo")]
        [StringLength(100)]
        public string social_vimeo { get; set; }

        [DisplayName("Youtube")]
        [StringLength(100)]
        public string social_youtube { get; set; }

        [DisplayName("Website")]
        [StringLength(100)]
        public string website { get; set; }



        public IEnumerable<SelectListItem> BusinessStatusCode { get; set; }

        public string businessstatusname { get; set; }

        public IEnumerable<SelectListItem> BusinessTypeCode { get; set; }
       
        public string businesstypename { get; set; }
    }

    public class OrganisationIndexModel
    {
        public List<OrganisationModel> Organisations { get; set; }
        public OrganisationModel NewOrganisation { get; set; }

        public OrganisationIndexModel()
        {
            Organisations = new List<OrganisationModel>();
            NewOrganisation = new OrganisationModel();
        }
    }

    public class OrganisationDetailsModel
    {
        public OrganisationModel Organisation { get; set; }
        public List<NoteModel> Notes { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<UserModel> Contacts { get; set; }
        public NoteModel NewNote { get; set; }
        public ProjectModel NewProject { get; set; }
        public TaskModel NewTask { get; set; }
        public UserModel NewContact { get; set; }

        public OrganisationDetailsModel()
        {
            Organisation = new OrganisationModel();
            Notes = new List<NoteModel>();
            Projects = new List<ProjectModel>();
            Tasks = new List<TaskModel>();
            Contacts = new List<UserModel>();
            NewNote = new NoteModel();
            NewProject = new ProjectModel();
            NewTask = new TaskModel();
            NewContact = new UserModel();
        }
    }
}