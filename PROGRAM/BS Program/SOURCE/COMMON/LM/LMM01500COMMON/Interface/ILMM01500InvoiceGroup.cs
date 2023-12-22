using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON.Interface
{
    public interface ILMM01500InvoiceGroup : R_IServiceCRUDBase<LMM01500InvoiceGroupDetailDTO>
    {
        LMM01500PropertyListDTO GetPropertyList();
        IAsyncEnumerable<LMM01500InvoiceGroupDTO> GetInvoceGroupList();
    }
}
