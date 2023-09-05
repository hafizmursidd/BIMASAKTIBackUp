using System.Collections.Generic;
using GLR00300Common;
using GLR00300Common.GLR00300Print;
using R_BusinessObjectFront;

namespace GLR00300Model
{
    public class GLR00300ReportModel : R_BusinessObjectServiceClientBase<GLR00300DataAccountTrialBalance>, IGLR00300Report
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLR00300";
        private const string DEFAULT_MODULE = "GL";
        public GLR00300ReportModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, 
                DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<GLR00300DataAccountTrialBalance> AccountTrialBalanceList(GLR00300ParamDBToGetReportDTO loParameter)
        {
            throw new System.NotImplementedException();
        }
    }
}