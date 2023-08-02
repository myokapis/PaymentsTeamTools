using TemplateEngine.Web;
using SettlementTracker.Models;

namespace SettlementTracker.Services
{

    public interface IDataService
    {
        IList<DailySummary> GetDailySummaries();
        Details GetDetails(SessionScope sessionScope);
        IList<SettlementFile> GetFiles(SessionScope pageScope);
        IList<Merchant> GetMerchants(SessionScope pageScope);
        SessionScope GetSessionScope();
        IList<Transaction> GetTransactions(SessionScope pageScope);
        IList<Option> GetTransactionStates();
        IList<Option> GetTransactionStatuses();
    }

}
