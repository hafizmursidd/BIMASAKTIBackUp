﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LMT05500Back;
using LMT05500Common.DTO;
using LMT05500Common.Interface;
using LMT05500Common.Logs;
using Microsoft.Extensions.Logging;
using R_Common;
using R_BackEnd;

namespace LMT05500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMT05500AgreementController : ControllerBase, ILMT05500Agreement
    {
        private LoggerLMT05500 _loggerLMT05500;
        private readonly ActivitySource _activitySource;

        public LMT05500AgreementController(ILogger<LMT05500AgreementController> logger)
        {
            //Initial and Get Logger
            LoggerLMT05500.R_InitializeLogger(logger);
            _loggerLMT05500 = LoggerLMT05500.R_GetInstanceLogger();
            _activitySource = LMT05500Activity.R_InitializeAndGetActivitySource(nameof(LMT05500AgreementController));
        }

        [HttpPost]
        public IAsyncEnumerable<LMT05500AgreementDTO> AgreementListStream()
        {
            string lcMethodName = nameof(AgreementListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT05500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
          
            var loEx = new R_Exception();
            IAsyncEnumerable<LMT05500AgreementDTO> loRtn = null;
            List<LMT05500AgreementDTO> loRtnTemp;
            try
            {
                var loDbParameter = new LMT05500DBParameter();
                var loCls = new LMT05500AgreementCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                _loggerLMT05500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerLMT05500.LogInfo("Call method GetAgreementList");
                loRtnTemp = loCls.GetAgreementList(loDbParameter);
                _loggerLMT05500.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT05500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLMT05500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;

        }
        [HttpPost]
        public IAsyncEnumerable<LMT05500PropertyDTO> GetPropertyListStream()
        {
            string lcMethodName = nameof(GetPropertyListStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT05500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LMT05500PropertyDTO> loRtn = null;
            List<LMT05500PropertyDTO> loRtnTemp;
            try
            {
                var loDbParameter = new LMT05500DBParameter();
                var loCls = new LMT05500AgreementCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT05500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerLMT05500.LogInfo("Call method GetAllPropertyList");
                loRtnTemp = loCls.GetAllPropertyList(loDbParameter);
                _loggerLMT05500.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT05500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLMT05500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMT05500UnitDTO> GetDepositUnitStream()
        {
            string lcMethodName = nameof(GetDepositUnitStream);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLMT05500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<LMT05500UnitDTO> loRtn = null;
            List<LMT05500UnitDTO> loRtnTemp;
            try
            {
                var loDbParameter = new LMT05500DBParameter();
                var loCls = new LMT05500AgreementCls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loDbParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                _loggerLMT05500.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerLMT05500.LogInfo("Call method GetDepositUnitList");
                loRtnTemp = loCls.GetDepositUnitList(loDbParameter);
                _loggerLMT05500.LogInfo("Call method to streaming data");
                loRtn = HelperStream(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMT05500.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLMT05500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        private async IAsyncEnumerable<T> HelperStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}