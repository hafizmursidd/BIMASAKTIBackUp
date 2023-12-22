using LMM01500COMMON;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500Model.ViewModel
{
    public class LMM01500ChargesViewModel : R_ViewModel<LMM01500ChargesDTO>
    {
        private LMM01500ChargesModel _modelCharges = new LMM01500ChargesModel();
        public ObservableCollection<LMM01500ChargesDTO> ChargesList =
            new ObservableCollection<LMM01500ChargesDTO>();
        public LMM01500TabParamDTO _TabParam = new LMM01500TabParamDTO();
        public async Task GetChargestList()
        {
        R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _TabParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, _TabParam.CINVGRP_CODE);

                var loResult = await _modelCharges.GetAllOtherChargerListAsyncModel();
                ChargesList = new ObservableCollection<LMM01500ChargesDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
    }
}
