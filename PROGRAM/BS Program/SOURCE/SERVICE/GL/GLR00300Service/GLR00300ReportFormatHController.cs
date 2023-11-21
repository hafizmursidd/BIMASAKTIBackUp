﻿using GLR00300Back;
using GLR00300Common.GLR00300Print;
using GLR00300Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseHeaderReportCOMMON;
using GLR00300Common.Logs;
using Microsoft.Extensions.Logging;
using GLR00300Service.DTOLogs;
using R_CommonFrontBackAPI.Log;

namespace GLR00300Service
{
    public class GLR00300ReportFormatHController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLR00300ParamDBToGetReportDTO _Parameter;
        private LoggerGLR00300 _loggerGLR00300Report;

        #region instantiate

        public GLR00300ReportFormatHController(ILogger<GLR00300ReportFormatHController> logger)
        {
            //Initial and Get instance
            LoggerGLR00300.R_InitializeLogger(logger);
            _loggerGLR00300Report = LoggerGLR00300.R_GetInstanceLogger();

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #endregion

        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLR00300AccountTrialBalanceH.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_Parameter));
            pcDataSourceName = "ResponseDataModel";
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }

        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO AllTrialBalanceReportPost(GLR00300ParamDBToGetReportDTO poParameter)
        {
            string lcMethodName = nameof(AllTrialBalanceReportPost);
            _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format H", lcMethodName));
            GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO> loCache = null;

            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _loggerGLR00300Report.LogInfo("Set GUID Param on method post");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo(string.Format("END method {0} on Format H", lcMethodName));

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllTrialBalanceReportGet(string pcGuid)
        {
            string lcMethodName = nameof(AllTrialBalanceReportGet);
            _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format H", lcMethodName));

            GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO> loResultGUID = null;
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            { 
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GLR00300ReportLogKeyDTO<GLR00300ParamDBToGetReportDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _Parameter = loResultGUID.poParam;

                _loggerGLR00300Report.LogInfo(string.Format("READ file report method {0}", lcMethodName));
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));


                //--------Program Old sebelum penambahan library log
                ////Get Parameter
                //_Parameter =
                //    R_NetCoreUtility.R_DeserializeObjectFromByte<GLR00300ParamDBToGetReportDTO>(
                //        R_DistributedCache.Cache.Get(pcGuid));
                //loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(),
                //    R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo(string.Format("END method {0} on Format H", lcMethodName));

            return loRtn;
        }

        #region Helper

        private GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO GenerateDataPrint(
            GLR00300ParamDBToGetReportDTO poParam)
        {
            _loggerGLR00300Report.LogInfo("START Method GenerateDataPrint on Controller");
            
            var loException = new R_Exception();
            GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO loRtn =
                new GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO();
            GLR00300Cls loCls = null;
            GLR00300AccountTrialBalanceResultFormat_EtoH_DTO loData = null;
            List<GLRR00300DataAccountTrialBalance> loConvertData = null;
            string lcPeriod;

            try
            {
                loCls = new GLR00300Cls();
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _loggerGLR00300Report.LogInfo("Call Method GetAllTrialBalanceReportData");
                var loCollectionFromDb = loCls.GetAllTrialBalanceReportData(poParam);
                loConvertData = FromRaw_To_Display(loCollectionFromDb);

                //CONDITIONAL IF DATA FROM USER TO DB NO RESULT (NULL)
                if (loCollectionFromDb.Count > 0)
                {
                    GLR00300_DataDetail_AccountTrialBalance getFirstDataToHeader = loCollectionFromDb.FirstOrDefault();

                    loData = new GLR00300AccountTrialBalanceResultFormat_EtoH_DTO()
                    {
                        Title = "Account Trial Balance",
                        Header = new GLR00300HeaderAccountTrialBalanceDTO()
                        {
                            CPERIOD = getFirstDataToHeader.CPERIOD_NAME,
                            CFROM_ACCOUNT_NO = getFirstDataToHeader.CFROM_ACCOUNT_NO,
                            CTO_ACCOUNT_NO = getFirstDataToHeader.CTO_ACCOUNT_NO,
                            CFROM_CENTER_CODE = getFirstDataToHeader.CFROM_CENTER_CODE,
                            CTO_CENTER_CODE = getFirstDataToHeader.CTO_CENTER_CODE,
                            CTB_TYPE_NAME = getFirstDataToHeader.CTB_TYPE_NAME,
                            CCURRENCY = getFirstDataToHeader.CCURRENCY,
                            CJOURNAL_ADJ_MODE_NAME = getFirstDataToHeader.CJOURNAL_ADJ_MODE_NAME,
                            CPRINT_METHOD_NAME = getFirstDataToHeader.CPRINT_METHOD_NAME,
                            CBUDGET_NO = getFirstDataToHeader.CBUDGET_NO
                        },
                        Column = new AccountTrialBalanceColumnDTO()
                    };
                }
                else
                {
                    lcPeriod = poParam.CYEAR + "-" + poParam.CTO_PERIOD_NO;
                    loData = new GLR00300AccountTrialBalanceResultFormat_EtoH_DTO()
                    {
                        Title = "Account Trial Balance",
                        Header = new GLR00300HeaderAccountTrialBalanceDTO()
                        {
                            CPERIOD = lcPeriod,
                            CFROM_ACCOUNT_NO = poParam.CFROM_ACCOUNT_NO,
                            CTO_ACCOUNT_NO = poParam.CTO_ACCOUNT_NO,
                            CFROM_CENTER_CODE = poParam.CFROM_CENTER_CODE,
                            CTO_CENTER_CODE = poParam.CTO_CENTER_CODE,
                            CTB_TYPE_NAME = "Audit",
                            CCURRENCY = "",
                            CJOURNAL_ADJ_MODE_NAME = "Merged",
                            CPRINT_METHOD_NAME = "",
                            CBUDGET_NO = poParam.CBUDGET_NO,
                        },
                        Column = new AccountTrialBalanceColumnDTO()
                    };
                }
                _loggerGLR00300Report.LogInfo("Set BaseHeader Report");
                //Assign raw data to Data list display
                loData.Data = loConvertData;

                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = "003",
                    CPRINT_NAME = "Account Trial Balance",
                    CUSER_ID = poParam.CUSER_ID,
                };
                _loggerGLR00300Report.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.GLR00300AccountTrialBalanceResult_FormatEtoH_DataFormat = loData;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo("END Method GenerateDataPrint on Controller");

            return loRtn;
        }

        #endregion

        private List<GLRR00300DataAccountTrialBalance> FromRaw_To_Display(
            List<GLR00300_DataDetail_AccountTrialBalance> poCollectionDataRaw)
        {
            _loggerGLR00300Report.LogInfo("START method for convert data to display");
            
            var loException = new R_Exception();
            List<GLRR00300DataAccountTrialBalance> loReturn = null;
            try
            {
                loReturn = new List<GLRR00300DataAccountTrialBalance>();

                loReturn = poCollectionDataRaw
                    .GroupBy(data => new
                    {
                        data.CGLACCOUNT_NO,
                        data.CGLACCOUNT_NAME,
                        data.CDBCR,
                        data.CBSIS,
                    }).Select(dataDetail => new GLRR00300DataAccountTrialBalance
                    {
                        CGLACCOUNT_NO = dataDetail.Key.CGLACCOUNT_NO,
                        CGLACCOUNT_NAME = dataDetail.Key.CGLACCOUNT_NAME,
                        CDBCR = dataDetail.Key.CDBCR,
                        CBSIS = dataDetail.Key.CBSIS,
                        DataDetail = dataDetail.Select
                        (itemDetail => new GLRR00300DataDetailAccountTrialBalance
                        {
                            CCENTER = itemDetail.CCENTER,
                            NBEGIN_BALANCE = itemDetail.NBEGIN_BALANCE,
                            NCREDIT = itemDetail.NCREDIT,
                            NDEBIT = itemDetail.NDEBIT,
                            NDEBIT_ADJ = itemDetail.NDEBIT_ADJ,
                            NCREDIT_ADJ = itemDetail.NCREDIT_ADJ,
                            NEND_BALANCE = itemDetail.NEND_BALANCE,
                            NBUDGET = itemDetail.NBUDGET,
                        }).ToList()
                    }).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300Report.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGLR00300Report.LogInfo("END method for convert data to display");

            return loReturn;

        }
    }
}