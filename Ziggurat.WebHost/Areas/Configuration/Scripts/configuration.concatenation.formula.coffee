$ ->
  class FormulaModel
    model = this

    parts           : ko.observableArray()
    newConstantPart : ko.observable()
    newPropertyPart : ko.observable()

    addNewPart: =>
      constant = @newConstantPart()
      if constant? and constant != ''
        @parts.push constant

  view = $("#concatenationFormula")[0]
  model = new FormulaModel()

  ko.applyBindings model, view