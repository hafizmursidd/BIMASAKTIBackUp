using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLR00300Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace GLR00300Front
{
    public partial class GLR00300
    {
        private GLR00300ViewModel _viewModelGLR00300 = new();

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
                await ServiceGetPeriod();
                await ServiceGetTrialBalance();
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
            var abc = poParam;
            try
            {
                var loparamnew = _viewModelGLR00300.TrialBalanceType;
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

        #region LookUpAccount

        private void BeforeOpenLookUpAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            //var param = new LML00200ParameterDTO()
            //{
            //    CCOMPANY_ID = clientHelper.CompanyId,
            //    CUSER_ID = clientHelper.UserId,
            //    CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
            //    CCHARGE_TYPE_ID = ""
            //};
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(LML00200);

        }
        private void AfterOpenLookUpAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            //var loTempResult = (LML00200DTO)eventArgs.Result;
            //if (loTempResult == null)
            //    return;

            //var loGetData = (LMM06000BillingRuleDTO)_conductorBillingRuleRef.R_GetCurrentData();

            //loGetData.CBOOKING_FEE_CHARGE_ID = loTempResult.CCHARGES_ID;
            //loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

        #endregion

        #region LookUpPrintByCEnter
        private void BeforeOpenLookupPrintByCenter(R_BeforeOpenLookupEventArgs eventArgs)
        {
            //var param = new LML00200ParameterDTO()
            //{
            //    CCOMPANY_ID = clientHelper.CompanyId,
            //    CUSER_ID = clientHelper.UserId,
            //    CPROPERTY_ID = BillingRuleViewModel.PropertyValueContext,
            //    CCHARGE_TYPE_ID = ""
            //};
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(LML00200);

        }
        private void AfterOpenLookUpPrintByCenter(R_AfterOpenLookupEventArgs eventArgs)
        {
            //var loTempResult = (LML00200DTO)eventArgs.Result;
            //if (loTempResult == null)
            //    return;

            //var loGetData = (LMM06000BillingRuleDTO)_conductorBillingRuleRef.R_GetCurrentData();

            //loGetData.CBOOKING_FEE_CHARGE_ID = loTempResult.CCHARGES_ID;
            //loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

        #endregion

        #region ButtonReport
        private async Task GenerateReport()
        {
            var loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
    }
}
