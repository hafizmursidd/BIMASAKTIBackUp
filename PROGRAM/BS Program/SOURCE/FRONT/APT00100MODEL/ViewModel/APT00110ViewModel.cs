using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs;
using System.ComponentModel.Design.Serialization;
using APT00100COMMON.DTOs.APT00100;

namespace APT00100MODEL.ViewModel
{
    public class APT00110ViewModel : R_ViewModel<APT00110DTO>
    {
        private APT00110Model loModel = new APT00110Model();

        private APT00100Model loInvoiceListModel = new APT00100Model();

        public APT00110DTO loInvoiceHeader = null;

        public List<GetPaymentTermListDTO> loPaymentTermList = null;

        public GetPaymentTermListResultDTO loPaymentTermRtn = null;

        public GetPaymentTermListDTO loPaymentTerm = new GetPaymentTermListDTO();

        public List<GetCurrencyListDTO> loCurrencyList = null;

        public GetCurrencyListResultDTO loCurrencyRtn = null;

        public GetCurrencyListDTO loCurrency = new GetCurrencyListDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        public APT00110ResultDTO loRtn = null;

        public GetCurrencyOrTaxRateDTO loCurrencyRate = null;

        public GetCurrencyOrTaxRateResultDTO loCurrencyRateRtn = null;

        public GetCurrencyOrTaxRateDTO loTaxRate = null;

        public GetCurrencyOrTaxRateResultDTO loTaxRateRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public GetCurrencyOrTaxRateParameterDTO loCurrencyOrTaxRateParameter = new GetCurrencyOrTaxRateParameterDTO();

        public GetAPSystemParamDTO loAPSystemParam = null;

        public async Task GetPaymentTermListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00110_GET_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loPaymentTermRtn = await loModel.GetPaymentTermListStreamAsync();
                loPaymentTermList = loPaymentTermRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCurrencyRtn = await loModel.GetCurrencyListStreamAsync();
                loCurrencyList = loCurrencyRtn.Data;
                loCurrencyList.ForEach(x => x.CDISPLAY = x.CCURRENCY_CODE + " - " + x.CCURRENCY_NAME);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loInvoiceListModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyRateAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCurrencyOrTaxRateParameterDTO loParam = null;
            try
            {/*
                loParam = new GetCurrencyOrTaxRateParameterDTO()
                {
                    CCURRENCY_CODE = loCurrencyOrTaxRateParameter.CCURRENCY_CODE,
                    CRATETYPE_CODE = "",
                    CREF_DATE = loCurrencyOrTaxRateParameter.CREF_DATE
                };*/
                loCurrencyRateRtn = await loModel.GetCurrencyOrTaxRateAsync(loCurrencyOrTaxRateParameter);
                loCurrencyRate = loCurrencyRateRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxRateAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCurrencyOrTaxRateParameterDTO loParam = null;
            try
            {/*
                loParam = new GetCurrencyOrTaxRateParameterDTO()
                {
                    CCURRENCY_CODE = loInvoiceHeader.CCURRENCY_CODE,
                    CRATETYPE_CODE = "",
                    CREF_DATE = loInvoiceHeader.CREF_DATE
                };*/
                loTaxRateRtn = await loModel.GetCurrencyOrTaxRateAsync(loCurrencyOrTaxRateParameter);
                loTaxRate = loTaxRateRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoiceHeaderAsync(APT00110DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00110ParameterDTO loParam = null;
            APT00110ParameterDTO loResult = null;
            try
            {
                loParam = new APT00110ParameterDTO()
                {
                    Data = poEntity
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loInvoiceHeader = loResult.Data;

                loInvoiceHeader.DDUE_DATE = DateTime.ParseExact(loInvoiceHeader.CDUE_DATE, "yyyyMMdd", null);
                loInvoiceHeader.DREF_DATE = DateTime.ParseExact(loInvoiceHeader.CREF_DATE, "yyyyMMdd", null);
                loInvoiceHeader.DDOC_DATE = DateTime.ParseExact(loInvoiceHeader.CDOC_DATE, "yyyyMMdd", null);

                loInvoiceHeader.CLOCAL_CURRENCY_CODE = loCompanyInfo.CLOCAL_CURRENCY_CODE;
                loInvoiceHeader.CBASE_CURRENCY_CODE = loCompanyInfo.CBASE_CURRENCY_CODE;
                loProperty.CPROPERTY_ID = loInvoiceHeader.CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void InvoiceHeaderValidation(APT00110DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROPERTY_ID);
                if (llCancel)
                {
                    loEx.Add("", "Please select property!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Please select department!");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CSUPPLIER_ID);
                if (llCancel)
                {
                    loEx.Add("", "Please select supplier!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CSUPPLIER_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Supplier Name is required!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CSUPPLIER_SEQ_NO);
                if (llCancel)
                {
                    loEx.Add("", "Supplier Reference No. is required!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDOC_NO);
                if (llCancel)
                {
                    loEx.Add("", "Supplier Reference Date is required!");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CDOC_DATE);
                if (llCancel)
                {
                    loEx.Add("", "Please select supplier!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPAY_TERM_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Please select term!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Please select currency!");
                }

                llCancel = poParam.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                }

                llCancel = poParam.NLCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Local Currency Rate must be greater than 0!");
                }

                llCancel = poParam.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                }

                llCancel = poParam.NLCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Base Currency Rate must be greater than 0!");
                }

                llCancel = poParam.LTAXABLE && string.IsNullOrWhiteSpace(poParam.CTAX_ID);
                if (llCancel)
                {
                    loEx.Add("", "Please select Tax ID!");
                }

                llCancel = poParam.LTAXABLE && poParam.NTAX_BASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Tax Base Rate must be greater than 0!");
                }

                llCancel = poParam.LTAXABLE && poParam.NTAX_CURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Tax Currency Rate must be greater than 0!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CTRANS_DESC);
                if (llCancel)
                {
                    loEx.Add("", "Description is required!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoiceHeaderAsync(APT00110DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00110ParameterDTO loParam = null;
            APT00110ParameterDTO loResult = null;
            try
            {
                loParam = new APT00110ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loInvoiceHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteInvoiceHeaderAsync(APT00110DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00110ParameterDTO loParam = null;
            try
            {
                loParam = new APT00110ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loProperty.CPROPERTY_ID
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
