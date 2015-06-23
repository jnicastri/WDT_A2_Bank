using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A2_NWBA.Code.Objects.Collections
{
    [Serializable]
    public class TransactionList: List<Transaction>
    {
        public TransactionList() { }
    }
}