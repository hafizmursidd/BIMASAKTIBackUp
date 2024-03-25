using R_APICommonDTO;

namespace LMT01500Common.DTO._4._Charges_Info
{
    public class LMT01500ChargesInfoHeaderDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CUNIT_NAME_LIST { get; set; }
        public decimal NTOTAL_ACTUAL_AREA { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public decimal NTOTAL_COMMON_AREA { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CCURRENCY { get; set; }

    }
}
