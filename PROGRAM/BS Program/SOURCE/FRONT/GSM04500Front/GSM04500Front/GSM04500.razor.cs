using GSM04500Common;
using GSM04500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using Microsoft.JSInterop;

namespace GSM04500Front
{
    public partial class GSM04500
    {
        private GSM04500ViewModel journalGroupViewModel = new();
        private R_ConductorGrid _conJournalGroupRef;
        private R_Grid<GSM04500DTO> _gridRef;
        private R_Conductor _conductorRef;

        private R_TabStrip _tabStrip;
        private R_TabPage _tabPageAccountSetting;
        [Inject] IJSRuntime JS { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await journalGroupViewModel.GetPropertyList();
                await journalGroupViewModel.GetJournalGroupTypeList();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                journalGroupViewModel.PropertyValueContext = lsProperty;
                await _gridRef.R_RefreshGrid(poParam);
                journalGroupViewModel.DropdownGroupType = true;

                if (_tabStrip.ActiveTab.Id == "Tab_AccountSetting")
                {
                    journalGroupViewModel.DropdownGroupType = false;
                    //  await _tabPageAccountSetting.InvokeRefreshTabPageAsync(journalGroupViewModel.PropertyValueContext);
                    // await _tabPageAccountSetting.InvokeRefreshTabPageAsync(journalGroupViewModel.JournalGroupTypeValue);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task JournalGroupDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsJournalGrpType = (string)poParam;
            try
            {
                journalGroupViewModel.JournalGroupTypeValue = lsJournalGrpType;
                await _gridRef.R_RefreshGrid(poParam);
                journalGroupViewModel.DropdownGroupType = true;

                if (_tabStrip.ActiveTab.Id == "Tab_AccountSetting")
                {
                    journalGroupViewModel.DropdownGroupType = false;
                    //  await _tabPageAccountSetting.InvokeRefreshTabPageAsync(journalGroupViewModel.PropertyValueContext);
                    // await _tabPageAccountSetting.InvokeRefreshTabPageAsync(journalGroupViewModel.JournalGroupTypeValue);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region Journal Group
        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await journalGroupViewModel.GetAllJournalAsync();
                eventArgs.ListEntityResult = journalGroupViewModel.JournalGroupList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                eventArgs.Result = await journalGroupViewModel.GetGroupJournaltOneRecord(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                await journalGroupViewModel.DeleteOneRecordJournalGroup(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                //if (R_eConductorMode.Normal== eventArgs.ConductorMode || R_eConductorMode.Add == eventArgs.ConductorMode)
                //{                   
                //await journalGroupViewModel.SaveJournalGroup(loParam, R_eConductorMode.Add);
                //}
                await journalGroupViewModel.SaveJournalGroup(loParam, eventArgs.ConductorMode);

                eventArgs.Result = journalGroupViewModel.JournalGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        //private async Task ServiceBeforeAdd(R_AfterAddEventArgs eventArgs)
        //{
        //    eventArgs.Data = new GSM04500DTO()
        //    {
        //        DCREATE_DATE = DateTime.Now,
        //        DUPDATE_DATE = DateTime.Now
        //    };
        //}

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                journalGroupViewModel.JournalGroupCurrent = loParam;
            }
        }

        public async Task ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;

                if (string.IsNullOrEmpty(loParam.CJRNGRP_CODE))
                    loEx.Add(new Exception("Journal Code is required."));
                if (string.IsNullOrEmpty(loParam.CJRNGRP_NAME))
                    loEx.Add(new Exception("Journal Group Name is required."));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region CHANGE TAB
        //CHANGE TAB

        private void Before_Open_AccountSetting(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM04500AccountSetting);;
            eventArgs.Parameter = journalGroupViewModel.JournalGroupCurrent;
        }

        private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            journalGroupViewModel.DropdownProperty = true;
            journalGroupViewModel.DropdownGroupType = true;
            if (eventArgs.TabStripTab.Id == "Tab_AccountSetting")
            {
                journalGroupViewModel.DropdownProperty = false;
                journalGroupViewModel.DropdownGroupType = false;
            }
        }
        #endregion

        #region Template
        private async Task _Staff_TemplateBtn_OnClick()
        {
           // var loData = new List<GSM04500DTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await journalGroupViewModel.DownloadTemplate();

                    var saveFileName = $"Journal Group.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Upload

        private void Before_Open_Upload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            string propertyId = journalGroupViewModel.PropertyValueContext;
            GSM04500PropertyDTO loparam = (journalGroupViewModel.PropertyList).Find(p => p.CPROPERTY_ID == propertyId);
           
            var param = new GSM004500ParamDTO()
            {
                CCOMPANY_ID = journalGroupViewModel.JournalGroupCurrent.CCOMPANY_ID,
                CUSER_ID = journalGroupViewModel.JournalGroupCurrent.CUSER_ID,
                CJRNGRP_TYPE = journalGroupViewModel.JournalGroupCurrent.CJRNGRP_TYPE,
                CPROPERTY_ID = loparam.CPROPERTY_ID,
                CPROPERTY_NAME = loparam.CPROPERTY_NAME
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSM04500Upload);
        }

        private async Task After_Open_Upload(R_AfterOpenPopupEventArgs eventArgs)
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

        #endregion
    }
}
