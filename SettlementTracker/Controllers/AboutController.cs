using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SettlementTracker.Models;
using SettlementTracker.Presenters;
using SettlementTracker.Services;

namespace SettlementTracker.Controllers
{

    public class AboutController : BaseController
    {
        private readonly AboutPresenter presenter;

        public AboutController(IDataService dataService, AboutPresenter presenter) : base(dataService)
        {
            this.dataService = dataService;
            this.presenter = presenter;
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
