﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trefoil.Web.Models
{
    public class BusinessFunctionModel
    {
        public int businessfunctionid { get; set; }

        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        public bool isdisabled { get; set; }

        [StringLength(100)]
        public string name { get; set; }
    }
}