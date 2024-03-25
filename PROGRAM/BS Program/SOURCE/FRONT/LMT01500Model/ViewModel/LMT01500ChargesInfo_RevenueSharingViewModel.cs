using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Utilities;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMT01500Model.ViewModel
{
    public class LMT01500ChargesInfo_RevenueSharingViewModel : R_ViewModel<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>
    {
        #region From Back
        private readonly LMT01500ChargesInfo_RevenueSharingModel _modelLMT01500ChargesInfo_RevenueSharingModel = new LMT01500ChargesInfo_RevenueSharingModel();
        public ObservableCollection<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> loListLLMT01500ChargesInfo_RevenueSharing = new ObservableCollection<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>();
        public ObservableCollection<LMT01500ChargesInfo_RevenueMinimumRentDTO> loListLLMT01500ChargesInfo_RevenueMinimumRent = new ObservableCollection<LMT01500ChargesInfo_RevenueMinimumRentDTO>();
        public LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO? loEntityChargesInfo_RevenueSharing = new LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO();
        public LMT01500GetHeaderParameterDTO loParameterList = new LMT01500GetHeaderParameterDTO();
        #endregion

        #region For Front

        #endregion

        #region ChargesInfo_RevenueSharing
        public async Task GetRevenueSharingSchemeList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500ChargesInfo_RevenueSharingModel.GetRevenueSharingSchemeListAsync(loParameterList);
                    loListLLMT01500ChargesInfo_RevenueSharing = new ObservableCollection<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {


                var loResult = await _modelLMT01500ChargesInfo_RevenueSharingModel.R_ServiceGetRecordAsync(poEntity);

                loEntityChargesInfo_RevenueSharing = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {

                }

                var loResult = await _modelLMT01500ChargesInfo_RevenueSharingModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntityChargesInfo_RevenueSharing = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _modelLMT01500ChargesInfo_RevenueSharingModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetRevenueMinimumRentList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelLMT01500ChargesInfo_RevenueSharingModel.GetRevenueMinimumRentListAsync(loParameterList);
                    loListLLMT01500ChargesInfo_RevenueMinimumRent = new ObservableCollection<LMT01500ChargesInfo_RevenueMinimumRentDTO>(loResult);
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