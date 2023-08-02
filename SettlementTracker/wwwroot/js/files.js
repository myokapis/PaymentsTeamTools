(function filesPageScript() {

    $(document).ready(function () {
        $("#files-div").on("click", "tbody tr", selectFile);
    });

    function selectFile() {
        row = $(this);
        $("#file-name").val(row.data("file-name"));
        $("#file-id").val(row.data("file-id"));
        $("#transactions-tab").trigger("click");
    }

})();
