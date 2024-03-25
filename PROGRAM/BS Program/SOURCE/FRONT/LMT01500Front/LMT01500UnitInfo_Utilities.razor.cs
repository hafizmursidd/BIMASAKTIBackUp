using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using LMT01500Common.DTO._3._Unit_Info;
using LMT01500Common.Utilities;
using LMT01500Model.ViewModel;
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

namespace LMT01500Front;

public partial class LMT01500UnitInfo_Utilities
{
    private readonly LMT01500UnitInfo_UtilitiesViewModel _viewModelLMT01500UnitInfo_Utilities = new();
    [Inject] private IClientHelper? _clientHelper { get; set; }

    private R_Conductor _conductorUnitInfo_Utilities;
    private R_Grid<LMT01500UnitInfoUnit_UtilitiesListDTO> _gridUnitInfo_Utilities;
    private R_NumericTextBox<decimal> FocusLabelEdit;

    private string _property = "";

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            //GET PARAMETER
         //   _viewModelLMT01500UnitInfo_Utilities.loParameterList = (LMT01500GetHeaderParameterDTO)poParameter;

            if (!string.IsNullOrEmpty(_viewModelLMT01500UnitInfo_Utilities.loParameterList.CREF_NO))
            {
             //   _viewModelLMT01500UnitInfo_Utilities.();
                _gridUnitInfo_Utilities.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    private async Task GetListUnitInfo_Utilities(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModelLMT01500UnitInfo_Utilities.GetUnitInfoList();
            eventArgs.ListEntityResult = _viewModelLMT01500UnitInfo_Utilities.loListLMT01500UnitInfo_Utilities;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }
    private async Task ServiceGetOneRecord_Utilities(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<LMT01500UnitInfoUnit_UtilitiesDetailDTO>(eventArgs.Data);

            await _viewModelLMT01500UnitInfo_Utilities.GetEntity(loParam);
            eventArgs.Result = _viewModelLMT01500UnitInfo_Utilities.loEntityUnitInfo_Utilities;
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
                    //Focus Async belum ditempel pada fieldnya
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
            var loParam = (LMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            await _viewModelLMT01500UnitInfo_Utilities.ServiceDelete(loParam);
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
            var loParam = (LMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            await _viewModelLMT01500UnitInfo_Utilities.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModelLMT01500UnitInfo_Utilities.loEntityUnitInfo_Utilities;
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

    #region LookupCharge
    private R_Lookup R_Lookup_Charge;
    private void BeforeOpenLookUp_ChargeID(R_BeforeOpenLookupEventArgs eventArgs)
    {

        var param = new LML00400ParameterDTO()
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CPROPERTY_ID = _property,
            CCHARGE_TYPE_ID = ""
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(LML00400);

    }
    private void AfterOpenLookUp_ChargeID(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (LML00400DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (LMT01500UnitInfoUnit_UtilitiesDetailDTO)_conductorUnitInfo_Utilities.R_GetCurrentData();

        loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
        loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;

    }
    #endregion
    #region LookupTax
    private R_Lookup R_Lookup_Tax;
    private void BeforeOpenLookUp_Tax(R_BeforeOpenLookupEventArgs eventArgs)
    {

        var param = new LML00100ParameterDTO()
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CUSER_ID = _clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(LML00100);

    }
    private void AfterOpenLookUp_Tax(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (LML00100DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (LMT01500UnitInfoUnit_UtilitiesDetailDTO)_conductorUnitInfo_Utilities.R_GetCurrentData();

        loGetData.CTAX_ID = loTempResult.CTAX_ID;
        loGetData.CTAX_NAME = loTempResult.CTAX_NAME;

    }
    #endregion

}

