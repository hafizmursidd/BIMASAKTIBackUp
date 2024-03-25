namespace LMT01500Common.DTO._6._Deposit
{
    public class LMT01500DepositDetailDTO
    {
        //External Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //Real DTOs
        public string? CSEQ_NO { get; set; }
        public bool LCONTRACTOR { get; set; }
        public string? CCONTRACTOR_ID { get; set; }
        public string? CCONTRACTOR_NAME { get; set; }
        public string? CDEPOSIT_JRNGRP_CODE { get; set; }
        public string? CDEPOSIT_JRNGRP_NAME { get; set; }
        public string? CDEPOSIT_DATE { get; set; }
        public string? CDEPOSIT_AMT { get; set; }
        public string? CDESCRIPTION { get; set; }
    }
}
