using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.DTOs.APT00121;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL.ViewModel
{
    public class APT00121ViewModel : R_ViewModel<APT00121DTO>
    {
        private APT00121Model loModel = new APT00121Model();

        public APT00121DTO loInvoiceItem = new APT00121DTO();

        public List<GetProductTypeDTO> loProductTypeList = null;

        public GetProductTypeResultDTO loProductTypeRtn = null;

        public GetProductTypeDTO loProductType = null;

        public APT00121ResultDTO loRtn = null;

        public APT00111HeaderDTO loHeader = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public TabItemEntryParameterDTO loTabParam = null;

        public async Task GetProductTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loProductTypeRtn = await loModel.GetProductTypeListStreamAsync();
                loProductTypeList = loProductTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            APT00121RefreshParameterDTO loParam = null;
            try
            {
                loParam = new APT00121RefreshParameterDTO()
                {
                    CREC_ID = loTabParam.Data.CREC_ID
                };
                loRtn = await loModel.RefreshInvoiceItemAsync(loParam);
                loInvoiceItem = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void InvoiceItemValidation(APT00121DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROD_DEPT_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Please select Product Department!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPROD_TYPE);
                if (llCancel)
                {
                    loEx.Add("", "Please select Product Type!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "P";
                if (llCancel)
                {
                    loEx.Add("", "Please select Product!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "E";
                if (llCancel)
                {
                    loEx.Add("", "Please select Expenditure!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CALLOC_ID) && poParam.CPROD_TYPE == "P";
                if (llCancel)
                {
                    loEx.Add("", "Please select Allocation ID!");
                }

                llCancel = poParam.NTRANS_QTY <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Purchase Qty must be > 0!");
                }

                llCancel = poParam.IBILL_UNIT == 0;
                if (llCancel)
                {
                    loEx.Add("", "Please select Purchase Unit!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CBILL_UNIT) && poParam.IBILL_UNIT == 4;
                if (llCancel)
                {
                    loEx.Add("", "Billing Unit is required!");
                }

                llCancel = poParam.NSUPP_CONV_FACTOR <= 0 && poParam.IBILL_UNIT == 4;
                if (llCancel)
                {
                    loEx.Add("", "Conversion Factor must be > 0!");
                }

                llCancel = poParam.NUNIT_PRICE <= 0;
                if (llCancel)
                {
                    loEx.Add("", "Unit Price must be > 0");
                }

                llCancel = poParam.CDISC_TYPE == "P" && (poParam.NDISC_PCT < 0 || poParam.NDISC_PCT > 100);
                if (llCancel)
                {
                    loEx.Add("", "Invalid Discount Percentage! Please input value between 0 and 100.");
                }

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_PCT < 0;
                if (llCancel)
                {
                    loEx.Add("", "Discount Amount cannot be < 0!");
                }

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_PCT > poParam.NAMOUNT;
                if (llCancel)
                {
                    loEx.Add("", "Invalid Discount Amount! Discount Amount cannot be > Total Price");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoiceItemAsync(APT00121DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00121ParameterDTO loParam = null;
            APT00121ParameterDTO loResult = null;
            try
            {
                loParam = new APT00121ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loHeader.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loInvoiceItem = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteInvoiceItemAsync(APT00121DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00121ParameterDTO loParam = null;
            try
            {
                loParam = new APT00121ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loHeader.CPROPERTY_ID
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
