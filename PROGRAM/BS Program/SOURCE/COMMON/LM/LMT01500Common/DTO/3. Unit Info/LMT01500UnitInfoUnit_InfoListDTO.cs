using System;

namespace LMT01500Common.DTO._3._Unit_Info
{
    public class LMT01500UnitInfoUnitInfoListDTO
    {
        public string? CUNIT_NAME { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_SIZE_AREA { get; set; }
        public decimal NCOMMON_SIZE_AREA { get; set; }
        public decimal NACTUAL_SIZE_AREA { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
