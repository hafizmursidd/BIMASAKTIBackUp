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
    public class GST00500DraftViewModel : R_ViewModel<GST00500DTO>
    {
        private GST00500DraftModel _modelGST00500Draft = new GST00500DraftModel();

        public ObservableCollection<GST00500DTO> DraftTransactionList =
            new ObservableCollection<GST00500DTO>();

        public async Task GetAllDraftTransaction()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500Draft.GetDraftListAsyncModel();
                DraftTransactionList = new ObservableCollection<GST00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);


            }
        }
    }
}
