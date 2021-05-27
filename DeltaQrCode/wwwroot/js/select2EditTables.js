var initializeTableBody = function () {
    // Select2 for StatusAnvelope
    $(document).find(".statuscurentTable").select2({
        tags: true,
        dropdownParent: "#select2Tables",
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

    // Initialize select2
    //Select2 for Position

    $(document).find(".pozitieinraftTable").select2({
        dropdownParent: "#select2Tables",
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

    var selectos = $(document).find(".select2customTable");

    $.each(selectos, function (index, item) {


        var defaultElem = $(item).closest("th").find(".default");
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

    


}

var changeStatus = $(document).find(".statuscurentTable");
$.each(changeStatus, function (index, item) {
    $(item).trigger("change");
});

$(document).on("change",
    ".statuscurentTable",
    function () {

        var val = $(this).val();

        var selectedStatusElemTable = $(this).closest(".group").find(".selectedstatusTable");
        console.log("selectedStatusElemTable", selectedStatusElemTable);

        $(selectedStatusElemTable).val(val);

        $(selectedStatusElemTable).trigger("update");


    });
$(document).on("update",".selectedstatusTable",
    function () {
        console.log("updatedStatusTable was hit!");
        var element = $(this);

        enableOrDisablePositionForStatusTable(element);
    });

var enableOrDisablePositionForStatusTable = function (statusElem) {
    var currentStatusElemTable = $(statusElem).closest(".group").find(".statuscurentTable");
    console.log("statusElem", statusElem);
    var selectedStatusElemTable = $(currentStatusElemTable).closest(".group").find(".selectedstatusTable");
    var positionElemTable = $(currentStatusElemTable).closest(".parenting").find(".pozitieinraftTable");

    console.log("positionElemTable", positionElemTable);
    positionElemTable.removeAttr("disabled");
    positionElemTable.removeClass("disabled");

    if ($(selectedStatusElemTable).val() != "InRaft") {
        $(positionElemTable).attr("disabled", "disabled");
        $(positionElemTable).addClass("disabled");
        $(positionElemTable).val("").trigger("change"); ///////
    }
};

