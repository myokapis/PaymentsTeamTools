using TemplateEngine.Formatters;

namespace SettlementTracker.Models
{
    public class SettlementFile
    {
        public int SettlementFileId { get; set; }

        [FormatDate("MM/dd/yyyy")]
        public DateTime SettlementFileDate { get; set; }
        public string? SettlementFileName { get; set; }
        public int? AcknowledgementFileId { get; set; }
        public string? AcknowledgementFileName { get; set; }

        [FormatDate("MM/dd/yyyy")]
        public DateTime? AcknowledgementFileDate { get; set; }
    }
}
