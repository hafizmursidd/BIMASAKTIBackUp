
using GSM04500Common;
using GSM04500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM04500Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Grid.Columns;

namespace GSM04500Front
{
    public partial class GSM04500AccountSetting
    {
        private GSM04501ViewModel JournalGOAViewModel = new();
        private R_Grid<GSM04510GOADTO> _gridRef;
        private R_ConductorGrid _conJournalGOARef;

        private GSM04502ViewModel GOADeptViewModel = new();
        private R_ConductorGrid _conGOADeptRef;
        private R_Grid<GSM04510GOADeptDTO> _gridGOADeptRef;
        //private R_Conductor _conGOADeptRef2;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid((GSM04500DTO)poParameter);
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
            try
            {
                JournalGOAViewModel.CurrentJournalGroup = (GSM04500DTO)eventArgs.Parameter;

                var loParam = JournalGOAViewModel.CurrentJournalGroup;
                await JournalGOAViewModel.GetAllJournalGrupGOAAsync(loParam.CPROPERTY_ID, loParam.CJRNGRP_TYPE, loParam.CJRNGRP_CODE);
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

        private async Task ServiceSaveGOA(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADTO)eventArgs.Data;

                await JournalGOAViewModel.SaveGOA(loParam, eventArgs.ConductorMode);
                eventArgs.Result = JournalGOAViewModel.GOA;

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
                JournalGOAViewModel.CurrentGOA = loParam;

                //Checking By Dept to enable disable add, delete edit
                JournalGOAViewModel.checking_ByDept = loParam.LDEPARTMENT_MODE ? true : false;

                await _gridGOADeptRef.R_RefreshGrid(loParam);
            }
        }

        #region GOALOOKUP
        //  Button LookUp GOA
        private void BeforeOpenLookUPGOA(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = new GSL00520ParameterDTO();
            loParam.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00520);

        }

        private void AfterOpenLookGOA(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            if (eventArgs.ColumnName == "GLAccount_No")
            {
                var loTempResult2 = R_FrontUtility.ConvertObjectToObject<GSL00520DTO>(eventArgs.Result);
                ((GSM04510GOADTO)eventArgs.ColumnData).CGLACCOUNT_NO = loTempResult2.CGLACCOUNT_NO;
                ((GSM04510GOADTO)eventArgs.ColumnData).CGLACCOUNT_NAME = loTempResult2.CGLACCOUNT_NAME;
            }
        }
        #endregion

        #region GroupOfAccountDept

        private async Task GridGOADept_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510GOADTO)eventArgs.Parameter;
                // var liParam = GOADeptViewModel.CurrentGOADEPT;
                await GOADeptViewModel.GetGOAAllByDept(loParam);
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
                loParam.CPROPERTY_ID = JournalGOAViewModel.CurrentGOA.CPROPERTY_ID;
                loParam.CJRNGRP_TYPE = JournalGOAViewModel.CurrentGOA.CJRNGRP_TYPE;
                loParam.CJRNGRP_CODE = JournalGOAViewModel.CurrentGOA.CJRNGRP_CODE;
                loParam.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE;

                await GOADeptViewModel.SaveGOADept(loParam, eventArgs.ConductorMode);
                eventArgs.Result = GOADeptViewModel.GOADept;
                // await _gridGOADeptRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceAfterAdd(R_AfterAddEventArgs eventArgs)
        {
            //eventArgs.Data = new GSM04510GOADeptDTO()
            //{
            //    CJRNGRP_CODE =
            //};
        }


        #region LookUpGOADEPT
        //  Button LookUp DeptCode
        private void BeforeOpenLookUpDeptCode(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            switch (eventArgs.ColumnName)
            {
                case "CDEPT_CODE":
                    eventArgs.Parameter = new GSL00700ParameterDTO();
                    eventArgs.TargetPageType = typeof(GSL00700);
                    break;
                case "GLAccount_No":
                    var loParam = new GSL00520ParameterDTO();
                    loParam.CGOA_CODE = JournalGOAViewModel.CurrentGOA.CGOA_CODE;
                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(GSL00520);
                    break;

            }
        }

        private void AfterOpenLookUpDeptCode(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            switch (eventArgs.ColumnName)
            {
                case "CDEPT_CODE":
                    var loTempResult = R_FrontUtility.ConvertObjectToObject<GSL00700DTO>(eventArgs.Result);
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CDEPT_CODE = loTempResult.CDEPT_CODE;
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CDEPT_NAME = loTempResult.CDEPT_NAME;
                    break;
                case "GLAccount_No":
                    var loTempResult2 = R_FrontUtility.ConvertObjectToObject<GSL00520DTO>(eventArgs.Result);
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CGLACCOUNT_NO = loTempResult2.CGLACCOUNT_NO;
                    ((GSM04510GOADeptDTO)eventArgs.ColumnData).CGLACCOUNT_NAME = loTempResult2.CGLACCOUNT_NAME;
                    break;
            }
        }
        #endregion

        #endregion
    }
}

