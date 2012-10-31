(function ($) {
	$(function () {		
		$(".ebc-table tr.pointer").live("click", function () {
			if ($(this).hasClass("handle")) {
				return;
			}
			var meta = $(this).metadata();
			if (meta.id) {
				window.location.href = ['index.php?controller=AdminBookings&action=update&id=', meta.id].join("");
			}
		});		
		if ($('#frmUpdateBooking').length > 0) {
			
			var vOpts = {
				errorContainer: $("#errContainer")
			};
			
			$("#payment_method").live("change", function () {
				if ($(this).val() == 'creditcard') {
					$(".cc").show();
				} else {
					$(".cc").hide();
				}
			});
		
			$('#frmUpdateBooking').validate(vOpts);
			
			$("#btnAddService").click(function (e) {
				$("#boxScript").toggle();
			});
		}
		
		var tOpts = {
			select: function (event, ui) {
				if (ui.index === 0) {
					window.location.href = 'index.php?controller=AdminBookings&action=schedule';
				}
			}
		};
		if ($("#tabs").length > 0) {
			$("#tabs").tabs(tOpts);
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
						var $this = $(this);
						$.ajax({
							type: "POST",
							data: {
								id: $this.data("id")
							},
							url: "index.php?controller=AdminBookings&action=delete",
							success: function (res) {
								$("#content").html(res);
								$("#tabs").tabs(tOpts).tabs("option", "selected", 1);
								
								$.ajax({
									type: "GET",
									url: "index.php?controller=Front&action=load&cid=" + $this.data("cid") + "&" + Math.floor(Math.random() * 999999),
									dataType: "html", 
									success: function (data) {
										data = data.replace("document.writeln('", "");
										data = data.replace("');", "");
										$("#tabs-3").html(data);
									}
								});
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
		if ($("#dialogEmail").length > 0) {
			$("#dialogEmail").dialog({
				autoOpen: false,
				resizable: false,
				draggable: false,
				width:600,
				height:480,
				modal: true,
				open: function () {
					var $this = $(this);
					$.get("index.php?controller=AdminBookings&action=reSend", {"id": $this.data("id")}, function (data) {
						$this.html(data);
					});
				},
				buttons: {
					'Send': function() {
						var $this = $(this);
						$.ajax({
							type: "POST",
							data: {
								id: $this.data("id"),
								message: $this.find("textarea").val()
							},
							url: "index.php?controller=AdminBookings&action=reSend",
							success: function (data) {

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
		if ($("#dialogDeleteBookedService").length > 0) {
			$("#dialogDeleteBookedService").dialog({
				autoOpen: false,
				resizable: false,
				draggable: false,
				width:320,
				height:240,
				modal: true,
				buttons: {
					'Delete': function() {
						var $this = $(this);
						$.post("index.php?controller=AdminBookings&action=deleteBookedService", {
							id: $this.data("id")
						}).success(function (data) {
							$.get("index.php?controller=AdminBookings&action=getBookedServices", {
								booking_id: $this.data("booking_id")
							}).success(function (data) {
								$("#boxServices").html(data);
							});
						});
						$(this).dialog('close');			
					},
					'Cancel': function() {
						$(this).dialog('close');
					}
				}
			});
		}
		$("#content").delegate(".datepick", "focusin", function () {
			$(this).attr("autocomplete", "off").datepicker({
				dateFormat: $(this).attr("rev"),
				firstDay: $(this).attr("rel")
			});
		}).delegate("a.icon-delete", "click", function (e) {
			e.preventDefault();
			$('#dialogDelete').data("id", $(this).attr('rel')).data("cid", $(this).attr("rev")).dialog('open');
			return false;
		}).delegate("a.icon-del", "click", function (e) {
			e.preventDefault();
			$('#dialogDeleteBookedService').data("id", $(this).attr('rel')).data("booking_id", $(this).attr('rev')).dialog('open');
			return false;
		}).delegate("a.icon-email", "click", function (e) {
			e.preventDefault();
			$('#dialogEmail').data("id", $(this).attr('rel')).dialog('open');
			return false;
		}).delegate("#service_id", "change", function () {
			$.get("index.php?controller=AdminBookings&action=getEmployees", {
				service_id: $("option:selected", $(this)).val(),
				employee_id: $("#employee_id option:selected").val()
			}).success(function (data) {
				$("#boxEmployee").html(data);
			});
		}).delegate("#employee_id", "change", function () {
			$.get("index.php?controller=AdminBookings&action=getServices", {
				employee_id: $("option:selected", $(this)).val(),
				service_id: $("#service_id option:selected").val()
			}).success(function (data) {
				$("#boxService").html(data);
			});
		}).delegate("#date", "change", function () {
			$(this).parent().submit();
		});
		if ($("#date").length > 0) {
			var rel = $("#date").attr("rel");
			$("#date").datepicker({
				dateFormat: rel.split("|")[1],
				firstDay: rel.split("|")[0],
				gotoCurrent: true,
				onSelect: function (dateText, inst) {
					$.get("index.php?controller=AdminBookings&action=getSchedule", {
						"date": dateText
					}).success(function (data) {
						$("#boxSchedule").html(data);
					});
				}
			}).datepicker("setDate", rel.split("|")[2]);
		}
	});
})(jQuery);