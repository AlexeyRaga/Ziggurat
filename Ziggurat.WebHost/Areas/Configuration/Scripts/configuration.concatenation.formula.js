var __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

$(function() {
  var FormulaModel, model, view;
  FormulaModel = (function() {

    function FormulaModel() {
      this.removePart = __bind(this.removePart, this);

      var _this = this;
      this.parts = ko.observableArray();
      this.newConstantPart = ko.observable();
      this.newPropertyPart = ko.observable();
      this.formula = ko.computed(function() {
        return _this.parts().map(function(item) {
          return item.text;
        }).join('');
      });
    }

    FormulaModel.prototype.addNewPart = function() {
      var constant, part, property, propertyText;
      constant = this.newConstantPart();
      property = this.newPropertyPart();
      if ((constant != null) && constant !== '') {
        part = {
          value: constant,
          text: constant
        };
        this.parts.push(part);
      }
      if ((property != null) && property !== '') {
        propertyText = $("#propList option[value=" + property + "]").text();
        part = {
          value: "{{" + property + "}}",
          text: "[" + propertyText + "]"
        };
        this.parts.push(part);
      }
      this.newConstantPart('');
      this.newPropertyPart('');
      return true;
    };

    FormulaModel.prototype.removePart = function(data) {
      return this.parts.remove(data);
    };

    return FormulaModel;

  })();
  view = $("#concatenationFormula")[0];
  model = new FormulaModel();
  return ko.applyBindings(model, view);
});
