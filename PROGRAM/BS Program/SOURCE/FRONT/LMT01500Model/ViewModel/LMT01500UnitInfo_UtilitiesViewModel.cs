using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMT01500Common.DTO._3._Unit_Info;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500UnitInfo_UtilitiesViewModel : R_ViewModel<LMT01500UnitInfoUnit_UtilitiesDetailDTO>
    {
        #region From Back
        private readonly LMT01500UnitInfo_UtilitiesModel _modelLMT01500UnitInfo_UtilitiesModel = new LMT01500UnitInfo_UtilitiesModel();
        public ObservableCollection<LMT01500UnitInfoUnit_UtilitiesListDTO> loListLMT01500UnitInfo_Utilities = new ObservableCollection<LMT01500UnitInfoUnit_UtilitiesListDTO>();
        public LMT01500UnitInfoUnit_UtilitiesDetailDTO? loEntityUnitInfo_Utilities = new LMT01500UnitInfoUnit_UtilitiesDetailDTO();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        public List<LMT01500ComboBoxDTO> loComboBoxDataCCHARGES_TYPE { get; set; } = new List<LMT01500ComboBoxDTO>();
        public List<LMT01500ComboBoxStartInvoicePeriodDTO> loComboBoxDataCSTART_INV_PRD { get; set; } = new List<LMT01500ComboBoxStartInvoicePeriodDTO>();
        #endregion

        #region For Front
        #endregion

        #region UnitInfo_Utilities
        public async Task GetUnitInfoList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500UnitInfo_UtilitiesModel.GetUnitInfoListAsync(poParameter: loParameterList);
                    loListLMT01500UnitInfo_Utilities = new ObservableCollection<LMT01500UnitInfoUnit_UtilitiesListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMT01500UnitInfoUnit_UtilitiesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelLMT01500UnitInfo_UtilitiesModel.R_ServiceGetRecordAsync(poEntity);

                loEntityUnitInfo_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT01500UnitInfoUnit_UtilitiesDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500UnitInfo_UtilitiesModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityUnitInfo_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT01500UnitInfoUnit_UtilitiesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500UnitInfo_UtilitiesModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCCHARGES_TYPE()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500UnitInfo_UtilitiesModel.GetComboBoxDataCCHARGES_TYPEAsync();
                loComboBoxDataCCHARGES_TYPE = new List<LMT01500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCSTART_INV_PRD()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500UnitInfo_UtilitiesModel.GetComboBoxDataCSTART_INV_PRDAsync();
                loComboBoxDataCSTART_INV_PRD = new List<LMT01500ComboBoxStartInvoicePeriodDTO>(loResult);
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