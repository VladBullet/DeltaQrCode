

$(document).ready(function () {
    var initialized = false;
    $("#myApptModal").on("shown.bs.modal", function () {

        var validator = new CustomValidation(apptFormValidationRules);
        var selectieBuna = $("#selectieBunaElem").val();

        $(document).ready(function () {
            checkDateAndTimeAvailable();
            validator.validate(validator);
        });

        if (!initialized) {


            // SAVE BTN CLICK EVENT
            $(document).on("click", "#apptsEditButton", function (event) {
                event.preventDefault();
                $this = $("#apptsEditButton");
                $this.prop("disabled", true);
                checkDateAndTimeAvailable();
                console.log("selection after check : ", selectieBuna);
                var result = validator.validate(validator);
                if (result.formIsValid) {
                    $.ajax({
                        type: "POST",
                        url: "/Appointments/EditAppt",
                        data: $('#apptform').serialize(),
                        dataType: "json",
                        success: function (response) {
                            CloseModalById('myApptModal');
                            ShowHeaderAlert(response, "success", 5000);
                            clearAppointments();
                            getAppointmentsForSelectedDate();
                        },
                        error: function (error) {
                            CloseModalById('myApptModal');
                            swalErrorTimer(error.responseText, 7000);
                        }
                    });
                }
                else {
                    updateUi(result.validationResults, "form-group", "error_span");
                }
                setTimeout(function () {
                    $this.prop("disabled", false);
                }, 1000);
            });

            $(document).on("click", "#apptsAddButton", function (event) {
                event.preventDefault();
                $this = $("#apptsAddButton");
                $this.prop("disabled", true);
                showLoading();
                checkDateAndTimeAvailable();
                var result = validator.validate(validator);

                if (result.formIsValid) {
                    $.ajax({
                        type: "POST",
                        url: "Appointments/AddAppt",
                        data: $('#apptform').serialize(),
                        dataType: "json",
                        success: function (response) {
                            CloseModalById('myApptModal');
                            ShowHeaderAlert(response, "success", 5000);
                            clearAppointments();
                            getAppointmentsForSelectedDate();
                        },
                        error: function (error) {
                            CloseModalById('myApptModal');
                            swalErrorTimer(error.responseText, 7000);
                        }
                    });
                }
                else {
                    updateUi(result.validationResults, "form-group", "error_span");
                }
                setTimeout(function () {
                    $this.prop("disabled", false);
                }, 1000);
                
                hideLoading();
            });


            initialized = true;
        }

        $(document).on("keyup change",
            ".validate",
            function () {
                var result = validator.validate(validator);
                updateUi(result.validationResults, "form-group", "error_span");
            });

        $(document).on("change",
            "#selectieBunaElem",
            function () {
                var result = validator.validate(validator);
                updateUi(result.validationResults, "form-group", "error_span");
            });




    });

});
