using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05000Common.DTO;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using static GSM05000Model.ViewModel.GSM05000TransactionViewModel;

namespace GSM05000Front
{
    public partial class GSM05000Transaction : R_Page
    {
        private GSM05000TransactionViewModel _GSM05000ViewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<GSM05000TransactionDTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                   await _GSM05000ViewModel.GetDelimiterList();

                //var loGroupDescriptor = new List<R_GridGroupDescriptor>
                //{
                //    new() { FieldName = "MODULE" }
                //};
                //await _gridRef.R_GroupBy(loGroupDescriptor);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000TransactionDetailDTO>(eventArgs.Data);
                await _GSM05000ViewModel.GetEntity(loParam);
                eventArgs.Result = _GSM05000ViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Transaction_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05000ViewModel.GetTransactionList();
                eventArgs.ListEntityResult = _GSM05000ViewModel.GridTransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ConvertGridToEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loData = (GSM05000TransactionDetailDTO)eventArgs.Data;
            eventArgs.GridData = new GSM05000TransactionDTO()
            { CTRANS_CODE = loData.CTRANS_CODE, CTRANSACTION_NAME = loData.CTRANSACTION_NAME };
        }
        private Task CheckIncrementFlag(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if ((bool)eventArgs == false)
                {
                    _GSM05000ViewModel.Data.LDEPT_MODE = false;
                    _GSM05000ViewModel.Data.LTRANSACTION_MODE = false;
                    _GSM05000ViewModel.Data.CPERIOD_MODE = "";
                    _GSM05000ViewModel.Data.INUMBER_LENGTH = 0;
                    _GSM05000ViewModel.Data.CPREFIX = "";
                    _GSM05000ViewModel.Data.CSUFFIX = "";
                    _GSM05000ViewModel.Data.CYEAR_FORMAT = "";

                    _GSM05000ViewModel.Data.CDEPT_DELIMITER = "";
                    _GSM05000ViewModel.Data.CTRANSACTION_DELIMITER = "";
                    _GSM05000ViewModel.Data.CPERIOD_DELIMITER = "";
                    _GSM05000ViewModel.Data.CNUMBER_DELIMITER = "";
                    _GSM05000ViewModel.Data.CPREFIX_DELIMITER = "";

                    _GSM05000ViewModel.DeptSequence = 0;
                    _GSM05000ViewModel.TrxSequence = 0;
                    _GSM05000ViewModel.PeriodSequence = 0;
                    _GSM05000ViewModel.LenSequence = 0;
                    _GSM05000ViewModel.PrefixSequence = 0;
                }

                GetUpdateSample();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }

        private Task CheckDeptMode(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _GSM05000ViewModel.AutoSequence(eNumericList.DeptMode, (bool)eventArgs);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }
        private void GetUpdateSample()
        {
            var loEx = new R_Exception();

            try
            {
                _GSM05000ViewModel.getRefNo();

                if (_GSM05000ViewModel.REFNO.Length > 30)
                {
                    loEx.Add("Err08", "Too Long");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private Task CheckTrxMode(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _GSM05000ViewModel.AutoSequence(eNumericList.TrxMode, (bool)eventArgs);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }

        private bool _radioGroup = false;
        private void CheckPeriodMode(object eventArgs)
        {
            var llCondition = (string)eventArgs != "N";
            _radioGroup = llCondition;
            if ((_GSM05000ViewModel.TempEntity.CPERIOD_MODE == "N" && llCondition) ||
                (_GSM05000ViewModel.TempEntity.CPERIOD_MODE != "N" && !llCondition))
            {
                _GSM05000ViewModel.AutoSequence(eNumericList.PeriodMode, llCondition);
            }
            _GSM05000ViewModel.TempEntity.CPERIOD_MODE = (string)eventArgs;
            GetUpdateSample();
        }
        private void CheckLenMode(object eventArgs)
        {
            var llCondition = (int)eventArgs != 0;
            if ((_GSM05000ViewModel.TempEntity.INUMBER_LENGTH == 0 && llCondition)
                || (_GSM05000ViewModel.TempEntity.INUMBER_LENGTH != 0 && !llCondition))
            {
                _GSM05000ViewModel.AutoSequence(eNumericList.LenMode, llCondition);
            }
            _GSM05000ViewModel.TempEntity.INUMBER_LENGTH = (int)eventArgs;
            GetUpdateSample();
        }
        private Task CheckApprovalFlag(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if ((bool)eventArgs == false)
                {
                    _GSM05000ViewModel.Data.LAPPROVAL_FLAG = false;
                    _GSM05000ViewModel.Data.CAPPROVAL_MODE = "";
                    _GSM05000ViewModel.Data.LAPPROVAL_DEPT = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }
        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
        }

        private void Saving(R_SavingEventArgs eventArgs)
        {
        }

        private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
        }

        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
        }
        private void BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            _GSM05000ViewModel.GetSequence();
        }

        private Task InstanceNumberingTab(R_InstantiateDockEventArgs eventArgs)
        {
            //eventArgs.TargetPageType = typeof(GSM05000Numbering);
            //eventArgs.Parameter =
            //    R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(_GSM05000ViewModel.Entity);
            return Task.CompletedTask;
        }

        private Task InstanceApprovalTab(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM05000Approval);
            eventArgs.Parameter =
                R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(_GSM05000ViewModel.Entity);
            return Task.CompletedTask;
        }

    }
}
