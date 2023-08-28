using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_CommonFrontBackAPI;
using GST00500Back;
using R_BackEnd;
using R_Common;

namespace GST00500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GST00500DraftController : ControllerBase, IGST00500Draft
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
        public IAsyncEnumerable<GST00500DTO> ApprovalDraftListStream()
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
               // loDbParameter.CUSER_ID = "HPC";

                var loCls = new GST00500DraftCls();
                loRtnTemp = loCls.Approval_Draft_List(loDbParameter);
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
