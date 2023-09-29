using GST00500Back;
using GST00500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Data.Common;

namespace GST00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GST00500InboxController : ControllerBase, IGST00500
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GST00500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GST00500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public GST00500UserNameDTO GetUserName()
        {
            var loEx = new R_Exception();
            GST00500UserNameDTO loReturn = null;
            var loParameter = new GST00500DBParameter();
            var loCls = new GST00500Cls();
            try
            {
                loReturn = new GST00500UserNameDTO();
                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loReturn = loCls.GetUserName(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public GST00500RejectListDTO ReasonRejectList()
        {
            var loEx = new R_Exception();
            GST00500RejectListDTO loRtn = null;
            var loParameter = new GST00500DBParameter();
            var loCls = new GST00500Cls();
            try
            {
                loRtn = new GST00500RejectListDTO();
                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetReasonRejectList(loParameter);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        /*
        [HttpPost]
        public IAsyncEnumerable<GST00500ApprovalTransactionDTO> GetError(GST00500ParameterDBDTO loparameter)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GST00500ApprovalTransactionDTO> loRtn = null;
            List<GST00500ApprovalTransactionDTO> loRtnTemp = null;
            GST00500Cls loCls = null;
            try
            {
                loCls = new GST00500Cls();
                var CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                var CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtnTemp = loCls.GetErrorList(CCOMPANYID, CUSER_ID, loparameter.GUID_ID);
                loRtn = GetErrorList(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        
        private async IAsyncEnumerable<GST00500ApprovalTransactionDTO> GetErrorList(List<GST00500ApprovalTransactionDTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        */

        [HttpPost]
        public IAsyncEnumerable<GST00500DTO> ApprovalInboxListStream()
        {
            var loEx = new R_Exception();
            GST00500DBParameter loDbParameter;
            IAsyncEnumerable<GST00500DTO> loRtn = null;
            List<GST00500DTO> loRtnTemp;
            try
            {
                loDbParameter = new GST00500DBParameter();
                loDbParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CTRANS_TYPE = "I";

                var loCls = new GST00500Cls();
                loRtnTemp = loCls.Approval_Inbox_List(loDbParameter);
                loRtn = GetApprovalInboxList(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;

        }

        private async IAsyncEnumerable<GST00500DTO> GetApprovalInboxList(List<GST00500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}