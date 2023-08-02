using System.ComponentModel.DataAnnotations.Schema;

namespace SettlementTracker.Models
{

    public class Details
    {
        private Lazy<Dictionary<string, IAccessible>> accessors;

        public Details()
        {
            accessors = new(GetAccessors, LazyThreadSafetyMode.PublicationOnly);
        }

        public CommonGroup CommonGroup { get; set; } = new CommonGroup();
        public ECommGroup ECommGroup { get; set; } = new ECommGroup();
        public VisaGroup VisaGroup { get; set; } = new VisaGroup();

        [NotMapped]
        public Dictionary<string, IAccessible> Accessors => accessors.Value;

        private Dictionary<string, IAccessible> GetAccessors()
        {
            return new Dictionary<string, IAccessible>()
            {
                { nameof(CommonGroup), new Accessible<CommonGroup>(CommonGroup) },
                { nameof(ECommGroup), new Accessible<ECommGroup>(ECommGroup) },
                { nameof(VisaGroup), new Accessible<VisaGroup>(VisaGroup) }
            };
        }
    }

}
