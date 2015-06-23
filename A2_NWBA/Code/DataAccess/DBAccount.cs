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
    public class DBAccount
    {
        public static AccountList GetAccountsByCustomer(int Id)
        {
            AccountList list = new AccountList();

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@CustomerId", SqlDbType.Int, Id);

            SqlTools.ExecuteReader("dbo.Accounts_RetrieveByCustomer", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        Account acc = new Account();
                        PopulateObjectFromReader(reader, acc);
                        list.Add(acc);
                    }
                });

            return list;
        }

        public static void Update(Account acc)
        {
            SqlParamsColl paramList = new SqlParamsColl();

            paramList.Add("@AccountNumber", SqlDbType.Int, acc.AccountNumber);
            paramList.Add("AvailableBalance", SqlDbType.Money, acc.AvailableBalance);

            SqlTools.ExecuteReader("Account_Update", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        PopulateObjectFromReader(reader, acc);
                    }
                });
        }

        public static void PopulateObjectFromReader(SqlDataReader reader, Account acc)
        {
            acc.AccountNumber = (int)reader["AccountNumber"];
            acc.Type = (char)reader.GetString(reader.GetOrdinal("AccountType"))[0];
            acc.AvailableBalance = (decimal)reader["AvailableBalance"];
            acc.CustomerId = (int)reader["CustomerId"];

            try
            {
                acc.LastUpdatedDate = Convert.ToDateTime(reader["LastUpdatedDate"]);
            }
            catch (Exception)
            {
                acc.LastUpdatedDate = null;
            }
        }

    }
}