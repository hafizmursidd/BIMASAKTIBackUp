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
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;

namespace GST00500Front
{
    public partial class GST00500Draft
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

            loEx.ThrowExceptionIfErrors();
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
    }
}
