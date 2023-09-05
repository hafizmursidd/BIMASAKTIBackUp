using GLR00300Common;
using GLR00300Common.GLR00300Print;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;
using GLR00300Back;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using R_CommonFrontBackAPI;
using R_Cache;

namespace GLR00300Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLR00300ReportController : ControllerBase
{
    private R_ReportFastReportBackClass _ReportCls;
    private GLR00300ParamDBToGetReportDTO _Parameter;

    #region instantiate
    public GLR00300ReportController()
    {
        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
        _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    }
    #endregion

    #region Event Handler
    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
    {
        pcFileTemplate = "Reports\\GLR00300AccountTrialBalanceFormatA.frx";
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GenerateDataPrint(_Parameter));
        pcDataSourceName = "ResponseDataModel";
    }

    //private void _ReportCls_R_SetMainParameter(ref Dictionary<string, object> poParameters)
    //{
    //    poParameters.Add("Parameter1", "Ini Parameter1");
    //    poParameters.Add("Parameter2", "Ini Parameter2");
    //}

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
        R_Exception loException = new R_Exception();
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(poParameter));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult AllTrialBalanceReportGet(string pcGuid)
    {
        R_Exception loException = new R_Exception();
        FileStreamResult loRtn = null;
        try
        {
            //Get Parameter
            _Parameter = R_NetCoreUtility.R_DeserializeObjectFromByte<GLR00300ParamDBToGetReportDTO>(R_DistributedCache.Cache.Get(pcGuid));
            loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        loException.ThrowExceptionIfErrors();

        return loRtn;
    }


    #region Helper
    private GLR00300AccountTrialBalanceResultWithBaseHeaderDTO GenerateDataPrint(GLR00300ParamDBToGetReportDTO poParam)
    {
        var loEx = new R_Exception();
        GLR00300AccountTrialBalanceResultWithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResultWithBaseHeaderDTO();

        try
        {
            GLR00300AccountTrialBalanceResultDTO loData = new GLR00300AccountTrialBalanceResultDTO()
            {
                Title = "Account Trial Balance",
                Header = new GLR00300HeaderAccountTrialBalanceDTO()
                {
                    CPERIOD = poParam.CPERIOD_NAME,
                    CFROM_ACCOUNT_NO = poParam.CFROM_ACCOUNT_NO,
                    CTO_ACCOUNT_NO = poParam.CTO_ACCOUNT_NO,
                    CFROM_CENTER_CODE = poParam.CFROM_CENTER_CODE,
                    CTO_CENTER_CODE = poParam.CTO_CENTER_CODE,
                    CTB_TYPE_NAME = poParam.CTB_TYPE_NAME,
                    CCURRENCY = poParam.CCURRENCY_TYPE_CODE,
                    CJOURNAL_ADJ_MODE_NAME = poParam.CJOURNAL_ADJ_MODE_NAME,
                    CPRINT_METHOD_NAME = poParam.CPRINT_METHOD_NAME,
                    CBUDGET_NO = poParam.CBUDGET_NO
                },
                Column = new AccountTrialBalanceColumnDTO()
            };


            GLR00300ParamDBToGetReportDTO poParam2 = new GLR00300ParamDBToGetReportDTO()
            {

            CCOMPANY_ID = "RCD",
            CUSER_ID = "hmc",
            CTB_TYPE_NAME = "N",
            CJOURNAL_ADJ_MODE_NAME = "S",
            CCURRENCY_TYPE_CODE = "L",
            CFROM_ACCOUNT_NO = "15.10.0001",
            CTO_ACCOUNT_NO = "15.10.9999",
            CFROM_CENTER_CODE = "MMKT",
            CTO_CENTER_CODE= "MMKT",
            CYEAR = "2023",
            CFROM_PERIOD_NO = "03",
            CTO_PERIOD_NO= "03",
            CPRINT_METHOD_CODE = "00",
            CPRINT_METHOD_NAME = "ZZ",
            CBUDGET_NO = "",
            CLANGUAGE_ID = "en"
            };

            var loCls = new GLR00300Cls();

            //poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            //poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            var loCollection = loCls.GetAllTrialBalanceReportData(poParam2);


            //


            loData.DataAccountTrialBalance = loCollection;

            //  Assembly loAsm = Assembly.Load("BIMASAKTI_GL_API");

            loRtn.GLR00300AccountTrialBalanceResult = loData;

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    #endregion


}