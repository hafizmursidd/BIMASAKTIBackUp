using GST00500Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using GST00500Back;
using R_BackEnd;
using R_Common;

namespace GST00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GST00500OutboxController : ControllerBase, IGST00500Outbox
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
        public List<GST00500ApprovalStatusDTO> GetApprovalStatusList()
        {
            var loEx = new R_Exception();
            GST00500DBParameter loParameter = null;
            GST00500OutboxCls loCls = null;
            List<GST00500ApprovalStatusDTO> loRtn = null;
            try
            {
                loCls = new GST00500OutboxCls();
                loParameter = new GST00500DBParameter();
                loRtn = new List<GST00500ApprovalStatusDTO>();

                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                loParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);

                loRtn = loCls.ApproverStatusList(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GST00500DTO> ApprovalOutboxListStream()
        {
            var loEx = new R_Exception();
            GST00500DBParameter loDbParameter;
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GST00500DTO> loRtn = null;
            List<GST00500DTO> loRtnTemp;
            try
            {
                loDbParameter = new GST00500DBParameter();

                loDbParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CTRANS_TYPE = "O";


                var loCls = new GST00500OutboxCls();
                loRtnTemp = loCls.Approval_Outbox_List(loDbParameter);
                loRtn = GetApprovalOutboxList(loRtnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GST00500DTO> GetApprovalOutboxList(List<GST00500DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
