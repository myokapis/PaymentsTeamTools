using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SettlementTracker.Models;
using SettlementTracker.Presenters;
using SettlementTracker.Services;

namespace SettlementTracker.Controllers
{
    public class DetailsController : BaseController
    {
        private readonly DetailsPresenter presenter;

        public DetailsController(IDataService dataService, DetailsPresenter presenter) : base(dataService)
        {
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
