using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APModel.ViewModel.APL00110
{
    public class LookupAPL00110ViewModel : R_ViewModel<APL00110DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        public ObservableCollection<APL00110DTO> SupplierInfoGrid = new ObservableCollection<APL00110DTO>();
        public APL00110ParameterDTO ParameterLookup { get; set; }

        public async Task GetSuplierInfoList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00110SupplierInfoLookUpAsync(ParameterLookup);

                SupplierInfoGrid = new ObservableCollection<APL00110DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
