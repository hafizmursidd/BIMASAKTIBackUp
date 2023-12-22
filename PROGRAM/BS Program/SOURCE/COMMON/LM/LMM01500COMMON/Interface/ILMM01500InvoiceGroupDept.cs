using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace LMM01500COMMON.Interface
{
    public interface ILMM01500InvoiceGroupDept : R_IServiceCRUDBase<LMM01500InvoiceGrpDeptDetailDTO>
    {
        IAsyncEnumerable<LMM01500InvoiceGrpDeptDTO> GetInvoiceGroupDeptList();
    }
}