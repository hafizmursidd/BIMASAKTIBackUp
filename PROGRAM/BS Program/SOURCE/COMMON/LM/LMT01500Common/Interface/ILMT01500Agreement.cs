using LMT01500Common.DTO._2._Agreement;
using LMT01500Common.Utilities;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMT01500Common.Interface
{
    public interface ILMT01500Agreement : R_IServiceCRUDBase<LMM01500AgreementDetailDTO>
    {
        IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCLeaseMode();
        IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCChargesMode();
    }
    
}
