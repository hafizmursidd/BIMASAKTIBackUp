using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GST00500Model
{
    public class GST00500OutboxModel :R_BusinessObjectServiceClientBase<GST00500DTO>, IGST00500Outbox
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GST00500Outbox";
        private const string DEFAULT_MODULE = "GS";
        public GST00500OutboxModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<GST00500DTO> ApprovalOutboxListStream()
        {
            throw new NotImplementedException();
        }

        public GST00500ApprovalStatusListDTO GetApprovalStatus()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GST00500DTO>> GetOutboxListAsyncModel()
        {
            var loEx = new R_Exception();
            List<GST00500DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GST00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGST00500Outbox.ApprovalOutboxListStream),
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
        
        public async Task<GST00500ApprovalStatusListDTO> GetApprovalStatusAsyncModel(GST00500DTO poEntity)
        {
            var loEx = new R_Exception();
            GST00500ApprovalStatusListDTO loResult = null;

            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CTTRANSACTION_CODE, poEntity.CTRANSACTION_CODE);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CREFERENCE_NO, poEntity.CREFERENCE_NO);;

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GST00500ApprovalStatusListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGST00500Outbox.GetApprovalStatus),
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
