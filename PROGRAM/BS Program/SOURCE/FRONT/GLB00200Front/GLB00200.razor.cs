using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GLB00200Common;
using GLB00200Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;

namespace GLB00200Front
{
    public partial class GLB00200 : R_Page
    {
        private GLB00200ViewModel _viewModelGLB00200 = new GLB00200ViewModel();
        //  private GLB00200ProcessReversingViewModel _viewModelGLB00200Process = new();
        private R_Conductor _conductorPeriod;

        private R_Grid<GLB00200DTO> _gridReversing;
        private R_ConductorGrid _conductorReversingJournal;

        [Inject] IClientHelper clientHelper { get; set; }
        //  public GLB00200DTO CurrentReversingJournal = new GLB00200DTO();

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await ServiceGetInitialProcess();
                await GetMonth();
                await _gridReversing.R_RefreshGrid(null);
                _viewModelGLB00200.COMPANYID = clientHelper.CompanyId;
                _viewModelGLB00200.USERID = clientHelper.UserId;

                _viewModelGLB00200.StateChangeAction = StateChangeInvoke;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGLB00200.GetInitialprocess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task GetMonth()
        {
            _viewModelGLB00200.GetMonthList = new List<GetMonthDTO>();

            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                GetMonthDTO month = new GetMonthDTO { Id = monthId };
                _viewModelGLB00200.GetMonthList.Add(month);
            }

        }
        private async Task OnChangedMonth(object poParam)
        {
            var loEx = new R_Exception();
            string PeriodMonthValue = (string)poParam;
            try
            {
                _viewModelGLB00200.PeriodMonth = PeriodMonthValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task GetList_ReversingJournal(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModelGLB00200.GetAllReversingJournalProcess();
                eventArgs.ListEntityResult = _viewModelGLB00200.ReversingJournalProcessList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetList_ReversingJournalWithCustomPeriod()
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModelGLB00200.GetAllReversingJournalProcess();

                if (_viewModelGLB00200.ReversingJournalProcessList.Count() < 1)
                {
                    loEx.Add(new Exception("Data Not Found!"));
                    await _gridReversing.R_RefreshGrid(null);
                    goto EndBlock;
                }
                _viewModelGLB00200.CurrentReversingJournal =
                    _viewModelGLB00200.ReversingJournalProcessList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #region SearchButton

        private async Task GetList_ReversingJournalWithSearch()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(_viewModelGLB00200.lcSearchText))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (!string.IsNullOrEmpty(_viewModelGLB00200.lcSearchText)
                    && _viewModelGLB00200.lcSearchText.Length < 3)
                {
                    loEx.Add(new Exception("Minimum search keyword is 3 characters!"));
                    goto EndBlock;
                }
                await _viewModelGLB00200.GetAllReversingJournalProcess();

                if (_viewModelGLB00200.ReversingJournalProcessList.Count() < 1)
                {
                    loEx.Add(new Exception("Data Not Found!"));
                    await _gridReversing.R_RefreshGrid(null);
                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        private async Task Service_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    _viewModelGLB00200.CurrentReversingJournal = (GLB00200DTO)eventArgs.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task ButtonDetail_GetList_ReversingJournal(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.TargetPageType = typeof(GLB00200Detail);
                eventArgs.Parameter = _viewModelGLB00200.CurrentReversingJournal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region ProcessButton
        private async Task OnClickProcess()
        {
            var loEx = new R_Exception();
            try
            {
                await _conductorReversingJournal.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Save Batch
        private void BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loData = (List<GLB00200DTO>)events.Data;

            if (loData.Count == 0)
            {
                R_MessageBox.Show("", "No Data Found!", R_eMessageBoxButtonType.OK);
                events.Cancel = true;
            }
            //Validasi Incement Flag
            if (_viewModelGLB00200.loGetInitialProcess.LINCREMENT_FLAG == false)
            {
                R_MessageBox.Show("", "Cannot process Recurring Journal with Manual Numbering! Transaction numbering setting for Recurring Journal should be auto increment, not manual numbering!", R_eMessageBoxButtonType.OK);
                events.Cancel = true;
            }
        }
        private async Task ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelGLB00200.loProcessReversingList = (List<GLB00200DTO>)eventArgs.Data;

                await _viewModelGLB00200.GetSelectedDataToReversingJournal();
                await _gridReversing.R_RefreshGrid(null);


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //R_MessageBox.Show("", $"Processing Master Ref. No. CREF_NO {_viewModelGLB00200.ResultProcessList} SUCCESSFUL!", R_eMessageBoxButtonType.OK);

                //if (!string.IsNullOrEmpty(_viewModelGLB00200.ResultFailedProcessList))
                //{
                //    loEx.Add(new Exception($"Failed to process Master Ref. No. CREF_NO {_viewModelGLB00200.ResultFailedProcessList} Failed!"));
                //    goto EndBlock;
                //}

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validation to BAckground red
        private void R_RowForBackGround(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (GLB00200DTO)eventArgs.Data;

            if (loData.LVALID == false)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    BackgroundColor = "RED"
                };
            }
        }
        #endregion
    }
}
