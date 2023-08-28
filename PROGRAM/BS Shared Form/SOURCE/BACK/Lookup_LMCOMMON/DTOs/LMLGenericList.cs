using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_LMCOMMON.DTOs
{
    public class LMLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
