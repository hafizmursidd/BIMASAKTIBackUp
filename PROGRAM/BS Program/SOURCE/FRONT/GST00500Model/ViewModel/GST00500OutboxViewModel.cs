using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GST00500Model.ViewModel
{
    public class GST00500OutboxViewModel : R_ViewModel<GST00500DTO>
    {
        private GST00500OutboxModel _modelGST00500Outbox = new GST00500OutboxModel();
        public  ObservableCollection<GST00500DTO> OutboxTransactionList = 
            new ObservableCollection<GST00500DTO>();
        public  ObservableCollection<GST00500ApprovalStatusDTO> OutboxApprovalStatusTransactionList = 
            new ObservableCollection<GST00500ApprovalStatusDTO>();

        public async Task GetAllOutboxTransaction()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500Outbox.GetOutboxListAsyncModel();
                OutboxTransactionList = new ObservableCollection<GST00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetApprovalStatus(GST00500DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500Outbox.GetApprovalStatusAsyncModel(poEntity);
                OutboxApprovalStatusTransactionList = new ObservableCollection<GST00500ApprovalStatusDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        
    }
}
