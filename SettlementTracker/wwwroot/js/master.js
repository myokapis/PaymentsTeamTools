(function masterPageScript() {
    $(document).ready(function () {
        $("#closeDialog").on("click", function () {
            let dialog = document.getElementById("errorDialog");
            dialog.close(false);
        });
    });

    master.postForm = function (url, onSuccess, onError, addData) {
        let data = getFormData(addData);

        postData(url, data)
            .then((response) => {
                response.json().then((data) => {
                    console.debug(data);
                    onSuccess(data);
                }).catch((error) => {
                    onError(error);
                })
            })
            .catch((error) => {
                onError(error);
            });
    }

    function getFormData(addData) {
        let form = $("#session-scope")[0];
        let formData = new FormData(form);

        // ensure dates get picked up even if the controls are disabled
        if (!formData.get("sessionScope.DateFrom")) {
            formData.set("sessionScope.DateFrom", $("#date-from").val());
            formData.set("sessionScope.DateTo", $("#date-to").val());
        }

        if (addData) addData(formData);
        
        return formData;
    }

    master.openErrorDialog = function (message, options) {
        $("#errorDialog #errorMessage").text(message);
        // TODO: apply options such as styles
        /*$("#errorDialog").dialog(options);*/
        $("#errorDialog").showModal();
    }

    // TODO: see which commented arguments are causing the post to break
    async function postData(url, data) {
        const response = await fetch(url, {
            method: "POST",
            //mode: "cors",
            //cache: "no-cache",
            //credentials: "same-origin",
            //headers: {
            //    'Content-Type': 'application/x-www-form-urlencoded',
            //},
            //redirect: "error",
            //referrerPolicy: "no-referrer",
            body: data
        });

        return response;
    }

})(window.master = window.master || {});
