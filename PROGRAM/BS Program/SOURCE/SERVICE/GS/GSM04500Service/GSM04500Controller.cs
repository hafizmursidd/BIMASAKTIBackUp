using GSM04500Back;
using GSM04500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04500Controller : ControllerBase, IGSM04500
    {
        [HttpPost]
        public GSM04500PropertyListDTO GetAllPropertyList()
        {
            var loEx = new R_Exception();
            GSM04500PropertyListDTO loRtn = null;
            var loParameter = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04500Cls();
                loRtn = new GSM04500PropertyListDTO();

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
        public GSM04500JournalGroupTypeListDTO GetAllJournalGroupTypeList()
        {
            var loEx = new R_Exception();
            GSM04500JournalGroupTypeListDTO loResult = null;
            var loParameter = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04500Cls();
                loResult = new GSM04500JournalGroupTypeListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResultTemp = loCls.GetAllJournalGroupTypeList(loParameter);
                loResult.Data = loResultTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            GSM04500Cls loCls;
            GSM04500DBParameter loDbParameter;

            try
            {
                loCls = new GSM04500Cls();
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
        public R_ServiceGetRecordResultDTO<GSM04500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04500DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04500DTO>();

            try
            {
                var loCls = new GSM04500Cls();
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
        public R_ServiceSaveResultDTO<GSM04500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM04500DTO> loRtn = null;
            GSM04500Cls loCls;

            try
            {
                loCls = new GSM04500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM04500DTO>();

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
        public IAsyncEnumerable<GSM04500DTO> GET_JOURNAL_GRP_LIST_STREAM()
        {
            var loEx = new R_Exception();
            GSM04500DBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM04500DTO> loRtn = null;
            List<GSM04500DTO> loRtnTemp;

            try
            {
                loDbParameter = new GSM04500DBParameter();

                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loDbParameter.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);

                var loCls = new GSM04500Cls();

                loRtnTemp = loCls.JOURNAL_GROUP_LIST(loDbParameter);
                loRtn = GET_JOURNAL_GROUP_LIST_SERVICE(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM04500DTO> GET_JOURNAL_GROUP_LIST_SERVICE(List<GSM04500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }



    }
}