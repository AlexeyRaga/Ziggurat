$ ->
    class FormulaModel
        constructor: ->
            @parts = ko.observableArray()
            @newConstantPart = ko.observable()
            @newPropertyPart = ko.observable()
    
            @formula = ko.computed =>
                @parts()
                    .map((item) -> item.text)
                    .join('')
                    
    
        addNewPart: ->
            constant = @newConstantPart()
            property = @newPropertyPart()
            if constant? and constant != ''
              part = { value: constant, text: constant }
              @parts.push part
            if property? and property != ''
              propertyText = $("#propList option[value=#{property}]").text();
              part = { value: "{{#{property}}}", text: "[#{propertyText}]" }
              @parts.push part
    
            @newConstantPart('')
            @newPropertyPart('')
            true
    
        removePart: (data) =>
          @parts.remove data

    view = $("#concatenationFormula")[0]
    model = new FormulaModel()

    ko.applyBindings model, view