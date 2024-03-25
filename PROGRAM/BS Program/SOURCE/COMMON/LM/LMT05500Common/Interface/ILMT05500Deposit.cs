using System.Collections.Generic;
using LMT05500Common.DTO;
using R_CommonFrontBackAPI;

namespace LMT05500Common.Interface
{
    public interface ILMT05500Deposit : R_IServiceCRUDBase<LMT05500DepositInfoDTO>
    {
        LMT05500DepositHeaderDTO DepositHeader();
        IAsyncEnumerable<LMT05500DepositListDTO> DepositListStream();
        IAsyncEnumerable<LMT05500DepositDetailListDTO> DepositDetailListStream();
    }
}