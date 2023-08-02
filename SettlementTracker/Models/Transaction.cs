using System.Linq;
using TemplateEngine.Formatters;

namespace SettlementTracker.Models
{

    public class Transaction
    {
        private static readonly int[] editable = { 100, 350 };

        public int Id { get; set; }
        public double Amount { get; set; }
        public int? ChargeId { get; set; }

        [FormatDate("yyyy-MM-dd")]
        public DateTime? CreatedDate { get; set; }
        public bool Editable => Status.HasValue && editable.Contains(Status.Value);
        public int MerchantAccountId { get; set; }
        public int RecordNo { get; set; }
        public int? RefundId { get; set; }
        public int SettlementFileId { get; set; }
        public int State { get; set; }
        public int? Status { get; set; }
    }

}
