using GLR00300Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;

namespace GLR00300Service;

    [ApiController]
    [Route("api/[controller]/[action]")]
public class GLR00300ReportController : ControllerBase
{
    private R_ReportFastReportBackClass _ReportCls;
    private GLR00300ParamDBToGetReportDTO _Parameter;

    #region instantiate
    //public GLR00300ReportController()
    //{
    //    _ReportCls = new R_ReportFastReportBackClass();
    //    _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
    //    //_ReportCls.R_SetMainParameter += _ReportCls_R_SetMainParameter;
    //    _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
    //    _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
    //}
    #endregion

    #region Event Handler
    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
    {
        pcFileTemplate = "Reports\\GLR00300AccountTrialBalanceFormatA.frx";
    }

    //private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    //{
    //    poData.Add(GenerateDataPrint(_Parameter));
    //    pcDataSourceName = "ResponseDataModel";
    //}

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

    #region Helper
    //private LMM01020ResultWithBaseHeaderPrintDTO GenerateDataPrint(GLR00300ParamDBToGetReportDTO poParam)
    //{
    //    var loEx = new R_Exception();
    //    LMM01020ResultWithBaseHeaderPrintDTO loRtn = new LMM01020ResultWithBaseHeaderPrintDTO();

    //    try
    //    {
    //        LMM01020ResultPrintDTO loData = new LMM01020ResultPrintDTO()
    //        {
    //            Title = "Water and Gas",
    //            Header = $"{poParam.CPROPERTY_ID} - {poParam.CPROPERTY_NAME}",
    //        };

    //        var loCls = new LMM01020Cls();

    //        poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
    //        poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

    //        var loHeaderParam = R_Utility.R_ConvertObjectToObject<LMM01020PrintParamDTO, LMM01020DTO>(poParam);
    //        var loDetailParam = R_Utility.R_ConvertObjectToObject<LMM01020PrintParamDTO, LMM01021DTO>(poParam);

    //        var loHeaderCollection = loCls.GetHDReportRateWG(loHeaderParam);
    //        var loDetailCollection = loCls.GetDetailReportRateWG(loDetailParam);

    //        loHeaderCollection.CRATE_WG_LIST = new List<LMM01021DTO>();
    //        loHeaderCollection.CRATE_WG_LIST.AddRange(loDetailCollection);

    //        loData.HeaderData = loHeaderCollection;

    //        var loParam = new BaseHeaderDTO()
    //        {
    //            CCOMPANY_NAME = "PT Realta Chackradarma",
    //            CPRINT_CODE = "003",
    //            CPRINT_NAME = "Water and Gas",
    //            CUSER_ID = "FMC",
    //        };

    //        Assembly loAsm = Assembly.Load("BIMASAKTI_LM_API");

    //        var lcResourceFile = "BIMASAKTI_LM_API.Image.CompanyLogo.png";

    //        using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
    //        {

    //            var ms = new MemoryStream();
    //            resFilestream.CopyTo(ms);
    //            var bytes = ms.ToArray();

    //            loParam.BLOGO_COMPANY = bytes;
    //        }

    //        loRtn.BaseHeaderData = loParam;
    //        loRtn.RateWG = loData;

    //    }
    //    catch (Exception ex)
    //    {
    //        loEx.Add(ex);
    //    }

    //    loEx.ThrowExceptionIfErrors();

    //    return loRtn;
  //  }
    #endregion


}