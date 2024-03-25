﻿using GLR00300Common;
using GLR00300Common.GLR00300Print;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;
using BaseHeaderReportCOMMON;
using GLR00300Back;
using GLR00300Common.Logs;
using GLR00300Service.DTOLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using R_CommonFrontBackAPI;
using R_Cache;
using Microsoft.Extensions.Logging;
using R_CommonFrontBackAPI.Log;
using System.Diagnostics;

namespace GLR00300Service;

public class GLR00300ReportFormatAController : R_ReportControllerBase
{
    private R_ReportFastReportBackClass _ReportCls;
    private GLR00300ParamDBToGetReportDTO _Parameter;
    private LoggerGLR00300 _loggerGLR00300Report;
    private readonly ActivitySource _activitySource;
    #region instantiate

    public GLR00300ReportFormatAController(ILogger<GLR00300ReportFormatAController> logger)
    {
        //Initial and Get instance
        LoggerGLR00300.R_InitializeLogger(logger);
        _loggerGLR00300Report = LoggerGLR00300.R_GetInstanceLogger();
        _activitySource = GLR00300Activity.R_InitializeAndGetActivitySource(nameof(GLR00300ReportFormatAController));

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }

    #endregion

    #region Event Handler

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
    {
        pcFileTemplate = System.IO.Path.Combine("Reports", "GLR00300AccountTrialBalanceA.frx");
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
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerGLR00300Report.LogInfo( string.Format("START method {0} on Format A", lcMethodName));

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
        _loggerGLR00300Report.LogInfo(string.Format("END method {0} on Format A", lcMethodName));

        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult AllTrialBalanceReportGet(string pcGuid)
    {
        string lcMethodName = nameof(AllTrialBalanceReportGet);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format A", lcMethodName));

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
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerGLR00300Report.LogError(loException);
        }
        loException.ThrowExceptionIfErrors();
        _loggerGLR00300Report.LogInfo(string.Format("END method {0} on Format A", lcMethodName));

        return loRtn;
    }


    #region Helper

    private GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO GenerateDataPrint(GLR00300ParamDBToGetReportDTO poParam)
    {
        string lcMethodName = nameof(GenerateDataPrint);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format A", lcMethodName));
        
        var loException = new R_Exception();
        GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO();

        GLR00300Cls loCls = null;
        GLR00300AccountTrialBalanceResultFormat_AtoD_DTO loData = null;
        List<GLR00300DataAccountTrialBalanceAD> loConvertData = null;
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
                GLR00300_DataDetail_AccountTrialBalance getFirstDataForHeader = loCollectionFromDb.FirstOrDefault();
                loData = new GLR00300AccountTrialBalanceResultFormat_AtoD_DTO()
                {
                    Title = "Account Trial Balance",
                    Header = new GLR00300HeaderAccountTrialBalanceDTO()
                    {
                        CPERIOD = getFirstDataForHeader.CPERIOD_NAME,
                        CFROM_ACCOUNT_NO = getFirstDataForHeader.CFROM_ACCOUNT_NO,
                        CTO_ACCOUNT_NO = getFirstDataForHeader.CTO_ACCOUNT_NO,
                        CFROM_CENTER_CODE = getFirstDataForHeader.CFROM_CENTER_CODE,
                        CTO_CENTER_CODE = getFirstDataForHeader.CTO_CENTER_CODE,
                        CTB_TYPE_NAME = getFirstDataForHeader.CTB_TYPE_NAME,
                        CCURRENCY = getFirstDataForHeader.CCURRENCY,
                        CJOURNAL_ADJ_MODE_NAME = getFirstDataForHeader.CJOURNAL_ADJ_MODE_NAME,
                        CPRINT_METHOD_NAME = getFirstDataForHeader.CPRINT_METHOD_NAME,
                        CBUDGET_NO = getFirstDataForHeader.CBUDGET_NO,
                    },
                    Column = new AccountTrialBalanceColumnDTO()
                };

            }
            else
            {
                lcPeriod = poParam.CYEAR + "-" + poParam.CTO_PERIOD_NO;
                loData = new GLR00300AccountTrialBalanceResultFormat_AtoD_DTO()
                {
                    Title = "Account Trial Balance",
                    Header = new GLR00300HeaderAccountTrialBalanceDTO()
                    {
                        CPERIOD = lcPeriod,
                        CFROM_ACCOUNT_NO = poParam.CFROM_ACCOUNT_NO,
                        CTO_ACCOUNT_NO = poParam.CTO_ACCOUNT_NO,
                        CFROM_CENTER_CODE = poParam.CFROM_CENTER_CODE,
                        CTO_CENTER_CODE = poParam.CTO_CENTER_CODE,
                        CTB_TYPE_NAME = "Normal",
                        CCURRENCY = "",
                        CJOURNAL_ADJ_MODE_NAME = "Split",
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
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "003",
                CPRINT_NAME = "Account Trial Balance",
                CUSER_ID = poParam.CUSER_ID,
            };
            _loggerGLR00300Report.LogInfo("Set Data Report");
            loRtn.BaseHeaderData = loParam;
            loRtn.GLR00300AccountTrialBalanceResult_FormatAtoD_DataFormat = loData;
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

    private List<GLR00300DataAccountTrialBalanceAD> FromRaw_To_Display(List<GLR00300_DataDetail_AccountTrialBalance> poCollectionDataRaw)
    {
        string lcMethodName = "Convert data to display";
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerGLR00300Report.LogInfo(string.Format("START method {0} on Format A", lcMethodName));

        var loException = new R_Exception();
        List<GLR00300DataAccountTrialBalanceAD> loReturn = null;
        try
        {
            loReturn = new List<GLR00300DataAccountTrialBalanceAD>();

            loReturn = poCollectionDataRaw.Select(item
                => new GLR00300DataAccountTrialBalanceAD
                {
                    CGLACCOUNT_NO = item.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = item.CGLACCOUNT_NAME,
                    CDBCR = item.CDBCR,
                    CBSIS = item.CBSIS,
                    NBEGIN_BALANCE = item.NBEGIN_BALANCE,
                    NDEBIT = item.NDEBIT,
                    NCREDIT = item.NCREDIT,
                    NDEBIT_ADJ = item.NDEBIT_ADJ,
                    NCREDIT_ADJ = item.NCREDIT_ADJ,
                    NEND_BALANCE = item.NCREDIT_ADJ,
                    NBUDGET = item.NBUDGET
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

        #endregion


    }

}
