using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.Utilities;
using System.Collections.Generic;

namespace LMT01500Common.Interface
{
    public interface ILMT01500AgreementList
    {
        IAsyncEnumerable<LMT01500AgreementListOriginalDTO> GetAgreementList();
        IAsyncEnumerable<LMT01500PropertyListDTO> GetPropertyList();
        IAsyncEnumerable<LMT01500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode();
        LMT01500ChangeStatusDTO GetChangeStatus(LMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCTransStatus();
        LMT01500ProcessResultDTO ProcessChangeStatus(LMT01500ChangeStatusParameterDTO poEntity);
    }
}
