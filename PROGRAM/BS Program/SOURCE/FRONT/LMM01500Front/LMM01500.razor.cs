using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using BlazorClientHelper;
using LMM01500COMMON;
using LMM01500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls.Tab;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls.Enums;

namespace LMM01500Front
{
    public partial class LMM01500
    {
        [Inject] IClientHelper clientHelper { get; set; }

        private LMM01500ViewModel _LMM01500ViewModel = new();
        private R_Conductor _conductorPropertyRef;

        private R_Conductor _conductorInvoiceGroupRef;
        private R_Grid<LMM01500InvoiceGroupDTO> _gridInvoiceGroupRef;
        private string loLabel = "Activate";
        private bool isInvoiceGroup;
        #region PropertyID

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await Service_PropertyDropdown(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Service_PropertyDropdown(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _LMM01500ViewModel.GetPropertyList();
                await _gridInvoiceGroupRef.R_RefreshGrid(null);
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
                _LMM01500ViewModel.PropertyValueContext = lsProperty;
                await _gridInvoiceGroupRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region InvoiceGroup
        private async Task GetInvoiceGroupList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _LMM01500ViewModel.GetInvoiceGroupList();
                eventArgs.ListEntityResult = _LMM01500ViewModel.InvoiceGroupList;
                if (_LMM01500ViewModel.InvoiceGroupList.Count < 1)
                {
                    //disable when, unit type didn't have data
                    _LMM01500ViewModel.InvoiceGroupValue = "";
                    // _LMM01500ViewModel._IsButtonAddEnable = false;
                    // await _gridInvoiceGroupValueRef.R_RefreshGrid(null);
                }
                else
                {
                    // _LMM01500ViewModel._IsButtonAddEnable = true;
                    _LMM01500ViewModel.InvoiceGroupValue = _LMM01500ViewModel.InvoiceGroupList[0].CINVGRP_CODE;
                    // await _gridInvoiceGroupRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceRecordInvoiceGroup(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01500InvoiceGroupDetailDTO>(eventArgs.Data);
                await _LMM01500ViewModel.GetInvoiceGroupDetail(loParam);
                eventArgs.Result = _LMM01500ViewModel.InvoiceGroupDetail;

                /*
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
                 */
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            //if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            //{
            //    //Enable button add, when user click another unit type
            //    //_LMM01500ViewModel._IsButtonAddEnable = true;

            //var loParam = (LMM01500InvoiceGroupDTO)eventArgs.Data;
            //_LMM01500ViewModel.PropertyValueContext = loParam.CINVGRP_CODE;
            //_LMM01500ViewModel.InvoiceGroup = loParam.CINVGRP_CODE;
            //}
        }
        private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                //switch (eventArgs.ConductorMode)
                //{
                //    case R_eConductorMode.Edit:
                //        //Focus Async
                //        await FocusLabelEdit.FocusAsync();
                //        break;
                //}

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion

        private R_Lookup R_Lookup_Additional;
        private R_Lookup R_Lookup_DEPT;
        private R_Lookup R_Lookup_BANK;
        private R_Lookup R_Lookup_BANKACC;

        #region BeforeLookup
        private void BeforeOpenLookUp_Additional(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01400ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _LMM01500ViewModel.PropertyValueContext,
                CCHARGES_TYPE_ID = "A"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01400);
        }
        private void AfterOpenLookUp_Additional(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01400DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMM01500InvoiceGroupDetailDTO)_conductorInvoiceGroupRef.R_GetCurrentData();

            loGetData.CSTAMP_ADD_ID = loTempResult.CCHARGES_ID;
            loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;

        }
        #endregion

        private async Task ServiceBeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            isInvoiceGroup = _LMM01500ViewModel.InvoiceGroupDetail.CINVOICE_DUE_MODE == "02";
        }
        private async Task ServiceBeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            isInvoiceGroup = false;
            _LMM01500ViewModel.InvoiceDueModeValue = _LMM01500ViewModel.InvoiceGroupDetail.CINVOICE_DUE_MODE;
        }

        #region DEPT
        private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new GSL00700ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _LMM01500ViewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _LMM01500ViewModel._selectedDeptCode = loTempResult.CDEPT_CODE;
            _LMM01500ViewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;

            //GeneralButtonEnable = !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CBANK_CODE);
        }
        #endregion

        #region LookupBANK

        private void BANK_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new GSL01200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CBANK_TYPE = "",
                CCB_TYPE = "B",
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01200);
        }
        private void BANK_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _LMM01500ViewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _LMM01500ViewModel._selectedBank = loTempResult.CCB_CODE;
            _LMM01500ViewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
            //GeneralButtonEnable = !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CBANK_CODE);
        }
        #endregion

        #region LookupBANK_ACCOUNT

        private void BANKACCOUNT_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01300ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CBANK_TYPE = "B",
                CCB_CODE = _LMM01500ViewModel._selectedBank,
                CDEPT_CODE = _LMM01500ViewModel._selectedDeptCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }
        private void BANKACCOUNT_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _LMM01500ViewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;
            //GeneralButtonEnable = !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CBANK_CODE);
        }
        #endregion

        #region RadioGroup
        private bool textBoxDueDays = true;
        private bool textBoxFixedDueDays;
        private bool textBoxRangeFixedDueDays;

        private async Task OnChangedRadioBtnDueMode(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                isInvoiceGroup = poParam.ToString() == "02";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task OnChangedRadioBtnGrpMode(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                string typeGroupMode = (string)poParam;

                textBoxDueDays = (typeGroupMode == "01");
                textBoxFixedDueDays = (typeGroupMode == "02");
                textBoxRangeFixedDueDays = (typeGroupMode == "03");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region To TAB Template
        private R_TabPage _tabPageTempalate;
        //CHANGE TAB
        private void Before_Open_TabTemplate(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM01500InvoiceGroupDept);
            if (_LMM01500ViewModel.InvoiceGroupList.Count > 0)
            {
                eventArgs.Parameter = _LMM01500ViewModel._tabParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
        }
        #endregion

        #region To TAB CHAREGS
        private R_TabPage _tabPageCharges;
        //CHANGE TAB
        private void Before_Open_TabCharges(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM01500Charges);
            if (_LMM01500ViewModel.InvoiceGroupList.Count > 0)
            {
                eventArgs.Parameter = _LMM01500ViewModel._tabParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }

        #endregion

        #region R_Storage
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Doc };
        private async Task InputFileChangeLMM01500GeneralInfo(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01500InvoiceGroupDetailDTO)_conductorInvoiceGroupRef.R_GetCurrentData();
                if (loData != null)
                {
                    //if TRUE OVERRIDE DATA EXIST

                    var loMS = new MemoryStream();
                    await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                    loData.OData = loMS.ToArray();
                    loData.CFileNameExtension = eventArgs.File.Name;
                    loData.CFileExtension = Path.GetExtension(eventArgs.File.Name);
                    loData.CFileName = Path.GetFileNameWithoutExtension(eventArgs.File.Name);
                    
                    loData.LINVOICE_TEMPLATE = true;
                    loData.CINVOICE_TEMPLATE = loData.CFileName + "." + loData.CFileExtension;

                }
            }
            // Set Data
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region save
        private async Task R_ServiceSaveGeneralInfo(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01500ViewModel.Save_InvoiceGroup((LMM01500InvoiceGroupDetailDTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _LMM01500ViewModel.InvoiceGroupDetail;
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
