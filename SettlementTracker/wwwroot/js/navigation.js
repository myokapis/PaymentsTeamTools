(function navigationScript() {
    $(document).ready(function () {
        $(".nav-button").on("click", tabClick);
    });

    function clearSelections(action) {
        if (action == "files") {
            $("#file-name").val("");
            $("#file-id").val("");
            $("#transaction-id").val("");
        }
        else if (action == "transactions") {
            $("#transaction-id").val("");
        }
    }

    function displayTabs(currentTab, selectedTab, selectedTopic) {
        $(".tab-div").hide();
        selectedTopic.show();

        currentTab.removeClass("nav-button-selected");
        currentTab.addClass("nav-button-unselected");
        selectedTab.removeClass("nav-button-unselected");
        selectedTab.addClass("nav-button-selected");
    }

    function invalidClick(currentTab, selectedTab) {
        if (currentTab.data("action") == selectedTab.data("action"))
            return true;

        let action = $(selectedTab).data("action");
        let transactionId = parseInt($("#transaction-id").val());
        let options = { title: "Selecting tab failed.", modal: true, width: 600 }

        if ((action == "details") && isNaN(transactionId)) {
            master.openErrorDialog(options, "Select a transaction to view.")
            return true;
        }

        return false;
    }

    navigation.loadContent = function (selectedTab, successActions) {
        let action = $(selectedTab).data("action");
        let selectedTopic = $(`#${action}-div`);
        clearSelections(action);

        master.postForm(`${action}/TabContent`,
            function (data) {
                console.debug(data);
                selectedTopic.html(data.html);
                if (successActions) successActions(selectedTab, selectedTopic)
            },
            function (error) {
                master.openErrorDialog({ title: "Selecting tab failed.", modal: true, width: 600 }, "An error occurred.");
            });
    }

    function tabClick() {
        let currentTab = $(".nav-button-selected");
        let selectedTab = $(this);

        if (invalidClick(currentTab, selectedTab))
            return false;

        navigation.loadContent(selectedTab, function (selectedTab, selectedTopic) {
            displayTabs(currentTab, selectedTab, selectedTopic);
            filters.activate(selectedTab);
        });
    }

})(window.navigation = window.navigation || {});
