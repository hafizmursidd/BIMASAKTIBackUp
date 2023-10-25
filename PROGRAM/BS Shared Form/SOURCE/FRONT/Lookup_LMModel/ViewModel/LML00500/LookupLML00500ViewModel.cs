using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_LMModel.ViewModel.LML00500
{
    public class LookupLML00500ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();

        public ObservableCollection<LML00500DTO> SalesmanList = new ObservableCollection<LML00500DTO>();
        public async Task GetSalesmanList(LML00500ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOMPANY_ID, poParam.CCOMPANY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUSER_ID, poParam.CUSER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);

                var loResult = await _model.LML00500GetSalesmanListAsync();
                SalesmanList = new ObservableCollection<LML00500DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
