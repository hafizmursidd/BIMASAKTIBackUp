using BlazorClientHelper;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMT01500Common.DTO._3._Unit_Info;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using System.Reflection.Emit;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Controls.Tab;

namespace LMT01500Front
{
    public partial class LMT01500UnitInfo
    {
        private readonly LMT01500UnitInfo_UnitInfoViewModel _viewModelLMT01500UnitInfo = new();

        private R_Conductor _conductorUnitInfo_UnitInfo;
        private R_Grid<LMT01500UnitInfoUnitInfoListDTO> _gridUnitInfo_UnitInfo;

        private bool _isDataExist;

        private bool _pageAgreementListOnCRUDmode;

        [Inject] private IClientHelper? _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelLMT01500UnitInfo.loParameterList.CREF_NO = (string)poParameter;

                if (!string.IsNullOrEmpty(_viewModelLMT01500UnitInfo.loParameterList.CREF_NO))
                {
                    _viewModelLMT01500UnitInfo.GetUnitInfoHeader();
                    _gridUnitInfo_UnitInfo.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task GetListUnitInfo(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelLMT01500UnitInfo.GetUnitInfoList();
                eventArgs.ListEntityResult = _viewModelLMT01500UnitInfo.loListLMT01500UnitInfo_UnitInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        #region TabStripTAB

        private R_TabStrip _tabStrip;
        private R_TabPage _tabPageUtilities;
        private R_NumericTextBox<decimal> FocusLabelEdit;
        private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            //journalGroupViewModel.DropdownProperty = true;
            //journalGroupViewModel.DropdownGroupType = true;

            //if (eventArgs.TabStripTab.Id == "Tab_AccountSetting")
            //{
            //    journalGroupViewModel.DropdownProperty = false;
            //    journalGroupViewModel.DropdownGroupType = false;
            //}
        }
        private void Before_Open_TabUtilities(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMT01500UnitInfo_Utilities);

            if (_viewModelLMT01500UnitInfo.loListLMT01500UnitInfo_UnitInfo.Count()>0)
            {
                eventArgs.Parameter = _viewModelLMT01500UnitInfo.R_GetCurrentData();
            }
            else
            {
                eventArgs.Parameter = "";
            }
        }
        #endregion

        private async Task ServiceGetOneRecord_UnitInfo(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMT01500UnitInfoUnitInfoDetailDTO>(eventArgs.Data);

                await _viewModelLMT01500UnitInfo.GetEntity(loParam);
                eventArgs.Result = _viewModelLMT01500UnitInfo.loEntityUnitInfo_UnitInfo;

                //var temp = (LMM06000BillingRuleDetailDTO)eventArgs.Result;
                //await PropertyDropdown_GetPeriodeList(null);
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

        #region Delete
        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data;
                await _viewModelLMT01500UnitInfo.ServiceDelete(loParam);
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


        #region SAVE
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data;
                await _viewModelLMT01500UnitInfo.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelLMT01500UnitInfo.loEntityUnitInfo_UnitInfo;
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
            R_PopupResult loResult = null;
            LMT01500UnitInfoUnitInfoDetailDTO loData = null;
            try
            {
                //Validation field from user
                // BillingRuleViewModel.ValidationFieldEmpty((LMT01500UnitInfoUnitInfoDetailDTO)eventArgs.Data);

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

        #region Lookup_UNIT_ID

        private R_Lookup R_Lookup_Unit_Id;
        private void BeforeOpenLookUp_UnitID(R_BeforeOpenLookupEventArgs eventArgs)
        {

            //var param = new GSL02()
            //{
            //    CCOMPANY_ID = clientHelper.CompanyId,
            //    CUSER_ID = clientHelper.UserId,
            //    CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
            //    CCHARGE_TYPE_ID = ""
            //};
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(LML00200);

        }
        private void AfterOpenLookUp_UnitID(R_AfterOpenLookupEventArgs eventArgs)
        {
            //var loTempResult = (LML00200DTO)eventArgs.Result;
            //if (loTempResult == null)
            //    return;

            //var loGetData = (LMM06000BillingRuleDetailDTO)_conductorBillingRuleRef.R_GetCurrentData();

            //loGetData.CDP_CHARGE_ID = loTempResult.CCHARGES_ID;
            //loGetData.CDP_CHARGE_NAME = loTempResult.CCHARGES_NAME;

        }

        #endregion
    }
}
