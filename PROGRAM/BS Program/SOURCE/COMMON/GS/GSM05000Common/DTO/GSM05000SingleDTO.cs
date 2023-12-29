using R_APICommonDTO;

namespace GSM05000Common.DTO
{
    public class GSM05000SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}