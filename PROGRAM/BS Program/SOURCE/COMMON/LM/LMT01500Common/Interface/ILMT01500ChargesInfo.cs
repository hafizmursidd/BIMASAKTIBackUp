using System.Collections.Generic;
using System.Threading.Tasks;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace LMT01500Common.Interface
{
    public interface ILMT01500ChargesInfo : R_IServiceCRUDBase<LMT01500ChargesInfoDetailDTO>
    {
        LMT01500ChargesInfoHeaderDTO GetChargesInfoHeader(LMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<LMT01500ChargesInfoListDTO> GetChargesInfoList();
        IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCFEE_METHOD();
        IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCINVOICE_PERIOD();
    }
}