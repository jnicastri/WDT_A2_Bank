using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A2_NWBA.Code.Objects
{
    [Serializable]
    public class Transaction
    {
        public int Id { get; set; }
        public char Type { get; set; }
        public int SourceAccountId { get; set; }
        public int? DestinationAccountNumber { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string Comment { get; set; }
        public DateTime? TransactionDate { get; set; }

        public string TransTypeName
        {
            get
            {
                return Enum.GetName(typeof(Enums.Enums.TransType), this.Type);
            }
        }
    }
}