using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Utils;


namespace A2_NWBA.Code.DataAccess
{
    public class DBCustomer
    {
        public static Customer GetCustomerById(int Id)
        {
            Customer custDetail = new Customer();

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@CustomerId", SqlDbType.Int, Id);

            SqlTools.ExecuteReader("Customer_Load", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        PopulateObjectFromReader(reader, custDetail);  
                    }
                });
            return custDetail;
        }

        public static Customer Update(Customer Cust)
        {
            SqlParamsColl paramList = new SqlParamsColl();

            paramList.Add("@CustomerId", SqlDbType.Int, Cust.Id);
            paramList.Add("@FirstName", SqlDbType.NVarChar, Cust.FirstName);
            paramList.Add("@LastName", SqlDbType.NVarChar, Cust.LastName);
            paramList.Add("@TFN", SqlDbType.NVarChar, Cust.TaxFileNumber);
            paramList.Add("@AddressStreet", SqlDbType.NVarChar, Cust.CustomerAddress.StreetDetail);
            paramList.Add("@AddressCity", SqlDbType.NVarChar, Cust.CustomerAddress.City);
            paramList.Add("@AddressState", SqlDbType.NVarChar, Cust.CustomerAddress.State);
            paramList.Add("@AddressZipCode", SqlDbType.NVarChar, Cust.CustomerAddress.ZipCode);
            paramList.Add("@PhoneNumber", SqlDbType.NVarChar, Cust.PhoneNumber);

            Customer updatedCustomer = new Customer();

            SqlTools.ExecuteReader("dbo.Customer_Update", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        PopulateObjectFromReader(reader, updatedCustomer);
                    }
                });
            return updatedCustomer;
        }

        public static CustomerList GetAllCustomers()
        {
            SqlParamsColl paramList = new SqlParamsColl();
            CustomerList list = new CustomerList();

            SqlTools.ExecuteReader("dbo.Admin_LoadCustomers", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        Customer cust = new Customer();
                        PopulateObjectFromReader(reader, cust);
                        list.Add(cust);
                    }
                });

            return list;
        }

        public static void PopulateObjectFromReader(SqlDataReader reader, Customer NewCust)
        {
            Address customerAddress = new Address();

            NewCust.Id = (int)reader["Id"];
            NewCust.FirstName = reader["FirstName"].ToString().Trim();
            NewCust.LastName = reader["LastName"].ToString().Trim();
            NewCust.TaxFileNumber = reader["TFN"].ToString().Trim();
            NewCust.PhoneNumber = reader["PhoneNumber"].ToString().Trim();

            customerAddress.StreetDetail = reader["AddressStreetDetail"].ToString().Trim();
            customerAddress.City = reader["AddressCity"].ToString().Trim();
            customerAddress.State = reader["AddressState"].ToString().Trim();
            customerAddress.ZipCode = reader["AddressZipCode"].ToString().Trim();

            NewCust.CustomerAddress = customerAddress;
        }

    }
}