using System.Collections.Generic;

namespace GLR00300Common.GLR00300Print
{
    public class GLR00300AccountTrialBalanceResultDTO
    {
        public string Title { get; set; }
        public GLR00300HeaderAccountTrialBalanceDTO Header{ get; set; }
        public AccountTrialBalanceColumnDTO Column { get; set; }
        public  List<GLR00300DataAccountTrialBalance> DataAccountTrialBalance  { get; set; }

        // public GLR00300AccountTrialBalanceDTO HeaderData { get; set; }

        //public GLR00300AccountTrialBalanceFooterDTO Footer { get; set; }
    }
    public class GLR00300AccountTrialBalanceResultWithBaseHeaderDTO
    {
        public GLR00300AccountTrialBalanceResultDTO GLR00300AccountTrialBalanceResult { get; set; }
    }
}