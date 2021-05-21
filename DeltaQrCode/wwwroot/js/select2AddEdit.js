


// Initialize select2
//Select2 for Position
$(".pozitieinraft").select2({
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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
    dropdownParent: "#editSetBody",
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




var selects = $('.select2custom');

$.each(selects, function (index, item) {


    var defaultElem = $(item).closest(".form-group").find(".default");
    var defaultVal = $(defaultElem).attr("data-value");
    var defaultText = $(defaultElem).attr("data-text");

    var option = new Option(defaultText, defaultVal, true, true);
    $(item).append(option).trigger('change');
    var data = { defaultText, defaultVal };
    // manually trigger the select2:select event
    $(item).trigger({
        type: 'select2:select',
        params: {
            data: data
        }
    });
});
