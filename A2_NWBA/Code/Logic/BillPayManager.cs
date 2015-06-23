using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.DataAccess;

namespace A2_NWBA.Code.Logic
{
    public class BillPayManager
    {
        public static BillPayItem GetBillPayItem(int BPAYId)
        {
            return DBBillPayItem.LoadBillPayItem(BPAYId);
        }

        public static BillPayItemList GetBillPayList(int CustomerId)
        {
            return DBBillPayItem.LoadCustomerBillPayList(CustomerId);
        }

        public static BillPayPayeeList GetBillPayPayeeList()
        {
            return DBBillPayPayee.LoadPayeeList();
        }

        public static int? UpdateInsertBillPayItem(int? BPAYId, int FromAccount, decimal Amount, int PayeeId, DateTime NextBillDate, char Frequency)
        {
            return DBBillPayItem.InsertUpdateBillPay(BPAYId, FromAccount, Amount, PayeeId, NextBillDate, Frequency);
        }

        public static void CancelBPAYItem(int BPAYId)
        {
            DBBillPayItem.CancelBPAYItem(BPAYId);
        }

        public static BillPayItemList Admin_GetAllBPAY()
        {
            return DBBillPayItem.LoadBillPaySchedule();
        }
    }   
}