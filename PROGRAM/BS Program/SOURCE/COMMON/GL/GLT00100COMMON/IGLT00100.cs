using GLT00100Common.DTOs;
using GLT00100Common.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common
{
    public interface IGLT00100 : R_IServiceCRUDBase<GLT00100DTO>
    {
        IAsyncEnumerable<GLT00100JournalGridDTO> GetJournalList();
        IAsyncEnumerable<GLT00100JournalGridDetailDTO> GetAllJournalDetailList();
        GLT00100JournalGridDTO ProcessReversingJournal(GLT00100JournalGridDTO poData);
        GLT00100JournalGridDTO UndoReversingJournal(GLT00100JournalGridDTO poData);
        GLT00100JournalGridDTO JournalProcesStatus(GLT00100JournalGridDTO poData);

        #region INIT
        VAR_GSM_COMPANYDTO GetCompanyDTO();
        VAR_GL_SYSTEM_PARAMDTO GetSystemParam();
        VAR_USER_DEPARTMENT_LISTDTO GetDeptList();
        VAR_CCURRENT_PERIOD_START_DATEDTO GetCurrentPeriodStartDate(VAR_GL_SYSTEM_PARAMDTO poData);
        VAR_CSOFT_PERIOD_START_DATEDTO GetSoftPeriodStartDate(VAR_GL_SYSTEM_PARAMDTO poData);
        VAR_IUNDO_COMMIT_JRNDTO GetIOption();
        VAR_GSM_TRANSACTION_CODEDTO GetLincementLapproval();
        VAR_GSM_PERIODDTO GetPeriod();
        StatusListDTO GetStatusList();
        CurrencyCodeListDTO GetCurrencyCodeList();
        GetCenterListDTO GetCenterList();
        GSM_TRANSACTION_APPROVALDTO GetTransactionApproval();
        #endregion
    }
 
}
