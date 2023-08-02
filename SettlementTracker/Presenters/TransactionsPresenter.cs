using TemplateEngine.Loader;
using TemplateEngine.Web;
using SettlementTracker.Services;
using SettlementTracker.Models;

namespace SettlementTracker.Presenters
{
    public class TransactionsPresenter : MasterPresenter
    {
        public TransactionsPresenter(ITemplateCache<IWebWriter> templateCache, IDataService dataService) : base(templateCache, dataService)
        {
        }

        public async Task<string> TabContent(SessionScope sessionScope)
        {
            IWebWriter writer;
            var transactions = dataService.GetTransactions(sessionScope).ToArray();

            if (transactions.Length > 0)
            {
                writer = await templateCache.GetWriterAsync("Transactions.tpl", "TAB");
                writer.SetMultiSectionFields("ROW", transactions);
            }
            else
            {
                writer = await templateCache.GetWriterAsync("Transactions.tpl", "NO_DATA");
            }
            
            return writer.GetContent(true);
        }

    }

}
