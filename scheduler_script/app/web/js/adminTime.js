(function ($) {
	$(function () {
		$(".working_day").click(function () {
			var checked = $(this).is(":checked"),
				$tr = $(this).closest("tr");
			$tr.find("select, input[type='text']").attr("disabled", checked);
		});
		
		if ($("#tabs").length > 0) {
			$("#tabs").tabs();
		}
		
		if ($("#frmTimeCustom").length > 0) {
			$("#frmTimeCustom").validate();
		}
		$("#content").delegate(".datepick", "focusin", function () {
			$(this).datepicker({
				firstDay: $(this).attr("rel"),
				dateFormat: $(this).attr("rev")
			});
		});
		
		$("a.icon-delete").live("click", function (e) {
			e.preventDefault();
			$('#dialogDelete').data("id", $(this).attr('rel')).dialog('open');
		});
		
		if ($("#dialogDelete").length > 0) {
			$("#dialogDelete").dialog({
				autoOpen: false,
				resizable: false,
				draggable: false,
				height:220,
				modal: true,
				buttons: {
					'Delete': function() {
						$.ajax({
							type: "POST",
							data: {
								id: $(this).data("id")
							},
							url: "index.php?controller=AdminTime&action=delete",
							success: function (res) {
								$("#tabs-2").html(res);
							}
						});
						$(this).dialog('close');			
					},
					'Cancel': function() {
						$(this).dialog('close');
					}
				}
			});
		}
		
	});
})(jQuery);