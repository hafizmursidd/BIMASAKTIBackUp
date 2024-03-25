using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.DTO._6._Deposit;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500DepositViewModel : R_ViewModel<LMT01500DepositDetailDTO>
    {
        #region From Back
        private readonly LMT01500DepositModel _modelLMT01500DepositModel = new LMT01500DepositModel();
        public ObservableCollection<LMT01500DepositListDTO> loListLMT01500Deposit = new ObservableCollection<LMT01500DepositListDTO>();
        public LMT01500DepositDetailDTO? loEntityDeposit = new LMT01500DepositDetailDTO();
        public LMT01500DepositHeaderDTO? loEntityDepositHeader = new LMT01500DepositHeaderDTO();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        #endregion

        #region For Front
        #endregion

        #region Deposit
        public async Task GetDepositHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500DepositModel.GetDepositHeaderAsync(poParameter: loParameterList);
                    loEntityDepositHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDepositList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500DepositModel.GetDepositListAsync(poParameter: loParameterList);
                    loListLMT01500Deposit = new ObservableCollection<LMT01500DepositListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMT01500DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelLMT01500DepositModel.R_ServiceGetRecordAsync(poEntity);

                loEntityDeposit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT01500DepositDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500DepositModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityDeposit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT01500DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500DepositModel.R_ServiceDeleteAsync(poEntity);
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