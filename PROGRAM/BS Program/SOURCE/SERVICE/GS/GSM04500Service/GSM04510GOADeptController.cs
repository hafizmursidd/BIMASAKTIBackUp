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
    public class GSM04510GOADeptController : ControllerBase, IGSM04510GOADept
    {

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04510GOADeptDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            GSM04510GOADeptCls loCls;
            GSM04510GOADeptDBParameter loDbParameter;

            try
            {
                loCls = new GSM04510GOADeptCls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.R_Delete(poParameter.Entity);
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
        public R_ServiceGetRecordResultDTO<GSM04510GOADeptDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04510GOADeptDTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04510GOADeptDTO>();

            try
            {
                var loCls = new GSM04510GOADeptCls();
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
        public R_ServiceSaveResultDTO<GSM04510GOADeptDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04510GOADeptDTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<GSM04510GOADeptDTO>();
            GSM04510GOADeptCls loCls;

            try
            {
                loCls = new GSM04510GOADeptCls();
                loRtn = new R_ServiceSaveResultDTO<GSM04510GOADeptDTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            };
            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM04510GOADeptDTO> JOURNAL_GRP_GOA_DEPT_LIST()
        {
            var loEx = new R_Exception();
            GSM04510GOADeptDBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM04510GOADeptDTO> loRtn = null;
            List<GSM04510GOADeptDTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM04510GOADeptDBParameter();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);
                loDbParameter.CJRNGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJOURNAL_GRP_CODE);
                loDbParameter.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGOA_CODE);


                var loCls = new GSM04510GOADeptCls();

                loRtnTemp = loCls.JOURNAL_GROUP_GOA_DEPT_LIST(loDbParameter);
                loRtn = GET_JOURNAL_GRP_GOA_DEPT_LIST(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM04510GOADeptDTO> GET_JOURNAL_GRP_GOA_DEPT_LIST(List<GSM04510GOADeptDTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
