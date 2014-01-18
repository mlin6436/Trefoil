using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Helper;

namespace Trefoil.Library
{
    [DataContract]
    public class OrganisationDto
    {
        [DataMember]
        public string address1_city { get; set; }

        [DataMember]
        public string address1_country { get; set; }

        [DataMember]
        public string address1_county { get; set; }

        [DataMember]
        public string address1_fax { get; set; }

        [DataMember]
        public string address1_line1 { get; set; }

        [DataMember]
        public string address1_line2 { get; set; }

        [DataMember]
        public string address1_line3 { get; set; }

        [DataMember]
        public string address1_name { get; set; }

        [DataMember]
        public string address1_postalcode { get; set; }

        [DataMember]
        public string address1_postofficebox { get; set; }

        [DataMember]
        public string address1_telephone1 { get; set; }

        [DataMember]
        public string address1_telephone2 { get; set; }

        [DataMember]
        public string address1_telephone3 { get; set; }

        [DataMember]
        public int businesstypeid { get; set; }

        [DataMember]
        public int businessstatusid { get; set; }

        [DataMember]
        public DateTime? createdon { get; set; }

        [DataMember]
        public string disabledreason { get; set; }

        [DataMember]
        public bool isdisabled { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int organisationid { get; set; }

        [DataMember]
        public string social_facebook { get; set; }

        [DataMember]
        public string social_linkedin { get; set; }

        [DataMember]
        public string social_twitter { get; set; }

        [DataMember]
        public string social_vimeo { get; set; }

        [DataMember]
        public string social_youtube { get; set; }

        [DataMember]
        public string website { get; set; }
    }
}
