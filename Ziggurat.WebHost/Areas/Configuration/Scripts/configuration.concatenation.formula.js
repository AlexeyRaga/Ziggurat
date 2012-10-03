var __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

$(function() {
  var FormulaModel, allProps, context, model, view;
  FormulaModel = (function() {

    function FormulaModel(formId, propertyId, existingProps) {
      this.formId = formId;
      this.propertyId = propertyId;
      this.reportFormulaChanges = __bind(this.reportFormulaChanges, this);

      this.removePart = __bind(this.removePart, this);

      this.parts = ko.observableArray(existingProps);
      this.newConstantPart = ko.observable();
      this.newPropertyPart = ko.observable();
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
      return this.reportFormulaChanges();
    };

    FormulaModel.prototype.removePart = function(data) {
      this.parts.remove(data);
      return this.reportFormulaChanges();
    };

    FormulaModel.prototype.reportFormulaChanges = function() {
      var formulaContext, values;
      values = this.parts().map(function(item) {
        return item.value;
      });
      formulaContext = {
        formId: this.formId,
        propertyId: this.propertyId,
        values: values
      };
      $.ajax('/Configuration/Property/SetConcatenationFormula', {
        type: 'POST',
        traditional: true,
        data: formulaContext,
        error: function(data, status, xhr) {
          return alert(xhr);
        }
      });
      return true;
    };

    return FormulaModel;

  })();
  view = $("#concatenationFormula")[0];
  allProps = $("#propList", view).find('option').filter(function(i, item) {
    return item.value;
  }).map(function(i, item) {
    return {
      value: item.value,
      text: item.text
    };
  });
  context = $(view).data();
  model = new FormulaModel(context.formid, context.propertyid, initConcatenationFormulaParts);
  return ko.applyBindings(model, view);
});
