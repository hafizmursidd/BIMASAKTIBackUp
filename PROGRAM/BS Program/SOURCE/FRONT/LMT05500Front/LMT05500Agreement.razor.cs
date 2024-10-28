using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMT05500Common.DTO;
using LMT05500Model;
using LMT05500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;

namespace LMT05500Front
{
    public partial class LMT05500Agreement
    {
        private LMT05500AgreementViewModel _agreementViewModel = new();
        private R_Grid<LMT05500AgreementDTO> _gridAgreementRef;
        private R_ConductorGrid _conGridAgreement;

        private R_Grid<LMT05500UnitDTO> _gridDepositUnitRef;
        private R_ConductorGrid _conGridDepositUnit;

        private R_TabStrip _tabStrip;
        private R_TabPage _tabPageDeposit;
        public bool _pageOnCRUDmode;


        #region PropertyID
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementViewModel.GetPropertyList();
                await _gridAgreementRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                _agreementViewModel.PropertyValueContext = lsProperty;
                await _gridAgreementRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region AgreementList
        private async Task R_ServiceAgreementListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementViewModel.GetAllAgreementList();
                eventArgs.ListEntityResult = _agreementViewModel.AgreementList;

                if (_agreementViewModel.AgreementList.Count > 0)
                {
                    _agreementViewModel._currentAgreement = _agreementViewModel.AgreementList[0];
                    await _gridDepositUnitRef.R_RefreshGrid(null);
                }
                else
                {
                    _agreementViewModel._currentAgreement = null;
                    _agreementViewModel.DepositUnitList.Clear();
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_DisplayAgreement(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMT05500AgreementDTO)eventArgs.Data;
                //  _agreementViewModel.PropertyValueContext = loParam.CPROPERTY_ID;

                _agreementViewModel._currentAgreement = loParam;
                await _gridDepositUnitRef.R_RefreshGrid(null);
            }
        }

        #endregion

        #region DepositUnit
        private async Task R_ServiceDepositUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementViewModel.GetAllDepositUnitList();
                eventArgs.ListEntityResult = _agreementViewModel.DepositUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region CHANGE TAB
        //CHANGE TAB
        private void Before_Open_Deposit(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMT05500Deposit);
            //you can use  _agreementViewModel._currentAgreement ==null, to assign on parameter;

            var temp = _gridAgreementRef.GetCurrentData();
            if (_agreementViewModel.AgreementList.Count > 0)
            {
                eventArgs.Parameter = _gridAgreementRef.GetCurrentData();
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pageOnCRUDmode = (bool)poValue;
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
