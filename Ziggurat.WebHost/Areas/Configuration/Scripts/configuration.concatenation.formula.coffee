$ ->
  class FormulaModel
    model = this

    parts           : ko.observableArray()
    formula         : ko.observableArray()	
    newConstantPart : ko.observable()
    newPropertyPart : ko.observable()
    newPropertyName : ko.observable()

    addNewPart: =>
      constant = @newConstantPart()
      property = @newPropertyPart()
      if constant? and constant != ''
        @parts.push constant
        @formula.push.constant
      if property? and property != ''
        propertyText = $('#propList option[value='+ property + ']').text();
        @formula.push '['+propertyText+']'
        @parts.push '['+property+']'
      @newConstantPart('')
      @newPropertyPart('')
      true

  view = $("#concatenationFormula")[0]
  model = new FormulaModel()

  ko.applyBindings model, view