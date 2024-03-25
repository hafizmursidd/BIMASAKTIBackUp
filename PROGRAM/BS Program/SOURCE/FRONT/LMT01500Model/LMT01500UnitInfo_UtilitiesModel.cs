using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.Context;
using LMT01500Common.DTO._2._Agreement;
using LMT01500Common.DTO._3._Unit_Info;
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
    public class LMT01500UnitInfo_UtilitiesModel : R_BusinessObjectServiceClientBase<LMT01500UnitInfoUnit_UtilitiesDetailDTO>, ILMT01500UnitInfo_Utilities
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT01500UnitInfo_Utilities";
        private const string DEFAULT_MODULE = "LM";

        public LMT01500UnitInfo_UtilitiesModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMT01500UnitInfoUnit_UtilitiesListDTO>> GetUnitInfoListAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<LMT01500UnitInfoUnit_UtilitiesListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500UnitInfoUnit_UtilitiesListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500UnitInfo_Utilities.GetUnitInfoList),
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

        public async Task<List<LMT01500ComboBoxDTO>> GetComboBoxDataCCHARGES_TYPEAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500UnitInfo_Utilities.GetComboBoxDataCCHARGES_TYPE),
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

        public async Task<List<LMT01500ComboBoxStartInvoicePeriodDTO>> GetComboBoxDataCSTART_INV_PRDAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxStartInvoicePeriodDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxStartInvoicePeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500UnitInfo_Utilities.GetComboBoxDataCSTART_INV_PRD),
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
        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ComboBoxStartInvoicePeriodDTO> GetComboBoxDataCSTART_INV_PRD()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500UnitInfoUnit_UtilitiesListDTO> GetUnitInfoList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}