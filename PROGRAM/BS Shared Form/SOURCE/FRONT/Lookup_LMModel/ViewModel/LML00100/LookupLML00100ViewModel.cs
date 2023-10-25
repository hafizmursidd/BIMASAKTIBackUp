using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMModel.ViewModel
{
    public class LookupLML00100ViewModel : R_ViewModel<LML00100DTO>
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();

        public ObservableCollection<LML00100DTO> SalesTaxList = new ObservableCollection<LML00100DTO>();

        public async Task GetSalesTaxList(LML00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOMPANY_ID, poParam.CCOMPANY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUSER_ID, poParam.CUSER_ID);

                var loResult = await _model.LML00100GetSalesTaxListAsync();

                SalesTaxList = new ObservableCollection<LML00100DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
