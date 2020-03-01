
function ShowHeaderAlert(response, type) {
    var alertParam = $("#HeaderAlert");
    console.log(response);
    alertParam.text(response);
    if (type == "success")
        alertParam.addClass("alert-success");
    else
        alertParam.addClass("alert-danger");
    alertParam.alert();
    alertParam.fadeTo(2000, 500).slideUp(500, function () {
        alertParam.slideUp(500);
    });
}