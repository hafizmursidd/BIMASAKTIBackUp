using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMM06000Common
{
    public class LMM06000PeriodDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }

    public class LMM06000PeriodListDTO : R_APIResultBaseDTO
    {
        public List<LMM06000PeriodDTO> Data { get; set; }
    }
}
