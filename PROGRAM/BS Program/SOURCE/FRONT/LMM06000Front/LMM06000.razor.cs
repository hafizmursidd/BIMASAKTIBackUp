
using BlazorClientHelper;
using LMM06000Common;
using LMM06000Model.ViewModel;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM06000Front
{
    public partial class LMM06000
    {
        private R_Conductor _conductorPropertyRef;
        private LMM06000ViewModel BillingRuleViewModel = new();

        private R_ConductorGrid _conductorUnitTypeRef;
        private R_Grid<LMM06000UnitTypeDTO> _gridUnitTypeRef;

        private R_Conductor _conductorBillingRuleRef;
        private R_Grid<LMM06000BillingRuleDTO> _gridBillingRuleRef;

        private string loLabel = "Activate";
        private R_TextBox FocusLabelAdd;
        private R_TextBox FocusLabelEdit;
        [Inject] IClientHelper clientHelper { get; set; }


        #region PropertyID

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetPropertyList();
                await _gridUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                BillingRuleViewModel.PropertyValueContext = lsProperty;
                await _gridUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region UnitType


        private async Task GetListRecordUnitType(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetAllUnitType();
                eventArgs.ListEntityResult = BillingRuleViewModel.UnitTypeList;
                await _gridBillingRuleRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_DisplayUnitType(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMM06000UnitTypeDTO)eventArgs.Data;

                if (BillingRuleViewModel.UnitTypeList.Count() < 1)
                {
                    BillingRuleViewModel.PropertyValueContext = "";
                    BillingRuleViewModel.UnitTypeValueContext = "";
                }
                else
                {
                    BillingRuleViewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    BillingRuleViewModel.UnitTypeValueContext = loParam.CUNIT_TYPE_ID;
                }
            }
            await _gridBillingRuleRef.R_RefreshGrid(null);
        }
        #endregion

        #region BillingRule

        private async Task GetListRecordBillingRules(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetAllBillingRule();
                eventArgs.ListEntityResult = BillingRuleViewModel.BillingRuleList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetOneRecordBillingRule(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM06000BillingRuleDetailDTO>(eventArgs.Data);

                await BillingRuleViewModel.GetBillingRuleOneRecord(loParam);

                eventArgs.Result = BillingRuleViewModel.BillingRuleDetail;
                var temp = (LMM06000BillingRuleDetailDTO)eventArgs.Result;
                await PropertyDropdown_GetPeriodeList(null);

                //Set ActiveInactive Value
                BillingRuleViewModel.ActiveInactiveEntity.PROPERTY_ID = temp.CPROPERTY_ID;
                BillingRuleViewModel.ActiveInactiveEntity.CUNIT_TYPE_ID = temp.CUNIT_TYPE_ID;
                BillingRuleViewModel.ActiveInactiveEntity.CBILLING_RULE_CODE = temp.CBILLING_RULE_CODE;

                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    BillingRuleViewModel.ActiveInactiveEntity.LACTIVE = false;
                }
                else
                {
                    loLabel = "Activate";
                    BillingRuleViewModel.ActiveInactiveEntity.LACTIVE = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_GetPeriodeList(R_ServiceGetListRecordEventArgs args)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetPeriodList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region BeforeLookup

        private R_Lookup R_Lookup_Unit_Charges_Button;
        //BEFORE LOOKUP INSTALLMENT DAN BOOKING FEE
        private void BeforeOpenLookUp_Unit_Charges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                CCHARGE_TYPE_ID = ""
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        //Before LookUp WITHDP
        private void BeforeOpenLookUp_Unit_Charges_WithDP(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                CCHARGE_TYPE_ID = "07"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        #endregion

        #region AfterLOOKUP

        //After LookUp Booking Fee
        private void AfterOpenLookUp_Unit_ChargesBookingFee(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            loGetData.CBOOKING_FEE_CHARGE_ID = loTempResult.CCHARGES_ID;
            loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

        //After Lookup Installment
        private void AfterOpenLookUp_Unit_ChargesWithInstallment(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            loGetData.CINSTALLMENT_CHARGE_ID = loTempResult.CCHARGES_ID;
            loGetData.CINSTALLMENT_CHARGE_NAME = loTempResult.CCHARGES_NAME;

        }

        //After Lookup Unit Charges With DP
        private void AfterOpenLookUp_Unit_ChargesWithDP(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            loGetData.CDP_CHARGE_ID = loTempResult.CCHARGES_ID;
            loGetData.CDP_CHARGE_NAME = loTempResult.CCHARGES_NAME;

        }
        #endregion

        #region ADD and Edit
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            eventArgs.Data = new LMM06000BillingRuleDetailDTO()
            {
                CUNIT_TYPE_ID = BillingRuleViewModel.UnitTypeValueContext,
                CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
                LACTIVE = true,
                LBOOKING_FEE = false,
                CBOOKING_FEE_CHARGE_ID = "",
                CDP_PERIOD_MODE = "",
                LWITH_DP = false,
                
                CDP_CHARGE_ID = "",
                LINSTALLMENT = false,
                CINSTALLMENT_CHARGE_ID = "",
                CINSTALLMENT_PERIOD_MODE = "",
                LBANK_CREDIT = false
            }; 
            
            await FocusLabelAdd.FocusAsync();

        }
        private async Task ServiceSaving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                if (!loParam.LBOOKING_FEE)
                {
                    loParam.CBOOKING_FEE_CHARGE_ID = "";
                }
                if (!loParam.LWITH_DP)
                {
                    loParam.IDP_PERCENTAGE = 0;
                    loParam.IDP_INTERVAL = 0;
                    loParam.CDP_PERIOD_MODE = "";
                    loParam.CDP_CHARGE_ID = "";
                }

                if (!loParam.LINSTALLMENT)
                {
                    loParam.IINSTALLMENT_PERCENTAGE = 0;
                    loParam.IINSTALLMENT_INTERVAL = 0;
                    loParam.CINSTALLMENT_PERIOD_MODE = "";
                    loParam.CINSTALLMENT_CHARGE_ID = "";
                }

                if (!loParam.LBANK_CREDIT)
                {
                    loParam.IBANK_CREDIT_PERCENTAGE = 0;
                    loParam.IBANK_CREDIT_INTERVAL = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                await BillingRuleViewModel.SaveUnitType_BillingRule(loParam, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = BillingRuleViewModel.BillingRuleDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;

                if (string.IsNullOrEmpty(loParam.CBILLING_RULE_CODE))
                    loEx.Add(new Exception("Billing Rule Code is required."));
                if (string.IsNullOrEmpty(loParam.CBILLING_RULE_NAME))
                    loEx.Add(new Exception("Billing Rule Name is required."));

                if (loParam.LBOOKING_FEE)
                {
                    if (string.IsNullOrEmpty(loParam.CBOOKING_FEE_CHARGE_ID))
                        loEx.Add(new Exception("Charge Id Booking Fee is required."));
                }
                if (loParam.LWITH_DP)
                {
                    if (string.IsNullOrEmpty(loParam.CDP_PERIOD_MODE))
                        loEx.Add(new Exception("Periode Mode With Dp is required."));
                    if (string.IsNullOrEmpty(loParam.CDP_CHARGE_ID))
                        loEx.Add(new Exception("Charge Id With DP is required."));
                }
                if (loParam.LINSTALLMENT)
                {
                    if (string.IsNullOrEmpty(loParam.CINSTALLMENT_PERIOD_MODE))
                        loEx.Add(new Exception("Periode Mode Installment is required."));
                    if (string.IsNullOrEmpty(loParam.CINSTALLMENT_CHARGE_ID))
                        loEx.Add(new Exception("Charge Id Installment is required."));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Delete

        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                await BillingRuleViewModel.DeleteUnitType_BillingRule(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Active/Inactive

        private void R_Before_Open_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "LMM06001";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task R_After_Open_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var result = (bool)eventArgs.Result;
                if (result)
                    await BillingRuleViewModel.ActiveInactiveProcessAsync();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            await _gridBillingRuleRef.R_RefreshGrid(null);
        }
        #endregion
    }
}
