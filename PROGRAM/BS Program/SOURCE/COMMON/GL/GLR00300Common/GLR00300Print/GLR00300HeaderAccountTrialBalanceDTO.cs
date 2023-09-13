using System.Collections.Generic;

namespace GLR00300Common.GLR00300Print
{

    public class GLR00300HeaderAccountTrialBalanceDTO
    {
        //HEADER TO Fast Report
        public string CPERIOD { get; set; }
        public string CFROM_ACCOUNT_NO { get; set; }
        public string CTO_ACCOUNT_NO { get; set; }
        public string CFROM_CENTER_CODE { get; set; }
        public string CTO_CENTER_CODE { get; set; }
        public string CTB_TYPE_NAME { get; set; }
        public string CCURRENCY { get; set; }
        public string CJOURNAL_ADJ_MODE_NAME { get; set; }
        public string CPRINT_METHOD_NAME { get; set; }
        public string CBUDGET_NO { get; set; }
    }

    #region Format E to Format H
    public class GLRR00300DataAccountTrialBalance
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public List<GLRR00300DataDetailAccountTrialBalance> DataDetail { get; set; }
    }
    public class GLRR00300DataDetailAccountTrialBalance
    {
        public string CCENTER { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NDEBIT_ADJ { get; set; }
        public decimal NCREDIT_ADJ { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public decimal NBUDGET { get; set; }
    }
    #endregion


    public class GLR00300_DataDetail_AccountTrialBalance
    {
        //DATA WITH HEADER FROM DB

        //HEADERFROM DB
        public string CPERIOD_NAME { get; set; }
        public string CFROM_ACCOUNT_NO { get; set; }
        public string CTO_ACCOUNT_NO { get; set; }
        public string CFROM_CENTER_CODE { get; set; }
        public string CTO_CENTER_CODE { get; set; }
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
        public string CCENTER { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NDEBIT_ADJ { get; set; }
        public decimal NCREDIT_ADJ { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public decimal NBUDGET { get; set; }
    }
}