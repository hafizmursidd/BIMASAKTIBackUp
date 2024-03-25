using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500ChargesInfoViewModel : R_ViewModel<LMT01500ChargesInfoDetailDTO>
    {
        #region From Back
        private readonly LMT01500ChargesInfoModel _modelLMT01500ChargesInfoModel = new LMT01500ChargesInfoModel();
        public ObservableCollection<LMT01500ChargesInfoListDTO> loListChargesInfo = new ObservableCollection<LMT01500ChargesInfoListDTO>();
        public LMT01500ChargesInfoDetailDTO? loEntityChargesInfo = new LMT01500ChargesInfoDetailDTO();
        public LMT01500ChargesInfoHeaderDTO? loEntityChargesInfoHeader = new LMT01500ChargesInfoHeaderDTO();
        public List<LMT01500ComboBoxDTO> loComboBoxDataCFEE_METHOD { get; set; } = new List<LMT01500ComboBoxDTO>();
        public List<LMT01500ComboBoxDTO> loComboBoxDataCINVOICE_PERIOD { get; set; } = new List<LMT01500ComboBoxDTO>();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        #endregion

        #region For Front
        #endregion

        #region ChargesInfo

        public async Task GetChargesInfoHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500ChargesInfoModel.GetChargesInfoHeaderAsync(poParameter: loParameterList);
                    loEntityChargesInfoHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesInfoList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500ChargesInfoModel.GetChargesInfoListAsync(poParameter: loParameterList);
                    loListChargesInfo = new ObservableCollection<LMT01500ChargesInfoListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMT01500ChargesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelLMT01500ChargesInfoModel.R_ServiceGetRecordAsync(poEntity);

                loEntityChargesInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT01500ChargesInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500ChargesInfoModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityChargesInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT01500ChargesInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500ChargesInfoModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCFEE_METHOD()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500ChargesInfoModel.GetComboBoxDataCFEE_METHODAsync();
                loComboBoxDataCFEE_METHOD = new List<LMT01500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetComboBoxDataCINVOICE_PERIOD()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500ChargesInfoModel.GetComboBoxDataCINVOICE_PERIODAsync();
                loComboBoxDataCINVOICE_PERIOD = new List<LMT01500ComboBoxDTO>(loResult);
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