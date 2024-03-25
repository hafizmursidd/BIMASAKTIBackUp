using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMT01500Common.DTO._1._AgreementList
{
    public class LMT01500ChangeStatusDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO {  get; set; }
        public string? CREF_DATE {  get; set; }
        public string? CHAND_OVER_DATE {  get; set; }
        public string? CSTART_DATE {  get; set; }
        public string? CEND_DATE {  get; set; }
        public string? CTRANS_STATUS_DESCR {  get; set; }
        public string? CTRANS_STATUS {  get; set; }
        public string? CACCEPT_DATE {  get; set; }
        //Revisi kah manieesss
        public string? CDOC_NO {  get; set; }
        public string? CREASON {  get; set; }
        public string? CNOTES {  get; set; }

    }
}
