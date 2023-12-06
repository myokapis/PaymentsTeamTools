function navClick() {
    let elem = $(this);
    let form = $("#page-scope")[0];
    $(form).attr("action", elem.data("action"));
    form.submit();
}