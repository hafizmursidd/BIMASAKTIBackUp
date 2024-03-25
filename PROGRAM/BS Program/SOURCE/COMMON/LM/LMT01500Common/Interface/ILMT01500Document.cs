using System.Collections.Generic;
using LMT01500Common.DTO._7._Document;
using LMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace LMT01500Common.Interface
{
    public interface ILMT01500Document : R_IServiceCRUDBase<LMT01500DocumentDetailDTO>
    {
        LMT01500DocumentHeaderDTO GetDocumentHeader(LMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<LMT01500DocumentListDTO> GetDocumentList();
        
    }
}