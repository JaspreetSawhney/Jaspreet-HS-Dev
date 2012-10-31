(function ($) {
	$(function () {		
		if ($('#frmCreateService').length > 0) {
			$('#frmCreateService').validate();
		}
		if ($('#frmUpdateService').length > 0) {
			$('#frmUpdateService').validate();
		}
		if ($('#frmCreateService').length > 0 || $('#frmUpdateService').length > 0) {
			$()
		}
		$("#content").delegate("#length, #before, #after", "change", function () {
			var total = 0,
				len_gth = parseInt($("#length").val(), 10),
				before = parseInt($("#before").val(), 10),
				after = parseInt($("#after").val(), 10);
			if (!isNaN(len_gth)) {
				total += len_gth;
			}
			if (!isNaN(before)) {
				total += before;
			}
			if (!isNaN(after)) {
				total += after;
			}
			$("#total").val(total);
		});
	
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
							url: "index.php?controller=AdminServices&action=delete",
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