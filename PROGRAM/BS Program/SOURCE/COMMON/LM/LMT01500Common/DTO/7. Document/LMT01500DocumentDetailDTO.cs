using System;

namespace LMT01500Common.DTO._7._Document
{
    public class LMT01500DocumentDetailDTO
    {
        //External Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }

        //Real DTOs
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CEXPIRED_DATE { get; set; }
        public string? CDESCRIPTION { get; set; }
        // Data For R_Storage
        public string? CDOC_FILE { get; set; }
        public string? CINVOICE_TEMPLATE { get; set; } = "";
        public bool LINVOICE_TEMPLATE { get; set; } = false;
        public string? CSTORAGE_ID { get; set; } = "";
        public string? CFileName { get; set; }
        public string? CFileExtension { get; set; }
        public string? CFileNameExtension { get; set; }
        public Byte[]? OData { get; set; }
    }
}
