using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Library;

namespace Trefoil.Entity
{
    public class RoleType
    {
        public DateTime? createdon { get; set; }

        [StringLength(100)]
        public string disabledreason { get; set; }

        public bool isdisabled { get; set; }

        public DateTime? modifiedon { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int roletypeid { get; set; }
    }
}
