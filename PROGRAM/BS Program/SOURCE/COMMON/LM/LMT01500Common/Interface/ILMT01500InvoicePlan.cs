using System.Collections.Generic;
using LMT01500Common.DTO._5._Invoice_Plan;
using LMT01500Common.Utilities;

namespace LMT01500Common.Interface
{
    public interface ILMT01500InvoicePlan
    {
        LMT01500InvoicePlanHeaderDTO GetInvoicePlanHeader(LMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<LMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeList();
        IAsyncEnumerable<LMT01500InvoicePlanListDTO> GetInvoicePlanList();
        
    }
}