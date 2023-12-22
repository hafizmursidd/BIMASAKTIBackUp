using Lookup_LMBACK;
using Lookup_LMCOMMON;
using Lookup_LMCOMMON.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using Lookup_LMCOMMON.Logs;
using Microsoft.Extensions.Logging;

namespace Lookup_LMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupLMController : ControllerBase, IPublicLookupLM
    {
        private LoggerLookupLM _loggerLookup;

        public PublicLookupLMController(ILogger<PublicLookupLMController> logger)
        {
            //Initial and Get Logger
            LoggerLookupLM.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupLM.R_GetInstanceLogger();
        }
        [HttpPost]
        public IAsyncEnumerable<LML00100DTO> LML00100GetSalesTaxList()
        {
            string lcMethodName = nameof(LML00100GetSalesTaxList);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00100DTO> loRtn = null;
            List<LML00100DTO> loReturnTemp;
            LML00100ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter = new LML00100ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
               
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllSalesTax");

                loReturnTemp = loCls.GetAllSalesTax(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
 
        [HttpPost]
        public IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList()
        {
            string lcMethodName = nameof(LML00200UnitChargesList);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00200DTO> loRtn = null;
            List<LML00200DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00200ParameterDTO();

                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);
                
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllUnitCharges");

                loReturnTemp = loCls.GetAllUnitCharges(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList()
        {
            string lcMethodName = nameof(LML00300SupervisorList);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00300DTO> loRtn = null;
            List<LML00300DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00300ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllSupervisor");

                loReturnTemp = loCls.GetAllSupervisor(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList()
        {
            string lcMethodName = nameof(LML00400UtilityChargesList);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00400DTO> loRtn = null;
            List<LML00400DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00400ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllUnitCharges");

                loReturnTemp = loCls.GetAllUtilityCharges(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LML00500DTO> LML00500SalesmanList()
        {
            string lcMethodName = nameof(LML00400UtilityChargesList);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LML00500DTO> loRtn = null;
            List<LML00500DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                var poParameter = new LML00500ParameterDTO();
                poParameter.CCOMPANY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCOMPANY_ID);
                poParameter.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CUSER_ID);
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllUnitCharges");

                loReturnTemp = loCls.GetAllSalesman(poParameter);
                loRtn = GetStreaming(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }

        #region StreamingProcess
        private async IAsyncEnumerable<T> GetStreaming<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        #endregion
    }
    
}