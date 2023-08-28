using GLB00200Back;
using GLB00200Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLB00200Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB00200Controller : ControllerBase, IGLB00200
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLB00200DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLB00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GLB00200DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLB00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLB00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public GLB00200InitalProcessDTO GetInitialProcess()
        {
            R_Exception loException = new R_Exception();
            GLB00200DBParameter loDbParameter = new();
            GLB00200InitalProcessDTO loReturn = null;
            try
            {
                var loCls = new GLB00200Cls();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loReturn = loCls.InitialProcess(loDbParameter);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        [HttpPost]
        public GLB00200JournalDetailListDTO DetailReversingJournalProcessList(GLB00200DTO loParam)
        {
            R_Exception loException = new R_Exception();
            GLB00200DBParameter loDbParameter;
            GLB00200JournalDetailListDTO loReturn = new();
            try
            {
                loDbParameter = new GLB00200DBParameter();
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CREC_ID = loParam.CREC_ID;

                var loCls = new GLB00200Cls();
                var temp = loCls.GetDetail_ReversingJournalList(loDbParameter);
                loReturn.Data = temp;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

       [HttpPost]
        public IAsyncEnumerable<GLB00200DTO> ReversingJournalProcessListStream()
        {
            R_Exception loException = new R_Exception();
            GLB00200DBParameter loDbParameter;
            List<GLB00200DTO> loReturnTemp;
            IAsyncEnumerable<GLB00200DTO> loRtn = null;
            try
            {
                loDbParameter = new GLB00200DBParameter();
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loDbParameter.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loDbParameter.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);
                var loCls = new GLB00200Cls();
                loReturnTemp = loCls.ReversingJournalProcessList(loDbParameter);
                loRtn = Get_ReversingJournalProcessList(loReturnTemp);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GLB00200DTO> Get_ReversingJournalProcessList(List<GLB00200DTO> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}