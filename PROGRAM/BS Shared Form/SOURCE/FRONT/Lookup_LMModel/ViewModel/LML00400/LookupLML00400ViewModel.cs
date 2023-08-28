using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMModel.ViewModel.LML00400
{
    public class LookupLML00400ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        public ObservableCollection< LML00400DTO> UtilityChargesList = new ObservableCollection<LML00400DTO>();
        
        public async Task GetUtitlityChargesList(LML00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.LML00400GetUtilityChargesListAsync(poParam);
                UtilityChargesList = new ObservableCollection<LML00400DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}