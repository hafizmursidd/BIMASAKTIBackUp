using System.Collections.Generic;
using R_APICommonDTO;

namespace LMT05500Common.DTO
{
    public class LMT05500GenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}