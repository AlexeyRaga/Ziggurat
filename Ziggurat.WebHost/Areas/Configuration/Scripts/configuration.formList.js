$(function () {
	$('#formsList').sortable({
		items: 'li:not(.nav-header)',
		update: function (e, element) {
			var item = element.item;
			var formId = item.data('id');
			var header = item.prevAll('.nav-header:first');
			var positionInHeader = item.index() - header.index() - 1;
			var headerText = header.text();
			updatePosition(formId, headerText, positionInHeader);
		}
	});

	function updatePosition(id, header, position) {
		$.post(
			'/Configuration/ProjectLayout/ChangeFormPosition',
			{ FormId: id, Header: header, Position: position }
		).error(function (response) { alert('Cannot report the position change'); });
	}
});