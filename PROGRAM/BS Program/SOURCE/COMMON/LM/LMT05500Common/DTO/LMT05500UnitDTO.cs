using System;
using System.Text;

namespace LMT05500Common.DTO
{
    public class LMT05500UnitDTO : LMT05500Header
    {
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_TYPE { get; set; }
        public decimal NGROSS_AREA { get; set; }
        public decimal NNET_AREA { get; set; }
        public decimal NCOMMON_AREA { get; set; }

    }
}
