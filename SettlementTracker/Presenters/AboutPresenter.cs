using TemplateEngine.Loader;
using TemplateEngine.Web;
using SettlementTracker.Services;
using SettlementTracker.Models;

namespace SettlementTracker.Presenters
{
    public class AboutPresenter : MasterPresenter
    {
        public AboutPresenter(ITemplateCache<IWebWriter> templateCache, IDataService dataService) : base(templateCache, dataService)
        {
        }

        public async Task<string> TabContent(SessionScope sessionScope)
        {
            var writer = await templateCache.GetWriterAsync("About.tpl", "TAB");
            return writer.GetContent(true);
        }

    }

}
