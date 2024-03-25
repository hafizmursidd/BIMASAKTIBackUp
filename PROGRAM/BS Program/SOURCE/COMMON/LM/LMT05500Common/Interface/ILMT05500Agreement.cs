using System;
using System.Collections.Generic;
using System.Text;
using LMT05500Common.DTO;

namespace LMT05500Common.Interface
{
    public interface ILMT05500Agreement
    {
        IAsyncEnumerable<LMT05500AgreementDTO> AgreementListStream();
        IAsyncEnumerable<LMT05500PropertyDTO> GetPropertyListStream();
        IAsyncEnumerable<LMT05500UnitDTO> GetDepositUnitStream();
    }
}
