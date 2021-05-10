// Select2 for Serviciu
$("#serviciu").select2({
    tags: true,
    dropdownParent: "#apptModalBody",
    theme: "bootstrap4",
    minimumResultsForSearch: Infinity,
    ajax: {
        url: '/Appointments/GetTipServiciu',
        dataType: 'json',
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

// select2 initial value for Serviciu
var serviciuObj = $('#serviciu');
var serviciuValue = $("#defaultServiciu").attr("data-value");
if (serviciuValue != NaN && serviciuValue != '') {
    var optionserviciu = new Option(serviciuValue, serviciuValue, true, true);
    serviciuObj.append(optionserviciu);
    serviciuObj.val(serviciuValue).trigger('change'); // Notify any JS components that the value changed
}


// Select2 for Durata
$("#Durata").select2({
    tags: true,
    dropdownParent: "#apptModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Appointments/GetTimeDictionary',
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

// select2 initial value for Durata
var durataObj = $('#Durata');
var durataVal = $("#defaultDurata").attr("data-value");
if (durataVal != NaN && durataVal != '') {
    var optionDurata = new Option(durataVal, durataVal, true, true);
    durataObj.append(optionDurata);
    durataObj.val(durataVal);
    durataObj.trigger('change'); // Notify any JS components that the value changed
}