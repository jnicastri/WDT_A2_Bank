using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A2_NWBA.Code.Objects
{
    [Serializable]
    public class Address
    {
        public string StreetDetail { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address() { }

        public Address(string Street, string City, string State, string Zip)
        {
            this.StreetDetail = Street;
            this.City = City;
            this.State = State;
            this.ZipCode = Zip;
        }
    }
}