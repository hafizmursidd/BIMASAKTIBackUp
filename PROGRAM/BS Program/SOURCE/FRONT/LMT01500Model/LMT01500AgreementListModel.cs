using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMT01500Common.Context;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.Interface;
using LMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMT01500Model
{
    public class LMT01500AgreementListModel : R_BusinessObjectServiceClientBase<LMT01500BlankDTO>, ILMT01500AgreementList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT01500AgreementList";
        private const string DEFAULT_MODULE = "LM";

        public LMT01500AgreementListModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                pcModule,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<LMT01500AgreementListOriginalDTO>> GetAgreementListAsync(string pcCPROPERTY_ID)
        {
            var loEx = new R_Exception();
            List<LMT01500AgreementListOriginalDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(LMT01500GetAgreementListContextDTO.CPROPERTY_ID, pcCPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500AgreementListOriginalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500AgreementList.GetAgreementList),
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

        public async Task<List<LMT01500PropertyListDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500PropertyListDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500AgreementList.GetPropertyList),
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

        public async Task<List<LMT01500VarGsmTransactionCodeDTO>> GetVarGsmTransactionCodeAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500VarGsmTransactionCodeDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500AgreementList.GetVarGsmTransactionCode),
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

        public async Task<LMT01500ProcessResultDTO> ProcessChangeStatusAsync(LMT01500ChangeStatusParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            var loResult = new LMT01500ProcessResultDTO();
            LMT01500ChangeStatusParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poEntity.CPROPERTY_ID))
                {
                    loParam = new LMT01500ChangeStatusParameterDTO()
                    {
                        CCOMPANY_ID = poEntity.CCOMPANY_ID,
                        CPROPERTY_ID = poEntity.CPROPERTY_ID,
                        CTRANS_CODE = poEntity.CTRANS_CODE,
                        CDOC_NO = poEntity.CDOC_NO,
                        CACCEPT_DATE = poEntity.CACCEPT_DATE,
                        CCHANGE_STATUS_TO = poEntity.CCHANGE_STATUS_TO,
                        CREASON = poEntity.CREASON,
                        CNOTES = poEntity.CNOTES
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMT01500ProcessResultDTO, LMT01500ChangeStatusParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(ILMT01500AgreementList.GetChangeStatus),
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

        public async Task<LMT01500ChangeStatusDTO> GetChangeStatusAsync(LMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new LMT01500ChangeStatusDTO();
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
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMT01500ChangeStatusDTO, LMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(ILMT01500AgreementList.GetChangeStatus),
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

        public async Task<List<LMT01500ComboBoxDTO>> GetComboBoxDataCTransStatusAsync()
        {
            var loEx = new R_Exception();
            List<LMT01500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT01500AgreementList.GetComboBoxDataCTransStatus),
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
        public IAsyncEnumerable<LMT01500AgreementListOriginalDTO> GetAgreementList()
        {
            throw new NotImplementedException();
        }

        public LMT01500ChangeStatusDTO GetChangeStatus(LMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCTransStatus()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500PropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT01500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode()
        {
            throw new NotImplementedException();
        }

        public LMT01500ProcessResultDTO ProcessChangeStatus(LMT01500ChangeStatusParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}