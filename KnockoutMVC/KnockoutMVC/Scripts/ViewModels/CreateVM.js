var urlPath = window.location.pathname;
$(function () {
    ko.applyBindings(CreateVM);
});
var CreateVM = ko.validatedObservable({
    
    CustomerId: ko.observable().extend({ required: true, maxLength: 10 }),
    CustomerName: ko.observable().extend({ required: true, maxLength: 50 }),
    ContactName: ko.observable().extend({ required: true, maxLength: 50 }),
    Address: ko.observable().extend({ required: true, maxLength: 50 }),
    City: ko.observable().extend({ required: true, maxLength: 50 }),
    PostalCode: ko.observable().extend({ required: true, maxLength: 10 }),
    Country: ko.observable().extend({ required: true, maxLength: 50 }),
    SaveCustomer: function () {
        if (!(CreateVM.isValid())) {

            errors = ko.validation.group(CreateVM);
            errors.showAllMessages();
        }
        else {
            $.ajax({
                url: '/Customer/Create',
                type: 'post',
                dataType: 'json',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                },
                error: function (err) {
                    if (err.responseText == "Creation Failed")
                    { window.location.href = '/Customer/Index/'; }
                    else {
                        alert("Status:" + err.responseText);
                        window.location.href = '/Customer/Index/';;
                    }
                },
                complete: function () {
                    window.location.href = '/Customer/Index/';
                }
            });
        }
    }
});
