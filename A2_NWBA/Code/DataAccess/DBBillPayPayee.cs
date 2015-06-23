using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Utils;

namespace A2_NWBA.Code.DataAccess
{
    public class DBBillPayPayee
    {
        public static BillPayPayeeList LoadPayeeList()
        {
            BillPayPayeeList list = new BillPayPayeeList();
            SqlParamsColl paramList = new SqlParamsColl();

            SqlTools.ExecuteReader("dbo.Payee_LoadAll", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(PopulateFromReader(reader));
                    }
                });

            return list;
        }
        
        public static BillPayPayee PopulateFromReader(SqlDataReader reader)
        {
            BillPayPayee payee = new BillPayPayee();
            Address address = new Address();

            payee.Id = (int)reader["Id"];
            payee.Name = reader["PayeeName"].ToString().Trim();
            payee.PhoneNumber = reader["PhoneNumber"].ToString().Trim();

            string streetDetail = reader["AddressStreetDetail"].ToString().Trim();

            if (streetDetail != null && streetDetail != "")
            {
                address.StreetDetail = streetDetail;
                address.City = reader["AddressCity"].ToString().Trim();
                address.State = reader["AddressState"].ToString().Trim();
                address.ZipCode = reader["AddressZipCode"].ToString().Trim();
            }

            payee.Address = address;

            return payee;
        }
    }
}