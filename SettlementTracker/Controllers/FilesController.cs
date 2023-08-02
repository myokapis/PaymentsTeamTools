using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SettlementTracker.Models;
using SettlementTracker.Presenters;
using SettlementTracker.Services;

namespace SettlementTracker.Controllers
{
    public class FilesController : BaseController
    {
        private readonly FilesPresenter presenter;

        public FilesController(IDataService dataService, FilesPresenter presenter) : base(dataService)
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
