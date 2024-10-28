using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMT05500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT05500Model.ViewModel
{
    public class LMT05500AgreementViewModel : R_ViewModel<LMT05500DepositInfoFrontDTO>
    {
        private LMT05500AgreementModel _model = new LMT05500AgreementModel();
        public List<LMT05500PropertyDTO> PropertyList { get; set; } = new List<LMT05500PropertyDTO>();
        public ObservableCollection<LMT05500AgreementDTO> AgreementList =
            new ObservableCollection<LMT05500AgreementDTO>();
        public ObservableCollection<LMT05500UnitDTO> DepositUnitList =
            new ObservableCollection<LMT05500UnitDTO>();

        public string PropertyValueContext = "";
        public string? UnitDescValue { get; set; }
        public LMT05500AgreementDTO _currentAgreement = null;

        public bool _llAgreementTab = true;

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetPropertyListStreamAsyncModel();
                PropertyList = loResult.Data;
                if (PropertyList.Count > 0)
                {
                    PropertyValueContext = PropertyList[0].CPROPERTY_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAllAgreementList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                var loResult = await _model.GetAgreementListStreamAsyncModel();
                AgreementList = new ObservableCollection<LMT05500AgreementDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetAllDepositUnitList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var temp = _currentAgreement;

                if (_currentAgreement != null)
                {
                    UnitDescValue = temp.CUNIT_DESCRIPTION;
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _currentAgreement.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _currentAgreement.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, _currentAgreement.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, _currentAgreement.CREF_NO);
                    var loResult = await _model.GetDepositUnitStreamAsyncModel();
                    DepositUnitList = new ObservableCollection<LMT05500UnitDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
