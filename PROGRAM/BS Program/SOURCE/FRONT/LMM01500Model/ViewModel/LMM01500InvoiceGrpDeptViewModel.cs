using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMM01500Model.ViewModel
{
    public class LMM01500InvoiceGrpDeptViewModel : R_ViewModel<LMM01500InvoiceGrpDeptDetailDTO>
    {
        private LMM01500InvoiceGroupDeptModel _modelInvGrpDept = new LMM01500InvoiceGroupDeptModel();
        public ObservableCollection<LMM01500InvoiceGrpDeptDTO> InvoiceGroupDeptList =
            new ObservableCollection<LMM01500InvoiceGrpDeptDTO>();
        public LMM01500InvoiceGrpDeptDetailDTO InvoiceGroupDeptDetail = new LMM01500InvoiceGrpDeptDetailDTO();
        public LMM01500TabParamDTO _TabParam = new LMM01500TabParamDTO();
        public string _selectedBank = "";
        public string _selectedDeptCode = "";
        public async Task GetInvoiceGroupDeptList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, _TabParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CINVGRP_CODE, _TabParam.CINVGRP_CODE);

                var loResult = await _modelInvGrpDept.GetInvoiceGroupDeptListModel();
                InvoiceGroupDeptList = new ObservableCollection<LMM01500InvoiceGrpDeptDTO>(loResult.ListData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task<LMM01500InvoiceGrpDeptDetailDTO> GetInvoiceGroupDetail(LMM01500InvoiceGrpDeptDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01500InvoiceGrpDeptDetailDTO loResult = new LMM01500InvoiceGrpDeptDetailDTO();
            try
            {
                var loParam = new LMM01500InvoiceGrpDeptDetailDTO()
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CUSER_ID = poEntity.CUSER_ID,
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CINVGRP_CODE = poEntity.CINVGRP_CODE,
                    CDEPT_CODE= poEntity.CDEPT_CODE
                };
                loResult = await _modelInvGrpDept.R_ServiceGetRecordAsync(loParam);
                
                InvoiceGroupDeptDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task Save_InvoiceGroupDept(LMM01500InvoiceGrpDeptDetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _modelInvGrpDept.R_ServiceSaveAsync(poEntity, peCRUDMode);
                InvoiceGroupDeptDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteInvoiceGroupDept(LMM01500InvoiceGrpDeptDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new LMM01500InvoiceGrpDeptDetailDTO()
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CPROPERTY_ID = poEntity.CPROPERTY_ID,
                    CINVGRP_CODE = poEntity.CINVGRP_CODE,
                    CDEPT_CODE = poEntity.CDEPT_CODE,
                    CINVOICE_TEMPLATE = poEntity.CINVOICE_TEMPLATE,

                    CBANK_CODE = poEntity.CBANK_CODE,
                    CBANK_ACCOUNT = poEntity.CBANK_ACCOUNT,
                    CUSER_ID = poEntity.CUSER_ID
                };

                await _modelInvGrpDept.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
