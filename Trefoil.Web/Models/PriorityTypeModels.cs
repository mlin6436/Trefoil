using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trefoil.Web.Models
{
    public class PriorityTypeModel
    {
        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        public bool isdisabled { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public int prioritytypeid { get; set; }
    }
}