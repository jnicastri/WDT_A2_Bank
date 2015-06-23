using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.DataAccess;

namespace A2_NWBA.Code.Logic
{
    public class CustomerManager
    {

        public static Customer GetCustomer(int Id)
        {
            Customer cust = DBCustomer.GetCustomerById(Id);

            if (cust == null)
                return null;
            else
                return cust;
        }

        public static Customer SaveCustomer(Customer cust)
        {
            return DBCustomer.Update(cust);
        }

        public static CustomerList Admin_GetAllCustomers()
        {
            return DBCustomer.GetAllCustomers();
        }
    }
}