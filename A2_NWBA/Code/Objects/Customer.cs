using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A2_NWBA.Code.Logic;
using A2_NWBA.Code.Objects;


namespace A2_NWBA.Code.Objects
{
    [Serializable]
    public class Customer
    {
        #region data/properties

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxFileNumber { get; set; }
        public Address CustomerAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName.Trim() + " " + this.LastName.Trim();
            }
        }

        #endregion

        public Customer() { }

        public Customer Save()
        {
            return CustomerManager.SaveCustomer(this);
        }
    }
}