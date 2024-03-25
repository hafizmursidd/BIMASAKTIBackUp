﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMModel.ViewModel.LML00200
{
    public class LookupLML00200ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML00200DTO> UnitChargesList = new ObservableCollection<LML00200DTO>();

        public async Task GetUnitChargesList(LML00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOMPANY_ID, poParam.CCOMPANY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUSER_ID, poParam.CUSER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCHARGE_TYPE_ID, poParam.CCHARGE_TYPE_ID);

                var loResult = await _model.LML00200GetUnitChargesListAsync();
                UnitChargesList = new ObservableCollection<LML00200DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML00200DTO> GetUnitCharges(LML00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML00200DTO loRtn = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOMPANY_ID, poParam.CCOMPANY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUSER_ID, poParam.CUSER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCHARGE_TYPE_ID, poParam.CCHARGE_TYPE_ID);

                var loResult = await _modelGetRecord.LML00200GetUnitChargesAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
