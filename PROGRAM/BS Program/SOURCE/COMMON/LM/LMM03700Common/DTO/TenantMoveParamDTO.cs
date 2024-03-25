using System.Collections.Generic;

namespace LMM03700Common.DTO
{
    public class TenantMoveParamDTO : TenantParamDTO
    {
        public string CFROM_TENANT_CLASSIFICATION_ID { get; set; }
        public string CTO_TENANT_CLASSIFICATION_ID { get; set; }
        public List<string> LIST_CTENANT_ID { get; set; }
    }
}