using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.Context;
using LMT01500Common.DTO._2._Agreement;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Interface;
using LMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace LMT01500Model
{
    public class LMT01500ChargesInfo_RevenueSharingModel : R_BusinessObjectServiceClientBase<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>, ILMT01500ChargesInfo_RevenueSharing
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT01500ChargesInfo_RevenueSharing";
        private const string DEFAULT_MODULE = "LM";

        public LMT01500ChargesInfo_RevenueSharingModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMT01500ChargesInfo_RevenueMinimumRentDTO>> GetRevenueMinimumRentListAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<LMT01500ChargesInfo_RevenueMinimumRentDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ChargesInfo_RevenueMinimumRentDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500ChargesInfo_RevenueSharing.GetRevenueMinimumRentList),
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

        public async Task<List<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>> GetRevenueSharingSchemeListAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500ChargesInfo_RevenueSharing.GetRevenueSharingSchemeList),
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


        #region Not Used!

        public IAsyncEnumerable<LMT01500ChargesInfo_RevenueMinimumRentDTO> GetRevenueMinimumRentList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> GetRevenueSharingSchemeList()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}