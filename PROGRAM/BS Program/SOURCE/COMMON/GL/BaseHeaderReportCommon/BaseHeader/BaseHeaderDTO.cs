using System;

namespace BaseHeaderReportCommon.BaseHeader
{
    public class BaseHeaderDTO
    {
        public byte[] BLOGO_COMPANY { get; set; }
        public string CPRINT_CODE { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CPRINT_NAME { get; set; }
        public string CUSER_ID { get; set; }
    }

    public class BaseHeaderResult
    {
        public BaseHeaderDTO BaseHeaderData { get; set; }
    }
}
