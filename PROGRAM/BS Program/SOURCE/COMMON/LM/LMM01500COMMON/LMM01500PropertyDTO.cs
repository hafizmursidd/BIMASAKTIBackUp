using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public class LMM01500PropertyDTO
    {
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }
    }
    public class LMM01500PropertyListDTO : R_APIResultBaseDTO
    {
        public List<LMM01500PropertyDTO> Data { get; set; }
    }
}
