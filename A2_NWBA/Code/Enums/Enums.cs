using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A2_NWBA.Code.Enums
{
    public class Enums
    {
        public enum TransType
        {
            Credit = 'D',
            DebitWithdraw = 'W',
            DebitTransfer = 'T',
            DebitServiceCharge = 'S',
            DebitBillPay = 'B'
        }

        public enum AccountType
        {
            Cheque = 'C',
            Savings = 'S'
        }

        public enum CycleFrequency
        {
            Monthly = 'M',
            Quarterly = 'Q',
            Annually = 'A',
            NonReoccuring = 'S'
        }

        public enum ATMMode
        {
            Deposit,
            Transfer,
            Withdraw
        }

        public enum PageMode
        {
            Edit,
            View
        }
    }
}