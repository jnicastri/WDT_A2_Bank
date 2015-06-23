using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A2_NWBA.Code.Objects
{
    [Serializable]
    public class BillPayPayee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }

        public BillPayPayee() { }
    }
}