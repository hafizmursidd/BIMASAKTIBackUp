namespace LMT01500Common.DTO._3._Unit_Info
{
    public class LMT01500UnitInfoUnitInfoDetailDTO
    {
        //Parameter External
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CUSER_ID { get; set; }

        //Real DTOs
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_SIZE_AREA { get; set; }
        public decimal NNET_SIZE_AREA { get; set; }
        public decimal NCOMMON_SIZE_AREA { get; set; }
        public decimal NACTUAL_SIZE_AREA { get; set; }
    }
}
