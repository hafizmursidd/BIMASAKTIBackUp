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
    public class GSM04500UploadTemplateController : ControllerBase, IGSM04500Template 
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

    }
}
