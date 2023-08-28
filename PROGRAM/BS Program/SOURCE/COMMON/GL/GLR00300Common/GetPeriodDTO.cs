using R_APICommonDTO;
using System.Collections.Generic;

namespace GLR00300Common
{
    public class GetPeriodDTO
    {
        public string Id { get; set; }
    }
    public class GetPeriodListDTO : R_APIResultBaseDTO
    {
        public List<GetPeriodDTO> Data { get; set; }
    }
}