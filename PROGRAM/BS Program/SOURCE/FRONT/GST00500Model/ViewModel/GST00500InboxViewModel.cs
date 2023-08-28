using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System.Linq;

namespace GST00500Model.ViewModel
{
    public class GST00500InboxViewModel : R_ViewModel<GST00500DTO>, R_IProcessProgressStatus
    {
        private GST00500InboxModel _modelGST00500 = new GST00500InboxModel();
        public ObservableCollection<GST00500DTO> InboxTransactionList = new ObservableCollection<GST00500DTO>();
        public GST00500UserNameDTO UserName = new GST00500UserNameDTO();

        public List<GST00500DTO> loInboxApprovaltBatchList = new List<GST00500DTO>();
        public List<GST00500RejectDTO> ReasonOfRejectList = new List<GST00500RejectDTO>();
        public GST00500RejectTransactionDTO ParamRejectTransactionStatus = new GST00500RejectTransactionDTO();

        public List<GST00500ApprovalTransactionDTO> GetListErrorTransaction = new List<GST00500ApprovalTransactionDTO>();

        public bool BtnReject = true;
        public bool BtnApprove = true;

        public string CCOMPANYID = "";
        public string CUSERID = "";

        public string Message = "";
        public int Percentage = 0;

        public string GUID_ID = null;

        public string ResultSuccesTransaction = "";

        public async Task GetAllInboxTransaction()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500.GetInboxListAsyncModel();
                InboxTransactionList = new ObservableCollection<GST00500DTO>(loResult);

                if (InboxTransactionList.Count < 1)
                {
                    BtnReject = false;
                    BtnApprove = false;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task GetUserName()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500.GetUserNameAsyncModel();
                UserName = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetReasonRejectTransaction()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500.GetReasonRejectListAsyncModel();
                ReasonOfRejectList = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        #region Seprated Data Selected


        public async Task GetSelectedDataToSaveApproval()
        {
            R_Exception loException = new R_Exception();
            List<GST00500DTO> loInboxApprovaltBatchListSelected = new List<GST00500DTO>();

            try
            {
                foreach (GST00500DTO item in loInboxApprovaltBatchList)
                {
                    if (item.LSELECTED == true)
                    {
                        item.VAR_SELECTED = 1;
                        loInboxApprovaltBatchListSelected.Add(item);
                    }
                }
                await ProcessApproveTransaction(loInboxApprovaltBatchListSelected);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedDataToRejectApproval()
        {
            R_Exception loException = new R_Exception();
            List<GST00500DTO> loInboxRejectListSelected = new List<GST00500DTO>();

            try
            {
                foreach (GST00500DTO item in loInboxApprovaltBatchList)
                {
                    if (item.LSELECTED == true)
                    {
                        item.VAR_SELECTED = 1;
                        loInboxRejectListSelected.Add(item);
                    }
                }
                await ProcessRejectTransaction(loInboxRejectListSelected);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        //To know if there is any data selected
        public bool IsDataSelectedExist()
        {
            R_Exception loException = new R_Exception();
            bool isSelected = false;
            try
            {
                var TempListToCheck = InboxTransactionList.ToList();
                isSelected = TempListToCheck.Any(item => item.LSELECTED == true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return isSelected;
        }
        #endregion
        #region Approve
        public async Task ProcessApproveTransaction(List<GST00500DTO> loInboxApprovaltBatchListSelected)
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParameters;

            try
            {
                loUserParameters = new List<R_KeyValue>();

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = CCOMPANYID;
                loUploadPar.USER_ID = CUSERID;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "GST00500Back.GST00500ProcessApproveCls";
                loUploadPar.BigObject = loInboxApprovaltBatchListSelected;

                GUID_ID = await loCls.R_BatchProcess<List<GST00500DTO>>(loUploadPar, loInboxApprovaltBatchListSelected.Count);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        #endregion

        #region Reject
        //Proses Transaction
        public async Task ProcessRejectTransaction(List<GST00500DTO> loInboxRejectBatchListSelected)
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParameters;

            try
            {
                loUserParameters = new List<R_KeyValue>();
                loUserParameters.Add(new R_KeyValue() { Key = ContextConstant.CREASON_CODE, Value = ParamRejectTransactionStatus.CREASON_CODE });
                loUserParameters.Add(new R_KeyValue() { Key = ContextConstant.TNOTES, Value = ParamRejectTransactionStatus.TNOTES });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = CCOMPANYID;
                loUploadPar.USER_ID = CUSERID;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "GST00500Back.GST00500ProcessRejectCls";
                loUploadPar.BigObject = loInboxRejectBatchListSelected;

                GUID_ID = await loCls.R_BatchProcess<List<GST00500DTO>>(loUploadPar, loInboxRejectBatchListSelected.Count);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        #endregion



        #region GET_ERROR
        private async Task GetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            GST00500ParameterDBDTO loParameter = new GST00500ParameterDBDTO()
            {
                GUID_ID = pcKeyGuid
            };

            try
            {
                var loError = await _modelGST00500.GetErroListAsync(loParameter);
                GetListErrorTransaction = loError.Data;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region ProgressStatus

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {

            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Finish Processing Reversing Journal!” ");
            }

            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
                await GetError(pcKeyGuid);
            }
        }

        public Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            return Task.CompletedTask;
        }

        public Task ReportProgress(int pnProgress, string pcStatus)
        {
            Console.WriteLine($"Step {pnProgress} with status {pcStatus}");
            return Task.CompletedTask;
        }
        #endregion

    }
}
