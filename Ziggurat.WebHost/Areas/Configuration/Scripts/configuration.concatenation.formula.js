var __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

$(function() {
  var FormulaModel, model, view;
  FormulaModel = (function() {
    var model;

    function FormulaModel() {
      this.addNewPart = __bind(this.addNewPart, this);

    }

    model = FormulaModel;

    FormulaModel.prototype.parts = ko.observableArray();

    FormulaModel.prototype.newConstantPart = ko.observable();

    FormulaModel.prototype.newPropertyPart = ko.observable();

    FormulaModel.prototype.addNewPart = function() {
      var constant;
      constant = this.newConstantPart();
      if ((constant != null) && constant !== '') {
        return this.parts.push(constant);
      }
    };

    return FormulaModel;

  })();
  view = $("#concatenationFormula")[0];
  model = new FormulaModel();
  return ko.applyBindings(model, view);
});
