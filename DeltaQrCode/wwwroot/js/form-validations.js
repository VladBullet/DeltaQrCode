////$.validator.addMethod(
////    "regex",
////    function (value, element, regexp) {
////        return this.optional(element) || regexp.test(value);
////    },
////    "Formatul nu este acceptat. Te rog corecteaza campul!"
////);

////// Initialize form validation
////$("#apptform").validate({
////    // Specify validation rules
////    rules: {
        
////        NumeClient: "required",
////        NumarInmatriculare: {
////            required: true,
////            regex: /^([A-Za-z]{1,2})(\d{2,3})([A-Za-z]{3})/g
////        },
////        NumarTelefon: {
////            required: true,
////            regex:
////                /(\+\d{1,3}\s?)?((\(\d{3}\)\s?)|(\d{3})(\s|-?))(\d{3}(\s|-?))(\d{4})(\s?(([E|e]xt[:|.|]?)|x|X)(\s?\d+))?/g
////        },
////        NrBucati: {
////            required: true,
////            range: [1, 4]
////        },
////        Diametru: {
////            required: true
////        },
////        Latime: {
////            required: true
////        },
////        Inaltime: {
////            required: true
////        },
////        Status: { requred: true },
////    },
////    // Specify validation error messages
////    messages: {
////        NumeClient: "Va rugam introduceti numele clientului!",
////        NumarInmatriculareLabel: {
////            required: "Va rugam introduceti numarul de inmatriculare!",
////            regex: "Numarul introdus nu este valid. Introduceti un numar de inmatriculare valid!"
////        },
////        NumarTelefon: {
////            required: "Va rugam introduceti numarul de telefon!",
////            regex: "Numarul introdus nu este valid. Introduceti un numar de telefon valid!"
////        },
////        NrBucati: {
////            required: "Va rugam introduceti numarul de bucati!",
////            range: "Puteti alege valori intre 1 si 4!"
////        },
////        Diametru: "Va rugam introduceti diametrul anvelopelor!",
////        Latime: "Va rugam introduceti latimea anvelopelor !",
////        Inaltime: "Va rugam introduceti inaltimea anvelopelor !",
////        Status: "Va rugam selectati status-ul anvelopelor !",
////    },
////    highlight: function (element) {
////        $(element).closest('.form-group').addClass('has-error');
////    },
////    unhighlight: function (element) {
////        $(element).closest('.form-group').removeClass('has-error');
////    },
////    errorElement: 'span',
////    errorClass: 'help-block',
////    errorPlacement: function (error, element) {
////        if (element.parent('.input-group').length) {
////            error.insertAfter(element.parent());
////        } else {
////            error.insertAfter(element);
////        }
////    }
////    // might need this in order to make it work 
////    //,
////    //submitHandler: function (form) {
////    //    $("#btn-salvarmyModal").addClass('bloqueia');
////    //    var dados = jQuery('#form_myModal').serialize();
////    //    alert($("#exigenciamyModal").val());
////    //    form.submit();
////});


////var addRules = function (rulesObj) {
////    for (var item in rulesObj) {
////        $('#' + item).rules('add', rulesObj[item]);
////    }
////};

////var removeRules = function (rulesObj) {
////    for (var item in rulesObj) {
////        $('#' + item).rules('remove');
////    }
////};

////var validateUzura = function (value) {
////    if (value == 1) {
////        // clear all rules
////        removeRules(stangaFataRules);
////        removeRules(dreaptaFataRules);
////        removeRules(stangaSpateRules);
////        removeRules(dreaptaSpate);

////        // add correct rules
////        addRules(stangaFataRules);
////    }
////    if (value == 2) {
////        // clear all rules
////        removeRules(stangaFataRules);
////        removeRules(dreaptaFataRules);
////        removeRules(stangaSpateRules);
////        removeRules(dreaptaSpate);

////        // add correct rulesv
////        addRules(stangaFataRules);
////        addRules(dreaptaFataRules);

////    }
////    if (value == 3) {
////        // clear all rules
////        removeRules(stangaFataRules);
////        removeRules(dreaptaFataRules);
////        removeRules(stangaSpateRules);
////        removeRules(dreaptaSpate);
////        // add correct rules
////        addRules(stangaFataRules);
////        addRules(dreaptaFataRules);
////        addRules(stangaSpateRules);
////    }
////    if (value == 4) {
////        // clear all rules
////        removeRules(stangaFataRules);
////        removeRules(dreaptaFataRules);
////        removeRules(stangaSpateRules);
////        removeRules(dreaptaSpate);
////        // add correct rules
////        addRules(stangaFataRules);
////        addRules(dreaptaFataRules);
////        addRules(stangaSpateRules);
////        addRules(dreaptaSpate);
////    }
////}