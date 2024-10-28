using BlazorClientHelper;
using Lookup_PMModel.ViewModel.LML00900;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700FRONT;

namespace UtilityFront
{
    public partial class TryLookUp
    {
        private LookupLML00900ViewModel _viewModel = new();
        [Inject] private IClientHelper _clientHelper { get; set; }

        #region LookUp_TO_Account
        private void BeforeOpenLookUpToAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //LML00900ParameterDTO temp = new();
                //eventArgs.Parameter = temp;
                //eventArgs.TargetPageType = typeof(LML00900);
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
                var loTempResult = (LML00900FrontDTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //var loGetData = _viewModel.InitialProcess;
                //loGetData.CMAX_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
                //loGetData.CMAX_GLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }



        #endregion
        public bool IsButtonVisible = true;
        private void BeforeOpenPopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT01700ParameterFrontChangePageDTO temp = new PMT01700ParameterFrontChangePageDTO
                {
                    CPROPERTY_ID = "ASHMD",
                    CDEPT_CODE = "FIN",
                    CTRANS_CODE = "802043",
                    CREF_NO = "PMM-00003-202407",
                    //For Unit Info, Charges, and Deposit
                    CBUILDING_ID = "TW-A",
                    CBUILDING_NAME = "Tower A",
                    //Updated : For call from Page PMT01200
                    CALLER_ACTION = "ADD",
                    COTHER_UNIT_ID = "",
                    CCHARGE_MODE = "",
                    CFLOOR_ID = ""

                    //CPROPERTY_ID = "ASHMD",
                    //CDEPT_CODE = "FIN",
                    //CTRANS_CODE = "802043",
                    //CREF_NO = "PMM-00007-202407",
                    ////For Unit Info, Charges, and Deposit
                    //CBUILDING_ID = "TW-A",
                    //CBUILDING_NAME = "Tower A",
                    ////Updated : For call from Page PMT01200
                    //CALLER_ACTION = "ADD",
                    //COTHER_UNIT_ID = "",
                    //CCHARGE_MODE = "01",
                    //CFLOOR_ID =""
                };

                eventArgs.Parameter = temp;
                eventArgs.TargetPageType = typeof(PMT01700LOO_Offer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        private void BeforeOpenPopup2(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT01700ParameterFrontChangePageDTO temp = new PMT01700ParameterFrontChangePageDTO
                {
                    CPROPERTY_ID = "ASHMD",
                    CDEPT_CODE = "FIN",
                    CTRANS_CODE = "802043",
                    CREF_NO = "PMM-00003-202407",
                    //For Unit Info, Charges, and Deposit
                    CBUILDING_ID = "TW-A",
                    CBUILDING_NAME = "Tower A",
                    //Updated : For call from Page PMT01200
                    CALLER_ACTION = "ADD",
                    COTHER_UNIT_ID = "",
                    CCHARGE_MODE = "",
                    CFLOOR_ID = ""

                    //CPROPERTY_ID = "ASHMD",
                    //CDEPT_CODE = "FIN",
                    //CTRANS_CODE = "802043",
                    //CREF_NO = "PMM-00007-202407",
                    ////For Unit Info, Charges, and Deposit
                    //CBUILDING_ID = "TW-A",
                    //CBUILDING_NAME = "Tower A",
                    ////Updated : For call from Page PMT01200
                    //CALLER_ACTION = "ADD",
                    //COTHER_UNIT_ID = "",
                    //CCHARGE_MODE = "01",
                    //CFLOOR_ID =""
                };

                eventArgs.Parameter = temp;
                eventArgs.TargetPageType = typeof(PMT01700LOO_Offer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        protected override Task<object> R_Set_Result_Detail()
        {
            var lcResult = "agagbsga";

            return Task.FromResult<object>(lcResult);
        }
        private void changeVAR()
        {
            IsButtonVisible = false;
        }
        private void changeVAR2()
        {
            IsButtonVisible = true;
        }
    }
}
