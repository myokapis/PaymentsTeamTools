using TemplateEngine.Writer;

namespace SettlementTracker.Models
{
    public interface IAccessible
    {
        int Count { get; }
        IEnumerable<KeyValuePair<string, string>> FieldValues { get; }
    }

    public class Accessible<T> : ViewModelAccessor<T>, IAccessible
    {
        public Accessible(T model) : base(model)
        {

        }
    }

}
