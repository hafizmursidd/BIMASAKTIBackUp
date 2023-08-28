using GSM06500Back;
using GSM06500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Data.Common;

namespace GSM06500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM06500Controller : ControllerBase, IGSM06500
    {
        
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM06500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            GSM06500Cls loCls;
            GSM06500DBParameter loDbParameter;

            try
            {
                loCls = new GSM06500Cls();
                loRtn = new R_ServiceDeleteResultDTO();
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
        public R_ServiceGetRecordResultDTO<GSM06500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM06500DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM06500DTO>();

            try
            {
                var loCls = new GSM06500Cls();
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
        public R_ServiceSaveResultDTO<GSM06500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM06500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM06500DTO> loRtn = null;
            GSM06500Cls loCls;

            try
            {
                loCls = new GSM06500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM06500DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

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
        public GSM06500PropertyListDTO GetAllPropertyList()
        {
            var loEx = new R_Exception();
            GSM06500PropertyListDTO loRtn = null;
            var loParameter = new GSM06500DBParameter();

            try
            {
                var loCls = new GSM06500Cls();
                loRtn = new GSM06500PropertyListDTO();

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
        public IAsyncEnumerable<GSM06500DTO> GetallTermOfpaymentOriginal()
        {
            var loEx = new R_Exception();
            GSM06500DBParameter loDbParameter;
            IAsyncEnumerable<GSM06500DTO> loRtn= null;
            List<GSM06500DTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM06500DBParameter();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; ;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                var loCls = new GSM06500Cls();

                loRtnTemp = loCls.TERM_OF_LIST(loDbParameter);
                loRtn = GetPaymentofTerm(loRtnTemp);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GSM06500DTO> GetPaymentofTerm(List<GSM06500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

    }
}