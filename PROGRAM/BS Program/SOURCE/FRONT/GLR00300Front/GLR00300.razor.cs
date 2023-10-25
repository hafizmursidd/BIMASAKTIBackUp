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
                await ServiceGetInitialProcess();
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
        private async Task ServiceGetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGLR00300.GetInitialProcess();
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
        private Task OnChangedCheckBoxPrintByCenter()
        {
            var loEx = new R_Exception();
            try
            {
                if (!_viewModelGLR00300._lPrintByCenter)
                {
                    _viewModelGLR00300.FromCenter.CCENTER_CODE = "";
                    _viewModelGLR00300.FromCenter.CCENTER_NAME = "";
                    _viewModelGLR00300.ToCenter.CCENTER_CODE = "";
                    _viewModelGLR00300.ToCenter.CCENTER_NAME = "";
                }
                else if (_viewModelGLR00300._lPrintByCenter && string.IsNullOrEmpty(_viewModelGLR00300.FromCenter.CCENTER_CODE) && string.IsNullOrEmpty(_viewModelGLR00300.ToCenter.CCENTER_CODE))
                {
                    _viewModelGLR00300.FromCenter.CCENTER_NAME = "Please Select Center Name.!";
                    _viewModelGLR00300.ToCenter.CCENTER_NAME = "Please Select Center Name.!";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            return Task.CompletedTask;
        }
        private Task OnChangedCheckBoxPrintBudget()
        {
            var loEx = new R_Exception();
            try
            {
                if (!_viewModelGLR00300._lPrintBudget)
                {
                    _viewModelGLR00300.BudgetNoValue = "";
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
                loParam.CUSER_ID = _clientHelper.UserId;
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
                loParam.CUSER_ID = _clientHelper.UserId;
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
            bool loFlag = false;
            try
            {
                #region ValidationEmpty

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

                #endregion

                var lcPeriodYear = _viewModelGLR00300.PeriodYear.ToString();

                // Inject Dummmy
                //var loParam = new GLR00300ParamDBToGetReportDTO()
                //{
                //    CCOMPANY_ID = "RCD",
                //    CUSER_ID = "HMC",
                //    CTB_TYPE_CODE = "A",
                //    CJOURNAL_ADJ_MODE_CODE = "S",
                //    CCURRENCY_TYPE_CODE = "L",
                //    CFROM_ACCOUNT_NO = "15.10.0001",
                //    CTO_ACCOUNT_NO = "15.10.9999",
                //    CFROM_CENTER_CODE = "MMKT",
                //    CTO_CENTER_CODE = "MMKT",
                //    CYEAR = "2023",
                //    CFROM_PERIOD_NO = "03",
                //    CTO_PERIOD_NO = "03",
                //    CPRINT_METHOD_CODE = "00",
                //    CBUDGET_NO = _viewModelGLR00300.BudgetNoValue
                //};

                //set Parameter FROM FRONT TO BACK

                var loParam = new GLR00300ParamDBToGetReportDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CFROM_PERIOD_NO = _viewModelGLR00300.PeriodId,
                    CTO_PERIOD_NO = _viewModelGLR00300.PeriodId,
                    CTB_TYPE_CODE = _viewModelGLR00300.TrialBalanceTypeValue,
                    CCURRENCY_TYPE_CODE = _viewModelGLR00300.CurrencyTypeValue,
                    CJOURNAL_ADJ_MODE_CODE = _viewModelGLR00300.JournalAdjustModeValue,
                    CYEAR = lcPeriodYear,
                    CFROM_ACCOUNT_NO = _viewModelGLR00300.FromAccount.CGLACCOUNT_NO,
                    CTO_ACCOUNT_NO = _viewModelGLR00300.ToAccount.CGLACCOUNT_NO,
                    CFROM_CENTER_CODE = _viewModelGLR00300.FromCenter.CCENTER_CODE,
                    CTO_CENTER_CODE = _viewModelGLR00300.ToCenter.CCENTER_CODE,
                    CPRINT_METHOD_CODE = _viewModelGLR00300.PrintMethodValue,
                    CBUDGET_NO = _viewModelGLR00300.BudgetNoValue
                };

                var loValidate = await R_MessageBox.Show("", "Are you sure print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    if (loParam.CTB_TYPE_CODE == "N")
                    {
                        if (loParam.CJOURNAL_ADJ_MODE_CODE == "S")
                        {
                            if ((!_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (N,S,false,false)
                            {
                                loFlag=true;
                                //FORMAT A
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatA/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatA/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if (!_viewModelGLR00300._lPrintByCenter && _viewModelGLR00300._lPrintBudget) // (N,S,false,true)
                            {
                                loFlag = true;
                                //FORMAT B
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatB/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatB/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                        else if (loParam.CJOURNAL_ADJ_MODE_CODE == "M")
                        {
                            if ((!_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (N,M,false,false)
                            {
                                loFlag = true;
                                //FORMAT C
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatC/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatC/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if ((!_viewModelGLR00300._lPrintByCenter) && (_viewModelGLR00300._lPrintBudget)) // (N,M,false,true)
                            {
                                loFlag = true;
                                //FORMAT D
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatD/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatD/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                    }
                    else if (loParam.CTB_TYPE_CODE == "A")
                    {
                        if (loParam.CJOURNAL_ADJ_MODE_CODE == "S")
                        {
                            if ((_viewModelGLR00300._lPrintByCenter) && !(_viewModelGLR00300._lPrintBudget)) // (A,S,True,false)
                            {
                                loFlag = true;
                                //FORMAT E
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatE/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatE/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if (_viewModelGLR00300._lPrintByCenter && _viewModelGLR00300._lPrintBudget) // (A,S,true,true)
                            {
                                loFlag = true;
                                //FORMAT F
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatF/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatF/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                        else if (loParam.CJOURNAL_ADJ_MODE_CODE == "M")
                        {
                            if ((_viewModelGLR00300._lPrintByCenter) && (!_viewModelGLR00300._lPrintBudget)) // (A,M,true,false)
                            {
                                loFlag = true;
                                //FORMAT G
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatG/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatG/AllTrialBalanceReportGet",
                                    loParam);
                            }
                            else if ((_viewModelGLR00300._lPrintByCenter) && (_viewModelGLR00300._lPrintBudget)) // (A,M,true,true)
                            {
                                loFlag = true;
                                //FORMAT H
                                await _reportService.GetReport(
                                    "R_DefaultServiceUrlGL",
                                    "GL",
                                    "rpt/GLR00300ReportFormatH/AllTrialBalanceReportPost",
                                    "rpt/GLR00300ReportFormatH/AllTrialBalanceReportGet",
                                    loParam);
                            }
                        }
                    }
                    if (!loFlag)
                    {
                        await R_MessageBox.Show("", "Can not process this command!", R_eMessageBoxButtonType.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            R_DisplayException(loEx);
        }

        #endregion
    }
}
