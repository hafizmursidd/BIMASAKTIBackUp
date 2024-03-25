using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMT01500Common.Context;
using LMT01500Common.DTO._5._Invoice_Plan;
using LMT01500Common.Interface;
using LMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMT01500Model
{
    public class LMT01500InvoicePlanModel : R_BusinessObjectServiceClientBase<LMT01500BlankDTO>, ILMT01500InvoicePlan
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT01500InvoicePlan";
        private const string DEFAULT_MODULE = "LM";

        public LMT01500InvoicePlanModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMT01500InvoicePlanHeaderDTO> GetInvoicePlanHeaderAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new LMT01500InvoicePlanHeaderDTO();
            LMT01500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new LMT01500GetHeaderParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CDEPT_CODE = poParameter.CDEPT_CODE,
                        CREF_NO = poParameter.CREF_NO
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMT01500InvoicePlanHeaderDTO, LMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(ILMT01500InvoicePlan.GetInvoicePlanHeader),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<LMT01500InvoicePlanChargesListDTO>> GetInvoicePlanChargeListAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<LMT01500InvoicePlanChargesListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500InvoicePlanChargesListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500InvoicePlan.GetInvoicePlanChargeList),
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

        public async Task<List<LMT01500InvoicePlanListDTO>> GetInvoicePlanListAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<LMT01500InvoicePlanListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500InvoicePlanListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500InvoicePlan.GetInvoicePlanList),
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
        public IAsyncEnumerable<LMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeList()
        {
            throw new NotImplementedException();
        }

        public LMT01500InvoicePlanHeaderDTO GetInvoicePlanHeader(LMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500InvoicePlanListDTO> GetInvoicePlanList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}