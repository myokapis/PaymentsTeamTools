using TemplateEngine.Loader;
using TemplateEngine.Web;
using SettlementTracker.Services;
using SettlementTracker.Models;

namespace SettlementTracker.Presenters
{
    public class FilesPresenter : MasterPresenter
    {
        public FilesPresenter(ITemplateCache<IWebWriter> templateCache, IDataService dataService) : base(templateCache, dataService)
        {
        }

        public async Task<string> TabContent(SessionScope sessionScope)
        {
            IWebWriter writer;
            var files = dataService.GetFiles(sessionScope).ToArray();

            if (files.Length > 0)
            {
                writer = await templateCache.GetWriterAsync("Files.tpl", "TAB");
                writer.SetMultiSectionFields("ROW", files);
            }
            else
            {
                writer = await templateCache.GetWriterAsync("Files.tpl", "NO_DATA");
            }


            return writer.GetContent(true);
        }

    }

}
