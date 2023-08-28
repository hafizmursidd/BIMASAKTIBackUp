using BlazorClientHelper;
using GSM04500Common;
using GSM04501Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;

namespace GSM04511Front
{
    public partial class GSM04511 : R_Page
    {

        private GSM04501ViewModel JournalGOAViewModel = new();
        private R_Grid<GSM04510GOADTO> _gridRef;
        private R_ConductorGrid _conJournalGOARef;

        private GSM04502ViewModel GOADeptViewModel = new();
        private R_ConductorGrid _conGOADeptRef;
        private R_Grid<GSM04510GOADeptDTO> _gridGOADeptRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            string lcJournalGRPType = null;
            string lcPropertyId = null;
            string lcJournalGRPCode = null;
            try
            {
                //lcGroupId ===> 11 untuk Tab Utility
                lcJournalGRPType = "11";
                lcPropertyId = "JBMPC";
                lcJournalGRPCode = "A";
                await JournalGOAViewModel.GetAllJournalGrupGOAAsync(lcJournalGRPType, lcPropertyId, lcJournalGRPCode);
                eventArgs.ListEntityResult = JournalGOAViewModel.GOAList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADTO)eventArgs.Data;
                eventArgs.Result = await JournalGOAViewModel.GetGOAOneRecord(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADTO)eventArgs.Data;
                loParam.CJRNGRP_TYPE = "11";
                loParam.CPROPERTY_ID = "JBMPC";
                loParam.CJRNGRP_CODE = "A";

                await JournalGOAViewModel.SaveGOA(loParam, eventArgs.ConductorMode);
                eventArgs.Result = JournalGOAViewModel.GOA;

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM04510GOADTO)eventArgs.Data;
                await _gridGOADeptRef.R_RefreshGrid(loParam);

            }
        }


        #region GroupOfAccountDept

        private async Task GridGOADept_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var liParam = ((GSM04510GOADTO)eventArgs.Parameter);
                await GOADeptViewModel.GetGOAAllByDept(liParam);
                eventArgs.ListEntityResult = GOADeptViewModel.GOADeptList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetRecordGOADeptAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADeptDTO)eventArgs.Data;
                eventArgs.Result = await GOADeptViewModel.GetGOADeptOneRecord(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveGOADept(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADeptDTO)eventArgs.Data;
                loParam.CJRNGRP_TYPE = "11";
                loParam.CPROPERTY_ID = "JBMPC";
                loParam.CJRNGRP_CODE = "A";

                await GOADeptViewModel.SaveGOADept(loParam, eventArgs.ConductorMode);
                eventArgs.Result = GOADeptViewModel.GOADept;
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //  Button LookUp DeptCode
        private R_Lookup R_LookupCdeptCodeButton;

        private void BeforeOpenLookUpCDeptCode(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {

                CCOMPANY_ID = clientHelper.CompanyId,
                CUSER_ID = clientHelper.UserId

            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void AfterOpenLookUpCDeptCode(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
                return;
            var loGetData = (GSM04510GOADeptDTO)_conGOADeptRef.R_GetCurrentData();
            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;

        }

        #endregion
    }
}
