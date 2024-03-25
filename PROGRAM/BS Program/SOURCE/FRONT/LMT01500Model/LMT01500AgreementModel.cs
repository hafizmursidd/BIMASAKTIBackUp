using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._2._Agreement;
using LMT01500Common.Interface;
using LMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMT01500Model
{
    public class LMT01500AgreementModel : R_BusinessObjectServiceClientBase<LMM01500AgreementDetailDTO>, ILMT01500Agreement
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT01500Agreement";
        private const string DEFAULT_MODULE = "LM";

        public LMT01500AgreementModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMT01500ComboBoxDTO>> GetComboBoxDataCChargesModeAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500Agreement.GetComboBoxDataCChargesMode),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<LMT01500ComboBoxDTO>> GetComboBoxDataCLeaseModeAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500Agreement.GetComboBoxDataCLeaseMode),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        #region NotUsed        
        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCChargesMode()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCLeaseMode()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}