using GST00500Common;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GST00500Model.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Helpers;
//using APT00100FRONT;
using LMM06000Front;

namespace GST00500Front
{
    public partial class GST00500Draft : R_Page
    {
        private GST00500DraftViewModel _viewModelGST00500Draft = new();
        private R_Grid<GST00500DTO> _gridDraftTransRef;
        private R_ConductorGrid _conductorDraftTrans;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridDraftTransRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetListDraftTransaction(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGST00500Draft.GetAllDraftTransaction();
                eventArgs.ListEntityResult = _viewModelGST00500Draft.DraftTransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Grid_DisplayDraft(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal
                    && _viewModelGST00500Draft.DraftTransactionList.Count > 0)
                {
                    var loCurrentTemp = (GST00500DTO)eventArgs.Data;
                    //CONVERT DTO TO Call Another Program
                    _viewModelGST00500Draft._currentRecord =
                        R_FrontUtility.ConvertObjectToObject<GST00500CurrentRecordParamDTO>(loCurrentTemp);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        
        #region ButtonView
        private void R_Before_ServiceOpenOthersProgram(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var lcProgramId = _viewModelGST00500Draft._currentRecord.CPROGRAM_ID;
            lcProgramId = "LMM06000";
            //var lcProgramId= "LMM06000";
            //var lcProgramId = "GSM06500";

            switch (lcProgramId)
            {
                case "APT00100":
                    eventArgs.Parameter = _viewModelGST00500Draft._currentRecord;
                //    eventArgs.TargetPageType = typeof(APT00100);
                    break;
                case "LMM06000":
                    eventArgs.Parameter = _viewModelGST00500Draft._currentRecord;
                    eventArgs.TargetPageType = typeof(LMM06000);
                    break;
            }
        }

        private void R_After_ServiceOpenOthersProgram()
        {

        }
        #endregion
    }
}
