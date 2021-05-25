

$(document).ready(function () {

    var initialized = false;

    $("#myHotelModal").on("shown.bs.modal", function () {



        calculateNrBucati();
        hideAllHiddenAnv();
        hideAllButtonEraseAnv();
        checkUzuraVal();

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



        if (!initialized) {


            $(document).on("click", "#apptsAddSubmitButton", function () {
                $this = $("#apptsAddSubmitButton");
                $this.prop("disabled", true);
                showLoading();
                /*                var result = validator.validate(validator);*/
                /*                if (result.formIsValid) {*/
                //console.log("submit add");
                var form = $('#apptform').serialize();
                $.ajax({
                    type: "POST",
                    url: "Hotel/AddModal",
                    data: $('#apptform').serialize(),
                    dataType: "json",
                    success: function (response) {
                        CloseModalById('myHotelModal');
                        $this.prop("disabled", false);
                        hideLoading();
                        swalSuccessTimer(response, "success", 5000, autoRefresh);
                        $('#hotelListState').change();

                    },
                    error: function (error) {
                        CloseModalById('myHotelModal');
                        $this.prop("disabled", false);
                        hideLoading();
                        swalErrorTimer(error.responseText, 7000, autoRefresh);

                    }
                });
                //}
                //else {
                //    //updateUi(result.validationResults, "form-group", "error_span");
                //}
                //setTimeout(function () {
                //    $this.prop("disabled", false);
                //    hideLoading();
                //}, 1000);
            });


            initialized = true;
        }

        $(document).on("keyup",
            ".uzura",
            function () {

                showHiddenAnv($(this));
                showButtonEraseAnv($(this));
                calculateNrBucati();

            });

        $(document).on("keyup",
            "#StangaFata_Uzura",
            function () {

                autoCompleteDrf();

            });
        $(document).on("keyup",
            "#StangaSpate_Uzura",
            function () {

                autoCompleteDrS();

            });

        $(document).on("change",
            ".duplicateValues",
            function () {

                var parent = $(this).closest(".parent");
                console.log("parent",parent);
                var form = 

                var val = $(this).val();
                var children = $(document).closest(".form-group").find(".toDuplicateValues");
                console.log(val);
                console.log($(this));
                console.log(children);

            });

        $(document).on("click",
            ".erase-data-anv",
            function () {


                eraseData($(this));
                hideHiddenAnv($(this));
                hideButtonEraseAnv($(this));
                calculateNrBucati();
            });

        // ==============================================================================================
        $(document).on("change",
            ".statuscurent",
            function () {

                var val = $(this).val();
                //console.log("val", val);
                var selectedStatusElem = $(this).closest(".form-group").find(".selectedstatus");
                $(selectedStatusElem).val(val);
                $(selectedStatusElem).trigger("updatedStatus");
            });
        $(".selectedstatus").on("updatedStatus",
            function () {
                console.log("updatedStatus was hit!");
                var element = $(this);
                enableOrDisablePositionForStatus(element);
            });



        // ==============================================================================================

        //$(document).on("keyup",
        //    ".validate",
        //    function () {
        //        var result = validator.validate(validator);
        //        updateUi(result.validationResults, "form-group", "error_span");
        //    });

        $(document).on("keyup", ".uzura", function () {
            //updateAndValidateUzura(true);
        });

    });

});

var showHiddenAnv = function (currentUzuraElement) {
    var parent = $(currentUzuraElement).closest(".parent");
    var elements = $(parent).find(".hiddenAnv");
    $.each(elements, function (index, item) {
        $(item).show();
    })

}

var hideHiddenAnv = function (currentUzuraElement) {
    var parent = $(currentUzuraElement).closest(".parent");
    var elements = $(parent).find(".hiddenAnv");
    $.each(elements, function (index, item) {
        $(item).hide();
    })
}

var hideAllHiddenAnv = function () {
    var elements = $(document).find(".hiddenAnv");
    $.each(elements, function (index, item) {
        $(item).hide();
    })
}

var showAllHiddenAnv = function () {
    var elements = $(document).find(".hiddenAnv");
    $.each(elements, function (index, item) {
        $(item).show();
    })
}

var hideButtonEraseAnv = function (element) {
    var parent = $(element).closest(".parent");
    var button = $(parent).find(".erase-data-anv");
    $(button).hide();

}
var showButtonEraseAnv = function (element) {
    var parent = $(element).closest(".parent");
    var button = $(parent).find(".erase-data-anv");
    $(button).show();

}

var hideAllButtonEraseAnv = function () {
    var elements = $(document).find(".erase-data-anv");
    $.each(elements, function (index, item) {
        $(item).hide();
    })
}

var showAllButtonEraseAnv = function () {
    var elements = $(document).find(".erase-data-anv");
    $.each(elements, function (index, item) {
        $(item).show();
    })
}

var eraseData = function (element) {
    var parent = $(element).closest(".parent");
    var children = $(parent).find(".form-control");
    $.each(children, function (index, item) {
        $(item).val(null).trigger("change");

    });
    var uzura = $(parent).find(".uzura");
    $(uzura).val("0");
}

var checkUzuraVal = function () {
    var elements = $(document).find(".uzura");
    $.each(elements, function (index, item) {
        if ($(item).val() != 0) {
            showHiddenAnv(item);
            showButtonEraseAnv(item);
        }

    });
}

var enableOrDisablePositionForStatus = function (statusElem) {
    var currentStatusElem = $(statusElem).closest(".form-group").find(".statuscurent");
    var selectedStatusElem = $(currentStatusElem).closest(".form-group").find(".selectedstatus");
    var positionElem = $(currentStatusElem).closest(".parent").find(".pozitieinraft");

    positionElem.removeAttr("disabled");
    positionElem.removeClass("disabled");

    if ($(selectedStatusElem).val() != "InRaft") {
        $(positionElem).attr("disabled", "disabled");
        $(positionElem).addClass("disabled");
        $(positionElem).val("").trigger("change"); ///////
    }
};

var calculateNrBucati = function () {
    var elements = $(document).find(".uzura");
    var count = 0;
    $.each(elements, function (index, item) {

        if ($(item).val() != 0) {
            count++;
        }

    });
    updateNrBucati(count);
}

var autoCompleteDrf = function () {

    showHiddenAnv("#DreaptaFata_Uzura");
    showButtonEraseAnv("#DreaptaFata_Uzura");

    var StFVal = $(document).find("#StangaFata_Uzura").val();
    var DrFVal = $(document).find("#DreaptaFata_Uzura");

    DrFVal.val(StFVal);
    calculateNrBucati();

    
}
var autoCompleteDrS = function () {

    showHiddenAnv("#DreaptaSpate_Uzura");
    showButtonEraseAnv("#DreaptaSpate_Uzura");

    var StSVal = $(document).find("#StangaSpate_Uzura").val();
    var DrSVal = $(document).find("#DreaptaSpate_Uzura");

    DrSVal.val(StSVal);
    calculateNrBucati();
    
}

var updateNrBucati = function (value) {
    var nrBuc = $(document).find("#SetAnvelope_NrBucati");
    nrBuc.val(value);
};
