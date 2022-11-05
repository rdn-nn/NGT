$(document).on("click", "[name='Perm']", function (e) {
    if (this.checked) {
        $(this).attr("value", "true");
    } else {
        $(this).attr("value", "false");
    }
});