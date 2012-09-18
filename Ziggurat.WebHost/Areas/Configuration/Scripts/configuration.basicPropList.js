$(function () {
    $('#propList').on('click', '.btn',
    function () {
        var button = $(this);
        var formId = $('#propList').data('formId');
        var isUsed = !button.hasClass('active');
        var row = button.closest('tr');
        var propertyId = row.data('id');

        row.toggleClass('error', !isUsed);
        row.toggleClass('normal', isUsed);

        var data = { formId: formId, propertyId: propertyId };

        if (isUsed) {
            $.post('/Configuration/Property/MakeUsed', data)
            .error(function () { alert('Unable to comply'); });
        } else {
            $.post('/Configuration/Property/MakeUnused', data)
            .error(function () { alert('Unable to comply'); });
        }

    });
});