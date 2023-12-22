using GST00500Common;
using GST00500Model.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Enums;
//using APT00100FRONT;
using LMM06000Front;

namespace GST00500Front
{
    public partial class GST00500Outbox : R_Page
    {
        private GST00500OutboxViewModel _viewModelGST00500Outbox = new();
        private R_Grid<GST00500DTO> _gridOutboxTransRef;
        private R_Grid<GST00500ApprovalStatusDTO> _gridOutboxTransStatusRef;
        private R_ConductorGrid _conductorOutboxTrans;
        private R_ConductorGrid _conductorOutboxTransStatus;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridOutboxTransRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #region Outbox

        private async Task ServiceGetListOutboxTransaction(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGST00500Outbox.GetAllOutboxTransaction();
                GST00500DTO ParamTransactionStatus = _viewModelGST00500Outbox.OutboxTransactionList.FirstOrDefault();
                eventArgs.ListEntityResult = _viewModelGST00500Outbox.OutboxTransactionList;

                //TO DISPLAY GRID APPROVE BY, ON THE BOTTOM OF MAIN GRID
                //if (_viewModelGST00500Outbox.OutboxTransactionList.Count > 0)
                //{
                //    await _gridOutboxTransStatusRef.R_RefreshGrid(ParamTransactionStatus);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Grid_DisplayOutbox(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal 
                    && _viewModelGST00500Outbox.OutboxTransactionList.Count > 0)
                {
                    GST00500DTO ParamTransactionStatus = (GST00500DTO)eventArgs.Data;
                    await _gridOutboxTransStatusRef.R_RefreshGrid(ParamTransactionStatus);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceGetListOutboxTransactionStatus(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                GST00500DTO ParamTransactionStatus = (GST00500DTO)eventArgs.Parameter;
                await _viewModelGST00500Outbox.GetAllApprovalStatus(ParamTransactionStatus);
                eventArgs.ListEntityResult = _viewModelGST00500Outbox.OutboxApprovalStatusTransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region ButtonView
        private void R_Before_ServiceOpenOthersProgram(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var lcProgramId = "APT00100";
            //var lcProgramId= "LMM06000";
            //var lcProgramId = "GSM06500";

            switch (lcProgramId)
            {
                case "APT00100":
                    eventArgs.Parameter = _viewModelGST00500Outbox._currentRecord;
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
