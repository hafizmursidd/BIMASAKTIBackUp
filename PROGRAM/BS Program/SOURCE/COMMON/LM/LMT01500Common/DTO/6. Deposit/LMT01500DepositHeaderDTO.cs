using R_APICommonDTO;

namespace LMT01500Common.DTO._6._Deposit
{
    public class LMT01500DepositHeaderDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CUNIT_NAME_LIST { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CCURRENCY { get; set; }
    }
}
