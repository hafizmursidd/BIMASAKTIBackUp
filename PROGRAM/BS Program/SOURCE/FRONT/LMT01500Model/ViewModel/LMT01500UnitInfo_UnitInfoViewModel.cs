using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.DTO._3._Unit_Info;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500UnitInfo_UnitInfoViewModel : R_ViewModel<LMT01500UnitInfoUnitInfoDetailDTO>
    {
        #region From Back
        private readonly LMT01500UnitInfo_UnitInfoModel _modelLMT01500UnitInfo_UnitInfoModel = new LMT01500UnitInfo_UnitInfoModel();
        public ObservableCollection<LMT01500UnitInfoUnitInfoListDTO> loListLMT01500UnitInfo_UnitInfo = new ObservableCollection<LMT01500UnitInfoUnitInfoListDTO>();
        public LMT01500UnitInfoUnitInfoDetailDTO? loEntityUnitInfo_UnitInfo = new LMT01500UnitInfoUnitInfoDetailDTO();
        public LMT01500UnitInfoHeaderDTO? loEntityUnitInfoHeader = new LMT01500UnitInfoHeaderDTO();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        #endregion
        #region For Front
        public string _cPropertyId = "";
        #endregion

        #region UnitInfo_UnitInfo
        
        public async Task GetUnitInfoHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500UnitInfo_UnitInfoModel.GetUnitInfoHeaderAsync(poParameter: loParameterList);
                    loEntityUnitInfoHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUnitInfoList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500UnitInfo_UnitInfoModel.GetUnitInfoListAsync(poParameter: loParameterList);
                    loListLMT01500UnitInfo_UnitInfo = new ObservableCollection<LMT01500UnitInfoUnitInfoListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMT01500UnitInfoUnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelLMT01500UnitInfo_UnitInfoModel.R_ServiceGetRecordAsync(poEntity);

                loEntityUnitInfo_UnitInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT01500UnitInfoUnitInfoDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500UnitInfo_UnitInfoModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityUnitInfo_UnitInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT01500UnitInfoUnitInfoDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500UnitInfo_UnitInfoModel.R_ServiceDeleteAsync(poEntity);
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