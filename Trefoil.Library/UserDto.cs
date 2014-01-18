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
    public class UserDto
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
        public int businessfunctionid { get; set; }

        [DataMember]
        public DateTime? createdon { get; set; }

        [DataMember]
        public string disabledreason { get; set; }

        [DataMember]
        public string emailaddress { get; set; }

        [DataMember]
        public string firstname { get; set; }

        [DataMember]
        public string fullname { get; set; }

        [DataMember]
        public string homephone { get; set; }

        [DataMember]
        public string interest { get; set; }

        [DataMember]
        public bool isdisabled { get; set; }

        [DataMember]
        public bool isinternal { get; set; }

        [DataMember]
        public string jobtitle { get; set; }

        [DataMember]
        public string lastname { get; set; }

        [DataMember]
        public string middlename { get; set; }

        [DataMember]
        public string mobilephone { get; set; }

        [DataMember]
        public string nickname { get; set; }

        [DataMember]
        public int? organisationid { get; set; }

        [DataMember]
        public int? parentsystemuserid { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public int roletypeid { get; set; }

        [DataMember]
        public int systemuserid { get; set; }

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
        public string title { get; set; }

        [DataMember]
        public string website { get; set; }
    }
}
