using Lookup_LMBACK;
using Lookup_LMCOMMON;
using Lookup_LMCOMMON.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;

namespace Lookup_LMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupLMController : ControllerBase, IPublicLookupLM
    {
        [HttpPost]
        public IAsyncEnumerable<LML00100DTO> LML00100GetSalesTaxList(LML00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LML00100DTO> loRtn = null;
            List<LML00100DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loReturnTemp = loCls.GetAllSalesTax(poParameter);
                loRtn = GetLML00100GetSalesTaxList(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<LML00100DTO> GetLML00100GetSalesTaxList(List<LML00100DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList(LML00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LML00200DTO> loRtn = null;
            List<LML00200DTO> loReturnTemp;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loReturnTemp = loCls.GetAllUnitCharges(poParameter);
                loRtn = GetLML00200UnitChargesList(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<LML00200DTO> GetLML00200UnitChargesList(List<LML00200DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        [HttpPost]
        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList(LML00300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LML00300DTO> loRtn = null;
            List<LML00300DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loReturnTemp = loCls.GetAllSupervisor(poParameter);
                loRtn = GetLML00300SupervisorList(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<LML00300DTO> GetLML00300SupervisorList(List<LML00300DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        [HttpPost]
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList(LML00400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LML00400DTO> loRtn = null;
            List<LML00400DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loReturnTemp = loCls.GetAllUtilityCharges(poParameter);
                loRtn = GetLML00400UtilityChargesList(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<LML00400DTO> GetLML00400UtilityChargesList(List<LML00400DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
    
}