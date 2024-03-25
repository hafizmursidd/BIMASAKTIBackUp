using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMT01500Common.DTO._3._Unit_Info;
using LMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace LMT01500Common.Interface
{
    public interface ILMT01500UnitInfo_Utilities : R_IServiceCRUDBase<LMT01500UnitInfoUnit_UtilitiesDetailDTO>
    {
        IAsyncEnumerable<LMT01500UnitInfoUnit_UtilitiesListDTO> GetUnitInfoList();
        IAsyncEnumerable<LMT01500ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE();
        IAsyncEnumerable<LMT01500ComboBoxStartInvoicePeriodDTO> GetComboBoxDataCSTART_INV_PRD();
    }
}