using System;

namespace PMT05500COMMON.DTO
{
    public class LMT05500DepositListDTO : LMT05500Header
    {

        public string CSEQ_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CCONTRACTOR_NAME { get; set; }
        public string CDEPOSIT_ID { get; set; }
        public string CDEPOSIT_NAME { get; set; }
        public string CDEPOSIT_DATE { get; set; }
        public decimal NDEPOSIT_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public string CINVOICE_NO { get; set; }
        public bool LPAYMENT { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}