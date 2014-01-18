using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trefoil.Web.Models
{
    public class NoteModel
    {
        public int? createdby { get; set; }

        public DateTime createdon { get; set; }

        [DisplayName("Description")]
        [StringLength(2000)]
        public string description { get; set; }

        public bool isdisabled { get; set; }

        public int? modifiedby { get; set; }

        public DateTime? modifiedon { get; set; }

        [Key]
        public int noteid { get; set; }

        public int? organisationid { get; set; }

        public int? projectid { get; set; }

        public int? taskid { get; set; }
    }
}