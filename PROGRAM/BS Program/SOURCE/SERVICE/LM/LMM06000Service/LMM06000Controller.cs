using LMM06000Back;
using LMM06000Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM06000Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06000Controller : ControllerBase, ILMM06000
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM06000BillingRuleDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM06000BillingRuleDetailDTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM06000BillingRuleDetailDTO>();

            try
            {
                var loCls = new LMM06000Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
        public R_ServiceSaveResultDTO<LMM06000BillingRuleDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM06000BillingRuleDetailDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM06000BillingRuleDetailDTO> loRtn = null;
            LMM06000Cls loCls;

            try
            {
                loCls = new LMM06000Cls();
                loRtn = new R_ServiceSaveResultDTO<LMM06000BillingRuleDetailDTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM06000BillingRuleDetailDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            LMM06000Cls loCls;

            try
            {
                loCls = new LMM06000Cls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06000BillingRuleDTO> BillingRuleListStream()
        {
            {
                var loEx = new R_Exception();
                LMM06000DBParameter loDbParameter;
                IAsyncEnumerable<LMM06000BillingRuleDTO> loRtn = null;
                List<LMM06000BillingRuleDTO> loRtnTemp;

                try
                {
                    loDbParameter = new LMM06000DBParameter();

                    loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    ;
                    loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                    loDbParameter.CUNIT_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_TYPE_ID);
                    loDbParameter.LACTIVE_ONLY = false; //sesuai pernyataan dari bu Reni

                    var loCls = new LMM06000Cls();
                    loRtnTemp = loCls.BillingRuleList(loDbParameter);
                    loRtn = Get_BillingRuleList(loRtnTemp);

                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.ThrowExceptionIfErrors();

                return loRtn;
            }
        }

        [HttpPost]
        public LMM06000PropertyListDTO GetAllPropertyList()
        {
            var loEx = new R_Exception();
            LMM06000PropertyListDTO loRtn = null;
            var loParameter = new LMM06000DBParameter();

            try
            {
                var loCls = new LMM06000Cls();
                loRtn = new LMM06000PropertyListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllPropertyList(loParameter);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM06000PeriodListDTO GetAllPeriodList()
        {
            var loEx = new R_Exception();
            LMM06000PeriodListDTO loRtn = null;

            var loParameter = new LMM06000DBParameter();

            try
            {
                var loCls = new LMM06000Cls();
                loRtn = new LMM06000PeriodListDTO();
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CULTURE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllPeriodList(loParameter);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06000UnitTypeDTO> GetAllUnitTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06000UnitTypeDTO> loRtn = null;
            List<LMM06000UnitTypeDTO> loRtnTemp;

            var loParameter = new LMM06000DBParameter();
            try
            {
                var loCls = new LMM06000Cls();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUNIT_TYPE_ID);
                
                loRtnTemp = loCls.GetAllUnitTypeList(loParameter);
                loRtn = Get_UnitType(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM06000ActiveInactiveDTO SetActiveInactive(LMM06000ActiveInactiveDTO loParameter)
        {
            R_Exception loEx = new R_Exception();
            LMM06000ActiveInactiveParameterDb loDbPar = new LMM06000ActiveInactiveParameterDb();
            LMM06000ActiveInactiveDTO loRtn = new LMM06000ActiveInactiveDTO();
            LMM06000Cls loCls = new LMM06000Cls();

            try
            {
                //loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CPROPERTY_ID = loParameter.CPROPERTY_ID;
                //loDbPar.CUNIT_TYPE_ID = loParameter.CUNIT_TYPE_ID;
                //loDbPar.CBILLING_RULE_CODE = loParameter.CBILLING_RULE_CODE;
                //loDbPar.LACTIVE = loParameter.LACTIVE;

                loCls.SetActiveInactiveDb(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

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