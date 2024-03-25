using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMT01500Common.DTO._2._Agreement;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500AgreementViewModel : R_ViewModel<LMM01500AgreementDetailDTO>
    {
        #region From Back
        private readonly LMT01500AgreementModel _modelLMT01500AgreementModel = new LMT01500AgreementModel();
        public LMM01500AgreementDetailDTO loEntityLMM01500AgreementDetail = new LMM01500AgreementDetailDTO();
        public List<LMT01500ComboBoxDTO> loComboBoxDataCLeaseMode { get; set; } = new List<LMT01500ComboBoxDTO>();
        public List<LMT01500ComboBoxDTO> loComboBoxDataCChargesMode { get; set; } = new List<LMT01500ComboBoxDTO>();
        #endregion
        #region For Front
        public string _cPropertyId = "";
        #endregion

        #region Agreement
        public async Task GetEntity(LMM01500AgreementDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelLMT01500AgreementModel.R_ServiceGetRecordAsync(poEntity);

                loEntityLMM01500AgreementDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMM01500AgreementDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500AgreementModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityLMM01500AgreementDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMM01500AgreementDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500AgreementModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCLeaseMode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500AgreementModel.GetComboBoxDataCLeaseModeAsync();
                loComboBoxDataCLeaseMode = new List<LMT01500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetComboBoxDataCChargesMode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500AgreementModel.GetComboBoxDataCChargesModeAsync();
                loComboBoxDataCChargesMode = new List<LMT01500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion


    }
}