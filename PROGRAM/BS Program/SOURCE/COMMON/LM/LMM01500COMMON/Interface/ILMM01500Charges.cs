using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace LMM01500COMMON.Interface
{
    public interface ILMM01500Charges : R_IServiceCRUDBase<LMM01500ChargesDTO>
    {
        IAsyncEnumerable<LMM01500ChargesDTO> GetAllOtherChargerList();
    }
}