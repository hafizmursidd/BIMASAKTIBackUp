using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM04500Common;
using GSM04500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM04500Front
{
    public partial class GSM04500Upload : R_Page
    {
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
        private GSM04500ViewModel_Upload _viewModel = new();
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] R_IExcel Excel { get; set; }
        private R_Grid<GSM04500UploadErrorValidateDTO> JournalGroup_gridRef;
        private bool IsFileExist = false;
        private bool IsUploadSuccesful = true;

        public byte[] fileByte = null;

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var Param = (GSM004500ParamDTO)poParameter;
                _viewModel.CurrentObjectParam = Param;
                _viewModel.CurrentObjectParam.CUSER_ID = clientHelper.UserId;

                _viewModel.StateChangeAction = StateChangeInvoke;
                 await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task _SourceUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //get file name
                _viewModel.SourceFileName = eventArgs.File.Name;

                //import excel from user
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                fileByte = loMS.ToArray();

                //validate type file
                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                if (eventArgs.File.Name.Length > 0)
                {
                    IsFileExist = true;
                }
                else
                {
                    IsFileExist = false;
                }

                ReadExcelFile();
                await JournalGroup_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (_viewModel.IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.ValidateFile();
                eventArgs.ListEntityResult = _viewModel.JournalGroupValidateUploadError;
                //Enable checklist overwrite
                _viewModel.ChecklistOverwrite = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        #region ReadExcel

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<GSM04500UploadFromExcelDTO> loExtract = new List<GSM04500UploadFromExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(fileByte, new string[] { "JournalGroup" });
                var loResult = R_FrontUtility.R_ConvertTo<GSM04500UploadFromExcelDTO>(loDataSet.Tables[0]);
                loExtract = new List<GSM04500UploadFromExcelDTO>(loResult);

                ////Convert to DTO for DB
                _viewModel.loUploadLJournalGroupList = loExtract.Select(x => new GSM04500UploadToDBDTO()
                {
                    JournalGroup = x.JournalGroup,
                    JournalGroupName = x.JournalGroupName,
                    EnableAccrual = x.EnableAccrual
                }).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var loMatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                _viewModel.IsErrorEmptyFile = loMatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (GSM04500UploadErrorValidateDTO)eventArgs.Data;

            if (loData.Var_Exists)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    FontColor = "red"
                };
            }
            else
            {
                _viewModel.BtnSave = true;
            }
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SaveFileBulkFile(clientHelper.CompanyId, clientHelper.UserId);
                    await this.Close(true, true);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
