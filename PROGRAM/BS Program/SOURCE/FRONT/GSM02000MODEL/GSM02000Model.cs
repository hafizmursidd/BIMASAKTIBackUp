﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM02000Model
{
    public class GSM02000Model : R_BusinessObjectServiceClientBase<GSM02000DTO>, IGSM02000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02000";
        private const string DEFAULT_MODULE = "gs";

        public GSM02000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region not implemented
        public GSM02000ListDTO<GSM02000GridDTO> GetAllSalesTax()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream()
        {
            throw new NotImplementedException();
        }
        public GSM02000ListDTO<GSM02000RoundingDTO> GetAllRounding()
        {
            throw new NotImplementedException();
        }
        public GSM02000ActiveInactiveDTO SetActiveInactive(GSM02000ActiveInactiveParamsDTO poParams)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetAllStreamAsync

        public async Task<List<GSM02000GridDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM02000GridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02000GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000.GetAllSalesTaxStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

        #region GetRoundingMode

        public async Task<GSM02000ListDTO<GSM02000RoundingDTO>> GetRoundingModeAsync()
        {
            var loEx = new R_Exception();
            GSM02000ListDTO<GSM02000RoundingDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM02000ListDTO<GSM02000RoundingDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000.GetAllRounding),
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

        #endregion
        
        #region SetActiveInactive
        public async Task SetActiveInactiveAsync(GSM02000ActiveInactiveParamsDTO poParams)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<GSM02000ActiveInactiveDTO, GSM02000ActiveInactiveParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000.SetActiveInactive),
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
        }
        #endregion
    }
}