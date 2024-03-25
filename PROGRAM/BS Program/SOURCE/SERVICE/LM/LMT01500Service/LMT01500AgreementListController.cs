using LMT01500Back;
using LMT01500Common.Context;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.Interface;
using LMT01500Common.Logs;
using LMT01500Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace LMT01500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMT01500AgreementListController : ControllerBase, ILMT01500AgreementList
    {
        private readonly LoggerLMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public LMT01500AgreementListController(ILogger<LMT01500AgreementListController> logger)
        {
            LoggerLMT01500.R_InitializeLogger(logger);
            _loggerLMT01500 = LoggerLMT01500.R_GetInstanceLogger();
            _activitySource = LMT01500Activity.R_InitializeAndGetActivitySource(nameof(LMT01500AgreementListController));
        }

        [HttpPost]
        public IAsyncEnumerable<LMT01500AgreementListOriginalDTO> GetAgreementList()
        {
            string? lcMethod = nameof(GetAgreementList);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            string? lcDbParameter = "";
            List<LMT01500AgreementListOriginalDTO> loRtnTmp;
            LMT01500AgreementListCls loCls;
            IAsyncEnumerable<LMT01500AgreementListOriginalDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    lcDbParameter = R_Utility.R_GetStreamingContext<string>(LMT01500GetAgreementListContextDTO.CPROPERTY_ID);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", lcDbParameter);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500AgreementListCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetAgreementListDb(poParameterInternal: loDbParameterInternal, pcCPROPERTY_ID: lcDbParameter);
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerLMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.LMT01500GetListStream(loRtnTmp);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCTransStatus()
        {
            string? lcMethod = nameof(GetComboBoxDataCTransStatus);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesWithCultureIDParameterDTO? loDbParameterInternal;
            List<LMT01500ComboBoxDTO> loRtnTmp;
            LMT01500AgreementListCls loCls;
            IAsyncEnumerable<LMT01500ComboBoxDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CULTURE_ID = R_BackGlobalVar.CULTURE;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500AgreementListCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetComboBoxDataCTransStatusDb(poParameterInternal: loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerLMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.LMT01500GetListStream(loRtnTmp);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<LMT01500PropertyListDTO> GetPropertyList()
        {
            string? lcMethod = nameof(GetPropertyList);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            List<LMT01500PropertyListDTO> loRtnTmp;
            LMT01500AgreementListCls loCls;
            IAsyncEnumerable<LMT01500PropertyListDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500AgreementListCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetPropertyListDb(poParameterInternal: loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerLMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.LMT01500GetListStream(loRtnTmp);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<LMT01500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode()
        {
            string? lcMethod = nameof(GetVarGsmTransactionCode);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            List<LMT01500VarGsmTransactionCodeDTO> loRtnTmp;
            LMT01500AgreementListCls loCls;
            IAsyncEnumerable<LMT01500VarGsmTransactionCodeDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500AgreementListCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetVarGsmTransactionCodeDb(pcCCOMPANY_ID: R_BackGlobalVar.COMPANY_ID, pcCTRANS_CODE: "802030");
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerLMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.LMT01500GetListStream(loRtnTmp);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public LMT01500ChangeStatusDTO GetChangeStatus(LMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetChangeStatus);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            LMT01500GetHeaderParameterDTO? loDbParameter;
            LMT01500AgreementListCls? loCls;
            LMT01500ChangeStatusDTO? loReturn = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbParameter.CPROPERTY_ID = poParameter.CPROPERTY_ID;
                    loDbParameter.CDEPT_CODE = poParameter.CDEPT_CODE;
                    loDbParameter.CREF_NO = poParameter.CREF_NO;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500AgreementListCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetChangeStatusDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));
#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public LMT01500ProcessResultDTO ProcessChangeStatus(LMT01500ChangeStatusParameterDTO poEntity)
        {
            string? lcMethod = nameof(ProcessChangeStatus);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            var loEx = new R_Exception();
            var loRtn = new LMT01500ProcessResultDTO();
            LMT01500ChangeStatusParameterDTO loDbPar = poEntity;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMT01500.LogInfo(string.Format("Initialize the loCls object as a new instance in method {0}", lcMethod));
                var loCls = new LMT01500AgreementListCls();
                _loggerLMT01500.LogDebug("{@ObjectLMT01500AgreementListCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("ProcessChangeStatusDb method of LMT01500AgreementListCls in method {0}", lcMethod));
                loRtn = loCls.ProcessChangeStatusDb(poParameter: loDbPar);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMT01500.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

    }
}
