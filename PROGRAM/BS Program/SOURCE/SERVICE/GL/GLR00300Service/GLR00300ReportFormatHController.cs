using BaseHeaderReportCommon.BaseHeader;
using GLR00300Back;
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

namespace GLR00300Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLR00300ReportFormatHController
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLR00300ParamDBToGetReportDTO _Parameter;

        #region instantiate
        public GLR00300ReportFormatHController()
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
            pcFileTemplate = "Reports\\GLR00300AccountTrialBalanceFormatH.frx";
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
        public R_DownloadFileResultDTO AllTrialBalanceReportPostFormatH(GLR00300ParamDBToGetReportDTO poParameter)
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
        public FileStreamResult AllTrialBalanceReportGetFormatH(string pcGuid)
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
        private GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO GenerateDataPrint(GLR00300ParamDBToGetReportDTO poParam)
        {
            var loEx = new R_Exception();
            GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO();
            GLR00300Cls loCls = null;
            GLR00300AccountTrialBalanceResultFormat_EtoH_DTO loData = null;
            List<GLRR00300DataAccountTrialBalance> loConvertData = null;
            string lcPeriod;

            try
            {
                loCls = new GLR00300Cls();
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

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

                //Assign raw data to Data list display
                loData.Data = loConvertData;

                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = "003",
                    CPRINT_NAME = "Account Trial Balance",
                    CUSER_ID = "HMC",
                };

                loRtn.BaseHeaderData = loParam;
                loRtn.GLR00300AccountTrialBalanceResult_FormatEtoH_DataFormat = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

        private List<GLRR00300DataAccountTrialBalance> FromRaw_To_Display(List<GLR00300_DataDetail_AccountTrialBalance> poCollectionDataRaw)
        {
            var loEx = new R_Exception();
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
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loReturn;

        }
    }
}
