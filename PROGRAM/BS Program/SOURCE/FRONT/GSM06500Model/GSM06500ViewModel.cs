using GSM06500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM06500Model
{
    public class GSM06500ViewModel : R_ViewModel<GSM06500DTO>
    {
        private GSM06500Model _model = new GSM06500Model();
        public ObservableCollection<GSM06500DTO> PaymentOfTermList = new ObservableCollection<GSM06500DTO>();
        public GSM06500DTO PaymentOfTerm { get; set; } = new GSM06500DTO();
        public List<GSM06500PropertyDTO> PropertyList { get; set; } = new List<GSM06500PropertyDTO>();
        public string PropertyValueContext = "";

        public async Task GetAllTermOfPaymentAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);

                var loResult = await _model.GetTermOfPaymentListAsyncModel();
                PaymentOfTermList = new ObservableCollection<GSM06500DTO>(loResult.ListData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetPropertyAsyncModel();
                PropertyList = loResult.Data;
                PropertyValueContext = PropertyList[0].CPROPERTY_ID;
    }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTermOfPayment(GSM06500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM06500DTO
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CUSER_ID = poEntity.CUSER_ID,
                    CPAY_TERM_CODE = poEntity.CPAY_TERM_CODE,
                    CPAY_TERM_NAME = poEntity.CPAY_TERM_NAME,
                    IPAY_TERM_DAYS = poEntity.IPAY_TERM_DAYS
                };
                await _model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM06500DTO> GetTermOfPaymentOneRecord(GSM06500DTO poProperty)
        {
            var loEx = new R_Exception();
            GSM06500DTO loResult = new GSM06500DTO();

            try
            {
                var loParam = new GSM06500DTO
                {
                    CPROPERTY_ID = poProperty.CPROPERTY_ID,
                    CPAY_TERM_CODE = poProperty.CPAY_TERM_CODE
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM06500DTO> SaveTermOfPayment(GSM06500DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM06500DTO loResult = null;

            try
            {
                poNewEntity.CPROPERTY_ID = PropertyValueContext;
                loResult = await _model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                PaymentOfTerm = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

    }
}
