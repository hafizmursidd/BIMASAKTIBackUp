using System;

namespace LMT01500Common.DTO._6._Deposit
{
    public class LMT01500DepositListDTO
    {
        public string? CSEQ_NO { get; set; }
        public string? CDEPOSIT_JRNGRP_CODE { get; set; }
        public string? CDEPOSIT_JRNGRP_NAME { get; set; }
        public string? CDEPOSIT_DATE { get; set; }
        public string? NDEPOSIT_AMT { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
