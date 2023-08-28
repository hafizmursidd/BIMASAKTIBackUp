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

namespace GST00500Front
{
    public partial class GST00500Outbox
    {
        private GST00500OutboxViewModel _viewModelGST00500Outbox = new();
        private R_Grid<GST00500DTO> _gridOutboxTransRef;
        private R_Grid<GST00500ApprovalStatusDTO> _gridOutboxTransStatusRef;
        private R_ConductorGrid _conductorOutboxTrans;
        private R_ConductorGrid _conductorOutboxTransStatus;
        private GST00500DTO ParamTransactionStatus = new();

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
                ParamTransactionStatus = _viewModelGST00500Outbox.OutboxTransactionList.FirstOrDefault();
                await _gridOutboxTransStatusRef.R_RefreshGrid(null);
                eventArgs.ListEntityResult = _viewModelGST00500Outbox.OutboxTransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Grid_DisplayOutbox(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                ParamTransactionStatus = (GST00500DTO)eventArgs.Data;

                await _gridOutboxTransStatusRef.R_RefreshGrid(null);
            }
        }
        private async Task ServiceGetListOutboxTransactionStatus(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGST00500Outbox.GetApprovalStatus(ParamTransactionStatus);
                eventArgs.ListEntityResult = _viewModelGST00500Outbox.OutboxApprovalStatusTransactionList;
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
