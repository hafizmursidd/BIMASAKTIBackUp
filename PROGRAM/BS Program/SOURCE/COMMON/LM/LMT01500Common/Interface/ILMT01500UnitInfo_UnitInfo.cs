using System.Collections.Generic;
using LMT01500Common.DTO._3._Unit_Info;
using LMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace LMT01500Common.Interface
{
    public interface ILMT01500UnitInfo_UnitInfo : R_IServiceCRUDBase<LMT01500UnitInfoUnitInfoDetailDTO>
    {
        LMT01500UnitInfoHeaderDTO GetUnitInfoHeader(LMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<LMT01500UnitInfoUnitInfoListDTO> GetUnitInfoList();
    }
}