var anvelopeFormValidationRules = [
    {
        ruleName: "numeClientRequired",
        ruleForElementId: "#NumeClient",
        check: "required",
        comparerValue: true,
        message: "Va rugam introduceti numele clientului!"
    },
    {
        ruleName: "numarInmatriculareRequired",
        ruleForElementId: "#NumarInmatriculare",
        check: "required",
        comparerValue: true,
        message: "Va rugam introduceti numarul de inmatriculare!"
    },
    {
        ruleName: "serieSasiuRequired",
        ruleForElementId: "#SerieSasiu",
        check: "required",
        comparerValue: true,
        message: "Va rugam introduceti seria de sasiu!"
    },
    {
        ruleName: "numarInmatriculareRegex",
        ruleForElementId: "#NumarInmatriculare",
        check: "regex",
        comparerValue: /^(\d|\w){6,15}$/,
        message: "Numarul de inmatriculare nu este valid. Introduceti un numar de inmatriculare valid!"
    },
    {
        ruleName: "serieSasiuRegex",
        ruleForElementId: "#SerieSasiu",
        check: "regex",
        comparerValue: /^(\d|\w){17}$/,
        message: "Seria de sasiu nu este valida. Introduceti o serie de sasiu valida!"
    },
    {
        ruleName: "numarTelefonRequired",
        ruleForElementId: "#NumarTelefon",
        check: "required",
        comparerValue: true,
        message: "Va rugam introduceti numarul de telefon!"
    },
    {
        ruleName: "numarTelefonRegex",
        ruleForElementId: "#NumarTelefon",
        check: "regex",
        comparerValue: /^((\+|)\d{1,3}( |-)?)?((\(\d {3}\))|\d{3})[- .]?\d{3}[- .]?\d{4}$|^((\+|)\d{1,3}( )?)?(\d{3}[ ]?){2}\d{3}$|^(\+\d{1,3}( )?)?(\d{3}[ ]?)(\d{2}[ ]?){2}\d{2}$|^(\(?\d{1,3}\)?)(( *)-|\/| )(\d{1,3}(-*)(\d{1,5}))$|^(\+?)((\d{2,})(-)( *)){1,4}(\d{1,5})$/g,
        message: "Numarul introdus nu este valid. Introduceti un numar de telefon valid!"
    }
];

//var uzuraRules = [
//    {
//        ruleName: "stangaFataRange",
//        ruleForElementId: "#StangaFata",
//        check: "range",
//        comparerValue: [1, 8],
//        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
//    },
//    {
//        ruleName: "dreaptaFataRange",
//        ruleForElementId: "#DreaptaFata",
//        check: "range",
//        comparerValue: [1, 8],
//        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
//    },
//    {
//        ruleName: "stangaSpateRange",
//        ruleForElementId: "#StangaSpate",
//        check: "range",
//        comparerValue: [1, 8],
//        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
//    },
//    {
//        ruleName: "dreaptaSpateRange",
//        ruleForElementId: "#DreaptaSpate",
//        check: "range",
//        comparerValue: [1, 8],
//        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
//    }
//];

var uzuraStFRules = [
    {
        ruleName: "stangaFataRange",
        ruleForElementId: "#StangaFata",
        check: "range",
        comparerValue: [1, 8],
        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
    }
];

var uzuraDrFRules = [
    {
        ruleName: "dreaptaFataRange",
        ruleForElementId: "#DreaptaFata",
        check: "range",
        comparerValue: [1, 8],
        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
    }
];

var uzuraStSRules = [
    {
        ruleName: "stangaSpateRange",
        ruleForElementId: "#StangaSpate",
        check: "range",
        comparerValue: [1, 8],
        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
    }
];

var uzuraDrSRules = [
    {
        ruleName: "dreaptaSpateRange",
        ruleForElementId: "#DreaptaSpate",
        check: "range",
        comparerValue: [1, 8],
        message: "Va rugam introduceti o valoare cuprinsa intre 1 si 8!"
    }
];
