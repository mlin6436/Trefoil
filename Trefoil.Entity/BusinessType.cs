﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Entity
{
    public class BusinessType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int businesstypeid { get; set; }

        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        public bool isdisabled { get; set; }

        public DateTime? modifiedon { get; set; }

        [StringLength(100)]
        public string name { get; set; }
    }
}
