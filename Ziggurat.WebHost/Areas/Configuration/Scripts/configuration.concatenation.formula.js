$(function() {
    function FormulaModel() {
        var model = this;

        this.parts = ko.observableArray([]);
        this.newConstantPart = ko.observable();
        this.newPropertyPart = ko.observable();

        this.addNewPart = function () {
            var constant = model.newConstantPart();
            alert(model.newConstantPart());
        };
    };

    var view = $('#concatenationFormula')[0];
    var model = new FormulaModel();

    ko.applyBindings(model, view);
});