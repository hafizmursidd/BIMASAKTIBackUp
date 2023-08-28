using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GST00500Common
{
    public class GST00500ApprovalTransactionDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CTRANSACTION_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREFERENCE_NO { get; set; }
        public bool LSUCCESSED { get; set; }
    }

    public class GST00500ApprovalTransactionListDTO : R_APIResultBaseDTO
    {
        public List<GST00500ApprovalTransactionDTO> Data { get; set; }
    }

}
