using LMM03700Common.DTO;
using LMM03700Model.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;

namespace LMM03700Front
{
    public partial class LMM03700
    {
        private LMM03710ViewModel _viewModelTenantClass = new();//viewModel TenantClass
       // private LMM03700ViewModel _viewTenantClassGrpModel = new();
        private R_ConductorGrid _conTenantClassGroupRef; //ref conductor TenantClassGrp
        private R_Grid<TenantClassificationGroupDTO> _gridTenantClassGroupRef; //ref grid TenantClassGrp
        private R_TabPage _tab2TenantClass; //ref TabPage tab2
        private R_TabStrip _tabStrip; //ref Tabstrip
        public bool _pageTenantClassOnCRUDmode = false; //to disable moving tab while crudmode
        private bool _comboboxPropertyEnabled = true; //to disable combobox while crudmode
        
        private R_TabPage _tabPage_TC;

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
                await _viewModelTenantClass.GetPropertyList();
             //   await _gridTenantClassGrpRef.R_RefreshGrid(null);
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

                _viewModelTenantClass._propertyId = lsProperty;
               // _gridTenantClassGroupRef.R_RefreshGrid(null);

                /*
                _viewTenantClassGrpModel._propertyId = lsProperty;//re assign when property klicked on combobox
                if (_conTenantClassGroupRef.R_ConductorMode == R_eConductorMode.Normal)
                { 
                    await _gridTenantClassGroupRef.R_RefreshGrid(null);

                    if (_tabStrip.ActiveTab.Id == "T_C")
                    {
                        //sending property ud to tab2 (will be catch at init master tab2)
                        await _tab2TenantClass.InvokeRefreshTabPageAsync(_viewTenantClassGrpModel._propertyId);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private void Before_Open_TC(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM03710);
            eventArgs.Parameter = _viewModelTenantClass._propertyId;
        }
    }
}
