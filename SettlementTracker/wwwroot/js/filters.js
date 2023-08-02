(function filtersScript() {
    $(document).ready(function () {
        $("#apply-filter").on("click", applyFilter);
        $("input[name='merchant-selector']").on("change", displayMerchantSelector);
        filters.activate($("[data-action='home']"));
    });

    filters.activate = function (selectedTab) {
        let tabFilterMask = selectedTab.data("filter-mask");

        $("form [data-filter-mask]").each(function (index, filterElem) {
            let elemFilterMask = parseInt($(filterElem).data("filter-mask"), 10);
            let isDisabled = ((elemFilterMask === NaN) || ((elemFilterMask & tabFilterMask) == 0));
            $(filterElem).prop("disabled", isDisabled);
        });
    }

    function applyFilter() {
        let selectedTab = $(".nav-button-selected")[0];
        navigation.loadContent(selectedTab);
    }

    function displayMerchantSelector() {
        $("select").toggle();
    }

})(window.filters = window.filters || {});
