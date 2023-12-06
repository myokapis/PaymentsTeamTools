using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BatchMonitoring.Models;
using BatchMonitoring.Presenters;
using BatchMonitoring.Services;

namespace BatchMonitoring.Controllers
{

    public class HomeController : BaseController
    {
        private readonly HomePresenter presenter;

        public HomeController(IDataService dataService, HomePresenter presenter) : base(dataService)
        {
            this.dataService = dataService;
            this.presenter = presenter;
        }

        /// <summary>
        /// Generates a view for the entire page
        /// </summary>
        /// <returns>A task containing html from a view</returns>
        [ResponseCache(Duration = 0)]
        public async Task<IActionResult> Index(SessionScope sessionScope)
        {
            return Html(await presenter.Index(GetSessionScope(sessionScope)));
        }

        public async Task<IActionResult> TabContent(SessionScope sessionScope)
        {
            var data = new
            {
                html = await presenter.TabContent(GetSessionScope(sessionScope))
            };

            return Json(data);
        }

    }

}
