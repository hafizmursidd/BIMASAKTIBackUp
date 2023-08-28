namespace GLR00300ReportCommon
{
    public class AccountTrialBalanceDTO
    {
        //HEADER
        public string CPERIOD { get; set; }
        public string CTB_TYPE_NAME { get; set; }
        public string CCURRENCY { get; set; }
        public string CJOURNAL_ADJ_MODE_NAME { get; set; }
        public string CPRINT_METHOD_NAME { get; set; }
        public string CBUDGET_NO { get; set; }


        //DATA
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NDEBIT_ADJ { get; set; }
        public decimal NCREDIT_ADJ { get; set; }
        public decimal NEND_BALANCE { get; set; }




        public string D_C { get; set; } 
        public string BS_IS { get; set; } 
        public string BEG_BALANCE { get; set; } 
        public string DEBIT { get; set; }
        public string CREDIT { get; set; } 
        public string DEBIT_ADJ { get; set; } 
        public string CREDIT_ADJ { get; set; } 
        public string END_BALANCE { get; set; }
        public string MTD_BUDGET { get; set; } 
    }
}