using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM05000Common.DTO;
using GSM05000Common.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM050000Model
{
    public class GSM05000TransactionModel : R_BusinessObjectServiceClientBase<GSM05000TransactionDetailDTO>, IGSM05000Transaction
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000Transaction";
        private const string DEFAULT_MODULE = "GS";
        public GSM05000TransactionModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM05000TransactionDTO> GetTransactionCodeListStream()
        {
            throw new NotImplementedException();
        }

        public GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList()
        {
            throw new NotImplementedException();
        }

        public GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GSM05000TransactionDTO>> GetAllTransactionStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM05000TransactionDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000TransactionDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000Transaction.GetTransactionCodeListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM05000ListDTO<GSM05000DelimiterDTO>> GetDelimiterAsync()
        {
            var loEx = new R_Exception();
            GSM05000ListDTO<GSM05000DelimiterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListDTO<GSM05000DelimiterDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000Transaction.GetDelimiterList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<GSM05000ExistDTO> CheckExistDataAsync(GSM05000TrxCodeParamsDTO poParams)
        {
            var loEx = new R_Exception();
            GSM05000ExistDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ExistDTO, GSM05000TrxCodeParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000Transaction.CheckExistData),
                    poParams,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
