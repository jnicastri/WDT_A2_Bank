using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A2_NWBA.Code.Objects
{
    [Serializable]
    public class BillPayItem
    {
        #region Data/Properties
        public int Id { get; set; }
        public int PayerAccount { get; set; }
        public int Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextScheduledDate { get; set; }
        public char CycleFrequency { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public bool IsReOccuring
        {
            get
            {
                return this.CycleFrequency == (char)Enums.Enums.CycleFrequency.NonReoccuring ? false : true;
            }
        }
        public string FrequencyString
        {
            get
            {
                return Enum.GetName(typeof(Enums.Enums.CycleFrequency), this.CycleFrequency);
            }
        }


        #endregion

        public BillPayItem() { }
    }
}