using LMM03700Common.DTO;
using System;
using System.Collections.Generic;
using LMM03700Common;
using R_BusinessObjectFront;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace LMM03700Model
{
    public class LMM03710Model : R_BusinessObjectServiceClientBase<TenantClassificationDTO>, ILMM03710
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM03710";
        private const string DEFAULT_MODULE = "LM";
        public LMM03710Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region Framework

        public IAsyncEnumerable<PropertyDTO> LMM03700GetPropertyData()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassificationGroupList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TenantClassificationDTO> GetTenantClassificationList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TenantDTO> GetAssignedTenantList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TenantDTO> GetAvailableTenantList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TenantDTO> GetTenantToMoveList()
        {
            throw new NotImplementedException();
        }

        public TenantResultDumpDTO AssignTenant(TenantParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public TenantResultDumpDTO MoveTenant(TenantMoveParamDTO poParam)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.LMM03700GetPropertyData),
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
        //GRID 1
        public async Task<List<TenantClassificationGroupDTO>> GetTenantClassGroupList()
        {
            var loEx = new R_Exception();
            List<TenantClassificationGroupDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantClassificationGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetTenantClassificationGroupList),
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
        //GRID 2
        public async Task<List<TenantClassificationDTO>> GetTenantClassificationListAsync()
        {
            var loEx = new R_Exception();
            List<TenantClassificationDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantClassificationDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetTenantClassificationList),
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
      
        //GRID 3
        public async Task<List<TenantDTO>> GetAssignedTenantListAsync()
        {
            var loEx = new R_Exception();
            List<TenantDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetAssignedTenantList),
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

        #region assign tenant
        public async Task<List<TenantDTO>> GetTenantToAssigntListAsync()
        {
            var loEx = new R_Exception();
            List<TenantDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetAvailableTenantList),
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
        public async Task AssignTenantAsync(TenantParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                await R_HTTPClientWrapper.R_APIRequestObject<TenantResultDumpDTO, TenantParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.AssignTenant),
                    poParam,
                    DEFAULT_MODULE
                    , _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region MoveTenant
        public async Task MoveTenantAsync(TenantMoveParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                await R_HTTPClientWrapper.R_APIRequestObject<TenantResultDumpDTO, TenantMoveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.MoveTenant),
                    poParam,
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
        public async Task<List<TenantDTO>> GetTenanToMoveListAsync()
        {
            var loEx = new R_Exception();
            List<TenantDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetTenantToMoveList),
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
        #endregion
    }
}
