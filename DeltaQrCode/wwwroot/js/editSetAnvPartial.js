

$(document).ready(function () {


    var initialized = false;
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


            // SAVE BTN CLICK EVENT
            $(document).on("click", "#editSaveButton", function () {
                $this = $("#editSaveButton");
                $this.prop("disabled", true);
                showLoading();
                /*                var result = validator.validate(validator);*/
                /*                if (result.formIsValid) {*/
                //console.log("edit");

                var form = $("#editform").serialize();
                $.ajax({
                    type: "POST",
                    url: "/Hotel/EditModal",
                    data: $("#editform").serialize(),
                    dataType: "json",
                    success: function (response) {
                        $this.prop("disabled", false);
                        hideLoading();
                        swalSuccessTimer(response, "success", 5000);

                    },
                    error: function (error) {
                        $this.prop("disabled", false);
                        hideLoading();
                        swalErrorTimer(error.responseText, 7000);

                    }
                });

                //} else {
                //    updateUi(result.validationResults, "form-group", "error_span");
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

        $(document).on("click",
            ".erase-data-anv",
            function (event) {

                event.stopPropagation();

                var parent = $(this).closest(".parent");
                var idElement = $(parent).find(".idElement");
                var id = $(idElement).val();
                console.log("this", (this));
                console.log(id);
                deleteAnvModalDialog(id);
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


var deleteAnvModalDialog = function (id) {

    var url = "/Hotel/DeleteModal"; // the url to the controller
    $.get(url + '?id=' + id,
        function (data) {
            $(document).find('#partialInfoDelete').html(data);
            $(document).find('#myHotelDeleteModal').modal('show');
            $(document).find('#confirmDelete').attr("data-deleteType", "anv")
        });
};

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

var updateNrBucati = function (value) {
    var nrBuc = $(document).find("#SetAnvelope_NrBucati");
    nrBuc.val(value);
};
