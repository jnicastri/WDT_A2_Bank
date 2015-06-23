using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.DataAccess;

namespace A2_NWBA.Code.Logic
{
    public class AccountManager
    {
        public static AccountList GetAccountsByCustomer(Customer Cust)
        {
            return DBAccount.GetAccountsByCustomer(Cust.Id);
        }

        public static void Withdraw(Account Acc, decimal Amount, string Comment)
        {
            DBTransaction.Insert_WithdrawTransaction(Acc.AccountNumber, Amount, Comment);
            //DBAccount.Update(Acc);
        }

        public static void Deposit(Account Acc, decimal Amount, string Comment)
        {
            DBTransaction.Insert_DepositTransaction(Acc.AccountNumber, Amount, Comment);
            //DBAccount.Update(Acc);
        }

        public static void Transfer(Account SourceAccount, int DestinationAccount, decimal Amount, string Comment)
        {
            DBTransaction.Insert_TransferTransaction(SourceAccount.AccountNumber, DestinationAccount, Amount, Comment);
            //DBAccount.Update(SourceAccount);
        }



    }
}