using System;
using System.Collections.Generic;
using Lookup_LMCOMMON.DTOs;

namespace Lookup_LMCOMMON
{
    public interface IPublicLookupLM
    {
        IAsyncEnumerable<LML00100DTO> LML00100GetSalesTaxList();
        IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList();
        IAsyncEnumerable<LML00300DTO> LML00300SupervisorList();
        IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList();
        IAsyncEnumerable<LML00500DTO> LML00500SalesmanList();
        IAsyncEnumerable<LML00600DTO> LML00600TenantList();
        IAsyncEnumerable<LML00700DTO> LML00700DiscountList();
    }
}
