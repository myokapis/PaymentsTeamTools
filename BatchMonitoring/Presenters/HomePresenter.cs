using TemplateEngine.Loader;
using TemplateEngine.Web;
using BatchMonitoring.Services;
using BatchMonitoring.Models;

namespace BatchMonitoring.Presenters
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

            WriteMasterSection(Body, (writer) =>
            {
                    writer.SelectSection("TAB");
                    writer.AppendSection(true);
            });

            return GetContent();
        }
        public async Task<string> TabContent(SessionScope sessionScope)
        {
            IWebWriter writer = await templateCache.GetWriterAsync("Home.tpl", "TAB");

            return writer.GetContent();
        }

    }

}
