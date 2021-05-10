

$(document).ready(function () {
    var initialized = false;
    $("#myHotelModal").on("shown.bs.modal", function () {

        // when loading validate all inputs
        var validator = new CustomValidation(anvelopeFormValidationRules);
        validator.addcustomValidationRules(uzuraStFRules);

        var disableElementsByIds = function (elements) {
            $.each(elements, function (index, element) {
                var item = $(document).find("#" + element);
                //item.addClass("readonly");
                item.attr("disabled", "disabled");
                item.addClass("disabled");
            });
        }
        var enableInputsByIds = function (elements) {
            $.each(elements,
                function (index, element) {
                    var item = $(document).find("#" + element);
                    //item.addClass("readonly");
                    item.removeAttr("disabled");
                    item.removeClass("disabled");
                });
        };

        var updateAndValidateUzura = function (shouldUpdateNrBuc) {
            var stF = $(document).find("#StangaFata");
            var drF = $(document).find("#DreaptaFata");
            var stS = $(document).find("#StangaSpate");
            var drS = $(document).find("#DreaptaSpate");
            var initialNr = 0;
            var uzura = new Uzura();

            // clear all validation rules
            //validator.removeCustomValidationRules(uzuraDrFRules);
            //validator.removeCustomValidationRules(uzuraStSRules);
            //validator.removeCustomValidationRules(uzuraDrSRules);

            // if stf & drF filled ->  then add validationRule for drF  ->  enable Input stS
            if (stF.val() != "" && drF.val() != "") {
                validator.addcustomValidationRules(uzuraDrFRules);
                if (!uzura.drF) {
                    uzura.activateField(drF.attr("id"));
                }
                enableInputsByIds([stS.attr("id")]);
                // if stF & drF & stS filled ->  then add validationRule for stS  ->  enable Input drS
                if (stS.val() != "") {
                    validator.addcustomValidationRules(uzuraStSRules);
                    if (!uzura.stS) {
                        uzura.activateField(stS.attr("id"));
                    }
                    enableInputsByIds([drS.attr("id")]);
                    // if stF & drF & stS & drS filled -> then add validationRule for drS -> all fields should be enabled
                    if (stF.val() != "" && drF.val() != "" && stS.val() != "" && drS.val() != "") {
                        validator.addcustomValidationRules(uzuraDrSRules);
                        if (!uzura.drS) {
                            uzura.activateField(drS.attr("id"));
                        }
                    }
                }
            }
            // if drS == "" -> unfill drS -> then remove validationRule for drS -> input remains enabled drS
            if (drS.val() == "") {
                if (uzura.drS) {
                    uzura.deactivate(drS.attr("id"));
                }
                validator.removeCustomValidationRules(uzuraDrSRules);

                // if drS == "" && stS == "" -> unfill stS -> remove validation rule for stS -> disable drS
                if (stS.val() == "") {
                    if (uzura.stS) {
                        uzura.deactivate(stS.attr("id"));
                    }
                    validator.removeCustomValidationRules(uzuraStSRules);
                    disableElementsByIds([drS.attr("id")]);
                    // if drS == "" && stS == "" && drF == "" -> unfill drF -> remove validation rule for drF -> disable stS
                    if (drF.val() == "") {
                        if (uzura.drF) {
                            uzura.deactivate(drF.attr("Id"));
                        }
                        validator.removeCustomValidationRules(uzuraDrFRules);
                        disableElementsByIds([stS.attr("id")]);
                    }
                }
            }
            if (shouldUpdateNrBuc) {
                updateNrBucati(uzura.nrBuc);
            }
            var result = validator.validate(validator);
            updateUi(result.validationResults, "form-group", "error_span");
        }

        var updateNrBucati = function (value) {
            var nrBuc = $(document).find("#NrBucati");
            nrBuc.val(value);
        };

        updateAndValidateUzura(false);

        if (!initialized) {


            // SAVE BTN CLICK EVENT
            $(document).on("click", "#apptsEditSubmitButton", function () {
                $this = $("#apptsEditSubmitButton");
                $this.prop("disabled", true);
                showLoading();
                var result = validator.validate(validator);
                if (result.formIsValid) {
                    console.log("edit");
                    $.ajax({
                        type: "POST",
                        url: "/Hotel/EditModal",
                        data: $('#apptform').serialize(),
                        dataType: "json",
                        success: function (response) {
                            CloseModalById('myHotelModal');
                            ShowHeaderAlert(response, "success", 5000);
                            $('#hotelListState').change();

                        },
                        error: function (error) {
                            CloseModalById('myHotelModal');
                            swalErrorTimer(error.responseText, 7000);

                        }
                    });
                } else {
                    updateUi(result.validationResults, "form-group", "error_span");
                }
                setTimeout(function () {
                    $this.prop("disabled", false);
                    hideLoading();
                }, 1000);
            });

            $(document).on("click", "#apptsAddSubmitButton", function () {
                $this = $("#apptsAddSubmitButton");
                $this.prop("disabled", true);
                showLoading();
                var result = validator.validate(validator);
                if (result.formIsValid) {
                    console.log("submit add");
                    $.ajax({
                        type: "POST",
                        url: "Hotel/AddModal",
                        data: $('#apptform').serialize(),
                        dataType: "json",
                        success: function (response) {
                            CloseModalById('myHotelModal');
                            ShowHeaderAlert(response, "success", 5000);
                            $('#hotelListState').change();

                        },
                        error: function (error) {
                            CloseModalById('myHotelModal');
                            swalErrorTimer(error.responseText, 7000);

                        }
                    });
                }
                else {
                    updateUi(result.validationResults, "form-group", "error_span");
                }
                setTimeout(function () {
                    $this.prop("disabled", false);
                    hideLoading();
                }, 1000);
            });


            initialized = true;
        }

        $(document).on("change",
            "#statusAnv",
            function () {
                $("#selPoz").trigger("updatedStatus");
            });

        $("#selPoz").on("updatedStatus",
            function () {
                var pos = $("#selPoz");
                pos.removeAttr("disabled");
                pos.removeClass("disabled");
                var statusVal = $("#statusAnv").val();
                if (statusVal != "InRaft") {
                    pos.attr("disabled", "disabled");
                    pos.addClass("disabled");
                }
            });
        $(document).on("keyup",
            ".validate",
            function () {
                var result = validator.validate(validator);
                updateUi(result.validationResults, "form-group", "error_span");
            });

        $(document).on("keyup", ".uzura", function () {
            updateAndValidateUzura(true);
        });

        
        

    });

});
