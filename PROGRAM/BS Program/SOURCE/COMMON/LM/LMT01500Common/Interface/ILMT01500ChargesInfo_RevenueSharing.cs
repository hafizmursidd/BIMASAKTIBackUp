using System.Collections.Generic;
using LMT01500Common.DTO._4._Charges_Info;
using R_CommonFrontBackAPI;

namespace LMT01500Common.Interface
{
    public interface ILMT01500ChargesInfo_RevenueSharing : R_IServiceCRUDBase<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>
    {
        IAsyncEnumerable<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> GetRevenueSharingSchemeList();
        IAsyncEnumerable<LMT01500ChargesInfo_RevenueMinimumRentDTO> GetRevenueMinimumRentList();
    }
}