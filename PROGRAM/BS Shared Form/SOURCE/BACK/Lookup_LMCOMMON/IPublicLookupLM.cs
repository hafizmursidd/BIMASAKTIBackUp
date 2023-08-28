using System;
using System.Collections.Generic;
using Lookup_LMCOMMON.DTOs;

namespace Lookup_LMCOMMON
{
    public interface IPublicLookupLM
    {
        IAsyncEnumerable<LML00100DTO> LML00100GetSalesTaxList(LML00100ParameterDTO poParameter);
        IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList(LML00200ParameterDTO poParameter);
        IAsyncEnumerable<LML00300DTO> LML00300SupervisorList(LML00300ParameterDTO poParameter);
        IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList(LML00400ParameterDTO poParameter);
    }
}
