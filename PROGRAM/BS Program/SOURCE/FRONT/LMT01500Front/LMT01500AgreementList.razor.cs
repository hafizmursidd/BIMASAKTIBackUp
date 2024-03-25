using BlazorClientHelper;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;

namespace LMT01500Front
{
    public partial class LMT01500AgreementList
    {

        #region Master List

        private readonly LMT01500AgreementListViewModel _viewModelLMT01500AgreementList = new();
        private R_ConductorGrid? _conductorLMT01500AgreementList;
        private R_Grid<LMT01500AgreementListOriginalDTO>? _gridRefLMT01500AgreementList;


        private bool _isDataExist;

        private bool _pageAgreementListOnCRUDmode;

        [Inject] private IClientHelper? _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelLMT01500AgreementList.GetPropertyList();
                await _viewModelLMT01500AgreementList.GetVarGsmTransactionCode();
                _isDataExist = true;
                await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Fungsi External

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelLMT01500AgreementList._cPropertyId = poParam;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                switch (_tabStripRef.ActiveTab.Id)
                {
                    case "Agreement":
                        await _tabAgreementRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "UnitInfo":
                        await _tabUnitInfoRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "ChargesInfo":
                        await _tabChargesInfoRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "InvoicePlan":
                        await _tabInvoicePlanRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "Deposit":
                        await _tabDepositRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "Document":
                        await _tabDocumentRef.InvokeRefreshTabPageAsync(null);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnClickNexttoPageAgreementButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (!loException.HasError)
                {
                    //IsSuccess = true;
                    await _tabStripRef?.SetActiveTabAsync("Agreement")!;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task BlankFunction()
        {
            var loException = new R_Exception();
            //APM00500ProductDetailDTO? poEntityAPM00500Detail = (APM00500ProductDetailDTO)_conductorAPM00500ProductDetail.R_GetCurrentData();

            try
            {
                var llTrue = await R_MessageBox.Show("", "You Clicked the Button!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #endregion

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelLMT01500AgreementList.GetAgreementList();
                eventArgs.ListEntityResult = _viewModelLMT01500AgreementList.loListLMT01500Agreement;
                _isDataExist = true;
                /*
                if (!_viewModelLMM02500.loGridListLMM02500.Any())
                {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    _loTabParameter.CTENANT_GROUP_ID = null;
                    _loTabParameter.CTENANT_GROUP_NAME = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                    _isDataExist = false;
                }
                else if (_viewModelLMM02500.loGridListLMM02500.Any())
                {
                    _loTabParameter.CTENANT_GROUP_ID =
                        _viewModelLMM02500.loGridListLMM02500.FirstOrDefault()!.CTENANT_GROUP_ID;
                    _isDataExist = true;
                }
                */
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //Untuk Tab - 1
                //var loConvertTabParameter = R_FrontUtility.ConvertObjectToObject<LMM02500TabParameterDTO>(eventArgs.Data);
                //_loTabParameter = loConvertTabParameter;
                //if (_clientHelper?.UserId != null) _loTabParameter.CUSER_LOGIN_ID = _clientHelper.UserId;
                await Task.CompletedTask;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        #region Master Tab
        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabAgreementRef;
        private R_TabPage? _tabUnitInfoRef;
        private R_TabPage? _tabChargesInfoRef;
        private R_TabPage? _tabInvoicePlanRef;
        private R_TabPage? _tabDepositRef;
        private R_TabPage? _tabDocumentRef;

        //private LMM02500TabParameterDTO _loTabParameter = new();

        #region Tab List
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "Agreement":
                    await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
                    break;
                case "UnitInfo":
                    await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
                    break;
                case "ChargesInfo":
                    await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
                    break;
                case "InvoicePlan":
                    await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
                    break;
                case "Deposit":
                    await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
                    break;
                case "Document":
                    await _gridRefLMT01500AgreementList.R_RefreshGrid(null);
                    break;
                default:
                    break;
            }

        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            _viewModelLMT01500AgreementList._lComboBoxProperty = true;
            eventArgs.Cancel = _pageAgreementListOnCRUDmode;

            if (eventArgs.TabStripTab.Id != "AgreementList")
            {
                _viewModelLMT01500AgreementList._lComboBoxProperty = false;
            }
        }
        #endregion

        #region Utulities
        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                //_viewModelLMM02500._comboBoxEnabled = (bool)poValue;
                _pageAgreementListOnCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        #endregion

        #region Master Tab

        #region Tab Agreement
        private void General_Before_Open_Agreement_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelLMT01500AgreementList._cPropertyId;
            eventArgs.TargetPageType = typeof(LMT01500Agreement);
        }
        #endregion

        #region Tab UnitInfo
        private void General_Before_Open_UnitInfo_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelLMT01500AgreementList._cPropertyId;
            eventArgs.TargetPageType = typeof(LMT01500UnitInfo);
        }
        #endregion

        #region Tab ChargesInfo
        private void General_Before_Open_ChargesInfo_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelLMT01500AgreementList._cPropertyId;
            eventArgs.TargetPageType = typeof(LMT01500ChargesInfo);
        }
        #endregion

        #region Tab InvoicePlan
        private void General_Before_Open_InvoicePlan_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelLMT01500AgreementList._cPropertyId;
            eventArgs.TargetPageType = typeof(LMT01500InvoicePlan);
        }
        #endregion

        #region Tab Deposit
        private void General_Before_Open_Deposit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelLMT01500AgreementList._cPropertyId;
            eventArgs.TargetPageType = typeof(LMT01500Deposit);
        }
        #endregion

        #region Tab Document
        private void General_Before_Open_Document_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelLMT01500AgreementList._cPropertyId;
            eventArgs.TargetPageType = typeof(LMT01500Document);
        }
        #endregion

        #endregion

        #endregion
    }
}
