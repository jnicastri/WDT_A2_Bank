using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Utils;

namespace A2_NWBA.Code.DataAccess
{
    public class DBTransaction
    {
        public static TransactionList GetAccountTransactions(int AccountNumber)
        {
            TransactionList list = new TransactionList();
            SqlParamsColl paramList = new SqlParamsColl();
            decimal feeAmount = 0;

            try
            {
                feeAmount = Decimal.Parse(ConfigurationManager.AppSettings["Fee_TransactionHistory"]);
            }
            catch (FormatException) { }

            paramList.Add("@AccountNumber", SqlDbType.Int, AccountNumber);
            paramList.Add("@FeeAmount", SqlDbType.Money, feeAmount);
            SqlTools.ExecuteReader("Account_RetrieveTransactions", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(PopulateObjectFromReader(reader)); 
                    }
                });
            return list;
        }

        public static void Insert_WithdrawTransaction(int AccountNumber, decimal Amount, string Comment){

            decimal feeAmount = 0;

            try
            {
                feeAmount = Decimal.Parse(ConfigurationManager.AppSettings["Fee_ATMTransaction"]);
            }
            catch (FormatException) { }

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@AccountNumber", SqlDbType.Int, AccountNumber);
            paramList.Add("@Amount", SqlDbType.Money, Amount);
            paramList.Add("@FeeAmount", SqlDbType.Money, feeAmount);
            if (Comment != null)
                paramList.Add("@Comment", SqlDbType.NVarChar, Comment);

            SqlTools.ExecuteNonQuery("dbo.Transaction_InsertWithdraw", paramList);
        }

        public static void Insert_DepositTransaction(int AccountNumber, decimal Amount, string Comment)
        {
            decimal feeAmount = 0;

            try
            {
                feeAmount = Decimal.Parse(ConfigurationManager.AppSettings["Fee_ATMTransaction"]);
            }
            catch (FormatException) { }

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@AccountNumber", SqlDbType.Int, AccountNumber);
            paramList.Add("@Amount", SqlDbType.Money, Amount);
            paramList.Add("@FeeAmount", SqlDbType.Money, feeAmount);
            if (Comment != null)
                paramList.Add("@Comment", SqlDbType.NVarChar, Comment);

            SqlTools.ExecuteNonQuery("dbo.Transaction_InsertDeposit", paramList);
        }

        public static void Insert_TransferTransaction(int SourceAccountNumber, int DestinationAccountNumber, decimal Amount, string Comment)
        {
            decimal feeAmount = 0;

            try
            {
                feeAmount = Decimal.Parse(ConfigurationManager.AppSettings["Fee_ATMTransaction"]);
            }
            catch (FormatException) { }

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@SourceAccountNumber", SqlDbType.Int, SourceAccountNumber);
            paramList.Add("@DestinationAccountNumber", SqlDbType.Int, DestinationAccountNumber);
            paramList.Add("@Amount", SqlDbType.Money, Amount);
            paramList.Add("@FeeAmount", SqlDbType.Money, feeAmount);
            if (Comment != null)
                paramList.Add("@Comment", SqlDbType.NVarChar, Comment);

            SqlTools.ExecuteNonQuery("dbo.Transaction_InsertTransfer", paramList);
        }

        public static TransactionList GetTransactionsByCustomer(int CustomerId)
        {
            TransactionList list = new TransactionList();
            SqlParamsColl paramList = new SqlParamsColl();

            paramList.Add("@CustomerId", SqlDbType.Int, CustomerId);

            SqlTools.ExecuteReader("dbo.Admin_LoadCustomerTransactions", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(PopulateObjectFromReader(reader));
                    }
                });
            return list;
        }

        public static Transaction PopulateObjectFromReader(SqlDataReader reader)
        {
            Transaction trans = new Transaction();

            trans.Id = (int)reader["Id"];
            trans.Type = (char)reader.GetString(reader.GetOrdinal("TransactionType"))[0];
            trans.SourceAccountId = (int)reader["AccountNumber"];

            trans.DestinationAccountNumber = null;
            trans.TransactionAmount = null;
            trans.Comment = null;

            if (!reader.IsDBNull(reader.GetOrdinal("DestAccount")))
                trans.DestinationAccountNumber = (int)reader["DestAccount"];
            if (!reader.IsDBNull(reader.GetOrdinal("Amount")))
                trans.TransactionAmount = (decimal)reader["Amount"];
            if (!reader.IsDBNull(reader.GetOrdinal("Comment")))
                trans.Comment = reader["Comment"].ToString().Trim();

            try
            {
                trans.TransactionDate = Convert.ToDateTime(reader["TransactionDate"]);
            }
            catch(Exception)
            {
                trans.TransactionDate = null;
            }
            return trans;
        }

    }
}