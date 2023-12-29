using GSM05000Back;
using GSM05000Common.DTO;
using GSM05000Common.Interface;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM05000TransactionController : ControllerBase, IGSM05000Transaction
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM05000TransactionDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000TransactionDetailDTO> poParameter)
        {
            R_Exception loEx = new();
            R_ServiceGetRecordResultDTO<GSM05000TransactionDetailDTO> loRtn = new();

            try
            {
                var loCls = new GSM05000TransactionCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
        public R_ServiceSaveResultDTO<GSM05000TransactionDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000TransactionDetailDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000TransactionDetailDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<GSM05000TransactionDTO> GetTransactionCodeListStream()
        {
            R_Exception loEx = new R_Exception();
            IAsyncEnumerable<GSM05000TransactionDTO> loRtn = null;
            List<GSM05000TransactionDTO> loResult;
            GSM05000ParameterDb loDbPar;
            GSM05000TransactionCls loCls;

            try
            {
                loDbPar = new GSM05000ParameterDb();
                
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM05000TransactionCls();
                
                loResult = loCls.GetTransactionCodeList(loDbPar);
                loRtn = GetTransactionCodeStream(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList()
        {
           
            R_Exception loEx = new();
            GSM05000ListDTO<GSM05000DelimiterDTO> loRtn = null;
            List<GSM05000DelimiterDTO> loResult;
            GSM05000ParameterDb loDbPar;
            GSM05000TransactionCls loCls;

            try
            {
                loDbPar = new GSM05000ParameterDb();
                
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE_MENU;

                loCls = new GSM05000TransactionCls();
                
                loResult = loCls.GetDelimiterList(loDbPar);
                loRtn = new GSM05000ListDTO<GSM05000DelimiterDTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams)
        {
            throw new NotImplementedException();
        }
        
        private async IAsyncEnumerable<GSM05000TransactionDTO> GetTransactionCodeStream(List<GSM05000TransactionDTO> poParameter)
        {
            foreach (GSM05000TransactionDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}