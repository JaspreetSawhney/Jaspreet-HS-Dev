(function ($) {
	$(function () {
		$(".ebc-table tbody tr").hover(
			function () {
				$(this).addClass("hover");
			}, 
			function () {
				$(this).removeClass("hover");
			}
		);
		if ($("#ebc-calendar-id").length > 0) {
			$("#ebc-calendar-id").change(function () {
				if ($(this).val() != "") {
					window.location.href = 'index.php?controller=AdminCalendars&action=view&cid=' + $(this).val();
				}
			});
		}
	});
})(jQuery);