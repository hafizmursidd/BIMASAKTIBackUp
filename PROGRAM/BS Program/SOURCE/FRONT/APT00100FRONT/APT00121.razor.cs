using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00121;
using APT00100MODEL.ViewModel;
using BlazorClientHelper;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APFRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace APT00100FRONT
{
    public partial class APT00121
    {
        private APT00121ViewModel loViewModel = new APT00121ViewModel();

        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private bool IsAllocationEnable = false;

        private string lcExpProdLabel = "Product/Expenditure";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loTabParam = (TabItemEntryParameterDTO)poParameter;
                if (!string.IsNullOrWhiteSpace(loViewModel.loTabParam.Data.CREC_ID))
                {;
                    await _conductorRef.R_GetEntity(null);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceItem_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetInfoAsync();
                eventArgs.Result = loViewModel.loInvoiceItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceItem_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteInvoiceItemAsync((APT00121DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void InvoiceItem_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00121DTO loData = (APT00121DTO)eventArgs.Data;
                loViewModel.InvoiceItemValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void InvoiceItem_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
        }

        private void InvoiceItem_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        private void InvoiceItem_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsAllocationEnable = false;
        }

        private void InvoiceItem_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            IsAllocationEnable = false;
        }

        private async Task InvoiceItem_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveInvoiceItemAsync((APT00121DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loInvoiceItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Lookup
        private void R_Before_Open_LookupProductDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loTabParam.Data.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                CUSER_LOGIN_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupProductDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CPROD_DEPT_CODE = loTempResult.CDEPT_CODE;
            loViewModel.Data.CPROD_DEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private void R_Before_Open_LookupProductExpenditure(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcTaxableFlag = "";
            string lcMode = "";
            try
            {
                if (loViewModel.loTabParam.Data.LTAXABLE)
                {
                    lcTaxableFlag = "1";
                }
                else
                {
                    lcTaxableFlag = "2";
                }
                if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    lcMode = "1";
                }
                else
                {
                    lcMode = "0";
                }
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    APL00300ParameterDTO loParam1 = new APL00300ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                        CTAXABLE_TYPE = lcTaxableFlag,
                        CACTIVE_TYPE = lcMode,
                        CLANGUAGE_ID = "",
                        CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE
                    };
                    eventArgs.Parameter = loParam1;
                    eventArgs.TargetPageType = typeof(APL00300);
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    APL00200ParameterDTO loParam2 = new APL00200ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                        CTAXABLE_TYPE = lcTaxableFlag,
                        CACTIVE_TYPE = lcMode,
                        CLANGUAGE_ID = "",
                        CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE
                    };
                    eventArgs.Parameter = loParam2;
                    eventArgs.TargetPageType = typeof(APL00200);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_After_Open_LookupProductExpenditure(R_AfterOpenLookupEventArgs eventArgs)
        {
            if (loViewModel.Data.CPROD_TYPE == "P")
            {
                APL00300DTO loTempResult1 = (APL00300DTO)eventArgs.Result;
                if (loTempResult1 == null)
                {
                    return;
                }
                loViewModel.Data.CPRODUCT_ID = loTempResult1.CPRODUCT_ID;
                loViewModel.Data.CPRODUCT_NAME = loTempResult1.CPRODUCT_NAME;
                //loViewModel.Data.CSUP_PRODUCT_ID = loTempResult1.CALIAS_ID;
                loViewModel.Data.CSUP_PRODUCT_NAME = loTempResult1.CALIAS_NAME;
                //loViewModel.Data.COTHER_TAX_ID = loTempResult1.CO
            }
            else if (loViewModel.Data.CPROD_TYPE == "E")
            {
                APL00200DTO loTempResult2 = (APL00200DTO)eventArgs.Result;
                if (loTempResult2 == null)
                {
                    return;
                }
            }
        }
        #endregion


        #region OnChange

        private async Task ProductTypeComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CPROD_TYPE = poParam;
                loViewModel.Data.CPRODUCT_ID = "";
                loViewModel.Data.CPRODUCT_NAME = "";
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = "Product";
                    IsAllocationEnable = true;
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = "Expenditure";
                    loViewModel.Data.CALLOC_ID = "";
                    loViewModel.Data.CALLOC_NAME = "";
                    IsAllocationEnable = false;
                }
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
