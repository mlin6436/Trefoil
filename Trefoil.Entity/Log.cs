using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Helper;
using Trefoil.Library;

namespace Trefoil.Entity
{
    public class Log
    {
        [Required]
        [StringLength(100)]
        public string applicationname { get; set; }
        
        public int? createdby { get; set; }

        public User CreatedbySystemuser { get; set; }

        public DateTime? createdon { get; set; }

        [Required]
        [StringLength(2000)]
        public string description { get; set; }

        public bool isresolved { get; set; }

        public LevelCode levelcode { get; set; }

        [Key]
        public int logid { get; set; }

        public int? modifiedby { get; set; }
        public User ModifiedbySystemuser { get; set; }

        public DateTime? modifiedon { get; set; }

        [StringLength(200)]
        public string subject { get; set; }
    }
}
