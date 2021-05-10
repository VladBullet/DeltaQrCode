
function CloseModalById(id) {
    var stringid = '#' + id;
    $(stringid).modal('toggle');
    afterModalClose();
};

$('.modal').on('hidden.bs.modal', function (event) {
    afterModalClose();
});

var preOpenModal = function () {
    var $body = $(document.body);
    var oldWidth = $body.innerWidth();
    $body.css("overflow", "hidden");
    $body.width(oldWidth);
};

var afterModalClose = function () {
    var $body = $(document.body);
    $body.css("overflow", "auto");
    $body.width("auto");
};

var makeModalScrollable = function (id) {
    var modal = $(document).find("#" + id);
    $(modal).css("overflow", "auto");
}

function Uzura() {
    var self = this;
    var nrBuc = 1;
    var stF = true;
    var drF = false;
    var stS = false;
    var drS = false;

    Object.defineProperty(this,
        'nrBuc',
        {
            get: function () {
                return nrBuc;
            }
        });

    Object.defineProperty(this,
        'stF',
        {
            get: function () {
                return stF;
            }
        });

    Object.defineProperty(this,
        'drF',
        {
            get: function () {
                return drF;
            }
        });

    Object.defineProperty(this,
        'stS',
        {
            get: function () {
                return stS;
            }
        });


    Object.defineProperty(this,
        'drS',
        {
            get: function () {
                return drS;
            }
        });

    this.activateField = function (field) {
        switch (field) {
            case "DreaptaFata":
                {
                    drF = true;
                    nrBuc += 1;
                    break;
                }
            case "StangaSpate":
                {
                    stS = true;
                    nrBuc += 1;
                    break;
                }
            case "DreaptaSpate":
                {
                    drS = true;
                    nrBuc += 1;
                    break;
                }
            default:
                break;
        };
    };

    this.deactivateAll = function () {
        drf = false;
        stS = false;
        drS = false;
        nrBuc = 1;
    };

    this.deactivate = function (field) {
        switch (field) {
            case "DreaptaFata":
                {
                    drF = false;
                    nrBuc -= 1;
                    break;
                }
            case "StangaSpate":
                {
                    stS = false;
                    nrBuc -= 1;
                    break;
                }
            case "DreaptaSpate":
                {
                    drS = false;
                    nrBuc -= 1;
                    break;
                }
            default:
                break;
        };
    };
}

function download(file, img) {

    //creating an invisible element
    var element = document.createElement('a');
    element.setAttribute('href', img);
    element.setAttribute('download', file);

    document.body.appendChild(element);

    //onClick property
    element.click();

    document.body.removeChild(element);
};
var showLoading = function () {
    $(".overlay").show();
    $(".spinner").show();
};
var hideLoading = function () {
    $(".overlay").hide();
    $(".spinner").hide();
};
