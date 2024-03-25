using LMM03700Common.DTO;

namespace LMM03700Back;

public class MoveTenantDBParamDTO : TenantParamDTO
{
    public string CFROM_TENANT_CLASSIFICATION_ID { get; set; }
    public string CTO_TENANT_CLASSIFICATION_ID { get; set; }
    public List<string> LIST_CTENANT_ID { get; set; }
}