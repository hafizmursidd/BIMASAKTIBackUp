using LMT05500Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using LMT05500Common.DTO;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_LMModel.ViewModel.LML00200;
using Lookup_LMModel.ViewModel.LML00600;

namespace LMT05500Front
{
    public partial class LMT05500DepositInfo
    {
        private LMT05500DepositViewModel _depositViewModel = new();
        private LMT05500AgreementViewModel _agreementViewModel = new();

        private R_Conductor _conductDepositInfo;
        private R_Lookup R_Lookup_Button_Contr;
        private R_Lookup R_Lookup_Button_Deposit;
        [Inject] IClientHelper clientHelper { get; set; }
        private bool _btnCrud = true;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMT05500DepositListDTO)poParameter;
                if (loParam != null)
                {
                    _btnCrud = true;
                    _depositViewModel._currentDeposit = loParam;
                    await _conductDepositInfo.R_GetEntity(null);
                }
                else
                {
                    _btnCrud = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region CRUD
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            LMT05500DepositInfoFrontDTO loParam = new();

            try
            {
                if (_depositViewModel._currentDeposit != null)
                {                   
                    loParam = new LMT05500DepositInfoFrontDTO()
                    {
                        CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                        CDEPT_CODE = _depositViewModel._currentDeposit.CDEPT_CODE,
                        CTRANS_CODE = _depositViewModel._currentDeposit.CTRANS_CODE,
                        CREF_NO = _depositViewModel._currentDeposit.CREF_NO,
                        CSEQ_NO = _depositViewModel._currentDeposit.CSEQ_NO
                    };
                    await _depositViewModel.GetEntity(loParam);
                    eventArgs.Result = _depositViewModel._depositInfoData;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoFrontDTO>(eventArgs.Data);

                await _depositViewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _depositViewModel._depositInfoData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMT05500DepositInfoFrontDTO)eventArgs.Data;

                //NEED TESTING
                //await _depositViewModel.GetEntity(loData);
                //if (_depositViewModel._depositInfoData.CREF_NO != null)
                await _depositViewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Lookup
        private void BeforeOpenLookUp_Contractor(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                CCUSTOMER_TYPE = "02"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void AfterOpenLookUp_Contractor(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMT05500DepositInfoFrontDTO)_conductDepositInfo.R_GetCurrentData();

            loGetData.CCONTRACTOR_ID = loTempResult.CTENANT_ID;
            loGetData.CCONTRACTOR_NAME = loTempResult.CTENANT_NAME;
        }
        private void BeforeOpenLookUp_Deposit(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                CCHARGE_TYPE_ID = "03"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }
        private void AfterOpenLookUp_Deposit(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            var loGetData = (LMT05500DepositInfoFrontDTO)_conductDepositInfo.R_GetCurrentData();

            loGetData.CDEPOSIT_ID = loTempResult.CCHARGES_ID;
            loGetData.CDEPOSIT_NAME = loTempResult.CCHARGES_NAME;
        }
        #endregion

        #region OnLostFokus
        private async Task LostFocusLookupContractor()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _depositViewModel.Data;
                if (!string.IsNullOrWhiteSpace(loGetData.CCONTRACTOR_ID))
                {
                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                    var param = new LML00600ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                        CUSER_ID = clientHelper.UserId,
                        CCUSTOMER_TYPE = "02",
                        CSEARCH_TEXT = _depositViewModel.Data.CCONTRACTOR_ID!,
                    };
                    var loResult = await loLookupViewModel.GetTenant(param);

                    
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(LookupLMFrontResources.Resources_LookupLM_Class),
                                "_ErrLookup01"));
                        loGetData.CCONTRACTOR_ID = "";
                        loGetData.CCONTRACTOR_NAME = "";
                    }
                    else
                    {
                        loGetData.CCONTRACTOR_ID = loResult.CTENANT_ID;
                        loGetData.CCONTRACTOR_NAME = loResult.CTENANT_NAME;
                    }
                     
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task LostFocusLookupDeposit()
        {
            var loEx = new R_Exception();

            try
            {
                var loGetData = _depositViewModel.Data;
                if (!string.IsNullOrWhiteSpace(loGetData.CDEPOSIT_ID))
                {
                    LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                    var param = new LML00200ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID,
                        CUSER_ID = clientHelper.UserId,
                        CCHARGE_TYPE_ID = "03",
                        CSEARCH_TEXT = _depositViewModel.Data.CCONTRACTOR_ID!,
                    };
                    var loResult = await loLookupViewModel.GetUnitCharges(param);


                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(LookupLMFrontResources.Resources_LookupLM_Class),
                                "_ErrLookup01"));
                        loGetData.CDEPOSIT_ID = "";
                        loGetData.CDEPOSIT_NAME = "";
                    }
                    else
                    {
                        loGetData.CDEPOSIT_ID = loResult.CCHARGES_ID;
                        loGetData.CDEPOSIT_NAME = loResult.CCHARGES_NAME;
                    }

                }
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
