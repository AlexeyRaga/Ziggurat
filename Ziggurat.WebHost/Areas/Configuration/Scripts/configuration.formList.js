$(function () {
	$('#formsList').sortable({
	    items: 'li',
        cancel: 'li.nav-header',
		update: function (e, element) {
			var item = element.item;
			var formId = item.data('id');
			var header = item.prevAll('.nav-header:first');
			var positionInHeader = item.index() - header.index() - 1;
			var headerText = header.text();
			updatePosition(formId, headerText, positionInHeader);
		}
	});

	$('#addBlockHeader').click(function () {
	    var input = $('#newHeaderText');
	    var newHeader = input.val();
	    newHeader = $.trim(newHeader);
	    if (newHeader == '') return;

	    var toFind = newHeader.toLowerCase();

	    var known = getKnownHeaders();
	    if ($.inArray(toFind, known) !== -1) {
	        alert('Header "' + newHeader + '" already exists');
	    } else {
	        $('#formsList').append('<li class="nav-header">' + newHeader + '</li>');
	    }
	    input.val('');
	});

	function getKnownHeaders() {
	    return $('#formsList')
            .find('li.nav-header')
            .map(function (i, elem) { return $(elem).text().toLowerCase(); })
            .toArray();
	}

	function updatePosition(id, header, position) {
		$.post(
			'/Configuration/ProjectLayout/ChangeFormPosition',
			{ FormId: id, Header: header, Position: position }
		).error(function (response) { alert('Cannot report the position change'); });
	}
});