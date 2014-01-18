using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Entity
{
    public class Note
    {
        public int? createdby { get; set; }

        public User CreatedbySystemuser { get; set; }

        public DateTime createdon { get; set; }

        [StringLength(2000)]
        public string description { get; set; }

        public bool isdisabled { get; set; }

        public int? modifiedby { get; set; }

        public User ModifiedbySystemuser { get; set; }

        public DateTime? modifiedon { get; set; }

        [Key]
        public int noteid { get; set; }

        public ICollection<Organisation> Organisations { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
