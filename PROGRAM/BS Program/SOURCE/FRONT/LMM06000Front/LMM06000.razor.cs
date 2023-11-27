﻿
using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM06000Common;
using LMM06000Model.ViewModel;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
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
        [Inject] public R_PopupService PopupService { get; set; }


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

        #region EnableGrid
        private bool _gridEnabled = true;
        private void ServiceSetOther(R_SetEventArgs eventArgs)
        {
            _gridEnabled = eventArgs.Enable;
        }
        #endregion

        #region CheckboxChanging
        private async Task CheckboxBookingFee(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region BookingFee
                //when Booking fee have value
                if (!string.IsNullOrEmpty(BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID))
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        CBOOKING_FEE_CHARGE_ID = BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID,
                        CCHARGES_NAME = BillingRuleViewModel.Data.CCHARGES_NAME
                    };
                }
                if (!lbCheckbox)
                {
                    BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID = "";
                    BillingRuleViewModel.Data.CCHARGES_NAME = "";
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;

                    BillingRuleViewModel.Data.CBOOKING_FEE_CHARGE_ID = loTempDt.CBOOKING_FEE_CHARGE_ID;
                    BillingRuleViewModel.Data.CCHARGES_NAME = loTempDt.CCHARGES_NAME;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task CheckboxBankCredit(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
              #region BankCredit
                if ((BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE) != 0 ||
                    (BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL) != 0)
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        IBANK_CREDIT_PERCENTAGE = BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE,
                        IBANK_CREDIT_INTERVAL = BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL
                    };
                }
                if (!lbCheckbox)
                {
                    BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE = 0;
                    BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL = 0;
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;

                    BillingRuleViewModel.Data.IBANK_CREDIT_PERCENTAGE = loTempDt.IBANK_CREDIT_PERCENTAGE;
                    BillingRuleViewModel.Data.IBANK_CREDIT_INTERVAL = loTempDt.IBANK_CREDIT_INTERVAL;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task CheckboxWithDP(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
                #region WithDP
                //when period have period
                if (!string.IsNullOrEmpty(BillingRuleViewModel.Data.CDP_PERIOD_MODE) ||
                    !string.IsNullOrEmpty(BillingRuleViewModel.Data.CDP_CHARGE_ID))
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        IDP_PERCENTAGE = BillingRuleViewModel.Data.IDP_PERCENTAGE,
                        IDP_INTERVAL = BillingRuleViewModel.Data.IDP_INTERVAL,
                        CDP_PERIOD_MODE = BillingRuleViewModel.Data.CDP_PERIOD_MODE,
                        CDP_CHARGE_ID = BillingRuleViewModel.Data.CDP_CHARGE_ID,
                        CDP_CHARGE_NAME = BillingRuleViewModel.Data.CDP_CHARGE_NAME
                    };
                }

                if (!lbCheckbox)
                {
                    BillingRuleViewModel.Data.IDP_PERCENTAGE = 0;
                    BillingRuleViewModel.Data.IDP_INTERVAL = 0;
                    BillingRuleViewModel.Data.CDP_PERIOD_MODE = "";
                    BillingRuleViewModel.Data.CDP_CHARGE_ID = "";
                    BillingRuleViewModel.Data.CDP_CHARGE_NAME = "";
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;
                    BillingRuleViewModel.Data.IDP_PERCENTAGE = loTempDt.IDP_PERCENTAGE;
                    BillingRuleViewModel.Data.IDP_INTERVAL = loTempDt.IDP_INTERVAL;
                    BillingRuleViewModel.Data.CDP_PERIOD_MODE = loTempDt.CDP_PERIOD_MODE;
                    BillingRuleViewModel.Data.CDP_CHARGE_ID = loTempDt.CDP_CHARGE_ID;
                    BillingRuleViewModel.Data.CDP_CHARGE_NAME = loTempDt.CDP_CHARGE_NAME;

                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task CheckboxInstallment(object poParam)
        {
            var loEx = new R_Exception();
            bool lbCheckbox = (bool)poParam;
            try
            {
               #region Installment
                //when this group box have value
                if (!string.IsNullOrEmpty(BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE) ||
                !string.IsNullOrEmpty(BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID))
                {
                    BillingRuleViewModel._IsDataNull = false;

                    BillingRuleViewModel.TemporaryBillingRuleDetail = new LMM06000BillingRuleDetailDTO()
                    {
                        IINSTALLMENT_PERCENTAGE = BillingRuleViewModel.Data.IINSTALLMENT_PERCENTAGE,
                        IINSTALLMENT_INTERVAL = BillingRuleViewModel.Data.IINSTALLMENT_INTERVAL,
                        CINSTALLMENT_PERIOD_MODE = BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE,
                        CINSTALLMENT_CHARGE_ID = BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID,
                        CINSTALLMENT_CHARGE_NAME = BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_NAME
                    };
                }

                if (!lbCheckbox)
                {
                    BillingRuleViewModel.Data.IINSTALLMENT_PERCENTAGE = 0;
                    BillingRuleViewModel.Data.IINSTALLMENT_INTERVAL = 0;
                    BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE = "";
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID = "";
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_NAME = "";
                }
                //re assign value from user
                if (lbCheckbox && !BillingRuleViewModel._IsDataNull)
                {
                    var loTempDt = BillingRuleViewModel.TemporaryBillingRuleDetail;
                    BillingRuleViewModel.Data.IINSTALLMENT_PERCENTAGE = loTempDt.IINSTALLMENT_PERCENTAGE;
                    BillingRuleViewModel.Data.IINSTALLMENT_INTERVAL = loTempDt.IINSTALLMENT_INTERVAL;
                    BillingRuleViewModel.Data.CINSTALLMENT_PERIOD_MODE = loTempDt.CINSTALLMENT_PERIOD_MODE;
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_ID = loTempDt.CINSTALLMENT_CHARGE_ID;
                    BillingRuleViewModel.Data.CINSTALLMENT_CHARGE_NAME = loTempDt.CINSTALLMENT_CHARGE_NAME;
                }
                #endregion
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        private async Task ServiceBeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            //disable when user click edit and edit, then user click cancel not save
            BillingRuleViewModel._IsButtonAddEnable = true;

            BillingRuleViewModel._IsDataNull = true;
        }

        #region UnitType
        private async Task GetListRecordUnitType(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await BillingRuleViewModel.GetAllUnitType();
                eventArgs.ListEntityResult = BillingRuleViewModel.UnitTypeList;
                if (BillingRuleViewModel.UnitTypeList.Count() < 1)
                {
                    //disable when, unit type didn't have data
                    BillingRuleViewModel.UnitTypeValueContext = "";
                    BillingRuleViewModel._IsButtonAddEnable = false;
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                }
                else
                {
                    BillingRuleViewModel._IsButtonAddEnable = true;
                    BillingRuleViewModel.UnitTypeValueContext = BillingRuleViewModel.UnitTypeList[0].CUNIT_TYPE_ID;
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                }
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
                //Enable button add, when user click another unit type
                BillingRuleViewModel._IsButtonAddEnable = true;

                var loParam = (LMM06000UnitTypeDTO)eventArgs.Data;
                BillingRuleViewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                BillingRuleViewModel.UnitTypeValueContext = loParam.CUNIT_TYPE_ID;
                await _gridBillingRuleRef.R_RefreshGrid(null);
            }
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

                //if (BillingRuleViewModel.UnitTypeList.Count() < 1)
                //{
                //BillingRuleViewModel._IsButtonAddEnable = true;
                //    //disable when, unit type didn't have data
                //    BillingRuleViewModel.UnitTypeValueContext = "";
                //    BillingRuleViewModel._IsButtonAddEnable = false;
                //    await _gridBillingRuleRef.R_RefreshGrid(null);
                //}
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
        private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //Focus Async
                        await FocusLabelEdit.FocusAsync();
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
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

            //var param = new LML00500ParameterDTO()
            //{
            //    CCOMPANY_ID = clientHelper.CompanyId,
            //    CUSER_ID = clientHelper.UserId,
            //    CPROPERTY_ID = "JBMPC"
            //};
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(LML00500);

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
            //disable button add when user click add
            BillingRuleViewModel._IsButtonAddEnable = false;
            // Focus Async
            await FocusLabelAdd.FocusAsync();

        }
        private async Task ServiceBeforeEdit(R_BeforeEditEventArgs eventArgs)
        {

            //disable button add when user click edit
            BillingRuleViewModel._IsButtonAddEnable = false;
        }
        private async Task ServiceAfterSave(R_AfterSaveEventArgs eventArgs)
        {
            //disable button add when user click edit
            BillingRuleViewModel._IsButtonAddEnable = true;
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
        private async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParamPopup = null;
            R_PopupResult loResult = null;
            LMM06000BillingRuleDetailDTO loData = null;
            try
            {
                //Validation field from user
                BillingRuleViewModel.ValidationFieldEmpty((LMM06000BillingRuleDetailDTO)eventArgs.Data);

                if (!loEx.HasError)
                {
                    try
                    {
                        loData = (LMM06000BillingRuleDetailDTO)eventArgs.Data;
                        if (loData.LACTIVE == true && _conductorBillingRuleRef.R_ConductorMode == R_eConductorMode.Add)
                        {
                            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM06001";
                            await loValidateViewModel
                                .RSP_ACTIVITY_VALIDITYMethodAsync();
                            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                                  loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                            {
                                eventArgs.Cancel = false;
                            }
                            else
                            {
                                loParamPopup = new GFF00900ParameterDTO()
                                {
                                    Data = loValidateViewModel.loRspActivityValidityList,
                                    IAPPROVAL_CODE = "LMM06001"
                                };
                                loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParamPopup);
                                
                                if (loResult.Success == false || (bool)loResult.Result == false)
                                {
                                    eventArgs.Cancel = true;
                                    return;
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }
                }
                loEx.ThrowExceptionIfErrors();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
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
        public async Task ServiceAfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        #endregion


        #region Active/Inactive

        private async Task R_Before_Open_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM06001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await BillingRuleViewModel.ActiveInactiveProcessAsync();
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "LMM06001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await BillingRuleViewModel.ActiveInactiveProcessAsync();
                    await _gridBillingRuleRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        /*
        public async Task Conductor_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            try
            {
                if (BillingRuleViewModel.CrateTime < DateTime.Today)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM05501"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "GSM05501" //Uabh Approval Code sesuai Spec masing masing
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                        eventArgs.Cancel = !(bool)loResult.Result;
                    }

                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        */
        #endregion
    }
}
