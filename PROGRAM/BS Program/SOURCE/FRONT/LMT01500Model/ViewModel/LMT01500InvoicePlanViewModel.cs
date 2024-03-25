using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.DTO._5._Invoice_Plan;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMT01500Model.ViewModel
{
    public class LMT01500InvoicePlanViewModel : R_ViewModel<LMT01500BlankDTO>
    {
        #region From Back
        private readonly LMT01500InvoicePlanModel _modelLMT01500InvoicePlanModel = new LMT01500InvoicePlanModel();
        public ObservableCollection<LMT01500InvoicePlanListDTO> loListLMT01500InvoicePlanList = new ObservableCollection<LMT01500InvoicePlanListDTO>();
        public ObservableCollection<LMT01500InvoicePlanChargesListDTO> loListLMT01500InvoicePlanChargesList = new ObservableCollection<LMT01500InvoicePlanChargesListDTO>();
        public LMT01500InvoicePlanHeaderDTO? loEntityChargesInfoHeader = new LMT01500InvoicePlanHeaderDTO();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        #endregion
        
        #region For Front
        #endregion

        #region InvoicePlan

        public async Task GetInvoicePlanHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500InvoicePlanModel.GetInvoicePlanHeaderAsync(poParameter: loParameterList);
                    loEntityChargesInfoHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoicePlanChargeList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500InvoicePlanModel.GetInvoicePlanChargeListAsync(poParameter: loParameterList);
                    loListLMT01500InvoicePlanChargesList = new ObservableCollection<LMT01500InvoicePlanChargesListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoicePlanList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500InvoicePlanModel.GetInvoicePlanListAsync(poParameter: loParameterList);
                    loListLMT01500InvoicePlanList = new ObservableCollection<LMT01500InvoicePlanListDTO>(loResult);
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