using GLR00300Back;
using GLR00300Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLR00300Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLR00300Controller : ControllerBase, IGLR00300
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLR00300DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLR00300DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GLR00300DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLR00300DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLR00300DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public GLR00300PeriodDTO GetPeriod()
        {
            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GLR00300PeriodDTO loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbParameter.CCOMPANY_ID = "rcd";
                loReturn = loCls.GetPeriod(loDbParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        [HttpPost]
        public GenericList<GLR00300DTO> GetTrialBalanceType()
        {
            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GenericList<GLR00300DTO> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                //loDbParameter.CCOMPANY_ID = "rcd";
                //loDbParameter.CLANGUAGE_ID = "en";
                var loReturnTemp = loCls.GetTrialBalanceTypeList(loDbParameter);
                loReturn = new GenericList<GLR00300DTO> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        [HttpPost]
        public GenericList<GLR00300DTO> GetPrintMethodType()
        {
            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GenericList<GLR00300DTO> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                //loDbParameter.CCOMPANY_ID = "rcd";
                //loDbParameter.CLANGUAGE_ID = "en";
                var loReturnTemp = loCls.GetPrintMethodTypeList(loDbParameter);
                loReturn = new GenericList<GLR00300DTO> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        [HttpPost]
        public GenericList<GLR00300BudgetNoDTO> GetBudgetNo(GLR00300DBParameterDTO loParam)
        {
            R_Exception loException = new R_Exception();
            GLR00300DBParameter loDbParameter = new();
            GenericList<GLR00300BudgetNoDTO> loReturn = null;
            try
            {
                var loCls = new GLR00300Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CYEAR = loParam.PERIOD_YEAR;
                loDbParameter.CCURRENCY_TYPE = loParam.CURRENCY_TYPE;

                //loDbParameter.CCOMPANY_ID = "rcd";
                //loDbParameter.CLANGUAGE_ID = "en";
                //loDbParameter.CYEAR = "2023";
                //loDbParameter.CCURRENCY_TYPE = "L";

                var loReturnTemp = loCls.GetBudgetNoList(loDbParameter);
                loReturn = new GenericList<GLR00300BudgetNoDTO> { Data = loReturnTemp };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
    }
}