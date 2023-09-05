using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GLR00300Common;
using GLR00300Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace GLR00300Front
{
    public partial class GLR00300
    {
        private GLR00300ViewModel _viewModelGLR00300 = new();
        private R_Conductor _conducterRef;
        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await ServiceGetTrialBalance();
                await ServiceGetPeriod();
                await ServiceGetPrintMethod();
                await ServiceGetGetBudgetNo();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }



        private async Task ServiceGetTrialBalance()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetTrialBalance();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetPeriod()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGLR00300.GetPeriodYear();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ServiceGetPrintMethod()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetPrintMethod();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task OnChange(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
               
                await ServiceGetGetBudgetNo();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetGetBudgetNo()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGLR00300.GetBudgetNo();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task IsTrialBalanceTypeNormal(object poParam)
        {
            var loEx = new R_Exception();
            string TrialBalanceValue = (string)poParam;
            try
            {
                _viewModelGLR00300.TrialBalanceTypeValue = TrialBalanceValue;

                if (_viewModelGLR00300.TrialBalanceTypeValue != "N")
                {
                    _viewModelGLR00300._lIsTrialBalanceTypeIsNormal = false;
                }
                else
                {
                    _viewModelGLR00300._lIsTrialBalanceTypeIsNormal = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private Task OnChangedCheckBox()
        {
            var loEx = new R_Exception();
            try
            {
                if (!_viewModelGLR00300._lPrintByCenter)
                {
                    _viewModelGLR00300.FromCenter = new GLR00300GetAllCenter();
                    _viewModelGLR00300.ToCenter = new GLR00300GetAllCenter();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            return Task.CompletedTask;
        }

        #region LookUp_FROM_Account
        private void BeforeOpenLookUpFromAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00510ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CGLACCOUNT_TYPE = "N";
                loParam.CUSER_LANGUAGE = _clientHelper.UserId;
                eventArgs.Parameter = loParam;

                eventArgs.TargetPageType = typeof(GSL00510);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void AfterOpenLookUpFromAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00510DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.FromAccount;
                loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
                loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion

        #region LookUp_TO_Account
        private void BeforeOpenLookUpToAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00510ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CGLACCOUNT_TYPE = "N";
                loParam.CUSER_LANGUAGE = _clientHelper.UserId;
                eventArgs.Parameter = loParam;

                eventArgs.TargetPageType = typeof(GSL00510);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void AfterOpenLookUpToAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00510DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.ToAccount;
                loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
                loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion

        #region LookUp_FROM_PrintByCenter
        private void BeforeOpenLookupFromPrintByCenter(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00900ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CUSER_ID = _clientHelper.UserId;
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00900);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void AfterOpenLookUpFromPrintByCenter(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00900DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.FromCenter;
                loGetData.CCENTER_CODE = loTempResult.CCENTER_CODE;
                loGetData.CCENTER_NAME = loTempResult.CCENTER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region LookUp_TO_PrintByCenter
        private void BeforeOpenLookupToPrintByCenter(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSL00900ParameterDTO();
                loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                loParam.CUSER_ID = _clientHelper.UserId;
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00900);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void AfterOpenLookUpToPrintByCenter(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00900DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                var loGetData = _viewModelGLR00300.ToCenter;
                loGetData.CCENTER_CODE = loTempResult.CCENTER_CODE;
                loGetData.CCENTER_NAME = loTempResult.CCENTER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion

        #region ButtonReport
        private async Task GenerateReport()
        {
            var loEx = new R_Exception();
            bool isValueEmpty = false;
            try
            {
                if (string.IsNullOrEmpty(_viewModelGLR00300.FromAccount.CGLACCOUNT_NO))
                {
                    loEx.Add(new Exception("Please select From Account No.!"));
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(_viewModelGLR00300.ToAccount.CGLACCOUNT_NO))
                {
                    loEx.Add(new Exception("Please select To Account No.!"));
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(_viewModelGLR00300.FromCenter.CCENTER_CODE) &&
                    _viewModelGLR00300._lPrintByCenter)
                {
                    loEx.Add(new Exception("Please select From Center Code.!"));
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(_viewModelGLR00300.ToCenter.CCENTER_CODE) &&
                    _viewModelGLR00300._lPrintByCenter)
                {
                    loEx.Add(new Exception("Please select To Center Code.!"));
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(_viewModelGLR00300.BudgetNoValue) &&
                    _viewModelGLR00300._lPrintBudget)
                {
                    loEx.Add(new Exception("Please select Budget No.!"));
                    goto EndBlock;
                }

                var lcPeriodYear = _viewModelGLR00300.PeriodYear.ToString();
                var lcPeriodMonth = _viewModelGLR00300.PeriodId;
                var lcConcatPeriod = lcPeriodYear + "-" + lcPeriodMonth;

                //GET VALUE TRIAL BALANCE TYPE (CODE DAN DESC)
                GLR00300DTO loCurrentTrialBalanceType = _viewModelGLR00300.TrialBalanceList.FirstOrDefault(item
                    => item.CCODE == _viewModelGLR00300.TrialBalanceTypeValue);
                //GET VALUE Currency TYPE (CODE DAN DESC)
                GLR00300DTO loCurrentCurrency = _viewModelGLR00300.CurrencyTypeList.FirstOrDefault(item
                    => item.CCODE == _viewModelGLR00300.CurrencyTypeValue);
                //GET VALUE ADJ MODE TYPE (CODE DAN DESC)
                GLR00300DTO loCurrentAdjMode = _viewModelGLR00300.JournalAdustModeList.FirstOrDefault(item
                    => item.CCODE == _viewModelGLR00300.JournalAdjustModeValue);


                //set Parameter FROM FRONT TO BACK
                var loParam = new GLR00300ParamDBToGetReportDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPERIOD_NAME = lcConcatPeriod,
                    CFROM_PERIOD_NO = lcPeriodMonth,
                    CTO_PERIOD_NO = lcPeriodMonth,
                    CTB_TYPE_CODE = loCurrentTrialBalanceType.CCODE,
                    CTB_TYPE_NAME = loCurrentTrialBalanceType.CDESCRIPTION,
                    CCURRENCY_TYPE_CODE = loCurrentCurrency.CCODE,
                    CCURRENCY_TYPE_NAME = loCurrentCurrency.CDESCRIPTION,
                    CJOURNAL_ADJ_MODE_CODE = loCurrentAdjMode.CCODE,
                    CJOURNAL_ADJ_MODE_NAME = loCurrentAdjMode.CDESCRIPTION,
                    CYEAR = lcPeriodYear,
                    CFROM_ACCOUNT_NO = _viewModelGLR00300.FromAccount.CGLACCOUNT_NO,
                    CTO_ACCOUNT_NO = _viewModelGLR00300.ToAccount.CGLACCOUNT_NO,

                    CFROM_CENTER_CODE = _viewModelGLR00300.FromCenter.CCENTER_CODE,
                    CTO_CENTER_CODE = _viewModelGLR00300.ToCenter.CCENTER_CODE,

                    CPRINT_METHOD_NAME = "_viewModelGLR00300.PrintMethodValue",
                    CBUDGET_NO = _viewModelGLR00300.BudgetNoValue
                };
                var loValidate = await R_MessageBox.Show("", "Are you sure print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                        "R_DefaultServiceUrlGL",
                        "GL",
                        "api/GLR00300Report/AllTrialBalanceReportPost",
                        "api/GLR00300Report/AllTrialBalanceReportGet",
                        loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            R_DisplayException(loEx);
        }
        public async Task ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
             //   var loParam = (GLR00300ParamDBToGetReportDTO)eventArgs.Data;


                if (string.IsNullOrEmpty(_viewModelGLR00300.FromAccount.CGLACCOUNT_NO))
                    loEx.Add(new Exception("Fom Account is required."));
                //if (string.IsNullOrEmpty(_viewModelGLR00300.FromAccount.CGLACCOUNT_NO))
                //    loEx.Add(new Exception("Fom Account is required."));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}
