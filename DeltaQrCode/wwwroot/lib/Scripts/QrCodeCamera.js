function ShowHeaderAlert(response, type) {
    ShowHeaderAlert(response, type, 2000);
};
function ShowHeaderAlert(response, type, milisecs) {
    var alertParam = $("#HeaderAlert");
    console.log(response);
    alertParam.text(response);
    if (type == "success")
        alertParam.addClass("alert-success");
    else
        alertParam.addClass("alert-danger");
    alertParam.alert();
    alertParam.fadeTo(milisecs, 500).slideUp(500, function () {
        alertParam.slideUp(500);
    });
}