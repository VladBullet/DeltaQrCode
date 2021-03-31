function ShowHeaderAlert(response, type) {
    ShowHeaderAlert(response, type, 5000);
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
    alertParam.fadeTo(milisecs, 1000).slideUp(1000, function () {
        alertParam.slideUp(milisecs);
    });
}