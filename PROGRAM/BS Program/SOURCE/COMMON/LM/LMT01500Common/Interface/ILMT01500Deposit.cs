using System.Collections.Generic;
using LMT01500Common.DTO._6._Deposit;
using LMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace LMT01500Common.Interface
{
    public interface ILMT01500Deposit : R_IServiceCRUDBase<LMT01500DepositDetailDTO>
    {
        LMT01500DepositHeaderDTO GetDepositHeader(LMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<LMT01500DepositListDTO> GetDepositList();
        
    }
}