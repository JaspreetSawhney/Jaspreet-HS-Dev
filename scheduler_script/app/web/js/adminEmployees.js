(function ($) {
	$(function () {
		if ($('#frmCreateEmployee').length > 0) {
			$('#frmCreateEmployee').validate();
		}
		if ($('#frmUpdateEmployee').length > 0) {
			$('#frmUpdateEmployee').validate();
		}
		
		$("a.icon-delete").live("click", function (e) {
			e.preventDefault();
			$('#dialogDelete').data('id', $(this).attr('rel')).dialog('open');
		});
		
		if ($("#tabs").length > 0) {
			$("#tabs").tabs();
		}
		
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
							url: "index.php?controller=AdminEmployees&action=delete",
							success: function (res) {
								$("#content").html(res);
								$("#tabs").tabs();
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