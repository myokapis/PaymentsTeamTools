using System.ComponentModel.DataAnnotations;
using TemplateEngine.Formatters;

namespace SettlementTracker.Models
{

    public class SessionScope : IValidatableObject
    {
        [FormatDate("yyyy-MM-dd")]
        public DateTime DateFrom { get; set; }

        [FormatDate("yyyy-MM-dd")]
        public DateTime DateTo { get; set; }

        public int? SettlementFileId { get; set; }
        public int? MerchantAccountId { get; set; }
        public double? TransactionAmount { get; set; }
        public int? TransactionId { get; set; }
        public int? TransactionState { get; set; }
        public int? TransactionStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateFrom.Date < DateTime.Now.Date.AddDays(-365))
                yield return new ValidationResult("Searches are limited to one year of history.", new[] { nameof(DateFrom) });
            
            if (DateTo.Date.Subtract(DateFrom).Days > 7)
                yield return new ValidationResult("Searches are limited to a range of seven days.", new[] { nameof(DateTo) });
        }
    }

}
