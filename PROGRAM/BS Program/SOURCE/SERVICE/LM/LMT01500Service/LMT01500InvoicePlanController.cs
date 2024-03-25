using LMT01500Back;
using LMT01500Common.Context;
using LMT01500Common.DTO._5._Invoice_Plan;
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
    public class LMT01500InvoicePlanController : ControllerBase, ILMT01500InvoicePlan
    {
        private readonly LoggerLMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public LMT01500InvoicePlanController(ILogger<LMT01500InvoicePlanController> logger)
        {
            LoggerLMT01500.R_InitializeLogger(logger);
            _loggerLMT01500 = LoggerLMT01500.R_GetInstanceLogger();
            _activitySource = LMT01500Activity.R_InitializeAndGetActivitySource(nameof(LMT01500AgreementListController));
        }

        [HttpPost]
        public LMT01500InvoicePlanHeaderDTO GetInvoicePlanHeader(LMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetInvoicePlanHeader);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            LMT01500GetHeaderParameterDTO? loDbParameter;
            LMT01500InvoicePlanHeaderDTO? loReturn = null;
            LMT01500InvoicePlanCls loCls;

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
                    loDbParameter = poParameter;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500InvoicePlanCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetInvoicePlanHeaderDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loReturn);

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
        public IAsyncEnumerable<LMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeList()
        {
            string? lcMethod = nameof(GetInvoicePlanChargeList);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            LMT01500GetHeaderParameterDTO? loDbParameter;
            List<LMT01500InvoicePlanChargesListDTO> loRtnTmp;
            LMT01500InvoicePlanCls loCls;
            IAsyncEnumerable<LMT01500InvoicePlanChargesListDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500InvoicePlanCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetInvoicePlanChargeListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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
        public IAsyncEnumerable<LMT01500InvoicePlanListDTO> GetInvoicePlanList()
        {
            string? lcMethod = nameof(GetInvoicePlanList);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            LMT01500GetHeaderParameterDTO? loDbParameter;
            List<LMT01500InvoicePlanListDTO> loRtnTmp;
            LMT01500InvoicePlanCls loCls;
            IAsyncEnumerable<LMT01500InvoicePlanListDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500InvoicePlanCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetInvoicePlanListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
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

    }
}
