function Validator(options) {

    var selectorRules = {};
    //the function executes the error and discards the error
    function Validate(inputElement, rule) {
        var errorMessage;
        var errorElement = inputElement.parentElement.querySelector(options.errorSelector);
        //get the selector's own rules
        var rules = selectorRules[rule.selector];
        //iterate through each rule and check
        for (var i = 0; i < rules.length; ++i) {
            errorMessage = rules[i](inputElement.value);
            //If there is an error, use the check
            if (errorMessage)
                break;
            ;
        }

        if (errorMessage) {
            errorElement.innerText = errorMessage;
            inputElement.parentElement.classList.add('invalid');
        } else {
            errorElement.innerText = '';
            inputElement.parentElement.classList.remove('invalid');
        }
        return  !errorMessage;
    }

    //get the element of the form to validate
    var formElement = document.querySelector(options.form);
    if (formElement) {
        //when submit form
        formElement.onsubmit = function (e) {
            e.preventDefault();

            var isFormValid = true;

            options.rules.forEach(function (rule) {
                var inputElement = formElement.querySelector(rule.selector);
                var isValid = Validate(inputElement, rule);
                if (!isValid) {
                    isFormValid = false;
                }
            });
            if (isFormValid) {
                formElement.submit();
            }
        }

        //iterate through each rule and process
        options.rules.forEach(function (rule) {

            //save the rules every time you input
            if (Array.isArray(selectorRules[rule.selector])) {
                selectorRules[rule.selector].push(rule.test);
            } else {
                selectorRules[rule.selector] = [rule.test];
            }

            var inputElement = formElement.querySelector(rule.selector);

            if (inputElement) {
                //handle every time the user clicks outside the input
                inputElement.onblur = function () {
                    //get the information in the input box when the user clicks outside the input box
                    // check validate information in the input
                    Validate(inputElement, rule);
                }

                //handle every time user input
                inputElement.oninput = function () {
                    var errorElement = inputElement.parentElement.querySelector(options.errorSelector);
                    errorElement.innerText = '';
                    inputElement.parentElement.classList.remove('invalid');
                }
            }
        });


    }
}
//rules of the inputs
Validator.isRequired = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            return value.trim() ? undefined : message
        }
    };
}

Validator.isEmail = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : message
        }
    };
}

Validator.isPass = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$/;
            return value.match(regex) ? undefined : message
        }
    };
}

Validator.isConfirmedPass = function (selector, getConfirValue){
    return {
        selector: selector,
        test: function (value){
            return value === getConfirValue() ? undefined : 'Passwords does not match';
        }
    }
}
Validator.isCheckPass = function (selector, getConfirValue, message){
    return {
        selector: selector,
        test: function (value){
            return value === getConfirValue() ? undefined : message;
        }
    }
}
Validator.isCompareDate = function (selector, getStartDateValue, message) {
    return {
        selector: selector,
        test: function (value) {
            return value >= getStartDateValue() ? undefined : message;
        }
    }
}

Validator.isPhone = function (selector, min) {
    return {
        selector: selector,
        test: function (value) {
            return value.length <= min ? undefined : `Please enter less than or equal to ${min} numeric digits`;
        }
    };
}