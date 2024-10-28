using BlazorClientHelper;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700FRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityModel;

namespace UtilityFront
{
    public partial class PMT06000Info : R_Page
    {
        R_Conductor? _conductorService;
        R_Grid<PMT06000OvtServiceGridDTO>? _gridService;
        PMT06000ViewModel _viewModel = new PMT06000ViewModel();

        private R_Lookup? R_LookupServiceLookup;

        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabUnit;
        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //await _viewModel.GetDepositList();
                //eventArgs.ListEntityResult = _viewModel.oListDeposit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //PMT01700LOO_Deposit_DepositDetailDTO loParam;
                //    loParam = new PMT01700LOO_Deposit_DepositDetailDTO();
                //    if (eventArgs.Data != null)
                //    {
                //        loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_Deposit_DepositDetailDTO>(eventArgs.Data);
                //    }
                //    /*
                //    else
                //    {
                //        loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                //        loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                //        loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                //        loParam.CTRANS_CODE = "802041";
                //        loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                //        loParam.CUSER_ID = _clientHelper.UserId;
                //    };
                //    */
                //    await _viewModel.GetEntity(loParam);

                //    eventArgs.Result = _viewModel.oEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Lookup
        private void BeforeOpenLookUpUtilityLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            //LML00200ParameterDTO? param = null;
            //if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            //{
            //    param = new LML00200ParameterDTO()
            //    {
            //        CCOMPANY_ID = _clientHelper.CompanyId,
            //        CUSER_ID = _clientHelper.UserId,
            //        CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
            //        CCHARGE_TYPE_ID = "03",
            //    };
            //}
            //eventArgs.Parameter = param;
            //eventArgs.TargetPageType = typeof(LML00200);
        }

        private void AfterOpenLookUputilityLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00200DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                //loTempResult = (LML00200DTO)eventArgs.Result;
                //if (loTempResult == null)
                //    return;
                ////loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                //_viewModel.Data.CDEPOSIT_ID = loTempResult.CCHARGES_ID;
                //_viewModel.Data.CDEPOSIT_NAME = loTempResult.CCHARGES_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private void BeforeOpenPopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT01700ParameterFrontChangePageDTO temp = new PMT01700ParameterFrontChangePageDTO
                {

                    CPROPERTY_ID = "ASHMD",
                    CDEPT_CODE = "ACC",
                    CTRANS_CODE = "802043",
                    CREF_NO = "PMM-00007-202407",
                    //For Unit Info, Charges, and Deposit
                    CBUILDING_ID = "TW-A",
                    CBUILDING_NAME = "Tower A",
                    //Updated : For call from Page PMT01200
                    CALLER_ACTION = "ADD",
                    COTHER_UNIT_ID = "",
                    CCHARGE_MODE = "01",
                    CFLOOR_ID = ""
                };

                eventArgs.Parameter = temp;
                eventArgs.TargetPageType = typeof(PMT01700LOO_Offer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

            #endregion
            //private void General_Before_Open_Unit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
            //{
            //    //eventArgs.Parameter = _viewModel.oParameter;
            //    //eventArgs.TargetPageType = typeof(PMT01700LOO_UnitCharges_Charges);
            //}

        }

    }
}
