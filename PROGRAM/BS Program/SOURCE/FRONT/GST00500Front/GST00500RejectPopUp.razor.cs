using GST00500Common;
using GST00500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;

namespace GST00500Front
{
    public partial class GST00500RejectPopUp
    {
        private R_ConductorGrid _conductorRejectPopUp;
        private R_Grid<GST00500RejectDTO> _gridRejectPopUpRef;

        private GST00500InboxViewModel _viewModelGST00500Inbox = new();

        private bool IsRejectModalHidden = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
                _viewModelGST00500Inbox.loInboxApprovaltBatchList = (List<GST00500DTO>)poParameter;
                IsRejectModalHidden = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGST00500Inbox.GetReasonRejectTransaction();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }


        private async Task OnClickOKRejectButton()
        {
            var loEx = new R_Exception();

            try
            {
                if ((string.IsNullOrEmpty(_viewModelGST00500Inbox.ParamRejectTransactionStatus.CREASON_CODE)) ||
                (string.IsNullOrEmpty(_viewModelGST00500Inbox.ParamRejectTransactionStatus.TNOTES)))
                {
                    loEx.Add(new Exception(""));
                }
                else
                {
                    await _viewModelGST00500Inbox.GetSelectedDataToRejectApproval();
                    await Close(true, true);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancelButton()
        {
            await Close(true, false);
        }



    }
}
