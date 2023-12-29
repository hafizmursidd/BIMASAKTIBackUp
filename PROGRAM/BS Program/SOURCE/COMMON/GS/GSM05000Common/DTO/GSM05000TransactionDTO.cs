using System;
using System.Text;

namespace GSM05000Common.DTO
{
    public class GSM05000TransactionDTO
    {
        public string CTRANS_CODE { get; set; }

        public string CTRANSACTION_NAME { get; set; }
        public string CMODULE_NAME { get; set; }

        // private string _CTRANSACTION;
        public string CTRANSACTION { get => CTRANSACTION_NAME + " (" + CTRANS_CODE + ")"; }
    }
}
