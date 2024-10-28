using BlazorClientHelper;
using LMT05500Common.DTO;
using LMT05500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT05500Front
{
    public partial class LMT05500Deposit
    {
        private LMT05500DepositViewModel _depositViewModel = new();
        private R_Grid<LMT05500DepositListDTO> _gridDepositRef;
        private R_Grid<LMT05500DepositDetailListDTO> _gridDepositDetailRef;

        private R_ConductorGrid _conGridDeposit;
        private R_ConductorGrid _conGridDepositDetail;

        private R_TabStrip _tabStripDeposit;
        private R_TabPage _tabPageSubDeposit;
        private bool _buttonView;
        private bool _buttonOnDepositGrid;
        private bool _pageDepositOnCRUDmode;
        [Inject] private IClientHelper _clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMT05500AgreementDTO)poParameter;
                if ((LMT05500AgreementDTO)poParameter != null)
                {
                    _depositViewModel._currentDataAgreement = (LMT05500AgreementDTO)poParameter;

                    await R_ServiceHeaderRecord((LMT05500AgreementDTO)poParameter);
                    _gridDepositRef.R_RefreshGrid(null);
                }
                else
                {
                    _depositViewModel._currentDataAgreement.CPROPERTY_ID = "";
                    _depositViewModel._currentDataAgreement.CDEPT_CODE = "";
                    _depositViewModel._currentDataAgreement.CTRANS_CODE = "";
                    _depositViewModel._currentDataAgreement.CREF_NO = "";
                }
                var temp = _depositViewModel._headerDeposit;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region HeaderDeposit
        private async Task R_ServiceHeaderRecord(LMT05500AgreementDTO poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                poParameter.CCOMPANY_ID = _clientHelper.CompanyId;
                poParameter.CUSER_ID = _clientHelper.UserId;
                await _depositViewModel.GetHeaderDeposit(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region Deposit List
        private async Task R_ServiceDepositListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _depositViewModel.GetAllDepositList();
                eventArgs.ListEntityResult = _depositViewModel._depositList;

                if (_depositViewModel._depositList.Count > 0)
                {
                    _buttonOnDepositGrid = true;
                    _depositViewModel._currentDeposit = _depositViewModel._depositList[0];
                    await _gridDepositDetailRef.R_RefreshGrid(null);
                }
                else
                {
                    _buttonOnDepositGrid = false;
                    _depositViewModel._depositDetailList.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_DisplayDeposit(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMT05500DepositListDTO)eventArgs.Data;
                //  _agreementViewModel.PropertyValueContext = loParam.CPROPERTY_ID;

                _depositViewModel._currentDeposit = loParam;
                await _gridDepositDetailRef.R_RefreshGrid(null);
            }
        }

        #endregion

        #region DepositDetail List
        private async Task R_ServiceDepositDetailListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var temp = _depositViewModel._currentDeposit;
                await _depositViewModel.GetAllDepositDetailList();
                eventArgs.ListEntityResult = _depositViewModel._depositDetailList;
                if (_depositViewModel._depositDetailList.Count > 0)
                {
                    _buttonView = true;
                }
                else
                {
                    _buttonView = false;
                    _depositViewModel._currentDepositDetail = null;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region BtnView
        private void R_Before_ServiceOpenOthersProgram(R_BeforeOpenDetailEventArgs eventArgs)
        {

            /*            
            var lcProgramId = "APT00100";

            switch (lcProgramId)
            {
                case "APT00100":
                    eventArgs.Parameter = _viewModelGST00500Inbox._currentRecord;
                    eventArgs.TargetPageType = typeof(LMM06000);
                    break;
            }
             */
        }
        #endregion
        #region CHANGE SUBTAB
        //CHANGE TAB
        private void SetOther(R_SetEventArgs eventArgs)
        {
            _pageDepositOnCRUDmode = eventArgs.Enable;
        }
        private void Before_Open_DepositInfo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                eventArgs.TargetPageType = typeof(LMT05500DepositInfo);
                if (_depositViewModel._depositList.Count > 0)
                {
                    eventArgs.Parameter = _gridDepositRef.GetCurrentData();
                }
                else
                {
                    eventArgs.Parameter = null;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        private void OnActiveTabDepositIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = !_pageDepositOnCRUDmode;
        }
        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pageDepositOnCRUDmode = (bool)poValue;
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
