using GLT00100Back;
using GLT00100Common;
using GLT00100Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLT00100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00100Controller : ControllerBase, IGLT00100
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLT00100DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLT00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GLT00100DTO>();
            GLT00100ParameterDTO loDbPar;
            try
            {
                var loCls = new GLT00100Cls();
                loDbPar = new GLT00100ParameterDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GLT00100DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLT00100DTO> poParameter)
        {
            R_ServiceSaveResultDTO<GLT00100DTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GLT00100Cls loCls;
            try
            {
                loCls = new GLT00100Cls();
                loRtn = new R_ServiceSaveResultDTO<GLT00100DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLT00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<GLT00100JournalGridDetailDTO> GetAllJournalDetailList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GLT00100JournalGridDetailDTO> loRtn = null;
            GLT00100ParameterDTO loDbPar;
            GLT00100Cls loCls;
            List<GLT00100JournalGridDetailDTO> loRtnTemp;
            try
            {
                loCls = new GLT00100Cls();
                loDbPar = new GLT00100ParameterDTO();
                loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbPar.CREC_ID = R_Utility.R_GetContext<string>(ContextConstant.CREC_ID);
                loRtnTemp = loCls.GetJournalDetailList(loDbPar);
                loRtn = ListStremHelper(loRtnTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GLT00100JournalGridDTO> GetJournalList()
        {
            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<GLT00100JournalGridDTO> loRtn = null;
            GLT00100ParameterDTO loDbPar;
            GLT00100Cls loCls;
            List<GLT00100JournalGridDTO> loRtnTemp;
            try
            {
                loDbPar = new GLT00100ParameterDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                loDbPar.CPERIOD = R_Utility.R_GetContext<string>(ContextConstant.CPERIOD);
                loDbPar.CSEARCH_TEXT = R_Utility.R_GetContext<string>(ContextConstant.CSEARCH_TEXT);
                loDbPar.CDEPT_CODE = R_Utility.R_GetContext<string>(ContextConstant.CDEPT_CODE);
                loDbPar.CSTATUS = R_Utility.R_GetContext<string>(ContextConstant.CSTATUS);
                loCls = new GLT00100Cls();
                loRtnTemp = loCls.GetJournalList(loDbPar);
                loRtn = ListStremHelper(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public GLT00100JournalGridDTO JournalProcesStatus(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            GLT00100ParameterDTO loParam = new GLT00100ParameterDTO();
            GLT00100JournalGridDTO loRtn = new GLT00100JournalGridDTO();
            GLT00100Cls loCls = new GLT00100Cls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.ProcessJournalStatus(loParam, poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public GLT00100JournalGridDTO ProcessReversingJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            GLT00100ParameterDTO loParam = new GLT00100ParameterDTO();
            GLT00100JournalGridDTO loRtn = new GLT00100JournalGridDTO();
            GLT00100Cls loCls = new GLT00100Cls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.ProcessReversing(loParam, poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public GLT00100JournalGridDTO UndoReversingJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            GLT00100ParameterDTO loParam = new GLT00100ParameterDTO();
            GLT00100JournalGridDTO loRtn = new GLT00100JournalGridDTO();
            GLT00100Cls loCls = new GLT00100Cls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.UndoReversingJournal(loParam, poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region INIT
        [HttpPost]
        public VAR_GSM_COMPANYDTO GetCompanyDTO()
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            VAR_GSM_COMPANYDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.GetJournalCompany(loDbParameter);

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
        public VAR_GL_SYSTEM_PARAMDTO GetSystemParam()
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbPar = new();
            VAR_GL_SYSTEM_PARAMDTO loReturn = null;
            InitCLS loCls;
            try
            {
                loCls = new InitCLS();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loReturn = loCls.GetSystemParam(loDbPar);

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
        public VAR_USER_DEPARTMENT_LISTDTO GetDeptList()
        {
            var loEx = new R_Exception();
            VAR_USER_DEPARTMENT_LISTDTO loRtn = null;
            GLT00100ParameterDTO loDbPar;
            InitCLS loCls;
            try
            {
                loDbPar = new GLT00100ParameterDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new InitCLS();

                loRtn = new VAR_USER_DEPARTMENT_LISTDTO();
                loRtn.Data = loCls.GetDeptList(loDbPar);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public VAR_CCURRENT_PERIOD_START_DATEDTO GetCurrentPeriodStartDate(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            VAR_CCURRENT_PERIOD_START_DATEDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.GetCurrentPeriodStartDate(loDbParameter, poData);

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
        public VAR_CSOFT_PERIOD_START_DATEDTO GetSoftPeriodStartDate(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            VAR_CSOFT_PERIOD_START_DATEDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.GetSoftPeriodStartDate(loDbParameter, poData);

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
        public VAR_IUNDO_COMMIT_JRNDTO GetIOption()
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            VAR_IUNDO_COMMIT_JRNDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.GetIOption(loDbParameter);

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
        public VAR_GSM_TRANSACTION_CODEDTO GetLincementLapproval()
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            VAR_GSM_TRANSACTION_CODEDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.GetTranscode(loDbParameter);

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
        public VAR_GSM_PERIODDTO GetPeriod()
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            VAR_GSM_PERIODDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
        public StatusListDTO GetStatusList()
        {
            R_Exception loEx = new R_Exception();
            StatusListDTO loRtn = null;
            List<StatusDTO> loResult;
            GLT00100ParameterDTO loDbPar;
            InitCLS loCls;

            try
            {
                loDbPar = new GLT00100ParameterDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loCls = new InitCLS();
                loResult = loCls.GetStatus(loDbPar);
                loRtn = new StatusListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public CurrencyCodeListDTO GetCurrencyCodeList()
        {

            R_Exception loEx = new R_Exception();
            CurrencyCodeListDTO loRtn = null;
            List<CurrencyCodeDTO> loResult;
            GLT00100ParameterDTO loDbPar;
            InitCLS loCls;

            try
            {
                loDbPar = new GLT00100ParameterDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new InitCLS();
                loResult = loCls.GetCurrency(loDbPar);
                loRtn = new CurrencyCodeListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GetCenterListDTO GetCenterList()
        {
            R_Exception loEx = new R_Exception();
            GetCenterListDTO loRtn = null;
            List<GetCenterDTO> loResult;
            GLT00100ParameterDTO loDbPar;
            InitCLS loCls;

            try
            {
                loDbPar = new GLT00100ParameterDTO();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new InitCLS();
                loResult = loCls.GetCenterList(loDbPar);
                loRtn = new GetCenterListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM_TRANSACTION_APPROVALDTO GetTransactionApproval()
        {
            R_Exception loException = new R_Exception();
            GLT00100ParameterDTO loDbParameter = new();
            GSM_TRANSACTION_APPROVALDTO loReturn = null;
            try
            {
                var loCls = new InitCLS();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.GetTransactionApproval(loDbParameter);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        #endregion

        #region Helper
        private async IAsyncEnumerable<T> ListStremHelper<T>(List<T> poParameter)
        {
            foreach (T item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
    }
}