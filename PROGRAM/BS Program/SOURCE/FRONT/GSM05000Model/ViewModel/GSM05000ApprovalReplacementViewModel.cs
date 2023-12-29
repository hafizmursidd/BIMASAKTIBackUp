﻿using GSM05000Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GSM05000Model.ViewModel
{
    public class GSM05000ApprovalReplacementViewModel : R_ViewModel<GSM05000ApprovalReplacementDTO>
    {
        private GSM05000ApprovalReplacementModel _Model = new GSM05000ApprovalReplacementModel();
        public ObservableCollection<GSM05000ApprovalReplacementDTO> ReplacementList = new ObservableCollection<GSM05000ApprovalReplacementDTO>();

        public GSM05000ApprovalReplacementDTO ReplacementEntity = new GSM05000ApprovalReplacementDTO();

        // public string TransactionCode = "";
        // public string DeptCode = "";
        public string SelectedUserId = "";

        public async Task GetReplacementList(string pcTransCode, string pcDeptCode, string? pcSelectedUserId)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, pcTransCode);
                R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CUSER_ID, pcSelectedUserId);
                // var loReturn = await _Model.GetReplacementListAsync();
                var loReturn = await _Model.GetReplacementListStreamAsync();

                // ReplacementList = new ObservableCollection<GSM05000ApprovalReplacementDTO>(loReturn.Data);
                ReplacementList = new ObservableCollection<GSM05000ApprovalReplacementDTO>(loReturn);

                foreach (var list in ReplacementList)
                {
                    list.DVALID_TO = DateTime.ParseExact(list.CVALID_TO, "yyyyMMdd", CultureInfo.InvariantCulture);
                    list.DVALID_FROM = DateTime.ParseExact(list.CVALID_FROM, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetReplacementEntity(GSM05000ApprovalReplacementDTO poEntity, string pcTransCode,
            string pcDeptCode, string pcSelectedUserId)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANSACTION_CODE = pcTransCode;
                poEntity.CDEPT_CODE = pcDeptCode;
                poEntity.CUSER_ID = pcSelectedUserId;
                ReplacementEntity = await _Model.R_ServiceGetRecordAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveEntity(GSM05000ApprovalReplacementDTO poNewEntity, eCRUDMode peCrudMode)
        {
            var loEx = new R_Exception();
            try
            {
                ReplacementEntity = await _Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);

                ReplacementEntity.DVALID_TO = DateTime.ParseExact(ReplacementEntity.CVALID_TO, "yyyyMMdd",
                    CultureInfo.InvariantCulture);
                ReplacementEntity.DVALID_FROM = DateTime.ParseExact(ReplacementEntity.CVALID_FROM, "yyyyMMdd",
                    CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteEntity(GSM05000ApprovalReplacementDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
