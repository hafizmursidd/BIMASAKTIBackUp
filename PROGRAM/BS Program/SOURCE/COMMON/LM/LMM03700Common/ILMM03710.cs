﻿using LMM03700Common.DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace LMM03700Common
{
    public interface ILMM03710 : R_IServiceCRUDBase<TenantClassificationDTO>
    {
        IAsyncEnumerable<PropertyDTO> LMM03700GetPropertyData();
        IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassificationGroupList();
        IAsyncEnumerable<TenantClassificationDTO> GetTenantClassificationList();
        IAsyncEnumerable<TenantDTO> GetAssignedTenantList();
        IAsyncEnumerable<TenantDTO> GetAvailableTenantList();
        IAsyncEnumerable<TenantDTO> GetTenantToMoveList();
        TenantResultDumpDTO AssignTenant(TenantParamDTO poParam);
        TenantResultDumpDTO MoveTenant(TenantMoveParamDTO poParam);
    }
}