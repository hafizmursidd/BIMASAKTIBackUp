using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public interface IGSM04500UploadTemplate
    {
        GSM04500UploadFileDTO DownloadTemplateFile();
        GSM04500ListDTO GetJournalGroupUploadList();
        GSM04500ListUploadErrorValidateDTO GetErrorProcess();
    }
}
