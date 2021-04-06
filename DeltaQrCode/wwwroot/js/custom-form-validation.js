var CustomValidation = function (rules) {
     var self = this;
    this.Rules = [];

    if (rules.length > 0) {
        this.Rules = rules;
    }

    //this.CheckTypes = {};
    //Object.defineProperty(CheckTypes, "required", {
    //    value: "required",
    //    writable: false,
    //    enumerable: true,
    //    configurable: true
    //}).defineProperty(CheckTypes, "regex", {
    //    value: "regex",
    //    writable: false,
    //    enumerable: true,
    //    configurable: true
    //}).defineProperty(CheckTypes, "range", {
    //    value: "range",
    //    writable: false,
    //    enumerable: true,
    //    configurable: true
    //}).defineProperty(CheckTypes, "equals", {
    //    value: "equals",
    //    writable: false,
    //    enumerable: true,
    //    configurable: true
    //});

    this.Checks = {

        required: function (elementId, comparer) {
            if (!comparer) {
                return true;
            }
            var isValid = false;
            var element = $(document).find(elementId);
            var elementValue = element.val();
            if (element.length != 0) {
                if (elementValue != NaN && elementValue != "" && elementValue.length != 0) {
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

    this.addCustomValidationRule = function (addRule) {
        try {
            var foundRule = this.Rules.find(x => x.ruleName == addRule.ruleName);
            if (foundRule) {
                throw new Error("Rule '" + addRule.ruleName + "' already exists! Please use a different name!");
            } else {
                this.Rules.push({ ruleName: addRule.ruleName, ruleForElementId: addRule.ruleForElementId, check: addRule.check, comparerValue: addRule.comparerValue, message: addRule.message });
            }
        } catch (e) {
            console.log(e);
        }

    };
    this.addcustomValidationRules = function (rules) {
        try {
            $.each(rules, function (index,rule) {
                self.addCustomValidationRule(rule);
            });
        } catch (e) {
            console.log(e);
        }

    };

    this.removeCustomValidationRule = function (ruleName) {
        try {
            this.Rules = $.grep(this.Rules, function (r) {
                return r.ruleName != ruleName;
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

    this.validate = function (validator) {
        var results = [];
        $.each(validator.Rules, function (index,rule) {
            var method = validator.Checks[rule.check];
            var isValid = method.call($(this), rule.ruleForElementId, rule.comparerValue);
            if (!isValid) {
                var result = { elementId: rule.ruleForElementId, isValid: isValid, message: rule.message };
                results.push(result);
            }
        });
        var formIsValid = true;
        if (results.length > 0) {
            formIsValid = false;
        }
        // OLD CODE - DON'T DELETE YET
        //for (var i = 0; i < results.length; i++) {
        //    if (!results[i].isValid) {
        //        formIsValid = false;
        //        break;
        //    }
        //}
        var model = { formIsValid: formIsValid, validationResults: results };
        return model;
    };
};