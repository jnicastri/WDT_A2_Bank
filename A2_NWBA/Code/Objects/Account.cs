using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using A2_NWBA.Code.Enums;

namespace A2_NWBA.Code.Objects
{
    [Serializable]
    public class Account
    {
        #region Data/Properties
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public char Type { get; set; }
        public DateTime? LastUpdatedDate {get; set;}
        public decimal AvailableBalance { get; set; }
        public decimal MinimumBalanceAllowed
        {
            get
            {
                string valueString = "";
                decimal val = 0;

                if (this.Type == (char)Enums.Enums.AccountType.Cheque)
                    valueString = ConfigurationManager.AppSettings["MinBalanceCheque"];
                else
                    valueString = ConfigurationManager.AppSettings["MinBalanceSavings"];

                try
                {
                    val = Decimal.Parse(valueString);
                }
                catch (FormatException)
                {
                    val = 0; // Setting Error to lowest value possible - the savings account value of $0.00
                }
                return val;
            }
        }

        public decimal MinimumOpeningBalance
        {
            get
            {
                string valueString = "";
                decimal val = 0;

                if (this.Type == (char)Enums.Enums.AccountType.Cheque)
                    valueString = ConfigurationManager.AppSettings["MinOpeningBalanceCheque"];
                else
                    valueString = ConfigurationManager.AppSettings["MinOpeningBalanceSavings"];

                try
                {
                    val = Decimal.Parse(valueString);
                }
                catch (FormatException) 
                {
                    val = 100; // Setting Error to just some safe value - better than zero!
                }
                return val;
            }
        }

        #endregion
        public Account() { }

    }
}