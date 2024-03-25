using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMT01500Common.Context;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Interface;
using LMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMT01500Model
{
    public class LMT01500ChargesInfoModel : R_BusinessObjectServiceClientBase<LMT01500ChargesInfoDetailDTO>, ILMT01500ChargesInfo
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT01500ChargesInfo";
        private const string DEFAULT_MODULE = "LM";

        public LMT01500ChargesInfoModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<LMT01500ChargesInfoHeaderDTO> GetChargesInfoHeaderAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new LMT01500ChargesInfoHeaderDTO();
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
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMT01500ChargesInfoHeaderDTO, LMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(ILMT01500ChargesInfo.GetChargesInfoHeader),
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

        public async Task<List<LMT01500ChargesInfoListDTO>> GetChargesInfoListAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<LMT01500ChargesInfoListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ChargesInfoListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500ChargesInfo.GetChargesInfoList),
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

        public async Task<List<LMT01500ComboBoxDTO>> GetComboBoxDataCFEE_METHODAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500ChargesInfo.GetComboBoxDataCFEE_METHOD),
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

        public async Task<List<LMT01500ComboBoxDTO>> GetComboBoxDataCINVOICE_PERIODAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500ChargesInfo.GetComboBoxDataCINVOICE_PERIOD),
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

        public LMT01500ChargesInfoHeaderDTO GetChargesInfoHeader(LMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ChargesInfoListDTO> GetChargesInfoList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCFEE_METHOD()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCINVOICE_PERIOD()
        {
            throw new NotImplementedException();
        }

        #endregion
    
    }
}