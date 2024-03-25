using LMT05500Common.DTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using System.Globalization;

namespace LMT05500Model.ViewModel
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

        public async Task GetHeaderDeposit()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _currentDataAgreement.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _currentDataAgreement.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, _currentDataAgreement.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, _currentDataAgreement.CREF_NO);
                _headerDeposit = await _model.GetDepositHeaderAsyncModel();
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
                poNewEntity.CPROPERTY_ID = _currentDeposit.CPROPERTY_ID;
                poNewEntity.CDEPT_CODE = _currentDeposit.CDEPT_CODE;
                poNewEntity.CREF_NO = _currentDeposit.CREF_NO;
                poNewEntity.CTRANS_CODE = _currentDeposit.CTRANS_CODE;
                poNewEntity.CDEPOSIT_ID = _currentDeposit.CDEPOSIT_ID;

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


        #region ConvertDTO
        public LMT05500DepositInfoDTO ConvertToEntityBack(LMT05500DepositInfoFrontDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMT05500DepositInfoDTO? loReturn = null;
            try
            {
                loReturn = new LMT05500DepositInfoDTO()
                {
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CTRANS_CODE = poEntity.CTRANS_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CSEQ_NO = poEntity.CSEQ_NO,
                    CINVOICE_NO = poEntity.CINVOICE_NO,
                    LCONTRACTOR = poEntity.LCONTRACTOR,
                    CUNIT_TYPE = poEntity.CUNIT_TYPE,
                    CCONTRACTOR_ID = poEntity.CCONTRACTOR_ID,
                    CCONTRACTOR_NAME = poEntity.CCONTRACTOR_NAME,
                    CDEPOSIT_ID = poEntity.CDEPOSIT_ID,
                    CDEPOSIT_NAME = poEntity.CDEPOSIT_NAME,
                    CDEPOSIT_DATE = ConvertDateTimeToStringFormat(poEntity.DDEPOSIT_DATE),
                    CLOCAL_CURRENCY = poEntity.CLOCAL_CURRENCY,
                    CDEPOSIT_AMT = poEntity.CDEPOSIT_AMT,
                    NBASE_RATE_AMOUNT = poEntity.NBASE_RATE_AMOUNT,
                    NCURRENCY_RATE_AMOUNT = poEntity.NCURRENCY_RATE_AMOUNT,
                    NREMAINING_AMOUNT = poEntity.NREMAINING_AMOUNT,
                    LPAYMENT = poEntity.LPAYMENT,
                    CDESCRIPTION = poEntity.CDESCRIPTION
                };

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
                loReturn = new LMT05500DepositInfoFrontDTO()
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CTRANS_CODE = poEntity.CTRANS_CODE,
                    CREF_NO = poEntity.CREF_NO,
                    CSEQ_NO = poEntity.CSEQ_NO,
                    CINVOICE_NO = poEntity.CINVOICE_NO,
                    LCONTRACTOR = poEntity.LCONTRACTOR,
                    CUNIT_TYPE = poEntity.CUNIT_TYPE,
                    CCONTRACTOR_ID = poEntity.CCONTRACTOR_ID,
                    CCONTRACTOR_NAME = poEntity.CCONTRACTOR_NAME,
                    CDEPOSIT_ID = poEntity.CDEPOSIT_ID,
                    CDEPOSIT_NAME = poEntity.CDEPOSIT_NAME,
                    DDEPOSIT_DATE = ConvertStringToDateTimeFormat(poEntity.CDEPOSIT_DATE),
                    CLOCAL_CURRENCY = poEntity.CLOCAL_CURRENCY,
                    CDEPOSIT_AMT = poEntity.CDEPOSIT_AMT,
                    NBASE_RATE_AMOUNT = poEntity.NBASE_RATE_AMOUNT,
                    NCURRENCY_RATE_AMOUNT = poEntity.NCURRENCY_RATE_AMOUNT,
                    NREMAINING_AMOUNT = poEntity.NREMAINING_AMOUNT,
                    LPAYMENT = poEntity.LPAYMENT,
                    CDESCRIPTION = poEntity.CDESCRIPTION
                };

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