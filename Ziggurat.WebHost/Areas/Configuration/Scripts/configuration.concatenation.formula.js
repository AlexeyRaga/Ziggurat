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

    FormulaModel.prototype.formula = ko.observableArray();

    FormulaModel.prototype.newConstantPart = ko.observable();

    FormulaModel.prototype.newPropertyPart = ko.observable();

    FormulaModel.prototype.newPropertyName = ko.observable();

    FormulaModel.prototype.addNewPart = function() {
      var constant, property, propertyText;
      constant = this.newConstantPart();
      property = this.newPropertyPart();
      if ((constant != null) && constant !== '') {
        this.parts.push(constant);
        this.formula.push.constant;
      }
      if ((property != null) && property !== '') {
        propertyText = $('#propList option[value=' + property + ']').text();
        this.formula.push('[' + propertyText + ']');
        this.parts.push('[' + property + ']');
      }
      this.newConstantPart('');
      this.newPropertyPart('');
      return true;
    };

    return FormulaModel;

  })();
  view = $("#concatenationFormula")[0];
  model = new FormulaModel();
  return ko.applyBindings(model, view);
});
