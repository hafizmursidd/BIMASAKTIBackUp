using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00400;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
namespace Lookup_APModel.ViewModel.APL00400
{
    public class LookupAPL00400ViewModel : R_ViewModel<APL00400DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        public ObservableCollection<APL00400DTO> ProductAllocationGrid = new ObservableCollection<APL00400DTO>();
        public APL00400ParameterDTO ParameterLookup { get; set; }

        public async Task GetSuplierInfoList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00400ProductAllocationLookUpAsync(ParameterLookup);

                ProductAllocationGrid = new ObservableCollection<APL00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}