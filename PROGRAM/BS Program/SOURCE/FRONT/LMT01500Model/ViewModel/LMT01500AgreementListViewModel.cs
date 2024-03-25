using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT01500Model.ViewModel
{
    public class LMT01500AgreementListViewModel : R_ViewModel<LMT01500AgreementListOriginalDTO>
    {
        #region From Back
        private readonly LMT01500AgreementListModel _modelLMT01500AgreementListModel = new LMT01500AgreementListModel();
        public ObservableCollection<LMT01500AgreementListOriginalDTO> loListLMT01500Agreement = new ObservableCollection<LMT01500AgreementListOriginalDTO>();
        public LMT01500ChangeStatusDTO? loEntityChangeStatus = new LMT01500ChangeStatusDTO();
        public List<LMT01500PropertyListDTO> loPropertyList { get; set; } = new List<LMT01500PropertyListDTO>();
        public List<LMT01500VarGsmTransactionCodeDTO> loGsmTransactionCode { get; set; } = new List<LMT01500VarGsmTransactionCodeDTO>();
        public List<LMT01500ComboBoxDTO> loComboBoxDataCTransStatus { get; set; } = new List<LMT01500ComboBoxDTO>();
        public LMT01500ProcessResultDTO? loEntityResultChangeStatus = new LMT01500ProcessResultDTO();

        #endregion

        #region For Front
        public string _cPropertyId = "";
        public bool _lComboBoxProperty = true;
        #endregion

        #region AgreementList

        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500AgreementListModel.GetPropertyListAsync();
                loPropertyList = new List<LMT01500PropertyListDTO>(loResult);
                _cPropertyId = loResult.First().CPROPERTY_ID!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(_cPropertyId))
                {
                    var loResult = await _modelLMT01500AgreementListModel.GetAgreementListAsync(pcCPROPERTY_ID: _cPropertyId);
                    loListLMT01500Agreement = new ObservableCollection<LMT01500AgreementListOriginalDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetVarGsmTransactionCode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500AgreementListModel.GetVarGsmTransactionCodeAsync();
                loGsmTransactionCode = new List<LMT01500VarGsmTransactionCodeDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region ChangeStatus

        public async Task GetComboBoxDataCTransStatus()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _modelLMT01500AgreementListModel.GetComboBoxDataCTransStatusAsync();
                loComboBoxDataCTransStatus = new List<LMT01500ComboBoxDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChangeStatus(LMT01500GetHeaderParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500AgreementListModel.GetChangeStatusAsync(poParameter);
                    loEntityChangeStatus = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessChangeStatus(LMT01500ChangeStatusParameterDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(poEntity.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500AgreementListModel.ProcessChangeStatusAsync(poEntity);
                    loEntityResultChangeStatus = loResult;
                }
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