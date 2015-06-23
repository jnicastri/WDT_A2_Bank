using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Utils;

namespace A2_NWBA.Code.DataAccess
{
    public class DBBillPayItem
    {
        public static BillPayItem LoadBillPayItem(int Id)
        {
            BillPayItem item = new BillPayItem();

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@BillPayId", SqlDbType.Int, Id);

            SqlTools.ExecuteReader("[dbo].[BillPay_LoadBillPay]", paramList, reader =>
                {
                    while (reader.Read())
                    {
                        PopulateFromReader(reader, item);
                    }
                });
            return item;
        }

        public static BillPayItemList LoadCustomerBillPayList(int CustomerId)
        {
            BillPayItemList list = new BillPayItemList();

            SqlParamsColl paramList = new SqlParamsColl();
            paramList.Add("@CustomerId", SqlDbType.Int, CustomerId);

            SqlTools.ExecuteReader("dbo.BillPay_LoadByCustomer", paramList, reader =>
            {
                while (reader.Read())
                {
                    BillPayItem item = new BillPayItem();
                    PopulateFromReader(reader, item);
                    list.Add(item);
                }
            });
            return list;
        }

        public static int? InsertUpdateBillPay(int? BPAYId, int FromAccount, decimal Amount, int PayeeId, DateTime NextBillDate, char Frequency)
        {
            int? returnedBPAYId = 0;

            SqlParamsColl paramList = new SqlParamsColl();

            if (BPAYId != null)
                paramList.Add("@BPAYId", SqlDbType.Int, BPAYId);

            paramList.Add("@FromAccountId", SqlDbType.Int, FromAccount);
            paramList.Add("@Amount", SqlDbType.Money, Amount);
            paramList.Add("@PayeeId", SqlDbType.Int, PayeeId);
            paramList.Add("@BillDate", SqlDbType.DateTime, NextBillDate);
            paramList.Add("@Frequency", SqlDbType.NVarChar, Frequency);

            returnedBPAYId = (int)SqlTools.ExecuteScalar("dbo.BillPay_InsertUpdate", paramList);

            if (returnedBPAYId != null)
                return returnedBPAYId;
            else
                return 0;
            
        }

        public static void CancelBPAYItem(int BPAYId)
        {
            SqlParamsColl paramList = new SqlParamsColl();

            paramList.Add("@BPAYId", SqlDbType.Int, BPAYId);

            SqlTools.ExecuteNonQuery("dbo.BillPay_Cancel", paramList);
        }

        public static BillPayItemList LoadBillPaySchedule()
        {
            BillPayItemList list = new BillPayItemList();
            SqlParamsColl paramList = new SqlParamsColl();

            SqlTools.ExecuteReader("dbo.Admin_LoadBillPaySchedule", paramList, reader =>
            {
                while (reader.Read())
                {
                    BillPayItem item = new BillPayItem();
                    PopulateFromReader(reader, item);
                    list.Add(item);
                }
            });
            return list;
        }

        public static void PopulateFromReader(SqlDataReader reader, BillPayItem item)
        {
            item.Id = (int)reader["Id"];
            item.Amount = (decimal)reader["Amount"];
            item.Payee = (int)reader["PayeeId"];
            item.PayerAccount = (int)reader["AccountNumber"];

            try
            {
                item.CycleFrequency = Convert.ToChar(reader["CycleFrequency"]);
            }
            catch (Exception) { }

            try
            {
                item.NextScheduledDate = Convert.ToDateTime(reader["ScheduleDate"]);
            }
            catch (Exception){ }

            try
            {
                item.LastDateUpdated = Convert.ToDateTime(reader["LastDateUpdated"]);
            }
            catch (Exception) { }

        }
    }
}