namespace Lookup_GSCOMMON.DTOs
{
    public class GSL01800DTO
    {
        private string iLEVEL_CATEGORY_ID_NAME;

        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public int ILEVEL { get; set; }
        public string ILEVEL_CATEGORY_ID_NAME { get => iLEVEL_CATEGORY_ID_NAME; set => iLEVEL_CATEGORY_ID_NAME = $"[{ILEVEL}] {CCATEGORY_ID} - {CCATEGORY_NAME}"; }
        public string CCATEGORY_TYPE { get; set; }
        public string CCATEGORY_TYPE_DESCR { get; set; }
    }
}
