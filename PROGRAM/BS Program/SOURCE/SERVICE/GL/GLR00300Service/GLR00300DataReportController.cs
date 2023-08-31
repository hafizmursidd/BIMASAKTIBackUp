using GLR00300Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLR00300Back;
using GLR00300Common.GLR00300Print;
using R_BackEnd;
using R_CommonFrontBackAPI;
using R_Common;

namespace GLR00300Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLR00300DataReportController : ControllerBase, IGLR00300Report
    {
        public R_ServiceGetRecordResultDTO<GLR00300DataAccountTrialBalance> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLR00300DataAccountTrialBalance> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceSaveResultDTO<GLR00300DataAccountTrialBalance> R_ServiceSave(R_ServiceSaveParameterDTO<GLR00300DataAccountTrialBalance> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLR00300DataAccountTrialBalance> poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GLR00300DataAccountTrialBalance> AccountTrialBalanceList(GLR00300ParamDBToGetReportDTO loParameter)
        {
            R_Exception loException = new R_Exception();
            GLR00300ParamDBToGetReportDTO loDbToGetReportParameter;
            IAsyncEnumerable<GLR00300DataAccountTrialBalance> loReturn  = null;

            try
            {
                 loDbToGetReportParameter = new GLR00300ParamDBToGetReportDTO();
                 var loCls = new GLR00300Cls();

                 loDbToGetReportParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                 loDbToGetReportParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                 loDbToGetReportParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var  loReturnTemp = loCls.GetAllTrialBalanceReportData(loParameter);
                loReturn = Get_AccountTrialBalance(loReturnTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        private async IAsyncEnumerable<GLR00300DataAccountTrialBalance> Get_AccountTrialBalance(List<GLR00300DataAccountTrialBalance> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
