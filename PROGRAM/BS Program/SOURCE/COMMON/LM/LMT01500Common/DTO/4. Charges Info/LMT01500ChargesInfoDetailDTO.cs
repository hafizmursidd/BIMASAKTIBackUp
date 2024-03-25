namespace LMT01500Common.DTO._4._Charges_Info
{
    public class LMT01500ChargesInfoDetailDTO
    {
        //External Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //REAL DTOs
        public string? CSEQ_NO { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_TYPE_DESCR { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_DESCR { get; set; }
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CYEAR { get; set; }
        public string? CMONTH { get; set; }
        public string? CDAYS { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CBILLING_MODE { get; set; }
        public string? CFEE_METHOD { get; set; }
        public decimal NFEE_AMT { get; set; }
        public string? CINVOICE_PERIOD { get; set; }
        public string? CINV_AMT { get; set; }
        public string? CINVGRP_CODE { get; set; }
        public string? CINVGRP_NAME { get; set; }
        public string? CDESCRIPTION { get; set; }
    }
}
