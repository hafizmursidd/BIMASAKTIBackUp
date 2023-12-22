using LMM01500COMMON;
using LMM01500COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500Model
{
    public class LMM01500ChargesModel : R_BusinessObjectServiceClientBase<LMM01500ChargesDTO>, ILMM01500Charges
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01500Charges";
        private const string DEFAULT_MODULE = "LM";

        public LMM01500ChargesModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM01500ChargesDTO> GetAllOtherChargerList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500ChargesListDTO> GetAllOtherChargerListAsyncModel()
        {
            var loEx = new R_Exception();
            LMM01500ChargesListDTO loResult = new LMM01500ChargesListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loResultTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500ChargesDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500Charges.GetAllOtherChargerList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loResultTemp;
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
