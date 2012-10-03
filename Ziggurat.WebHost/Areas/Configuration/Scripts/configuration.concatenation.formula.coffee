$ ->
    class FormulaModel
        constructor: (@formId, @propertyId, existingProps) ->
            @parts = ko.observableArray(existingProps)
            @newConstantPart = ko.observable()
            @newPropertyPart = ko.observable()                    
    
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
    
            @newConstantPart ''
            @newPropertyPart ''
            @reportFormulaChanges()
    
        removePart: (data) =>
            @parts.remove data
            @reportFormulaChanges()

        reportFormulaChanges: =>
            values = @parts().map((item) -> item.value)
            formulaContext = {formId: @formId, propertyId: @propertyId, values: values}
            #formulaContext = JSON.stringify(formulaContext)
            $.ajax '/Configuration/Property/SetConcatenationFormula',
                type: 'POST',
                traditional: true,
                data: formulaContext,
                error: (data, status, xhr) -> alert xhr
            true

    view = $("#concatenationFormula")[0]
    allProps = $("#propList", view)
        .find('option')
        .filter((i, item) -> item.value)
        .map((i, item) -> { value: item.value, text: item.text });

    context = $(view).data()

    model = new FormulaModel(context.formid, context.propertyid, initConcatenationFormulaParts)

    ko.applyBindings model, view