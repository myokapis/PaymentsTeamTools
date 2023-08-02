(function detailsPageScript() {
    $(document).ready(function () {
        bindDetailTabEvents();
    });

    function bindDetailTabEvents() {
        $("#details-div").on("click", ".details-tab", detailsTabClick);
    }

    function detailsTabClick() {
        let selectedTab = $(this);
        let currentTab = $(".details-tab-selected");
        currentTab.removeClass("details-tab-selected");
        currentTab.addClass("details-tab-unselected");
        selectedTab.removeClass("details-tab-unselected");
        selectedTab.addClass("details-tab-selected");
        showDetails(selectedTab);
    }

    function showDetails(selectedTab) {
        let selectedSection = $(`#details-${$(selectedTab).data("section")}-table`);
        let currentSection = $(".details-section-active");
        currentSection.removeClass("details-section-active");
        currentSection.addClass("details-section-inactive");
        selectedSection.removeClass("details-section-inactive");
        selectedSection.addClass("details-section-active");
    }

})(window.details = window.details || {});
