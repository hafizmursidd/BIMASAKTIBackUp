using LMM01500BACK;
using LMM01500COMMON;
using LMM01500COMMON.Interface;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01500Controller : ControllerBase, ILMM01500InvoiceGroup
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01500InvoiceGroupDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01500InvoiceGroupDetailDTO> poParameter)
        {

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM01500InvoiceGroupDetailDTO>();

            try
            {
                var loCls = new LMM01500InvoiceGroupCls();
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
        public R_ServiceSaveResultDTO<LMM01500InvoiceGroupDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01500InvoiceGroupDetailDTO> poParameter)
        {

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM01500InvoiceGroupDetailDTO> loRtn = null;
            LMM01500InvoiceGroupCls loCls;

            try
            {
                loCls = new LMM01500InvoiceGroupCls();
                loRtn = new R_ServiceSaveResultDTO<LMM01500InvoiceGroupDetailDTO>();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500InvoiceGroupDetailDTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            LMM01500InvoiceGroupCls loCls;

            try
            {
                loCls = new LMM01500InvoiceGroupCls();
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
        public LMM01500PropertyListDTO GetPropertyList()
        {

            var loEx = new R_Exception();
            LMM01500PropertyListDTO loRtn = null;
            var loParameter = new LMM01500DBParam();
            try
            {
                var loCls = new LMM01500InvoiceGroupCls();
                loRtn = new LMM01500PropertyListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn = loCls.GetPropertyList(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01500InvoiceGroupDTO> GetInvoceGroupList()
        {
            var loEx = new R_Exception();
            LMM01500DBParam loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM01500InvoiceGroupDTO> loRtn = null;
            List<LMM01500InvoiceGroupDTO> loRtnTemp;

            try
            {
                loDbParameter = new LMM01500DBParam();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);


                var loCls = new LMM01500InvoiceGroupCls();
                loRtnTemp = loCls.GetInvoiceGroupList(loDbParameter);

                loRtn = GET_INVOICEGROUP_SERVICE(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        private async IAsyncEnumerable<LMM01500InvoiceGroupDTO> GET_INVOICEGROUP_SERVICE(List<LMM01500InvoiceGroupDTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }


    }
}