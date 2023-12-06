using Microsoft.AspNetCore.Mvc;
using BatchMonitoring.Models;
using BatchMonitoring.Services;

namespace BatchMonitoring.Controllers
{

    public class BaseController : Controller
    {
        protected IDataService dataService;

        public BaseController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        /// A helper method for returning view content as html
        /// </summary>
        /// <param name="content">The html view content as a string</param>
        /// <returns>An content result with a content type header set to "text/html"</returns>
        protected ContentResult Html(string content)
        {
            return Content(content, "text/html");
        }

        /// <summary>
        /// A helper method to ensure that a valid page scope is available
        /// </summary>
        /// <param name="pageScope">A custom class for persisting data between pages</param>
        /// <returns>The page scope from the calling method if it is valid otherwise a default page scope</returns>
        protected SessionScope GetSessionScope(SessionScope sessionScope)
        {
            return ModelState.IsValid ? sessionScope : dataService.GetSessionScope();
        }

    }

}
