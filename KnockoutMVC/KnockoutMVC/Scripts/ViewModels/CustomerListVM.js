var urlPath = window.location.pathname;
$(function () {
    ko.applyBindings(CustomerListVM);
    CustomerListVM.getCustomers();
});

//View Model
var CustomerListVM = {
    Customers: ko.observableArray([]),
    getCustomers: function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: '/Customer/FetchCustomers',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Customers(data); //Put the response in ObservableArray
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    },
};

self.editCustomer = function (customer) {
    window.location.href = '/Customer/Edit/' + customer.CustomerId;
};
self.deleteCustomer = function (customer) {
    window.location.href = '/Customer/Delete/' + customer.CustomerId;
};

//Model
function Customers(data) {
    this.CustomerId = ko.observable(data.CustomerId);
    this.CustomerName = ko.observable(data.CustomerName);
    this.ContactName = ko.observable(data.ContactName);
    this.Address = ko.observable(data.Address);
    this.City = ko.observable(data.City);
    this.PostalCode = ko.observable(data.PostalCode);
    this.Country = ko.observable(data.Country);
}