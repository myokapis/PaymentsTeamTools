using TemplateEngine.Formatters;

namespace SettlementTracker.Models
{
    public class SettlementFileMerchant
    {
        public int SettlementFileId { get; set; }
        public int MerchantAccountId { get; set; }
        public string? MerchantKey { get; set; }
        public string? MerchantName { get; set; }
    }
}
