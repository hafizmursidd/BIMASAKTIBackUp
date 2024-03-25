using LMM03700Common;
using LMM03700Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03700Model.ViewModel
{
    public class LMM03700ViewModel : R_ViewModel<TenantClassificationDTO>
    {
        private LMM03700Model _modelLMM03700 = new LMM03700Model();
        private LMM03710Model _modelLMM03710 = new LMM03710Model();
       
        public ObservableCollection<TenantClassificationGroupDTO> _TenantClassificationGroupList { get; set; } = new ObservableCollection<TenantClassificationGroupDTO>();
        public TenantClassificationGroupDTO _TenantClassificationGroupRecord { get; set; } = new TenantClassificationGroupDTO();
        public List<PropertyDTO> _PropertyList { get; set; } = new List<PropertyDTO>();
        public string _propertyId { get; set; } = "";
      
        public async Task GetTenantClassGroupList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, _propertyId);
                var loResult = await _modelLMM03700.GetTenantClassGroupListAsync();
                _TenantClassificationGroupList = new ObservableCollection<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
