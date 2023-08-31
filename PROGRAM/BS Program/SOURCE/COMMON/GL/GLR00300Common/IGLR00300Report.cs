using GLR00300Common.GLR00300Print;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLR00300Common
{
    public interface IGLR00300Report : R_IServiceCRUDBase<GLR00300DataAccountTrialBalance>
    {
        IAsyncEnumerable<GLR00300DataAccountTrialBalance> AccountTrialBalanceList(GLR00300ParamDBToGetReportDTO loParameter);
    }
}
