using LMM03700Common.DTO;
using LMM03700Common;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace LMM03700Model
{
    public class LMM03700Model : R_BusinessObjectServiceClientBase<TenantClassificationGroupDTO>, ILMM03700
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM03700";
        private const string DEFAULT_MODULE = "LM";
        public LMM03700Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<TenantClassificationGroupDTO>> GetTenantClassGroupListAsync()
        {
            var loEx = new R_Exception();
            List<TenantClassificationGroupDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantClassificationGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03700.GetTenantClassGroupList),
                    DEFAULT_MODULE, _SendWithContext,
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
