using System.Collections.Generic;

namespace GLR00300Common.GLR00300Print
{
    public class GLR00300AccountTrialBalanceResultDTO
    {
        public string Title { get; set; }
        public GLR00300HeaderAccountTrialBalanceDTO Header{ get; set; }
        public AccountTrialBalanceColumnDTO Column { get; set; }
        public  List<GLR00300_DataDetail_AccountTrialBalance> DataAccountTrialBalance  { get; set; }

        // public GLR00300AccountTrialBalanceDTO HeaderData { get; set; }

        //public GLR00300AccountTrialBalanceFooterDTO Footer { get; set; }
    }
    public class GLR00300AccountTrialBalanceResultWithBaseHeaderDTO : BaseHeaderReportCommon.BaseHeader.BaseHeaderResult
    {
        public GLR00300AccountTrialBalanceResultDTO GLR00300_ACCOUNT_TRIAL_BALANCE_RESULT_DATA { get; set; }
    }


    public class GLR00300AccountTrialBalanceResultFormat_EtoH_DTO
    {
        public string Title { get; set; }
        public GLR00300HeaderAccountTrialBalanceDTO Header { get; set; }
        public AccountTrialBalanceColumnDTO Column { get; set; }
        public List<GLRR00300DataAccountTrialBalance> Data { get; set; }

    }
    public class GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO : BaseHeaderReportCommon.BaseHeader.BaseHeaderResult
    {
        public GLR00300AccountTrialBalanceResultFormat_EtoH_DTO GLR00300AccountTrialBalanceResult_FormatEtoH_DataFormat { get; set; }
    }
}