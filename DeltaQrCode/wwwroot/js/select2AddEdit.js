// Initialize select2
//Select2 for Position
$(".pozitieinraft").select2({
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetAvailablePositions',
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query = {
                term: params.term,
                nrbuc: $(document).find("#NrBucati").val()
            }
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result,
                    function (item) {
                        return {
                            id: item.id,
                            text: item.text
                        };
                    }),
            };
        }
    }
});

// Select2 for Marca
$(".marca").select2({
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
$(".tipsezon").select2({
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
$(".statuscurent").select2({
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
$(".diametru").select2({
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
                results: $.map(result,
                    function (item, val) {
                        return {
                            id: val,
                            text: item
                        };
                    }),
            };
        }
    }
});


// Select2 for Latime
$(".latime").select2({
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

// Select2 for Inaltime
$(".inaltime").select2({
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

// Select2 for DOT
$(".dot").select2({
    tags: true,
    dropdownParent: "#hotelModalBody",
    theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: '/Hotel/GetDot',
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



// select2 initial value for position
var selPozObj = $('.pozitieinraft');
var selPozVal = $("#defaultPosition").attr("data-value");
if (selPozVal != NaN && selPozVal != '') {
    var optionPoz = new Option(selPozVal, selPozVal, true, true);
    selPozObj.append(optionPoz);
    selPozObj.val(selPozVal);
    selPozObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for marca
var marcaObj = $('.marca');
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
var tipSezonObj = $('.tipsezon');
var tipSezonValue = $("#defaultTipSezon").attr("data-value");
if (tipSezonValue != NaN && tipSezonValue != '') {
    var optionTipSezon = new Option(tipSezonValue, tipSezonValue, true, true);
    tipSezonObj.append(optionTipSezon);
    tipSezonObj.val(tipSezonValue).trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for StatusAnvelope
var statusAnvObj = $('.statuscurent');
var statusAnvValue = $("#defaultStatusAnv").attr("data-value");
if (statusAnvValue != NaN && statusAnvValue != '') {
    var optionstatusAnv = new Option(statusAnvValue, statusAnvValue, true, true);
    statusAnvObj.append(optionstatusAnv);
    statusAnvObj.val(statusAnvValue).trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Diametru
var diametruObj = $('.diametru');
var diametruVal = $("#defaultDiametru").attr("data-value");
if (diametruVal != NaN && diametruVal != '') {
    var optionDiametru = new Option(diametruVal, diametruVal, true, true);
    diametruObj.append(optionDiametru);
    diametruObj.val(diametruVal);
    diametruObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Latime
var latimeObj = $('.latime');
var latimeVal = $("#defaultLatime").attr("data-value");
if (latimeVal != NaN && latimeVal != '') {
    var optionLatime = new Option(latimeVal, latimeVal, true, true);
    latimeObj.append(optionLatime);
    latimeObj.val(latimeVal);
    latimeObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for Inaltime
var inaltimeObj = $('.inaltime');
var inaltimeVal = $("#defaultInaltime").attr("data-value");
if (inaltimeVal != NaN && inaltimeVal != '') {
    var optionInaltime = new Option(inaltimeVal, inaltimeVal, true, true);
    inaltimeObj.append(optionInaltime);
    inaltimeObj.val(inaltimeVal);
    inaltimeObj.trigger('change'); // Notify any JS components that the value changed
}

// select2 initial value for DOT
var dotObj = $('.dot');
var dotVal = $("#defaultDot").attr("data-value");
if (dotVal != NaN && dotVal != '') {
    var optionDot = new Option(dotVal, dotVal, true, true);
    dotObj.append(optionDot);
    dotObj.val(dotVal);
    dotObj.trigger('change'); // Notify any JS components that the value changed
}