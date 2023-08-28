using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GSM04500Back;
using GSM04500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;

namespace GSM04500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04500UploadTemplateController : ControllerBase, IGSM04500UploadTemplate 
    {
        [HttpPost]
        public GSM04500UploadFileDTO DownloadTemplateFile()
        {
            var loEx = new R_Exception();
            var loRtn = new GSM04500UploadFileDTO();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Journal Group.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM04500ListDTO GetJournalGroupUploadList()
        {
            var loEx = new R_Exception();
            GSM04500ListDTO loRtn = null;
            var loParameter = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04500ValidateUploadTemplateCls();
                loRtn = new GSM04500ListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CJRNGRP_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CJRNGRP_TYPE);

                loRtn = loCls.GetUploadJournalGroupList(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM04500ListUploadErrorValidateDTO GetErrorProcess()
        {
            var loEx = new R_Exception();
            GSM04500ListUploadErrorValidateDTO loRtn = null;

            try
            {
                var lcKeyGuid = R_Utility.R_GetContext<string>("KeyGuid");

                var loCls = new GSM04500ValidateUploadTemplateCls();
                loRtn = new GSM04500ListUploadErrorValidateDTO();

                var loResult = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);
                     loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
