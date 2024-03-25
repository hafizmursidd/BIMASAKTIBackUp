using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GLT00100Common.DTOs;
using GLT00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLT00100Front
{
    public partial class RapidApproveGLT00100 : R_Page
    {
        private GLT00100ViewModel _JournalListViewModel = new();
        private R_Conductor _conductorRef;

        private R_ConductorGrid _conductorGridRef;
        private R_Grid<GLT00100JournalGridDTO> _gridRef;

        private R_Grid<GLT00100JournalGridDetailDTO> _gridDetailRef;
        private R_ConductorGrid _conductorGridDetailRef;
        [Inject] IClientHelper _clientHelper { get; set; }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                List<GLT00100JournalGridDTO> itemList = R_FrontUtility.ConvertObjectToObject<List<GLT00100JournalGridDTO>>(poParameter);
                _JournalListViewModel._JournalList = new ObservableCollection<GLT00100JournalGridDTO>(itemList);

                await _JournalListViewModel.GetDepartmentList();
                await InititalProcess();

                _JournalListViewModel.COMPANYID = _clientHelper.CompanyId;
                _JournalListViewModel.USERID = _clientHelper.UserId;
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InititalProcess()
        {
            _JournalListViewModel.lcDeptCode =
                _JournalListViewModel._JournalList.Select(m => m.CDEPT_CODE).FirstOrDefault();
            _JournalListViewModel.Data.CDEPT_CODE = _JournalListViewModel.lcDeptCode;


            _JournalListViewModel.Data.CDEPT_NAME = _JournalListViewModel.AllDeptData
                .FirstOrDefault(m => m.CDEPT_CODE == _JournalListViewModel.lcDeptCode)?.CDEPT_NAME;


            string crefPrdYY = _JournalListViewModel._JournalList.Select(m => m.CREF_PRD).FirstOrDefault();
            string firstFourDigits = crefPrdYY.Substring(0, Math.Min(4, crefPrdYY.Length));
            if (int.TryParse(firstFourDigits, out int isoPeriodYy))
            {
                _JournalListViewModel.Data.ISOFT_PERIOD_YY = isoPeriodYy;
            }
            string crefPrdMM = _JournalListViewModel._JournalList.Select(m => m.CREF_PRD).FirstOrDefault();
            if (crefPrdMM.Length == 6)
            {
                _JournalListViewModel.Data.CSOFT_PERIOD_MM = crefPrdMM.Substring(4, 2);
            }


            bool allStatusMatch = true;
            string referenceStatus = _JournalListViewModel._JournalList.FirstOrDefault()?.CSTATUS; // Mengambil CSTATUS pertama sebagai referensi

            foreach (var journalData in _JournalListViewModel._JournalList)
            {
                if (journalData.CSTATUS != referenceStatus)
                {
                    allStatusMatch = false;
                    break;
                }
            }

            _JournalListViewModel.Data.CSTATUS_NAME = allStatusMatch && _JournalListViewModel.statusMappings.ContainsKey(referenceStatus)
                ? _JournalListViewModel.statusMappings[referenceStatus]
                : "All";
            _JournalListViewModel.Data.CSTATUS = allStatusMatch && _JournalListViewModel.statusMappings.ContainsKey(referenceStatus)
                ? referenceStatus
                : "";
        }
        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
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
        private async Task OnClose()
        {
            await Close(false, null);
        }
        #region SaveBatchApprove
        private void R_BeforeSaveBatchApprove(R_BeforeSaveBatchEventArgs events)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (List<GLT00100JournalGridDTO>)events.Data;
                if (loData.Count == 0)
                {
                    R_MessageBox.Show("", "No Data Found!", R_eMessageBoxButtonType.OK);
                    events.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_AfterSaveBatchApprove(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var itemToCheck = _JournalListViewModel.loProcessRapidApproveOrCommitList.FirstOrDefault(item => item.CSTATUS == "20");
                if (itemToCheck != null)
                {
                    await R_MessageBox.Show("", "Selected Journal Approved Successfully!", R_eMessageBoxButtonType.OK);
                    _JournalListViewModel.buttonRapidApprove = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceSaveBatchApprove(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _JournalListViewModel.buttonRapidApprove = false;
                List<GLT00100JournalGridDTO> dataList = ((IEnumerable<GLT00100JournalGridDTO>)eventArgs.Data).ToList();
                _JournalListViewModel.loProcessRapidApproveOrCommitList = dataList;
                await _JournalListViewModel.RapidApproveOrCommitJournal();
                await Task.Delay(20 * dataList.Count);
                _JournalListViewModel._JournaDetailList.Clear();
                await _gridRef.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChangeRapidApprove()
        {
            var loEx = new R_Exception();
            try
            {
                var res = await R_MessageBox.Show("", "Are you sure want to process selected Journal(s)?", R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.Yes)
                {
                    //await _viewModel.ProcessBatch();
                    await _conductorGridRef.R_SaveBatch();
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
