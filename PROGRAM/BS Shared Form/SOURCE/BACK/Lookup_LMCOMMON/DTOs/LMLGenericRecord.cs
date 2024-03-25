using R_APICommonDTO;

namespace Lookup_LMCOMMON.DTOs
{
    public class LMLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
