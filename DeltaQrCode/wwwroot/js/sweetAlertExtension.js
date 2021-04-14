
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

var swalErrorTimer = function (message, footerText, timer, callbackFunctionAfterClose = null) {
    Swal.fire({
        title: 'Ooops..',
        html: message,
        footer: footerText,
        icon: 'error',
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
}