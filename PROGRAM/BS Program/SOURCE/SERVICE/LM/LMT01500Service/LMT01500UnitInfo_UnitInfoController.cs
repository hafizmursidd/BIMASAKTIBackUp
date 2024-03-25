using LMT01500Back;
using LMT01500Common.Context;
using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.DTO._3._Unit_Info;
using LMT01500Common.Interface;
using LMT01500Common.Logs;
using LMT01500Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace LMT01500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMT01500UnitInfo_UnitInfoController : ControllerBase, ILMT01500UnitInfo_UnitInfo
    {
        private readonly LoggerLMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public LMT01500UnitInfo_UnitInfoController(ILogger<LMT01500UnitInfo_UnitInfoController> logger)
        {
            LoggerLMT01500.R_InitializeLogger(logger);
            _loggerLMT01500 = LoggerLMT01500.R_GetInstanceLogger();
            _activitySource = LMT01500Activity.R_InitializeAndGetActivitySource(nameof(LMT01500AgreementListController));
        }

        [HttpPost]
        public LMT01500UnitInfoHeaderDTO GetUnitInfoHeader(LMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetUnitInfoHeader);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            LMT01500GetHeaderParameterDTO? loDbParameter;
            LMT01500UnitInfoHeaderDTO? loReturn = null;
            LMT01500UnitInfo_UnitInfoCls loCls;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbParameter = poParameter;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500UnitInfo_UnitInfoCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetUnitInfoHeaderDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public IAsyncEnumerable<LMT01500UnitInfoUnitInfoListDTO> GetUnitInfoList()
        {
            string? lcMethod = nameof(GetUnitInfoList);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMT01500UtilitiesParameterDTO? loDbParameterInternal;
            LMT01500GetHeaderParameterDTO? loDbParameter;
            List<LMT01500UnitInfoUnitInfoListDTO> loRtnTmp;
            LMT01500UnitInfo_UnitInfoCls loCls;
            IAsyncEnumerable<LMT01500UnitInfoUnitInfoListDTO>? loReturn = null;
            LMT01500Utilities? loUtilities = null;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbParameterInternal = new();
                loDbParameter = new();

                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);

                _loggerLMT01500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameterInternal.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(loDbParameterInternal.CCOMPANY_ID))
                {
                    loDbParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID);
                    loDbParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE);
                    loDbParameter.CREF_NO = R_Utility.R_GetStreamingContext<string>(LMT01500GetHeaderParameterContextConstantDTO.CREF_NO);
                    loDbParameterInternal.CUSER_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMT01500.LogDebug("{@ObjectParameterInternal}", loDbParameterInternal);
                _loggerLMT01500.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _loggerLMT01500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMT01500.LogDebug("{@LMT01500UnitInfo_UnitInfoCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetUnitInfoListDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                _loggerLMT01500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMT01500.LogInfo(string.Format("initialization Utilities in Method {0}", lcMethod));
                loUtilities = new();

                _loggerLMT01500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = loUtilities.LMT01500GetListStream(loRtnTmp);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMT01500UnitInfoUnitInfoDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMT01500UnitInfoUnitInfoDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMT01500UnitInfoUnitInfoDetailDTO>();

            try
            {
                _loggerLMT01500.LogInfo(string.Format("Initialize the loCls in method {0}", lcMethod));
                var loCls = new LMT01500UnitInfo_UnitInfoCls();
                _loggerLMT01500.LogDebug("{@LMT01500UnitInfo_UnitInfoCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (!string.IsNullOrEmpty(poParameter.Entity.CCOMPANY_ID))
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT01500.LogDebug("{@ObjectParameter}", poParameter.Entity);


                _loggerLMT01500.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMT01500.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMT01500UnitInfoUnitInfoDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMT01500UnitInfoUnitInfoDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceSaveResultDTO<LMT01500UnitInfoUnitInfoDetailDTO> loReturn = new();
            try
            {
                _loggerLMT01500.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                LMT01500UnitInfo_UnitInfoCls? loCls = new();
                _loggerLMT01500.LogDebug("{@ObjectLMT01500UnitInfo_UnitInfoCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT01500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerLMT01500.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", lcMethod));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMT01500UnitInfoUnitInfoDetailDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception? loException = new R_Exception();
            R_ServiceDeleteResultDTO? loReturn = new();
            try
            {
                _loggerLMT01500.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                _loggerLMT01500.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerLMT01500.LogInfo(string.Format("Initialize the loCls object as a new instance of Cls in method {0}", lcMethod));
                LMT01500UnitInfo_UnitInfoCls? loCls = new();
                _loggerLMT01500.LogDebug("{@ObjectLMT01500UnitInfo_UnitInfoCls}", loCls);

                _loggerLMT01500.LogInfo(string.Format("Perform the delete operation using the R_Delete method of Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }
    }
}
