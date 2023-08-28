using GSM04500Back;
using GSM04500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM04500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04510GOAController : ControllerBase, IGSM04510GOA
    {

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04510GOADTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04510GOADTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04510GOADTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04510GOADTO>();

            try
            {
                var loCls = new GSM04510GOACls();
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
        public R_ServiceSaveResultDTO<GSM04510GOADTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04510GOADTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM04510GOADTO> loRtn = null;
            GSM04510GOACls loCls;

            try
            {
                loCls = new GSM04510GOACls();
                loRtn = new R_ServiceSaveResultDTO<GSM04510GOADTO>();

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
        public IAsyncEnumerable<GSM04510GOADTO> JOURNAL_GRP_GOA_LIST()
        {
            var loEx = new R_Exception();
            GSM04510GOADBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM04510GOADTO> loRtn = null;
            List<GSM04510GOADTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM04510GOADBParameter();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);
                loDbParameter.CJOURNAL_GRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJOURNAL_GRP_CODE);

                //loDbParameter.CCOMPANY_ID = "RCD";
                //loDbParameter.CUSER_ID = "HMC";
                //loDbParameter.CPROPERTY_ID = "JBMPC";
                //loDbParameter.CJRNGRP_TYPE = "10";
                //loDbParameter.CJRNGRP_CODE = "A";

                var loCls = new GSM04510GOACls();

                loRtnTemp = loCls.JOURNAL_GROUP_GOA_LIST(loDbParameter);
                loRtn = GET_JOURNAL_GROUP_GOA_LIST(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM04510GOADTO> GET_JOURNAL_GROUP_GOA_LIST(List<GSM04510GOADTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
