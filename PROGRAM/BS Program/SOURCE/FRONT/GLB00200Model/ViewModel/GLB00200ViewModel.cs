﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLB00200Common;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace GLB00200Model.ViewModel
{
    public class GLB00200ViewModel : R_ViewModel<GLB00200DTO>,R_IProcessProgressStatus
    {
        private GLB00200Model _modelGLB00200Model = new GLB00200Model();

        //List for Front
        public ObservableCollection<GLB00200DTO> ReversingJournalProcessList =
            new ObservableCollection<GLB00200DTO>();
        public ObservableCollection<GLB00200JournalDetailDTO> DetailReversingJournalList =
            new ObservableCollection<GLB00200JournalDetailDTO>();
        //List for Process
        public List<GLB00200DTO> loProcessReversingList = new List<GLB00200DTO>();
        //List for Convert
        public List<int> NO_Convert = new List<int>();

        public GLB00200InitalProcessDTO loGetInitialProcess = new GLB00200InitalProcessDTO();
        public int PeriodYear = 2023;// DateTime.Now.Year;
        public string PeriodMonth = DateTime.Now.Month.ToString("D2");
        public string lcSearchText = "";
        public List<GetMonthDTO> GetMonthList { get; set; }
        public GLB00200DTO CurrentReversingJournal = new GLB00200DTO();

        public bool ButtonEnable = false;

        public string COMPANYID;
        public string USERID;

        List<GLB00200DTO> InboxApprovaltBatchListSelected = new List<GLB00200DTO>();
        private int Var_Data_Count = 0;

        public string Message = "";
        public int Percentage = 0;

        public Action StateChangeAction { get; set; }
        
        public async Task GetInitialprocess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLB00200Model.GetInitialProcessAsyncModel();
                loGetInitialProcess = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task GetAllReversingJournalProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcPeriod = PeriodYear.ToString() + PeriodMonth;

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPERIOD, lcPeriod);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, lcSearchText);

                var loResult = await _modelGLB00200Model.GetReversingJournalProcessAsyncModel();
                ReversingJournalProcessList = new ObservableCollection<GLB00200DTO>(loResult.Data);

                if (ReversingJournalProcessList.Count > 0)
                {
                    ButtonEnable = true;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task GetDetail_ReversingJournal()
        {
            R_Exception loException = new R_Exception();
            try
            {
                GLB00200DTO loParam = new GLB00200DTO()
                {
                    CREC_ID = CurrentReversingJournal.CREC_ID
                };
                var Result =
                    await _modelGLB00200Model.GetDetail_ReversingJournalAsyncModel(loParam);
                DetailReversingJournalList = new ObservableCollection<GLB00200JournalDetailDTO>(Result.Data);
                ConvertBigIntToInt();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }

        public async Task ConvertBigIntToInt()
        {
            R_Exception loException = new R_Exception();
            try
            {
                foreach (var item in DetailReversingJournalList)
                {
                    int temp = Convert.ToInt32(item.INO);
                    item.NO_Convert = temp;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        }

        public async Task GetSelectedDataToReversingJournal()
        {
            R_Exception loException = new R_Exception();
            List<GLB00200DTO> tempDataSelected = new List<GLB00200DTO>();
            try
            {
                foreach (var item in loProcessReversingList)
                {
                    if (item.LSELECTED == true)
                    {
                        tempDataSelected.Add(item);
                    }
                }

                if (tempDataSelected.Count == 0)
                {
                    loException.Add(new Exception("Please select Reversing Journal to process!"));
                    goto EndBlock;
                }
                Var_Data_Count = tempDataSelected.Count;

                InboxApprovaltBatchListSelected = tempDataSelected;
                await ProcessDataSelected(COMPANYID, USERID);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        #region "Process"
        public async Task ProcessDataSelected(string COMPANYID, string USERID)
        {
            var loEx = new R_Exception();
            List<GLB00200DTO> loTemp = new List<GLB00200DTO>();
            loTemp = InboxApprovaltBatchListSelected;
            try
            {
                var loUserParameters = new List<R_KeyValue>();
                //Instantiate ProcessClient
                R_ProcessAndUploadClient loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GL",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlGL",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                R_BatchParameter loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = COMPANYID;
                loUploadPar.USER_ID = USERID;
                loUploadPar.UserParameters = loUserParameters;
                loUploadPar.ClassName = "GLB00200Back.GLB00200ProcessingCls";
                loUploadPar.BigObject = loTemp;

                await loCls.R_BatchProcess<List<GLB00200DTO>>(loUploadPar, loTemp.Count);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        #endregion

        #region ProgressBar

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Finish Processing Reversing Journal! ");
            }

            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                Message = "Process Completed But Fail";
            }
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            foreach (R_APICommonDTO.R_Error item in ex.ErrorList)
            {
                Message = string.Format($"{item.ErrDescp}");
            }
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            StateChangeAction();

            await Task.CompletedTask;
        }
        #endregion


    }
}