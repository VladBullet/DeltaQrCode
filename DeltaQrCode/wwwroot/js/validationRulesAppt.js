var apptFormValidationRules = [
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
        ruleName: "numarInmatriculareRegex",
        ruleForElementId: "#NumarInmatriculare",
        check: "regex",
        comparerValue: /^(\d|\w){6,15}$/,
        message: "Numarul de inmatriculare nu este valid. Introduceti un numar de inmatriculare valid!"
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
    },
    {
        ruleName: "emailClientRequired",
        ruleForElementId: "#EmailClient",
        check: "required",
        comparerValue: true,
        message: "Va rugam introduceti adresa de email!"
    },
    {
        ruleName: "emailClientRequired",
        ruleForElementId: "#EmailClient",
        check: "regex",
        comparerValue: /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/,
        message: "Adresa de email nu este valida. Introduceti o adresa de email valida!"
    },
    {
        ruleName: "rampIdRange",
        ruleForElementId: "#RampId",
        check: "range",
        comparerValue: [1,4],
        message: "Va rugam introduceti o rampa existenta[1,2,3,4]!"
    },  {
        ruleName: "selectieDataEquals",
        ruleForElementId: "#selectieBunaElem",
        check: "equals",
        comparerValue: true,
        message: "Va rugam introduceti selectati un interval liber!"
    },
];