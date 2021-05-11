
var dateToday = new Date();
/*console.log(dateToday);*/
$(function () {
    $("#datepicker").datepicker({ dateFormat: "yy-mm-dd" }).datepicker("setDate", dateToday);

    /* Enables scrolling to a set position  scrollToId('.hour-container', '#hour4');*/
    var scrollToId = function (scrollContainer, aid) {
        var aTag = $(aid);
        $(scrollContainer).animate({ scrollTop: aTag.offset().top }, 'slow');
    };

    /* Dates ensure clicked, not dragged! */
    var $dateControl = $('.date');
    $dateControl.on('mousedown',
        function (evt) {
            $dateControl.on('mouseup mousemove',
                function handler(evt) {
                    if (evt.type === 'mouseup') {
                        // click
                        $('.date').removeClass('date-selected');

                        $(evt.currentTarget).addClass('date-selected');
                        showAppointmentsForDate(evt.currentTarget.innerHTML);
                    } else {
                        // drag
                    }
                    $dateControl.off('mouseup mousemove', handler);
                });
        });
    /*end page load*/
});

var scrollToWorkingHours = function () {
    $('.hour-container').animate({ scrollTop: '125px' }, 'slow');
};

var clearAppointments = function () {
    // Clear the existing appts.
    $('.appt-no-height').remove();
};

var apptNewModalDialog = function (startDate, startHour, rampId) {
    var url = "/Appointments/ModalAdd";
    $.get(url,
        { startDateStr: startDate, startHour: startHour, rampId: rampId },
        function (data) {
            $('#partialInfo').html(data);
            preOpenModal();
            $('#myApptModal').modal('show');
            makeModalScrollable("myApptModal");
            console.log("add listener");
            var dateFromPage = $("#datepicker").val();
            var datePage = new Date(dateFromPage);
            $("#datepickerModal").datepicker({ "setDate": datePage, format: "yy-mm-dd", minDate: 0 });
            //enableselect2forServiciu();
        });
};
var apptEditModalDialog = function (id) {

    var url = "/Appointments/ModalEdit"; // the url to the controller
    $.get(url + '?id=' + id,
        function (data) {
            CloseModalById('myApptMenuModal');
            $('#partialInfo').html(data);
            preOpenModal();
            $('#myApptModal').modal('show');
            makeModalScrollable("myApptModal");


            var dateFromPage = $("#datepicker").val();
            var datePage = new Date(dateFromPage);
            $("#datepickerModal").datepicker({ "setDate": datePage, format: "yy-mm-dd", minDate: 0 });
            //enableselect2forServiciu();

        });
};

var apptMenuModalDialog = function (id, confirm) {

    var url = "/Appointments/MenuModal"; // the url to the controller
    $.get(url + '?id=' + id + '&confirm=' + confirm,
        function (data) {
            $('#partialInfoMenu').html(data);
            preOpenModal();
            $('#myApptMenuModal').modal('show');
            makeModalScrollable("myApptMenuModal");
        });
};

var showAppointmentsForDate = function (calendarDate) {

    $.get("/Appointments/GetAppointmentsForDate",
        { apptDate: calendarDate },
        function (response) {

            clearAppointments();
            // Settings
            var marginLeftPerItem = 20;
            var oneHourPxHeight = 60;

            var oneMinutePxHeight = oneHourPxHeight / 60;
            if (response != "") {
                console.log(response);
                //var obj = $.parseJSON(response);
                $.each(response,
                    function (key, data) {
                        var rampId = "#Ramp" + data.rampId;
                        // Add appts in the right position
                        for (var i = 0; i < data.appointments.length; i++) {
                            var row = data.appointments[i];
                            var minutesText = row.startTime_Minutes === 0 ? '00' : row.startTime_Minutes;
                            var apptRow = $(rampId + " " + ".hour" + row.startTime_Hour + "_" + minutesText);

                            var warningSpan = '';
                            if (!row.confirmed && row.changedByClient) {
                                warningSpan = '<i class="fas fa-exclamation-triangle" style="margin-left:5px"></i>';
                            }
                            else if (row.confirmed && row.changedByClient) {
                                warningSpan = '<i class="fas fa-check-circle" style="margin-left:5px"></i>';
                            }

                            var stringToAppend = '<div data-confirm="' +
                                row.confirmed +
                                '" id="appt' +
                                row.id +
                                '" class="appt-no-height">' +
                                row.numarInmatriculare +
                                ' | ' +
                                row.numeClient +
                                ' | ' +
                                row.numarTelefon +
                                ' | ' +
                                row.serviciu +
                                warningSpan +
                                ' </div>';

                            apptRow.append(stringToAppend);
                            var objHeight = (row.durataInMinute * oneMinutePxHeight) + 'px';
                            var marginTop = (row.startTime_Minutes * oneMinutePxHeight) + 'px';
                            var marginLeft = ((apptRow.children().length - 1) * marginLeftPerItem) + 'px';

                            var thisAppt = $("#appt" + row.id);
                            thisAppt.css("height", objHeight);
                            //thisAppt.css("margin-top", marginTop);
                            thisAppt.css("margin-left", marginLeft);

                            if (row.confirmed) {
                                thisAppt.css("background-color", 'lightgreen');
                            }
                            if (!row.confirmed && row.changedByClient) {
                                thisAppt.css("background-color", '#ff7b42');
                            }
                        }
                    });
            };

            /* click event for editing an appt*/
            $('.appt-no-height').on('click',
                function (event) {
                    // dont allow the event to propagate to hour click.
                    event.stopPropagation();
                    var confirm = $(this).attr("data-confirm");
                    var fullId = $(this).attr("id").replace('appt', '');
                    apptMenuModalDialog(fullId, confirm);
                });

            /* scroll to a better position */
            scrollToWorkingHours();
        });
};



/* click event on the hour */
$('.hour').on('click',
    function (event) {

        event.stopPropagation();
        var self = $(this);

        if (!$(this).hasClass('noclick')) {
            var startHourOnly = $(this).attr("class").replace('hour-red', '');
            startHourOnly = startHourOnly.replaceAll("hour", "");

            var selectedDateDiv = $('#datepicker');
            var rampNr = self.closest(".ramp").attr("data-value");
            // Make the modal here!
            console.log("add on click");
            apptNewModalDialog(selectedDateDiv.val(), startHourOnly, rampNr);
        }
    });


/* Get the appointments for whatever date is selected */
var getAppointmentsForSelectedDate = function () {
    var selectedDateDiv = $('#datepicker');
    var parts = selectedDateDiv.val();
    showAppointmentsForDate(parts);
};
$("#datepicker").on("change",
    function () {
        clearAppointments();
        getAppointmentsForSelectedDate();
        addNoClick();
    });


var deleteApptModalDialog = function (id) {

    var url = "/Appointments/DeleteModal"; // the url to the controller
    $.get(url + '?id=' + id,
        function (data) {
            CloseModalById('myApptMenuModal');
            $('#partialInfoDelete').html(data);
            preOpenModal();
            $('#myApptDeleteModal').modal('show');
            makeModalScrollable("myApptDeleteModal");
        });
};

$(document).on('click',
    '#deleteButton',
    function (event) {
        // dont allow the event to propagate
        event.stopPropagation();
        var id = $(this).attr("data-value");
        deleteApptModalDialog(id);
    });

var infoApptModalDialog = function (id) {

    var url = "/Appointments/InfoModal"; // the url to the controller
    $.get(url + '?id=' + id,
        function (data) {
            CloseModalById('myApptMenuModal');
            $('#partialInfoInformatii').html(data);
            preOpenModal();
            $('#myApptInfoModal').modal('show');
            makeModalScrollable("myApptInfoModal");
        });
};

$(document).on('click',
    '#infoButton',
    function (event) {
        // dont allow the event to propagate
        event.stopPropagation();
        var id = $(this).attr("data-value");
        infoApptModalDialog(id);
    });

var confirmApptModalDialog = function (id, confirm) {

    var url = "/Appointments/ConfirmModal"; // the url to the controller
/*    console.log("confirmed is: " + confirm);*/
    $.get(url + '?id=' + id + '&confirm=' + confirm,
        function (data) {
            CloseModalById('myApptMenuModal');
            $('#partialInfoConfirm').html(data);
            preOpenModal();
            $('#myApptConfirmModal').modal('show');
            makeModalScrollable("myApptConfirmModal");
        });
};

$(document).on('click',
    '#confirmButton',
    function (event) {

        // dont allow the event to propagate
        event.stopPropagation();
        var id = $(this).attr("data-value");
        var confirm = $(this).attr("data-confirm");
        confirmApptModalDialog(id, confirm);
    });

$(document).on('click',
    '#editButton',
    function (event) {
        // dont allow the event to propagate
        event.stopPropagation();
        var id = $(this).attr("data-value");
        apptEditModalDialog(id);
    });

$(document).on("change",
    ".changeableDateTimeRamp",
    function () {
        checkDateAndTimeAvailable();
    });

$(document).on("change",
    ".changeable", function () {
        var saveBtn = $(document).find(".btn-ok");
        $(saveBtn).attr("disabled", false);
    });


$(document).on('select2:select',
    "#Durata",
    function (e) {
        var data = e.params.data;
        var durataval = $(document).find("#durataValue");
        $(durataval).attr("data-value", data.id);
        $(durataval).trigger("change");
    });


var checkDateAndTimeAvailable = function () {
    var datepickerVal = $(document).find("#datepickerModal").val();
    var hour = $(document).find("#StartTime_Hour").val();
    var min = $(document).find("#StartTime_Minutes").val();
    var time = hour + "_" + min;
    var appt = $(document).find("#apptIdForGetSpans");
    var apptId = null;
    if (appt != undefined) {
        apptId = $(appt).attr("data-value");
    }
    var durata = $(document).find("#durataValue").attr("data-value");
    var rampId = $(document).find("#RampId").val();
    var span = $(document).find("#availableHourSpan");

    var url = "/Appointments/GetAvailableSpans"; // the url to the controller
    $.get(url +
        '?startDateStr=' +
        datepickerVal +
        '&startHour=' +
        time +
        '&duration=' +
        durata +
        '&rampId=' +
        rampId +
        "&apptId=" +
        apptId,
        function (data) {
            span.text("").removeClass("text-danger").removeClass("text-success");
            span.addClass("hide");
            var selectieBuna = $(document).find("#selectieBunaElem");

/*            console.log("selectie before: ", selectieBuna.val());*/
            $(selectieBuna).val(data.selectedIsAvailable);
/*            console.log("selectie after: ", selectieBuna.val());*/

            if (data.selectedIsAvailable) {

                var spanSelectie = $(document).find("#selectieBunaElem").closest('.' + "form-group").find('.error_span');
                spanSelectie.text("").addClass("hide");

                span.text(
                    "Selectia de data si ora este buna! Nu se suprapune cu alta programare!")
                    .removeClass("text-danger").addClass("text-success");
                span.removeClass("hide");
            } else {
                var message = "Selectia nu este buna!";
                if (data.availableSpans.length > 0) {
                    message +=
                        " Pentru durata selectata, urmatoarele ore sunt disponibile:";
                    $.each(data.availableSpans,
                        function (index, value) {
                            message = message + " " + value;
                        });
                }
                span.text(message).addClass("text-danger").removeClass("text-success");
                span.removeClass("hide");
            }
        });
};

$(document).ready(function () {
    addNoClick();
});


var addNoClick = function () {
    var dateFromPage = $("#datepicker").val();
    var datePage = new Date(dateFromPage);
    var date = $.datepicker.formatDate('yy/mm/dd', datePage);
    var today = $.datepicker.formatDate('yy/mm/dd', new Date());
    var hours = $(document).find(".hour");
    $.each(hours, function (key, data) {
        $(data).removeClass("noclick");

    })
    if (today > date) {
        $.each(hours, function (key, data) {
            $(data).addClass("noclick");
        })
    }
};

var ondownload = function () {
    var dateFromPage = $("#datepicker").val();
    var datePage = new Date(dateFromPage);
    var date = $.datepicker.formatDate('yy/mm/dd', datePage);
    var url = "/Appointments/Download?date=" + date;
    window.location.href = url;
}



// document ready 
// getDate from datetime picker
// check date with today
// if lower add class noclick to all "hour" classes
