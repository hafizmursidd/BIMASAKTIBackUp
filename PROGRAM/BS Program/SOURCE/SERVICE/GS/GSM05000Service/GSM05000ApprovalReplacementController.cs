﻿using GSM05000Common.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05000Common.DTO;
using R_CommonFrontBackAPI;
using GSM05000Back;
using R_BackEnd;
using R_Common;

namespace GSM05000Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM05000ApprovalReplacementController : ControllerBase, IGSM05000ApprovalReplacement
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM05000ApprovalReplacementDTO> R_ServiceGetRecord(
            R_ServiceGetRecordParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
        {
            R_Exception loEx = new();
            R_ServiceGetRecordResultDTO<GSM05000ApprovalReplacementDTO> loRtn = new();

            try
            {
                var loCls = new GSM05000ApprovalReplacementCls();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO> R_ServiceSave(
            R_ServiceSaveParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
        {
            R_Exception loEx = new();
            R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO> loRtn = null;
            GSM05000ApprovalReplacementCls loCls;

            try
            {
                loCls = new GSM05000ApprovalReplacementCls();
                loRtn = new R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO>();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(
            R_ServiceDeleteParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
        {
            R_Exception loEx = new();
            R_ServiceDeleteResultDTO loRtn = new();
            GSM05000ApprovalReplacementCls loCls;

            try
            {
                loCls = new GSM05000ApprovalReplacementCls();

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream()
        {
            R_Exception loEx = new();
            IAsyncEnumerable<GSM05000ApprovalReplacementDTO> loRtn = null;
            List<GSM05000ApprovalReplacementDTO> loResult;
            GSM05000ParameterDb loDbPar;
            GSM05000ApprovalReplacementCls loCls;

            try
            {
                loDbPar = new GSM05000ParameterDb
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CDEPT_CODE),
                    CUSER_ID = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CUSER_ID),
                    CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
                };

                loCls = new GSM05000ApprovalReplacementCls();
                loResult = loCls.GSM05000GetApprovalReplacement(loDbPar);
                loRtn = GetStream(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #region "Helper ListStream Functions"

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
        {
            foreach (T item in poParameter)
            {
                yield return item;
            }
        }

        #endregion
    }
}
