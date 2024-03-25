using System.Collections.Generic;
using LMM03700Common.DTO;
using R_CommonFrontBackAPI;

namespace LMM03700Common
{
    public interface ILMM03700 : R_IServiceCRUDBase<TenantClassificationGroupDTO>
    {
        IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList();
    }
}