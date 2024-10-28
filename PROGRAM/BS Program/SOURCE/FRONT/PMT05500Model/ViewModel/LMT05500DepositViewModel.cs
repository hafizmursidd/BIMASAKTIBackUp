﻿using PMT05500COMMON.DTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using System.Globalization;
using R_BlazorFrontEnd.Helpers;
using PMT05500FrontResources;

namespace PMT05500Model.ViewModel
{
    public class LMT05500DepositViewModel : R_ViewModel<LMT05500DepositInfoFrontDTO>
    {
        private LMT05500DepositModel _model = new LMT05500DepositModel();

        public LMT05500DepositHeaderDTO _headerDeposit = new LMT05500DepositHeaderDTO();
        public ObservableCollection<LMT05500DepositListDTO> _depositList =
            new ObservableCollection<LMT05500DepositListDTO>();
        public ObservableCollection<LMT05500DepositDetailListDTO> _depositDetailList =
            new ObservableCollection<LMT05500DepositDetailListDTO>();

        public LMT05500AgreementDTO _currentDataAgreement = new LMT05500AgreementDTO();
        public LMT05500DepositListDTO _currentDeposit = null;
        public LMT05500DepositDetailListDTO _currentDepositDetail = null;

        public LMT05500DepositInfoFrontDTO _depositInfoData = new LMT05500DepositInfoFrontDTO();
        public LMT05500DepositInfoFrontDTO TemporaryData = new LMT05500DepositInfoFrontDTO();
        public bool _IsDataNull = true;
        public async Task GetHeaderDeposit(LMT05500AgreementDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loParam = new LMT05500DBParameter()
                {
                    CCOMPANY_ID = poParameter.CCOMPANY_ID,
                    CUSER_ID = poParameter.CUSER_ID,
                    CPROPERTY_ID = poParameter.CPROPERTY_ID,
                    CDEPT_CODE = poParameter.CDEPT_CODE,
                    CTRANS_CODE = poParameter.CTRANS_CODE,
                    CREF_NO = poParameter.CREF_NO
                };

                _headerDeposit = await _model.GetDepositHeaderAsyncModel(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetAllDepositList()
        {
            R_Exception loException = new R_Exception();
            try
            {

                #region inject

                //var prop = "jbmpc";
                //var dept = "acc";
                //var trans = "802200";
                //var cref = "REF_28022024";
                //R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, prop);
                //R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, dept);
                //R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, trans);
                //R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, cref);
                //var loResult = await _model.DepositListStreamAsyncModel();
                //_depositList = new ObservableCollection<LMT05500DepositListDTO>(loResult.Data);

                #endregion

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _currentDataAgreement.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _currentDataAgreement.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, _currentDataAgreement.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, _currentDataAgreement.CREF_NO);
                var loResult = await _model.DepositListStreamAsyncModel();
                _depositList = new ObservableCollection<LMT05500DepositListDTO>(loResult.Data);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetAllDepositDetailList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (_currentDeposit != null)
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _currentDeposit.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _currentDeposit.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, _currentDeposit.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, _currentDeposit.CREF_NO);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CSEQ_NO, _currentDeposit.CSEQ_NO);
                }
                var loResult = await _model.DepositDetailListAsyncModel();
                _depositDetailList = new ObservableCollection<LMT05500DepositDetailListDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region CRUD

        public async Task GetEntity(LMT05500DepositInfoFrontDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity: ConvertToEntityBack(poEntity));
                _depositInfoData = ConvertToEntityFront(loResult);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT05500DepositInfoFrontDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var temp = ConvertToEntityBack(poEntity);
                // Validation Before Delete
                await _model.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT05500DepositInfoFrontDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CCOMPANY_ID = _currentDeposit.CCOMPANY_ID;
                poNewEntity.CUSER_ID = _currentDeposit.CUSER_ID;
                poNewEntity.CPROPERTY_ID = _currentDeposit.CPROPERTY_ID;
                poNewEntity.CDEPT_CODE = _currentDeposit.CDEPT_CODE;
                poNewEntity.CREF_NO = _currentDeposit.CREF_NO;
                poNewEntity.CTRANS_CODE = _currentDeposit.CTRANS_CODE;

                var loResult = await _model.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);
                _depositInfoData = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validation
        public void ValidationFieldEmpty(LMT05500DepositInfoFrontDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (poEntity.LCONTRACTOR && string.IsNullOrWhiteSpace(poEntity.CCONTRACTOR_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5501");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(poEntity.CDEPOSIT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5502");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(poEntity.DDEPOSIT_DATE.ToString()))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5503");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
                if (poEntity.NDEPOSIT_AMT < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT05500_Class), "5504");
                    loEx.Add(loErr);
                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region ConvertDTO
        public LMT05500DepositInfoDTO ConvertToEntityBack(LMT05500DepositInfoFrontDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMT05500DepositInfoDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoDTO>(poEntity);
                loReturn.CDEPOSIT_DATE = ConvertDateTimeToStringFormat(poEntity.DDEPOSIT_DATE);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }
        public LMT05500DepositInfoFrontDTO ConvertToEntityFront(LMT05500DepositInfoDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMT05500DepositInfoFrontDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<LMT05500DepositInfoFrontDTO>(poEntity);
                loReturn.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(poEntity.CDEPOSIT_DATE);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }
        private DateTime ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
            }
            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
            return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
        }
        private string ConvertDateTimeToStringFormat(DateTime ptEntity)
        {
            if (ptEntity == DateTime.MinValue)
            {
                // Jika DateTime adalah DateTime.MinValue, kembalikan string kosong
                return ""; // atau null, tergantung pada kebutuhan Anda
            }

            // Format DateTime ke string "yyyyMMdd"
            return ptEntity.ToString("yyyyMMdd");
        }

        #endregion
    }
}