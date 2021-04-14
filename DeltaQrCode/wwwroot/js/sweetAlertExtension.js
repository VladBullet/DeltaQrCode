
var swalSuccessTimer = function (message, footerText, timer, callbackFunctionAfterClose = null) {
    Swal.fire({
        title: 'Vesti bune!',
        html: message,
        footer: footerText,
        icon: 'success',
        timer: timer,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading();
        },
        willClose: () => {
            if (callbackFunctionAfterClose != null && typeof callbackFunctionAfterClose == 'function') {
                callbackFunctionAfterClose.call();
            }
        }
    });
};

var swalErrorTimer = function (message, timer, callbackFunctionAfterClose = null) {
    Swal.fire({
        title: 'Ooops..',
        html: message,
        footer: "Acest mesaj se va inchide automat!",
        icon: 'error',
        confirmButtonText: "Inchide",
        timer: timer,
        timerProgressBar: true,
        didOpen: () => {
        },
        willClose: () => {
            if (callbackFunctionAfterClose != null && typeof callbackFunctionAfterClose == 'function') {
                callbackFunctionAfterClose.call();
            }
        }
    });
};

var swalError = function (message, callbackFunctionAfterClose = null) {
    Swal.fire({
        title: 'Ooops..',
        html: message,
        icon: 'error',
        willClose: () => {
            if (callbackFunctionAfterClose != null && typeof callbackFunctionAfterClose == 'function') {
                callbackFunctionAfterClose.call();
            }
        }
    });
};