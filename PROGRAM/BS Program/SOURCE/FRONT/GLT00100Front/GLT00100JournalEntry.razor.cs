using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GLT00100Common.DTOs;
using GLT00100Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLT00100Front ;

    public partial class GLT00100JournalEntry : R_Page
{
    private GLT00100ViewModel _JournalListViewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GLT00100JournalGridDTO> _gridRef;
    private R_Grid<GLT00100JournalGridDetailDTO> _gridDetailRef;
    private R_ConductorGrid _conductorGridDetailRef;
    public bool _enableCrudJournalDetail = false;

    [Inject] IClientHelper clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        var abc = _JournalListViewModel.Data;
        var abcd = (GLT00100JournalGridDTO)poParameter;
        try
        {
            await _JournalListViewModel.GetGsmCompany();
            await _JournalListViewModel.GetSystemParam();
            await _JournalListViewModel.GetCurrencyList();
            await _JournalListViewModel.GetStatusList();
            await _JournalListViewModel.GetCenterList();
            VAR_GL_SYSTEM_PARAMDTO systemparamData = new VAR_GL_SYSTEM_PARAMDTO()
            {
                CSTART_PERIOD = _JournalListViewModel.CurrentPeriodStartCollection.CSTART_DATE,
                CCURRENT_PERIOD_MM = _JournalListViewModel.SystemParamCollection.CCURRENT_PERIOD_MM,
                CCURRENT_PERIOD_YY = _JournalListViewModel.SystemParamCollection.CSTART_PERIOD_YY,
                CSOFT_PERIOD_MM = _JournalListViewModel.SystemParamCollection.CSOFT_PERIOD_MM,
                CSOFT_PERIOD_YY = _JournalListViewModel.SystemParamCollection.CSOFT_PERIOD_YY
            };
            await _JournalListViewModel.GetCurrentPeriodStart(systemparamData);
            GLT00100JournalGridDTO pcParam = (GLT00100JournalGridDTO)poParameter;


            if (pcParam.CREC_ID != null)
            {
                _JournalListViewModel.EnablePrint = true;
                _JournalListViewModel.EnableCopy = true;
                _JournalListViewModel.Journal = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(poParameter);
                _JournalListViewModel._Journal.CREC_ID = _JournalListViewModel.Journal.CREC_ID;
                _JournalListViewModel.LcCrecID = _JournalListViewModel.Journal.CREC_ID;
                _JournalListViewModel.GetJournalDetailList();
                _conductorRef.R_GetEntity(_JournalListViewModel.Journal.CREC_ID);
            }
          

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Form
    private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _JournalListViewModel.GetJournal(_JournalListViewModel.Journal);
            eventArgs.Result = _JournalListViewModel.Journal;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task JournalForm_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(eventArgs.Data);
            loParam.DetailList = new List<GLT00100JournalGridDetailDTO>(_JournalListViewModel._JournaDetailList);

            await _JournalListViewModel.SaveJournal(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _JournalListViewModel.Journal;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var data = (GLT00100DTO)eventArgs.Data;

        _JournalListViewModel.allStatusData = _JournalListViewModel.allStatusData
            .Where(item => !string.IsNullOrEmpty(item.CCODE))
            .ToList();

        _JournalListViewModel.Drefdate = DateTime.Now;
        _JournalListViewModel.Ddocdate = DateTime.Now;
        _JournalListViewModel.Drevdate = DateTime.Now;
        data.CCREATE_BY = clientHelper.UserId;
        data.CUPDATE_BY = clientHelper.UserId;
        data.CUPDATE_DATE = DateTime.Now.ToLongDateString();
        data.CCREATE_DATE = DateTime.Now.ToLongDateString();
        _enableCrudJournalDetail = true;
        _JournalListViewModel.EnablePrint = false;
        _JournalListViewModel._JournaDetailListTemp = _JournalListViewModel._JournaDetailList;
        _JournalListViewModel._JournaDetailList = new();
    }
    public async Task JournalForm_RSaving(R_SavingEventArgs eventArgs)
    {
        var loParam = (GLT00100DTO)eventArgs.Data;
        var loEx = new R_Exception();

        try
        {
            ((GLT00100DTO)eventArgs.Data).CREF_DATE = _JournalListViewModel.Drefdate.ToString("yyyyMMdd");
            ((GLT00100DTO)eventArgs.Data).CDOC_DATE = _JournalListViewModel.Ddocdate.ToString("yyyyMMdd");
            ((GLT00100DTO)eventArgs.Data).CREVERSE_DATE = _JournalListViewModel.Drevdate.ToString("yyyyMMdd");

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private void ValidationFormGLT00100JournalEntry(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = (GLT00100DTO)eventArgs.Data;
            if (eventArgs.ConductorMode != R_eConductorMode.Normal)
            {
                if (loParam.CREF_NO == null && _JournalListViewModel.TransactionCodeCollection.LINCREMENT_FLAG == false)
                {
                    loEx.Add("", "Reference No. is required!");
                }

                if (_JournalListViewModel.Drefdate < DateTime.ParseExact(_JournalListViewModel.CurrentPeriodStartCollection.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Reference Date cannot be before Current Period!");
                }

                if (_JournalListViewModel.Drefdate > _JournalListViewModel.Drevdate)
                {
                    loEx.Add("", "Reference Date cannot be after Reversing Date!");
                }

                if (_JournalListViewModel.Ddocdate != null && _JournalListViewModel.Ddocdate < DateTime.ParseExact(_JournalListViewModel.CurrentPeriodStartCollection.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }

                if (loParam.CDOC_NO == null && _JournalListViewModel.Ddocdate != null)
                {
                    loEx.Add("", "Please input Document No.!");
                }

                if (_JournalListViewModel.Ddocdate != DateTime.MinValue &&
                    _JournalListViewModel.Drevdate != DateTime.MinValue &&
                    _JournalListViewModel.Ddocdate > _JournalListViewModel.Drevdate)
                {
                    loEx.Add("", "Document Date cannot be after Reversing Date!");
                }

                if (_JournalListViewModel.Drevdate == null)
                {
                    loEx.Add("", "Reversing Date is required!");
                }

                if (string.IsNullOrEmpty(loParam.CTRANS_DESC))
                {
                    loEx.Add("", "Description is required!");
                }
                if ((loParam.NDEBIT_AMOUNT > 0 || loParam.NCREDIT_AMOUNT > 0) && loParam.NDEBIT_AMOUNT != loParam.NCREDIT_AMOUNT)
                {
                    loEx.Add("", "Total Debit Amount must be equal to Total Credit Amount");
                }

                if (loParam.NPRELIST_AMOUNT > 0 && loParam.NPRELIST_AMOUNT != loParam.NDEBIT_AMOUNT)
                {
                    loEx.Add("", "Journal amount is not equal to Prelist!");
                }

                if (loParam.NLBASE_RATE <= 0)
                {
                    loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                }

                if (loParam.NLCURRENCY_RATE <= 0)
                {
                    loEx.Add("", "Local Currency Rate must be greater than 0!");
                }

                if (loParam.NBBASE_RATE <= 0)
                {
                    loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                }

                if (loParam.NBCURRENCY_RATE <= 0)
                {
                    loEx.Add("", "Base Currency Rate must be greater than 0!");
                }

            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task JournalForm_RDisplay(R_DisplayEventArgs eventArgs)
    {
        var data = (GLT00100DTO)eventArgs.Data;
        if (eventArgs.ConductorMode == R_eConductorMode.Normal)
        {
            _JournalListViewModel.Drefdate =
                DateTime.ParseExact(data.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            _JournalListViewModel.Ddocdate =
                DateTime.ParseExact(data.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            _JournalListViewModel.Drevdate =
                DateTime.ParseExact(data.CREVERSE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            _JournalListViewModel.EnableDept = false;

            if (data.CSTATUS != "99")
            {
                _JournalListViewModel.EnableDelete = true;
            }


            if (data.CSTATUS == "00" || data.CSTATUS == "10")
            {
                _JournalListViewModel.EnableSubmit = true;
                if (data.CSTATUS == "10")
                {
                    _JournalListViewModel.SubmitLabel = "Undo Submit";
                }
                else
                {
                    _JournalListViewModel.SubmitLabel = "Submit";
                }
            }

            if (data.CSTATUS == "10" && _JournalListViewModel.TransactionCodeCollection.LAPPROVAL_FLAG == true)
            {
                _JournalListViewModel.EnableApprove = true;
            }

            data.IPERIOD = int.Parse(_JournalListViewModel.CSOFT_PERIOD_YY);
            if ((data.CSTATUS == "20" || data.CSTATUS == "10" && _JournalListViewModel.TransactionCodeCollection.LAPPROVAL_FLAG == false) || (data.CSTATUS == "80" && _JournalListViewModel.IundoCollection.IOPTION != 1) && data.IPERIOD >= _JournalListViewModel.SystemParamCollection.ISOFT_PERIOD_YY)
            {
                _JournalListViewModel.EnableCommit = true;
            }
            if (data.CSTATUS == "80")
            {
                _JournalListViewModel.CommitLabel = "Undo Commit";
            }

            if (data.CSTATUS != "80")
            {
                _JournalListViewModel.EnableCommit = false;
            }

        }

        if (eventArgs.ConductorMode != R_eConductorMode.Normal)
        {
            _JournalListViewModel.EnableDept = true;
        }
    }

    private async Task JournalForm_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        _enableCrudJournalDetail = false;

        var res = await R_MessageBox.Show("", "You haven’t saved your changes. Are you sure want to cancel? [Yes/No]",
            R_eMessageBoxButtonType.YesNo);
        if (res == R_eMessageBoxResult.Yes)
        {
            Close(false, false);
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add)
            {
                _JournalListViewModel._JournaDetailList = _JournalListViewModel._JournaDetailListTemp;
            }
        }
    }

    private void JournalForm_BeforeEdit(R_BeforeEditEventArgs eventArgs)
    {
        _enableCrudJournalDetail = true;

    }

    private async Task CopyJournalEntryProcess()
    {
        var loEx = new R_Exception();
        try
        {
            var eventArgs = new R_ServiceSaveEventArgs(_JournalListViewModel.Journal, R_eConductorMode.Edit);
            eventArgs.Data = _JournalListViewModel.Journal;

            var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(eventArgs.Data);
            loParam.DetailList = new List<GLT00100JournalGridDetailDTO>(_JournalListViewModel._JournaDetailList);

            await _JournalListViewModel.SaveJournal(loParam, eCRUDMode.EditMode);
            eventArgs.Result = _JournalListViewModel.Journal;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Detail
    private async Task JournalDet_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {

            await _JournalListViewModel.GetJournalDetailList();
            eventArgs.ListEntityResult = _JournalListViewModel._JournaDetailList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task JournalDet_RDisplay(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {

            var data = (GLT00100JournalGridDetailDTO)eventArgs.Data;

            if (data.NDEBIT == 0 && data.NCREDIT == 0)
            {
                data.CDBCR = "D";
            }
            else if (data.NDEBIT == 0 && data.NCREDIT > 0)
            {
                data.CDBCR = "C";
            }
            else
            {
                data.CDBCR = "";
            }
            data.NAMOUNT = data.NDEBIT + data.NCREDIT;
            if (data.CCENTER_CODE == null)
            {
                foreach (var item in _JournalListViewModel.CenterListData)
                {
                    if (data.CCENTER_NAME == item.CCENTER_NAME)
                    {
                        data.CCENTER_CODE = item.CCENTER_CODE;
                    }
                }
            }
            data.CDOCUMENT_DATE = _JournalListViewModel.Ddocdate.ToString("yyyyMMdd");
            data.CDOCUMENT_NO = _JournalListViewModel.Data.CDOC_NO;



        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void JournalDet_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        eventArgs.Result = eventArgs.Data;
    }

    private void JournalDet_Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var data = (GLT00100JournalGridDetailDTO)eventArgs.Data;
            if (string.IsNullOrWhiteSpace(data.CGLACCOUNT_NO))
            {
                loEx.Add("", "Account No. is required!");
            }

            if (string.IsNullOrWhiteSpace(data.CCENTER_CODE) && (data.CBSIS == 'B' && _JournalListViewModel.CompanyCollection.LENABLE_CENTER_BS == true) || (data.CBSIS == 'I' && _JournalListViewModel.CompanyCollection.LENABLE_CENTER_IS == true))
            {
                loEx.Add("", $"Center Code is required for Account No. {data.CGLACCOUNT_NO}!");
            }

            if (data.NDEBIT == 0 && data.NCREDIT == 0)
            {
                loEx.Add("", "Journal amount cannot be 0!");
            }

            if (data.NDEBIT > 0 && data.NCREDIT > 0)
            {
                loEx.Add("", "Journal amount can only be either Debit or Credit!");
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {
                if (_JournalListViewModel._JournaDetailList.Any(item => item.CGLACCOUNT_NO == data.CGLACCOUNT_NO))
                {
                    loEx.Add("", $"Account No. {data.CGLACCOUNT_NO} already exists!");
                }
            }


            if ((_JournalListViewModel._JournaDetailList.Count(item => item.CGLACCOUNT_NO == data.CGLACCOUNT_NO) > 0) && eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                loEx.Add("", $"Account No. {data.CGLACCOUNT_NO} already exists!");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void JournalDet_AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var data = (GLT00100JournalGridDetailDTO)eventArgs.Data;

        if (_JournalListViewModel._JournaDetailList.Any())
        {
            // Find the maximum INO value in the list and increment it by 1
            int maxINO = _JournalListViewModel._JournaDetailList.Max(item => item.INO);
            data.INO = maxINO + 1;
        }
        else
        {
            // If the list is empty, set INO to 1 (or another initial value)
            data.INO = 1;
        }

        eventArgs.Data = data;
    }
    private void Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CPROGRAM_CODE = "GLM00100",
            CUSER_ID = clientHelper.UserId,
            CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName,
            CBSIS = "",
            CDBCR = "",
            CCENTER_CODE = "",
            CPROPERTY_ID = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;
        var loGetData = (GLT00100JournalGridDetailDTO)eventArgs.ColumnData;
        loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        loGetData.CBSIS = loTempResult.CBSIS_DESCR.Contains("B") ? 'B' : (loTempResult.CBSIS_DESCR.Contains("I") ? 'I' : default(char));

    }
    #endregion

    #region Process
    private async Task ApproveJournalProcess()
    {
        var loEx = new R_Exception();
        try
        {
            if (_JournalListViewModel.Journal.LALLOW_APPROVE == false)
            {
                R_MessageBox.Show("", "You don’t have right to approve this journal!", R_eMessageBoxButtonType.OK);
                goto EndBlock;
            }

            await _JournalListViewModel.ApproveJournal(_JournalListViewModel._Journal);
            await _JournalListViewModel.GetJournal(new GLT00100DTO() { CREC_ID = _JournalListViewModel._Journal.CREC_ID });
            if (_JournalListViewModel.Journal.CSTATUS == "20")
            {
                R_MessageBox.Show("", "Selected Journal Approved Successfully!", R_eMessageBoxButtonType.OK);
            }
            var param = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(_JournalListViewModel._Journal);
            await _JournalListViewModel.GetJournal(param);
            _JournalListViewModel.EnableDelete = true;
            _JournalListViewModel.SubmitLabel = "Submit";
        EndBlock:;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task CommitJournalProcess()
    {
        var loEx = new R_Exception();
        try
        {
            GLT00100JournalGridDTO lcdata = new GLT00100JournalGridDTO()
            {
                CDEPT_CODE = _JournalListViewModel.Journal.CDEPT_CODE,
                CREF_NO = _JournalListViewModel.Journal.CREF_NO,
                CREC_ID = _JournalListViewModel.Journal.CREC_ID,
                CSTATUS = _JournalListViewModel.Journal.CSTATUS,
                LCOMMIT_APRJRN = _JournalListViewModel.Journal.LCOMMIT_APRJRN,
                LUNDO_COMMIT = _JournalListViewModel.Journal.LUNDO_COMMIT
            };
            if (_JournalListViewModel.Journal.CSTATUS == "80" && _JournalListViewModel.IundoCollection.IOPTION == 2)
            {
                var result = await R_MessageBox.Show("", "Are you sure want to undo commit this journal? [Yes/No]", R_eMessageBoxButtonType.YesNo);
                await _JournalListViewModel.UndoReversingJournal(lcdata);
                if (result == R_eMessageBoxResult.Yes)
                {
                    goto commit;
                }
                goto EndBlock;
            }
            else
            {
                var result = await R_MessageBox.Show("", "Are you sure want to commit this journal? [Yes/No]", R_eMessageBoxButtonType.YesNo);
                if (result == R_eMessageBoxResult.Yes)
                {
                    if (_JournalListViewModel.SystemParamCollection.IREVERSE_JRN_POST == 1)
                    {
                        await _JournalListViewModel.ProcessCommitJournal(lcdata);
                    }
                }
                else
                {
                    goto EndBlock;
                }
            }
        commit:;
            await _JournalListViewModel.CommitJournal(lcdata);
            await _JournalListViewModel.GetJournal(new GLT00100DTO()
            {
                CREC_ID = _JournalListViewModel.Journal.CREC_ID,
            });
            if (_JournalListViewModel.CSTATUS_TEMP == "80")
            {
                R_MessageBox.Show("", "Selected Journal Undo Commit Successfully!", R_eMessageBoxButtonType.OK);
                _JournalListViewModel.CommitLabel = "Commit";

            }
            else
            {
                R_MessageBox.Show("", "Selected Journal Committed Successfully!", R_eMessageBoxButtonType.OK);
                _JournalListViewModel.CommitLabel = "Undo Commit";
            }
            var param = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(_JournalListViewModel._Journal);
            await _JournalListViewModel.GetJournal(param);
        EndBlock:;
            _JournalListViewModel.EnableDelete = true;
            _JournalListViewModel.EnableSubmit = false;
            _JournalListViewModel.SubmitLabel = "Submit";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task SubmitJournalProcess()
    {
        var loEx = new R_Exception();
        try
        {
            if (_JournalListViewModel.Journal.CSTATUS == "10")
            {
                var result = await R_MessageBox.Show("", "Are you sure want to undo submit this journal? [Yes/No] ",
                    R_eMessageBoxButtonType.YesNo);
                if (result == R_eMessageBoxResult.Yes)
                {
                    _JournalListViewModel.SubmitLabel = "Submit";
                    goto Submit;
                }

            }
            var res = await R_MessageBox.Show("", "Are you sure want to submit this journal? [Yes/No] ",
                R_eMessageBoxButtonType.YesNo);
            if (res == R_eMessageBoxResult.Yes)
            {
                _JournalListViewModel.SubmitLabel = "Undo Submit";
                goto Submit;

            }

            goto End;
        Submit:;
            await _JournalListViewModel.SubmitJournal(_JournalListViewModel._Journal);
            var param = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(_JournalListViewModel._Journal);
            await _JournalListViewModel.GetJournal(param);
        End:;
            _JournalListViewModel.EnableDelete = true;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DeleteJournalProcess()
    {
        var loEx = new R_Exception();
        try
        {
            var result = await R_MessageBox.Show("", "Are you sure want to delete this journal?’[Yes/No]",
                R_eMessageBoxButtonType.YesNo);
            if (result == R_eMessageBoxResult.Yes)
            {
                goto Delete;
            }

            goto EndBlock;
        Delete:;
            await _JournalListViewModel.DeleteJournal(_JournalListViewModel._Journal);
            var param = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(_JournalListViewModel._Journal);
            await _JournalListViewModel.GetJournal(param);
        EndBlock:;
            _JournalListViewModel.EnableDelete = false;
            _JournalListViewModel.SubmitLabel = "Submit";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    //private async Task CloseJournalEntry()
    //{
    //    var loEx = new R_Exception();
    //    try
    //    {
    //        var result = await R_MessageBox.Show("", "Are you sure want to Close this journal?’[Yes/No]",
    //            R_eMessageBoxButtonType.YesNo);
    //        if (result == R_eMessageBoxResult.Yes)
    //        {
    //            goto Delete;
    //        }

    //        goto EndBlock;
    //    Delete:;
    //        await _JournalListViewModel.CloseJournal(_JournalListViewModel.JournalEntity);
    //        var param = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(_JournalListViewModel.JournalEntity);
    //        await _JournalListViewModel.GetJournal(param);
    //    EndBlock:;
    //        _JournalListViewModel.EnableDelete = true;
    //        _JournalListViewModel.SubmitLabel = "Submit";
    //        _JournalListViewModel.CommitLabel = "Commit";

    //    }
    //    catch (Exception ex)
    //    {
    //        loEx.Add(ex);
    //    }

    //    loEx.ThrowExceptionIfErrors();
    //}
    #endregion

    /*
    #region Print

    private R_Lookup R_LookupBtnPrint;
    private async Task Before_Open_lookupPrint(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GLTR00100DTO()
        {
            CREC_ID = _JournalListViewModel.Journal.CREC_ID
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GLTR00100);
    }

    private void After_Open_lookupPrint(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GLTR00100)eventArgs.Result;
        if (loTempResult == null)
        {
            return;
        }
    }
    #endregion
    */
    protected override Task<object> R_Set_Result_PredefinedDock()
    {
        var lcResult = _JournalListViewModel.Journal;

        return Task.FromResult<object>(lcResult);
    }
    #region lookupDept

    private R_Lookup R_LookupBtnDept;

    private async Task Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00700ParameterDTO
        {
            CUSER_ID = clientHelper.UserId,
            CCOMPANY_ID = clientHelper.CompanyId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00700);
    }

    private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00700DTO)eventArgs.Result;
        if (loTempResult == null)
        {
            return;
        }

        _JournalListViewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
        _JournalListViewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
    }

    #endregion
}

