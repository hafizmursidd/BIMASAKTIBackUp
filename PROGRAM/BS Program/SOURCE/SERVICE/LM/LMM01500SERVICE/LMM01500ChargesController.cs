using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using R_CommonFrontBackAPI;
using LMM01500BACK;
using R_BackEnd;
using R_Common;
using LMM01500COMMON.Interface;
namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01500ChargesController : ControllerBase, ILMM01500Charges
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01500ChargesDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01500ChargesDTO> poParameter)
        {
            var loEx = new R_Exception();
          var  loRtn = new R_ServiceGetRecordResultDTO<LMM01500ChargesDTO>();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new LMM01500ChargesCls();
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
        public R_ServiceSaveResultDTO<LMM01500ChargesDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01500ChargesDTO> poParameter)
        {
            var loEx = new R_Exception();
            var  loRtn = new R_ServiceSaveResultDTO<LMM01500ChargesDTO>();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new LMM01500ChargesCls();
                
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500ChargesDTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new LMM01500ChargesCls();
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMM01500ChargesDTO> GetAllOtherChargerList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01500ChargesDTO> loRtn = null;
            List<LMM01500ChargesDTO> loTempRtn = null;
            var loParameter = new LMM01500ChargesDTO();

            try
            {
                var loCls = new LMM01500ChargesCls();
                loTempRtn = new List<LMM01500ChargesDTO>();
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);
                
                loTempRtn = loCls.GetListOtherCharges(loParameter);
                loRtn = GetAllOtherChargerListStream<LMM01500ChargesDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<T> GetAllOtherChargerListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

    }
}
