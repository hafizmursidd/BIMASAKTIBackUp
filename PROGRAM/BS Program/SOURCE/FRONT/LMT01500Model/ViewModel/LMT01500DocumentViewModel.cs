using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using LMT01500Common.Utilities;
using LMT01500Common.DTO._7._Document;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500DocumentViewModel : R_ViewModel<LMT01500DocumentDetailDTO>
    {
        #region From Back
        private readonly LMT01500DocumentModel _modelLMT01500DocumentModel = new LMT01500DocumentModel();
        public ObservableCollection<LMT01500DocumentListDTO> loListLMT01500Document = new ObservableCollection<LMT01500DocumentListDTO>();
        public LMT01500DocumentDetailDTO? loEntityDocument = new LMT01500DocumentDetailDTO();
        public LMT01500DocumentHeaderDTO? loEntityDocumentHeader = new LMT01500DocumentHeaderDTO();
        public List<LMT01500PropertyListDTO> loPropertyList { get; set; } = new List<LMT01500PropertyListDTO>();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        #endregion
        #region For Front

        #endregion

        #region Document

        public async Task GetDocumentHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500DocumentModel.GetDocumentHeaderAsync(poParameter: loParameterList);
                    loEntityDocumentHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDocumentList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500DocumentModel.GetDocumentListAsync(poParameter: loParameterList);
                    loListLMT01500Document = new ObservableCollection<LMT01500DocumentListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMT01500DocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelLMT01500DocumentModel.R_ServiceGetRecordAsync(poEntity);

                loEntityDocument = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT01500DocumentDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500DocumentModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityDocument = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT01500DocumentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500DocumentModel.R_ServiceDeleteAsync(poEntity);
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