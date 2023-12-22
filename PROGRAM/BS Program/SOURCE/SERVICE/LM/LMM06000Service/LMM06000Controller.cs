﻿using LMM06000Back;
using LMM06000Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using LMM06000Common.Logs;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LMM06000Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06000Controller : ControllerBase, ILMM06000
    {
        private LoggerLMM06000 _loggerLMM06000;

        public LMM06000Controller(ILogger<LMM06000Controller> logger)
        {
            //Initial and Get Logger
            LoggerLMM06000.R_InitializeLogger(logger);
            _loggerLMM06000 = LoggerLMM06000.R_GetInstanceLogger();
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM06000BillingRuleDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM06000BillingRuleDetailDTO> poParameter)
        {
            _loggerLMM06000.LogInfo("START process method R_ServiceGetRecord on Controller");

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM06000BillingRuleDetailDTO>();

            try
            {
                var loCls = new LMM06000Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM06000.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMM06000.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("END process method R_ServiceGetRecord on Controller");

            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<LMM06000BillingRuleDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM06000BillingRuleDetailDTO> poParameter)
        {
            _loggerLMM06000.LogInfo("START process method R_ServiceSave on Controller");

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM06000BillingRuleDetailDTO> loRtn = null;
            LMM06000Cls loCls;

            try
            {
                loCls = new LMM06000Cls();
                loRtn = new R_ServiceSaveResultDTO<LMM06000BillingRuleDetailDTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM06000.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("END process method R_ServiceSave on Controller");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM06000BillingRuleDetailDTO> poParameter)
        {
            _loggerLMM06000.LogInfo("START process method R_ServiceDelete on Controller");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            LMM06000Cls loCls;

            try
            {
                loCls = new LMM06000Cls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM06000.LogInfo("Call method R_Delete");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMM06000.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("END process method R_ServiceDelete on Controller");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06000BillingRuleDTO> BillingRuleListStream()
        {
            _loggerLMM06000.LogInfo("START process method BillingRuleListStream on Controller");
            var loEx = new R_Exception();
            LMM06000DBParameter loDbParameter;
            IAsyncEnumerable<LMM06000BillingRuleDTO> loRtn = null;
            List<LMM06000BillingRuleDTO> loRtnTemp;

            try
            {
                loDbParameter = new LMM06000DBParameter();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; ;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CUNIT_TYPE_CTG_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_TYPE_ID);
                loDbParameter.LACTIVE_ONLY = false; // this value false
                
                _loggerLMM06000.LogInfo("Get Parameter BillingRuleListStream on Controller");
                _loggerLMM06000.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                var loCls = new LMM06000Cls();
                _loggerLMM06000.LogInfo("Call method BillingRuleList");
                loRtnTemp = loCls.BillingRuleList(loDbParameter);
                _loggerLMM06000.LogInfo("Call method GetBillingRule to streaming data");
                loRtn = Get_BillingRuleList(loRtnTemp);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMM06000.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerLMM06000.LogInfo("END process method BillingRuleListStream on Controller");
            return loRtn;
        }

        [HttpPost]
        public LMM06000PropertyListDTO GetAllPropertyList()
        {
            _loggerLMM06000.LogInfo("START process method GetAllPropertyList on Controller");

            var loEx = new R_Exception();
            LMM06000PropertyListDTO loRtn = null;
            var loParameter = new LMM06000DBParameter();

            try
            {
                var loCls = new LMM06000Cls();
                loRtn = new LMM06000PropertyListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM06000.LogInfo("Call method GetAllPropertyList");
                var loResult = loCls.GetAllPropertyList(loParameter);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMM06000.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("END process method GetAllPropertyList on Controller");
            return loRtn;
        }

        [HttpPost]
        public LMM06000PeriodListDTO GetAllPeriodList()
        {
            _loggerLMM06000.LogInfo("START process method GetAllPeriodList on Controller");
            var loEx = new R_Exception();
            LMM06000PeriodListDTO loRtn = null;

            var loParameter = new LMM06000DBParameter();

            try
            {
                var loCls = new LMM06000Cls();
                loRtn = new LMM06000PeriodListDTO();
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CULTURE = R_BackGlobalVar.CULTURE;
                _loggerLMM06000.LogInfo("Call method GetAllPeriodList");
                var loResult = loCls.GetAllPeriodList(loParameter);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMM06000.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("END process method GetAllPeriodList on Controller");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06000UnitTypeDTO> GetAllUnitTypeList()
        {
            _loggerLMM06000.LogInfo("START process method GetAllUnitTypeList on Controller");

            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06000UnitTypeDTO> loRtn = null;
            List<LMM06000UnitTypeDTO> loRtnTemp;

            try
            {
                var loDbParameter = new LMM06000DBParameter();
                var loCls = new LMM06000Cls();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                _loggerLMM06000.LogDebug("DbParameter {@Parameter} ", loDbParameter);

                _loggerLMM06000.LogInfo("Call method GetAllUnitTypeList");
                loRtnTemp = loCls.GetAllUnitTypeList(loDbParameter);
                _loggerLMM06000.LogInfo("Call method UnitType to streaming data");
                loRtn = Get_UnitType(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMM06000.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo("END process method GetAllUnitTypeList on Controller");

            return loRtn;
        }

        [HttpPost]
        public LMM06000ActiveInactiveDTO SetActiveInactive(LMM06000ActiveInactiveDTO loParameter)
        {
            string lcMethodName = nameof(SetActiveInactive);
            _loggerLMM06000.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

           R_Exception loEx = new R_Exception();
            LMM06000ActiveInactiveDTO loRtn = null;
            try
            {
                loRtn = new LMM06000ActiveInactiveDTO();
                LMM06000ActiveInactiveParameterDb loDbPar = new LMM06000ActiveInactiveParameterDb();
                LMM06000Cls loCls = new LMM06000Cls();
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;


                _loggerLMM06000.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLMM06000.LogDebug("{@Parameter} ", loParameter);

                _loggerLMM06000.LogInfo("Call method SetActiveInactive");
                loCls.SetActiveInactiveDb(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLMM06000.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLMM06000.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
           
            return loRtn;
        }
        private async IAsyncEnumerable<LMM06000BillingRuleDTO> Get_BillingRuleList(List<LMM06000BillingRuleDTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        private async IAsyncEnumerable<LMM06000UnitTypeDTO> Get_UnitType(List<LMM06000UnitTypeDTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

    }
}