using TemplateEngine.Loader;
using TemplateEngine.Web;
using SettlementTracker.Services;
using TemplateEngine.Writer;
using SettlementTracker.Models;

namespace SettlementTracker.Presenters
{

    public class HomePresenter : MasterPresenter
    {
        public HomePresenter(ITemplateCache<IWebWriter> templateCache, IDataService dataService) : base(templateCache, dataService)
        {
        }

        public async Task<string> Index(SessionScope sessionScope)
        {
            await SetupWriters("Master.tpl", "Home.tpl");
            WriteMasterSection(Head);
            WriteSessionScope(sessionScope);
            var dailySummaries = dataService.GetDailySummaries().ToArray();

            WriteMasterSection(Body, (writer) =>
            {
                if(dailySummaries.Length > 0)
                {
                    writer.SelectSection("TAB");
                    WriteDaySummary(writer, dailySummaries);
                    writer.DeselectSection();
                }
                else
                {
                    writer.SelectSection("TAB");
                    writer.AppendSection(true);
                }
                
            });

            return GetContent();
        }
        public async Task<string> TabContent(SessionScope sessionScope)
        {
            IWebWriter writer; 
            var dailySummaries = dataService.GetDailySummaries().ToArray();

            if (dailySummaries.Length > 0)
            {
                writer = await templateCache.GetWriterAsync("Home.tpl", "TAB");
                WriteDaySummary(writer, dailySummaries);
              }
            else
            {
                writer = await templateCache.GetWriterAsync("Home.tpl", "NO_DATA");
            }

            return writer.GetContent();
        }

        private void WriteDaySummary(IWebWriter writer, IList<DailySummary> dailySummaries)
        {
            writer.SetMultiSectionFields("COLUMN", dailySummaries);
            writer.AppendSection();
        }

        private void WriteSessionScope(SessionScope sessionScope)
        {
            var merchants = dataService.GetMerchants(sessionScope);
            var baseOptions = new List<Option> { new Option { Text = "", Value = "" } };
            var keyOptions = merchants.Select(m => new Option(m.AccountKey, m.AccountId.ToString())).OrderBy(o => o.Text);
            var nameOptions = merchants.Select(m => new Option(m.Name, m.AccountId.ToString())).OrderBy(o => o.Text);
            var stateOptions = dataService.GetTransactionStates();
            var statusOptions = dataService.GetTransactionStatuses();

            masterWriter.SetSectionFields(sessionScope, SectionOptions.Set);
            masterWriter.SetOptionFields("MERCHANT_KEY", baseOptions.Concat(keyOptions), sessionScope.MerchantAccountId.ToString());
            masterWriter.SetOptionFields("MERCHANT_NAME", baseOptions.Concat(nameOptions), sessionScope.MerchantAccountId.ToString());
            masterWriter.SetOptionFields("TRANSACTION_STATE", stateOptions, sessionScope.TransactionState.ToString());
            masterWriter.SetOptionFields("TRANSACTION_STATUS", statusOptions, sessionScope.TransactionStatus.ToString());
        }

    }

}
