using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Library
{
    [DataContract]
    public class NoteDto
    {
        [DataMember]
        public int? createdby { get; set; }

        [DataMember]
        public DateTime createdon { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public bool isdisabled { get; set; }

        [DataMember]
        public int? modifiedby { get; set; }

        [DataMember]
        public DateTime? modifiedon { get; set; }

        [DataMember]
        public int noteid { get; set; }
    }
}
