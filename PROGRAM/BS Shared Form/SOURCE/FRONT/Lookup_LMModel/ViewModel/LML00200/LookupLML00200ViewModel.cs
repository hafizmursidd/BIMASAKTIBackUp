using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMModel.ViewModel.LML00200
{
    public class LookupLML00200ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();

        public ObservableCollection<LML00200DTO> UnitChargesList = new ObservableCollection<LML00200DTO>();

        public async Task GetUnitChargesList(LML00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.LML00200GetUnitChargesListAsync(poParam);
                UnitChargesList = new ObservableCollection<LML00200DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
