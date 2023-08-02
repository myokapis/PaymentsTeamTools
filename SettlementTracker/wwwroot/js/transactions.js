(function transactionPageScript() {
    $(document).ready(function () {
        bindEvents();
    });

    function bindEvents() {
        $("#transactions-div").on("click", "tbody tr", selectTransaction);
    }

    function selectTransaction() {
        row = $(this);
        $("#transaction-id").val(row.data("transaction-id"));
        $("#details-tab").trigger("click");
    }
})();
