using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using LMM01500Model.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using System.Diagnostics.Tracing;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_CommonFrontBackAPI;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls.Enums;

namespace LMM01500Front
{
    public partial class LMM01500InvoiceGroupDept
    {
        private LMM01500InvoiceGrpDeptViewModel _LMM01500InvoiceGrpDeptViewModel = new();
        private R_Conductor _conductorInvoiceGroupDeptRef;
        private R_Grid<LMM01500InvoiceGrpDeptDTO> _gridInvoiceGroupRef;
        [Inject] IClientHelper clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _LMM01500InvoiceGrpDeptViewModel._TabParam = (LMM01500TabParamDTO)poParameter;
               await _gridInvoiceGroupRef.R_RefreshGrid((LMM01500TabParamDTO)poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetInvoiceGroupDeptList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if ((LMM01500TabParamDTO)eventArgs.Parameter != null)
                   
                {

                    await _LMM01500InvoiceGrpDeptViewModel.GetInvoiceGroupDeptList();
                    eventArgs.ListEntityResult = _LMM01500InvoiceGrpDeptViewModel.InvoiceGroupDeptList;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecordInvoiceGroup(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01500InvoiceGrpDeptDetailDTO>(eventArgs.Data);
                await _LMM01500InvoiceGrpDeptViewModel.GetInvoiceGroupDetail(loParam);
                eventArgs.Result = _LMM01500InvoiceGrpDeptViewModel.InvoiceGroupDeptDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region SAVE
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMM01500InvoiceGrpDeptDetailDTO)eventArgs.Data;
                await _LMM01500InvoiceGrpDeptViewModel.Save_InvoiceGroupDept(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _LMM01500InvoiceGrpDeptViewModel.InvoiceGroupDeptDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region R_Storage
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Doc };
        private async Task InputFileChangeLMM01500GeneralInfo(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01500InvoiceGrpDeptDetailDTO)_conductorInvoiceGroupDeptRef.R_GetCurrentData();

                // Set Data
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loData.OData = loMS.ToArray();
                loData.CFileNameExtension = eventArgs.File.Name;
                loData.CFileExtension = Path.GetExtension(eventArgs.File.Name);
                loData.CFileName = Path.GetFileNameWithoutExtension(eventArgs.File.Name);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region DELETE
        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM01500InvoiceGrpDeptDetailDTO)eventArgs.Data;
                await _LMM01500InvoiceGrpDeptViewModel.DeleteInvoiceGroupDept(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #endregion

        #region LookupDept

        private R_Lookup R_Lookup_DEPT;
        private R_Lookup R_Lookup_BANK;
        private R_Lookup R_Lookup_BANKACC;
        private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new GSL00700ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _LMM01500InvoiceGrpDeptViewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _LMM01500InvoiceGrpDeptViewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;

            //GeneralButtonEnable = !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CBANK_CODE);
        }

        #endregion

        #region LookupBANK

        private void BANK_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {

            var param = new GSL01200ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CBANK_TYPE = "",
                CCB_TYPE = "B",
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01200);
        }
        private void BANK_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _LMM01500InvoiceGrpDeptViewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _LMM01500InvoiceGrpDeptViewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
            //GeneralButtonEnable = !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CBANK_CODE);
        }
        #endregion

        #region LookupBANK_ACCOUNT

        private void BANKACCOUNT_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01300ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CBANK_TYPE = "B",
                CCB_CODE = _LMM01500InvoiceGrpDeptViewModel._selectedBank,
                CDEPT_CODE = _LMM01500InvoiceGrpDeptViewModel._selectedDeptCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }
        private void BANKACCOUNT_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _LMM01500InvoiceGrpDeptViewModel.Data.CBANK_CODE = loTempResult.CCB_ACCOUNT_NO;
            _LMM01500InvoiceGrpDeptViewModel.Data.CBANK_NAME = loTempResult.CCB_ACCOUNT_NAME;
            //GeneralButtonEnable = !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_LMM01500ViewModel.Data.CBANK_CODE);
        }
        #endregion


    }
}
