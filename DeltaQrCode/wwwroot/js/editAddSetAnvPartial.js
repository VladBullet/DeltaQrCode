
// Initialize select2
//Select2 for Position
$("#selPoz").select2({
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetAvailablePositions',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result,
                    function (item) {
                        return {
                            id: item,
                            text: item
                        };
                    }),
            };
        }
    }
});

// Select2 for Marca
$("#Marca").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetMarci',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result,
                    function (item) {
                        return {
                            id: item,
                            text: item
                        };
                    }),
            };
        }
    }
});

// Select2 for Flota
$("#Flota").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetFlote',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result,
                    function (item) {
                        return {
                            id: item,
                            text: item
                        };
                    }),
            };
        }
    }
});
// Select2 for TipSezon
$("#tipSezon").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    minimumResultsForSearch: Infinity,
    ajax: {
        url: '/Hotel/GetTireTypes',
        dataType: 'json',
        processResults: function (result) {
            return {
                results: $.map(result,
                    function (item) {
                        return {
                            id: item,
                            text: item
                        };
                    }),
            };
        }
    }
});
// Select2 for StatusAnvelope
$("#statusAnv").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    minimumResultsForSearch: Infinity,
    ajax: {
        url: '/Hotel/GetStatusAnvelope',
        dataType: 'json',
        processResults: function (result) {
            return {
                results: $.map(result,
                    function (item) {
                        return {
                            id: item,
                            text: item
                        };
                    }),
            };
        }
    }
});

// Select2 for Diametru
$("#Diametru").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetDiametru',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item,
                        text: item
                    };
                }),
            };
        }
    }
});

// Select2 for Latime
$("#Latime").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetLatime',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item,
                        text: item
                    };
                }),
            };
        }
    }
});

// Select2 for Inaltime
$("#Inaltime").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetInaltime',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item,
                        text: item
                    };
                }),
            };
        }
    }
});

// Select2 for Diametru
$("#Diametru").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetDiametru',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (time, item) {
                    return {
                        id: item,
                        text: time
                    };
                }),
            };
        }
    }
});

// Select2 for Latime
$("#Latime").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetLatime',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item,
                        text: item
                    };
                }),
            };
        }
    }
});

// Select2 for Inaltime
$("#Inaltime").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetInaltime',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item,
                        text: item
                    };
                }),
            };
        }
    }
});



// select2 initial value for position
var selPozObj = $('#selPoz');
var selPozVal = $("#defaultPosition").attr("data-value");
if (selPozVal != NaN && selPozVal != '') {
    var optionPoz = new Option(selPozVal, selPozVal, true, true);
    selPozObj.append(optionPoz);
    selPozObj.val(selPozVal);
    selPozObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for marca
var marcaObj = $('#Marca');
var marcaVal = $("#defaultMarca").attr("data-value");
if (marcaVal != NaN && marcaVal != '') {
    var optionMarca = new Option(marcaVal, marcaVal, true, true);
    marcaObj.append(optionMarca);
    marcaObj.val(marcaVal);
    marcaObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for flota
var flotaObj = $('#Flota');
var flotaVal = $("#defaultFlota").attr("data-value");
if (flotaVal != NaN && flotaVal != '') {
    var optionFlota = new Option(flotaVal, flotaVal, true, true);
    flotaObj.append(optionFlota);
    flotaObj.val(flotaVal);
    flotaObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for tipSezon
var tipSezonObj = $('#tipSezon');
var tipSezonValue = $("#defaultTipSezon").attr("data-value");
if (tipSezonValue != NaN && tipSezonValue != '') {
    var optionTipSezon = new Option(tipSezonValue, tipSezonValue, true, true);
    tipSezonObj.append(optionTipSezon);
    tipSezonObj.val(tipSezonValue).trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for StatusAnvelope
var statusAnvObj = $('#statusAnv');
var statusAnvValue = $("#defaultStatusAnv").attr("data-value");
if (statusAnvValue != NaN && statusAnvValue != '') {
    var optionstatusAnv = new Option(statusAnvValue, statusAnvValue, true, true);
    statusAnvObj.append(optionstatusAnv);
    statusAnvObj.val(statusAnvValue).trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Diametru
var diametruObj = $('#Diametru');
var diametruVal = $("#defaultDiametru").attr("data-value");
if (diametruVal != NaN && diametruVal != '') {
    var optionDiametru = new Option(diametruVal, diametruVal, true, true);
    diametruObj.append(optionDiametru);
    diametruObj.val(diametruVal);
    diametruObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Latime
var latimeObj = $('#Latime');
var latimeVal = $("#defaultLatime").attr("data-value");
if (latimeVal != NaN && latimeVal != '') {
    var optionLatime = new Option(latimeVal, latimeVal, true, true);
    latimeObj.append(optionLatime);
    latimeObj.val(latimeVal);
    latimeObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Inaltime
var inaltimeObj = $('#Inaltime');
var inaltimeVal = $("#defaultInaltime").attr("data-value");
if (inaltimeVal != NaN && inaltimeVal != '') {
    var optionInaltime = new Option(inaltimeVal, inaltimeVal, true, true);
    inaltimeObj.append(optionInaltime);
    inaltimeObj.val(inaltimeVal);
    inaltimeObj.trigger('change'); // Notify any JS components that the value changed
}
// select2 initial value for Diametru
var diametruObj = $('#Diametru');
var diametruVal = $("#defaultDiametru").attr("data-value");
if (diametruVal != NaN && diametruVal != '') {
    var optionDiametru = new Option(diametruVal, diametruVal, true, true);
    diametruObj.append(optionDiametru);
    diametruObj.val(diametruVal);
    diametruObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Latime
var latimeObj = $('#Latime');
var latimeVal = $("#defaultLatime").attr("data-value");
if (latimeVal != NaN && latimeVal != '') {
    var optionLatime = new Option(latimeVal, latimeVal, true, true);
    latimeObj.append(optionLatime);
    latimeObj.val(latimeVal);
    latimeObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Inaltime
var inaltimeObj = $('#Inaltime');
var inaltimeVal = $("#defaultInaltime").attr("data-value");
if (inaltimeVal != NaN && inaltimeVal != '') {
    var optionInaltime = new Option(inaltimeVal, inaltimeVal, true, true);
    inaltimeObj.append(optionInaltime);
    inaltimeObj.val(inaltimeVal);
    inaltimeObj.trigger('change'); // Notify any JS components that the value changed
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

// when loading validate all inputs
var validator = new CustomValidation(anvelopeFormValidationRules);
validator.addcustomValidationRules(uzuraStFRules);


$(document).ready(function () {
    updateAndValidateUzura(false);
});


// SAVE BTN CLICK EVENT
$(document).one("click", "#apptsEditSubmitButton", function () {
    $(this).attr("disabled", "disabled");
    var result = validator.validate(validator);
    if (result.formIsValid) {
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
                ShowHeaderAlert(error.responseText, "error", 5000);
            }
        });
    } else {
        updateUi(result.validationResults, "form-group", "error_span");
    }
});

$("#apptsAddSubmitButton").one("click",function () {
    $(this).attr("disabled", "disabled");
    var result = validator.validate(validator);
    if (result.formIsValid) {
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
                console.log(error);
                ShowHeaderAlert(error.responseText, "error", 5000);
            }
        });
    }
    else {
        updateUi(result.validationResults, "form-group", "error_span");
    }
});


$(document).on("keyup",
    ".validate",
    function () {
        var result = validator.validate(validator);
        updateUi(result.validationResults, "form-group", "error_span");
    });



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

$(document).on("keyup", ".uzura", function () {
    updateAndValidateUzura(true);
});

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