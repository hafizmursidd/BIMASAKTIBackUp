using BlazorClientHelper;
using GLT00100Common.DTOs;
using GLT00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLT00100Front;

    public partial class GLT00100 : R_Page
    {
        private GLT00100ViewModel _JournalListViewModel = new();
        private R_Conductor _conductorRef;

        private R_ConductorGrid _conductorGridRef;
        private R_Grid<GLT00100JournalGridDTO> _gridRef;

        private R_Grid<GLT00100JournalGridDetailDTO> _gridDetailRef;
        private R_ConductorGrid _conductorGridDetailRef;
        [Inject] IClientHelper clientHelper { get; set; }


    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            VAR_GL_SYSTEM_PARAMDTO systemparamData = new VAR_GL_SYSTEM_PARAMDTO()
            {
                CSTART_PERIOD = _JournalListViewModel.CurrentPeriodStartCollection.CSTART_DATE,
                CCURRENT_PERIOD_MM = _JournalListViewModel.SystemParamCollection.CCURRENT_PERIOD_MM,
                CCURRENT_PERIOD_YY = _JournalListViewModel.SystemParamCollection.CSTART_PERIOD_YY,
                CSOFT_PERIOD_MM = _JournalListViewModel.SystemParamCollection.CSOFT_PERIOD_MM,
                CSOFT_PERIOD_YY = _JournalListViewModel.SystemParamCollection.CSOFT_PERIOD_YY
            };

            await _JournalListViewModel.GetSystemParam();
            await _JournalListViewModel.GetCurrentPeriodStart(systemparamData);
            await _JournalListViewModel.GetSoftPeriodStart(systemparamData);
            await _JournalListViewModel.GetGsmCompany();
            await _JournalListViewModel.GetGSMPeriod();
            await _JournalListViewModel.GetTransactionCode();
            await _JournalListViewModel.GetIundo();
            await _JournalListViewModel.GetStatusList();
            await _JournalListViewModel.GetDepartmentList();
            GetMonth();
            _JournalListViewModel.COMPANYID = clientHelper.CompanyId;
            _JournalListViewModel.USERID = clientHelper.UserId;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public void GetMonth()
    {
        _JournalListViewModel.GetMonthList = new List<GetMonthDTO>();

        for (int i = 1; i <= 12; i++)
        {
            string monthId = i.ToString("D2");
            GetMonthDTO month = new GetMonthDTO { Id = monthId };
            _JournalListViewModel.GetMonthList.Add(month);
        }

    }
    public async Task OnclickSearch(object poParam)
    {
        var loEx = new R_Exception();
        try
        {
            loEx.ThrowExceptionIfErrors();
            if (string.IsNullOrEmpty((_JournalListViewModel).Data.CSEARCH_TEXT))
            {
                loEx.Add(new Exception("Please input keyword to search!"));
                goto EndBlock;
            }
            if (!string.IsNullOrEmpty(_JournalListViewModel.Data.CSEARCH_TEXT)
                && _JournalListViewModel.Data.CSEARCH_TEXT.Length < 3)
            {
                loEx.Add(new Exception("Minimum search keyword is 3 characters!"));
                goto EndBlock;
            }

            await _JournalListViewModel.ShowAllJournals();
            if (_JournalListViewModel._JournalList.Count() < 1)
            {
                _JournalListViewModel._JournaDetailList.Clear();
                loEx.Add(new Exception("Data Not Found!"));
                _JournalListViewModel.EnableButton = false;
            }
        EndBlock:;
            //GLT00100DTO param;
            //param.CPERIOD = _JournalListViewModel.Journal.CPERIOD;
            GLT00100JournalGridDTO param = new GLT00100JournalGridDTO()
            {
                CPERIOD = _JournalListViewModel.Data.CPERIOD,
                CSEARCH_TEXT = _JournalListViewModel.Data.CSEARCH_TEXT,
                CSTATUS = _JournalListViewModel.Data.CSTATUS,
                CDEPT_CODE = _JournalListViewModel.Data.CDEPT_CODE
            };
            await _gridRef.R_RefreshGrid(param);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();

    }
    public async Task OnClickShowAll(object poParam)
    {
        var loEx = new R_Exception();
        try
        {
            _JournalListViewModel.Data.CSEARCH_TEXT = "";
            await _JournalListViewModel.ShowAllJournals();
            if (_JournalListViewModel._JournalList.Count() < 1)
            {
                _JournalListViewModel._JournaDetailList.Clear();
                loEx.Add(new Exception("Data Not Found!"));
                _JournalListViewModel.EnableButton = false;
            }
            GLT00100JournalGridDTO param = new GLT00100JournalGridDTO()
            {
                CPERIOD = _JournalListViewModel.Data.CPERIOD,
                CSEARCH_TEXT = "",
                CSTATUS = _JournalListViewModel.Data.CSTATUS,
                CDEPT_CODE = _JournalListViewModel.Data.CDEPT_CODE
            };
            await _gridRef.R_RefreshGrid(param);

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();

    }

    #region JournalGrid
    private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _JournalListViewModel.ShowAllJournals();
            eventArgs.ListEntityResult = _JournalListViewModel._JournalList;

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    private async Task JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(eventArgs.Data);
            await _JournalListViewModel.GetJournal(loParam);
            eventArgs.Result = _JournalListViewModel.Journal;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
    {
        R_Exception loEx = new R_Exception();
        try
        {

            _JournalListViewModel._Journal = (GLT00100JournalGridDTO)eventArgs.Data;
            _JournalListViewModel.LcCrecID = _JournalListViewModel._Journal.CREC_ID;

            await _JournalListViewModel.GetJournalDetailList();
            _JournalListViewModel.EnableDept = false;

            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {

                if (_JournalListViewModel._JournalList == null)
                {
                    _JournalListViewModel.EnableButton = false;
                }
                else
                {
                    _JournalListViewModel.EnableButton = true;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task ApproveJournalProcess()
    {
        var loEx = new R_Exception();
        try
        {
            if (_JournalListViewModel._Journal.LALLOW_APPROVE == false)
            {
                R_MessageBox.Show("", "You don’t have right to approve this journal!", R_eMessageBoxButtonType.OK);
                goto EndBlock;
            }

            GLT00100JournalGridDTO data = new GLT00100JournalGridDTO()
            {
                CSTATUS = _JournalListViewModel._Journal.CSTATUS,
                LCOMMIT_APRJRN = _JournalListViewModel._Journal.LCOMMIT_APRJRN,
                LUNDO_COMMIT = _JournalListViewModel._Journal.LUNDO_COMMIT,
                CREC_ID = _JournalListViewModel._Journal.CREC_ID
            };
            await _JournalListViewModel.ApproveJournal(data);
            await _JournalListViewModel.GetJournal(new GLT00100DTO() { CREC_ID = _JournalListViewModel._Journal.CREC_ID });
            if (_JournalListViewModel.Journal.CSTATUS == "20")
            {
                R_MessageBox.Show("", "Selected Journal Approved Successfully!", R_eMessageBoxButtonType.OK);

            }
            await _JournalListViewModel.ShowAllJournals();

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
                CDEPT_CODE = _JournalListViewModel._Journal.CDEPT_CODE,
                CREF_NO = _JournalListViewModel._Journal.CREF_NO,
                CREC_ID = _JournalListViewModel._Journal.CREC_ID
            };
            if (_JournalListViewModel._Journal.CSTATUS == "80" && _JournalListViewModel.IundoCollection.IOPTION == 3)
            {
                var result = await R_MessageBox.Show("", "Are you sure want to undo commit this journal? [Yes/No]", R_eMessageBoxButtonType.YesNo);
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
                    await _JournalListViewModel.CommitJournal(lcdata);
                }
                else
                {
                    goto EndBlock;
                }
            }
        commit:;
            GLT00100JournalGridDTO data = new GLT00100JournalGridDTO()
            {
                CSTATUS = _JournalListViewModel._Journal.CSTATUS,
                LCOMMIT_APRJRN = _JournalListViewModel._Journal.LCOMMIT_APRJRN,
                LUNDO_COMMIT = _JournalListViewModel._Journal.LUNDO_COMMIT
            };
            await _JournalListViewModel.CommitJournal(data);
            await _JournalListViewModel.GetJournal(new GLT00100DTO()
            {
                CREC_ID = _JournalListViewModel._Journal.CREC_ID,
            });
            if (_JournalListViewModel.CSTATUS_TEMP == "80")
            {
                await R_MessageBox.Show("", "Selected Journal Undo Commit Successfully!", R_eMessageBoxButtonType.OK);
            }
            else
            {
                await R_MessageBox.Show("", "Selected Journal Committed Successfully!", R_eMessageBoxButtonType.OK);
            }
            await _JournalListViewModel.ShowAllJournals();
        EndBlock:;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    #endregion

    #region JournalGridDetail
    private async Task JournalGridDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
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
        R_DisplayException(loEx);
    }
    #endregion

    #region Predefine Journal Entry
    private void Predef_JournalEntry(R_InstantiateDockEventArgs eventArgs)
    {
         eventArgs.TargetPageType = typeof(GLT00100JournalEntry);
        eventArgs.Parameter = _JournalListViewModel._Journal;
    }
    private async Task AfterPredef_JournalEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion

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

    #region RapidApprove
    private async Task R_Before_Open_PopupRapidApprove(R_BeforeOpenPopupEventArgs eventArgs)
    {

        if (_JournalListViewModel.TransactionApprovalCollection.CRESULT == null)
        {
            await R_MessageBox.Show("", "You don’t have right to approve this journal type!", R_eMessageBoxButtonType.OK);
            goto EndBlock;
        }
        eventArgs.Parameter = _JournalListViewModel._JournalList;
       eventArgs.TargetPageType = typeof(RapidApproveGLT00100);
    EndBlock:;
    }
    private async Task R_After_Open_PopupRapidApprove(R_AfterOpenPopupEventArgs eventArgs)
    {
        GLT00100ParameterDTO param = new GLT00100ParameterDTO()
        {
            CPERIOD = _JournalListViewModel._Journal.CPERIOD,
            CSEARCH_TEXT = _JournalListViewModel._Journal.CSEARCH_TEXT,
            CSTATUS = "20",
            CDEPT_CODE = _JournalListViewModel._Journal.CDEPT_CODE
        };

        await _gridRef.R_RefreshGrid(param);
        var firstJournal = _JournalListViewModel._JournalList.FirstOrDefault();
        if (firstJournal != null)
        {
            await _gridDetailRef.R_RefreshGrid(firstJournal);
        }
        else
        {
            _JournalListViewModel._JournaDetailList.Clear();
        }
    }
    #endregion

    #region RapidCommit
    private async Task R_Before_Open_PopupRapidCommit(R_BeforeOpenPopupEventArgs eventArgs)
    {

        eventArgs.Parameter = _JournalListViewModel._JournalList;
        eventArgs.TargetPageType = typeof(RapidCommitGLT00100);
    }
    private async Task R_After_Open_PopupRapidCommit(R_AfterOpenPopupEventArgs eventArgs)
    {
        GLT00100ParameterDTO param = new GLT00100ParameterDTO()
        {
            CPERIOD = _JournalListViewModel._Journal.CPERIOD,
            CSEARCH_TEXT = _JournalListViewModel._Journal.CSEARCH_TEXT,
            CSTATUS = "80",
            CDEPT_CODE = _JournalListViewModel._Journal.CDEPT_CODE
        };

        await _gridRef.R_RefreshGrid(param);
        var firstJournal = _JournalListViewModel._JournalList.FirstOrDefault();
        if (firstJournal != null)
        {
            await _gridDetailRef.R_RefreshGrid(firstJournal);
        }
        else
        {
            _JournalListViewModel._JournaDetailList.Clear();
        }
    }
    #endregion

}
