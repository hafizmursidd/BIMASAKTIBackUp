using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using LMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using LMM01500COMMON.Interface;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace LMM01500Model
{
    public class LMM01500Model : R_BusinessObjectServiceClientBase<LMM01500InvoiceGroupDetailDTO>, ILMM01500InvoiceGroup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01500";
        private const string DEFAULT_MODULE = "LM";
        public LMM01500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public LMM01500PropertyListDTO GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01500InvoiceGroupDTO> GetInvoceGroupList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500PropertyListDTO> GetPropertyAsyncModel()
        {
            var loEx = new R_Exception();
            LMM01500PropertyListDTO loResult = new LMM01500PropertyListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500InvoiceGroup.GetPropertyList),
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

        public async Task<LMM01500InvoiceGroupListDTO> GetInvoiceGroupAsyncModel()
        {
            var loEx = new R_Exception();
            LMM01500InvoiceGroupListDTO loResult = new LMM01500InvoiceGroupListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loResultTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500InvoiceGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500InvoiceGroup.GetInvoceGroupList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.ListData = loResultTemp;
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