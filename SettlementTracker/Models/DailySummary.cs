namespace SettlementTracker.Models
{

    public class DailySummary
    {
        public int FileCount { get; set; }
        public int MerchantCount { get; set; }
        public int PendingCount { get; set; }
        public int RejectedCount { get; set; }
        public int SettledCount { get; set; }
        public int TransactionCount => PendingCount + RejectedCount + SettledCount + UnsentCount;

        [TemplateEngine.Formatters.FormatDate("yyyy-MM-dd")]
        public DateTime Date { get; set; }
        public int UnsentCount { get; set; }
    }

}
