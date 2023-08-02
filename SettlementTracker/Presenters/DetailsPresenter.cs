using TemplateEngine.Loader;
using TemplateEngine.Web;
using SettlementTracker.Services;
using TemplateEngine.Writer;
using SettlementTracker.Models;

namespace SettlementTracker.Presenters
{
    public class DetailsPresenter : MasterPresenter
    {
        public DetailsPresenter(ITemplateCache<IWebWriter> templateCache, IDataService dataService) : base(templateCache, dataService)
        {
        }

        public async Task<string> TabContent(SessionScope sessionScope)
        {
            var details = dataService.GetDetails(sessionScope);
            var detailAccessor = new ViewModelAccessor<Details>(details);

            var writer = await templateCache.GetWriterAsync("Details.tpl", "TAB");
            SetTabNames(writer, detailAccessor);
            SetTabFields(writer, detailAccessor);

            return writer.GetContent(true);
        }

        private void SetTabFields(IWebWriter writer, ViewModelAccessor<Details> detailAccessor)
        {
            var visible = true;
            writer.SelectSection("DETAILS");

            foreach (var accessor in detailAccessor.Model.Accessors)
            {
                writer.SetField("SectionName", accessor.Key);
                writer.SetField("SectionClass", visible ? "details-section-active" : "details-section-inactive");
                visible = false;
                writer.SelectSection("ROW");

                foreach(var field in accessor.Value.FieldValues)
                {
                    writer.SetField("FieldName", field.Key);
                    writer.SetField("FieldValue", field.Value);
                    writer.AppendSection();
                }

                writer.DeselectSection();
                writer.AppendSection();
            }

            writer.AppendSection();
        }

        private void SetTabNames(IWebWriter writer, ViewModelAccessor<Details> detailAccessor)
        {
            var enabled = true;
            writer.SelectSection("TAB_NAMES");

            foreach(var fieldValue in detailAccessor.FieldValues)
            {
                writer.SetField("TabName", fieldValue.Key);
                writer.SetField("TabClass", enabled ? "details-tab-selected" : "details-tab-unselected");
                writer.AppendSection();
                enabled = false;
            }

            writer.DeselectSection();
        }

    }

}
