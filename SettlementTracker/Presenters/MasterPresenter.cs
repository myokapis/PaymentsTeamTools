using TemplateEngine.Loader;
using TemplateEngine.Web;
using SettlementTracker.Services;

namespace SettlementTracker.Presenters
{

    /// <summary>
    /// A base presenter class containing convenience methods for common code that is used in multiple presenters
    /// </summary>
    /// <remarks>If you find yourself writing similar code in more than one presenter, then consider 
    /// refactoring that code into the master presenter.</remarks>
    public class MasterPresenter : MasterPresenterBase
    {
        protected readonly IDataService dataService;
        protected readonly ITemplateCache<IWebWriter> templateCache;

        public MasterPresenter(ITemplateCache<IWebWriter> templateCache, IDataService dataService) : base(templateCache)
        {
            this.dataService = dataService;
            this.templateCache = templateCache;
        }

    }

}
