var Rules = [
    { ruleName: "", ruleForElementId: "", check: "required", comparerValue: "", messge: "This filed is required! Please insert a value!" },
    { ruleName: "", ruleForElementId: "", check: "regex", comparerValue: "", messge: "This filed doesn't match the correct" },
    {
        ruleName: "", ruleForElementId: "", check: "range", comparerValue: [1, 3], messge: "This filed is required! Please insert a value!"
    },
    { ruleName: "", ruleForElementId: "", check: "equals", comparerValue: "3", messge: "This filed is required! Please insert a value!" }
];

var CheckTypes = {
    required: "required",
    regex: "regex",
    range: "range",
    equals: "equals"
};

var Checks = {

    required: function (elementId) {
        var isValid = false;
        var element = $(document).find(elementId);
        var elementValue = element.val();
        if (element.length != 0) {
            if (elementValue != NaN && elementValue != "") {
                isValid = true;
            }
        }
        return isValid;
    },
    regex: function (elementId, regexString) {
        try {
            var isValid = false;
            var regex = new RegExp(regexString);
            var element = $(document).find(elementId);
            var elementValue = element.val();
            if (element.length != 0) {
                if (elementValue != NaN && elementValue.length != 0 && elementValue.match(regexString)) {
                    //throw new Error("Please pass a correct regex! Don't use quotes at the start and end!");      
                    isValid = true;
                }
            }
            return isValid;
        } catch (e) {
            console.log("Something went wrong! Make sure pass a correct regex! Don't use quotes at the start and end of the pattern!", e);
        }

    },
    range: function (elementId, range) {
        var isValid = false;
        var minRange = range[0];
        var maxRange = range[1];
        var element = $(document).find(elementId);
        var elementValue = element.val();
        var nrValue = parseInt(elementValue);
        if (element.length != 0) {
            if (elementValue != NaN && elementValue.length != 0 && nrValue != NaN && minRange <= nrValue && nrValue <= maxRange) {
                isValid = true;
            }
        }
        return isValid;
    },
    equals: function (elementId, compareValue) {
        var isValid = false;
        var element = $(document).find(elementId);
        var elementValue = element.val();
        if (element.length > 0) {
            if (elementValue != NaN && compareValue != NaN && elementValue.length != 0 && elementValue == compareValue) {
                isValid = true;
            }
        }
        return isValid;
    }
};
// ------------------- HOW TO USE Checks   ---------------
// ------------------- --------------- IDEA  - MAKE IT USE "NAME" INSTEAD OF ELEMENT_ID

//var equals = Checks.equals;
//var id = "#test";
//var value = "t";
//var result = equals.call($(this), id, value);

var addCustomValidationRule = function (ruleName, ruleForElementId, check, comparerValue, message) {
    try {
        var foundRule = Rules.find(x => x.ruleName == ruleName);
        if (foundRule) {
            throw new Error("Rule '" + ruleName + "' already exists! Please use a different name!");
        }
        Rules.push({ ruleName: ruleName, ruleForElementId: ruleForElementId, check: check, comparerValue: comparerValue, messge: message });
    } catch (e) {
        console.log(e);
    }

};

var removeCustomValidationRule = function (ruleName) {
    try {
        Rules = $.grep(Rules, function (e) {
            return e.ruleName != ruleName;
        });
    } catch (e) {
        console.log(e);
    }
    //or use this

    //var foundRule = Rules.find(x => x.ruleName == ruleName);
    //if (foundRule) {
    //    var searchedIndex = $.inArray(foundRule, Rules);
    //    if (searchedIndex >= 0) {
    //        Rules = Rules.slice(searchedIndex, 1);
    //        return;
    //    }
    //}
};

var ValidateRules = function (rules) {
    var results = [];
    $.each(rules, function (ruleName, ruleForElementId, check, comparerValue,message) {
        var method = Checks[check];
        var isValid = method.call($(this), ruleForElementId, comparerValue);
        var result = { elementId: ruleForElementId, isValid: isValid, message: message };
        results.push(result);
    });
    var formIsValid = true;
    for (var i = 0; i < results.length; i++) {
        if (!results[i].isValid) {
            formIsValid = false;
            break;
        }
    }
    var model = { formIsValid: formIsValid, validationResults: results }
};