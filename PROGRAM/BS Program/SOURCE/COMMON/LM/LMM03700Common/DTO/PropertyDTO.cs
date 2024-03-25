using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM03700Common.DTO
{
    public class PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }
    }
    public class PropertyListDTO : R_APIResultBaseDTO
    {
        public List<PropertyDTO> Data { get; set; }
    }
}