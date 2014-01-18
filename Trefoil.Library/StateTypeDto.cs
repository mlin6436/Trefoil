﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Library
{
    [DataContract]
    public class StateTypeDto
    {
        [DataMember]
        public DateTime? createdon { get; set; }

        [DataMember]
        public string disabledreason { get; set; }

        [DataMember]
        public bool isdisabled { get; set; }

        [DataMember]
        public DateTime? modifiedon { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int statetypeid { get; set; }
    }
}
