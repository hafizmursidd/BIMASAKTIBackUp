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
        public GST00500ApprovalStatusListDTO GetApprovalStatus()
        {
            var loEx = new R_Exception();
            var loParameter = new GST00500DBParameter();
            var poEntity = new GST00500DTO();
            var loCls = new GST00500OutboxCls();
            var loRtn = new GST00500ApprovalStatusListDTO();
            try
            {
                loParameter.CCOMPANYID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID =  R_BackGlobalVar.CULTURE;
                poEntity.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTTRANSACTION_CODE);
                poEntity.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                poEntity.CREFERENCE_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREFERENCE_NO);

                //loParameter.CCOMPANYID = "RCD";
                //loParameter.CLANGUAGE_ID = "EN";
                //poEntity.CTRANSACTION_CODE = "000000";
                //poEntity.CDEPT_CODE = "ACC";
                //poEntity.CREFERENCE_NO = "REF.202306.0001";

                var temp =  loCls.ApprovalStatus(poEntity, loParameter);
                loRtn.Data = temp;
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
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                
                //loDbParameter.CUSER_ID = "HPC";
                //loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.COMPANY_ID;
                // loDbParameter.CLANGUAGE_ID = "En";

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
