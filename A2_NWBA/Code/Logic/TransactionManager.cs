using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.DataAccess;

namespace A2_NWBA.Code.Logic
{
    public class TransactionManager
    {
        public static TransactionList GetTransactionList(int AccountNumber)
        {
            return DBTransaction.GetAccountTransactions(AccountNumber);
        }

        public static TransactionList Admin_GetTransactionsByCustomer(int CustomerId)
        {
            return DBTransaction.GetTransactionsByCustomer(CustomerId);
        }
    }
}