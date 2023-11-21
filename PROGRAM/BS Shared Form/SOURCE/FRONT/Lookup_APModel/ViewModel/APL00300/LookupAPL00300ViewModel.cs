using Lookup_APCOMMON.DTOs.APL00300;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_APModel.ViewModel.APL00300
{
    public class LookupAPL00300ViewModel : R_ViewModel<APL00300DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        public ObservableCollection<APL00300DTO> ProductLookupGrid = new ObservableCollection<APL00300DTO>();
        public APL00300DTO ProductLookupEntity = new APL00300DTO();
        public APL00300ParameterDTO ParameterLookup { get; set; }
        public string Category { get; set; } = "";
        public List<APL00300DTO> RadioButton { get; set; } = new List<APL00300DTO>()
        {
            new APL00300DTO() { Code = "A", Desc = "All Categories"},
            new APL00300DTO() {  Code = "S", Desc = "Selected Categories" }
        };
        public async Task GetProductLookup()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00300ProductLookUpAsync(ParameterLookup);

                ProductLookupGrid = new ObservableCollection<APL00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
