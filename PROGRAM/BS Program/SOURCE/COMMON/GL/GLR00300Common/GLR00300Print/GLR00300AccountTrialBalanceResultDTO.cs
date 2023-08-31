using System.Collections.Generic;

namespace GLR00300Common.GLR00300Print
{
    public class GLR00300AccountTrialBalanceResultDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public AccountTrialBalanceColumnDTO Column { get; set; }
        public GLR00300AccountTrialBalanceDTO HeaderData{ get; set; }

       // public GLR00300AccountTrialBalanceDTO HeaderData { get; set; }

        //public GLR00300AccountTrialBalanceFooterDTO Footer { get; set; }
    }
}