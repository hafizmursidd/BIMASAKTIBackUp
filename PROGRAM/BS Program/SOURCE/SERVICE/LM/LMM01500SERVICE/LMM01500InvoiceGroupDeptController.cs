using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM01500BACK;
using LMM01500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using LMM01500COMMON.Interface;

namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01500InvoiceGroupDeptController : ControllerBase, ILMM01500InvoiceGroupDept
    {
        public R_ServiceGetRecordResultDTO<LMM01500InvoiceGrpDeptDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01500InvoiceGrpDeptDetailDTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM01500InvoiceGrpDeptDetailDTO>();

            try
            {
                var loCls = new LMM01500InvoiceGroupDeptCls();
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

        public R_ServiceSaveResultDTO<LMM01500InvoiceGrpDeptDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01500InvoiceGrpDeptDetailDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM01500InvoiceGrpDeptDetailDTO> loRtn = null;
            LMM01500InvoiceGroupDeptCls loCls;

            try
            {
                loCls = new LMM01500InvoiceGroupDeptCls();
                loRtn = new R_ServiceSaveResultDTO<LMM01500InvoiceGrpDeptDetailDTO>();

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

        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500InvoiceGrpDeptDetailDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            LMM01500InvoiceGroupDeptCls loCls;

            try
            {
                loCls = new LMM01500InvoiceGroupDeptCls();
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

        public IAsyncEnumerable<LMM01500InvoiceGrpDeptDTO> GetInvoiceGroupDeptList()
        {
            var loEx = new R_Exception();
            LMM01500DBParam loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM01500InvoiceGrpDeptDTO> loRtn = null;
            List<LMM01500InvoiceGrpDeptDTO> loRtnTemp;

            try
            {
                loDbParameter = new LMM01500DBParam();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);

                var loCls = new LMM01500InvoiceGroupDeptCls();
                loRtnTemp = loCls.GetInvoiceGroupDeptList(loDbParameter);

                loRtn = GET_INVOICEGROUPDEPT_SERVICE(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        private async IAsyncEnumerable<LMM01500InvoiceGrpDeptDTO> GET_INVOICEGROUPDEPT_SERVICE(List<LMM01500InvoiceGrpDeptDTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }


    }
}
