/**
 * @requires JABB 0.1 or higher
 */
(function (window, undefined) {
	var document = window.document;
	function StivaApp(options) {
		if (!(this instanceof StivaApp)) {
			return new StivaApp(options);
		}
		this.options = {};
		this.preload = null;
		this.services = null;
		this.basket = null;
		this.date = null;
		this.version = "1.0";
		this.getVersion = function () {
			return this.version;
		};
		this.init(options);
		return this;
	}
	
	StivaApp.prototype = {
		bindServices: function () {
			var i, len, btn, self = this;	
						
			btn = JABB.Utils.getElementsByClass("SBScript_Event_Button", self.services, "input");
			for (i = 0, len = btn.length; i < len; i++) {
				btn[i].onclick = function (inst) {
					return function (e) {
						inst.loadService(this.getAttribute("rev"));
					}
				}(self);
			}
		},
		
		bindService: function () {
			var self = this,
				span = JABB.Utils.getElementsByClass("t-available", self.services, "span"),
				rel;
			for (var i = 0, len = span.length; i < len; i++) {
				span[i].onclick = function () {
					if (JABB.Utils.hasClass(this, "t-selected")) {
						JABB.Utils.removeClass(this, "t-selected");
						rel = "_" + this.getAttribute("rel");
					} else {
						var block = JABB.Utils.getElementsByClass("t-block", self.services, "span");
						for (var j = 0, blen = block.length; j < blen; j++) {
							JABB.Utils.removeClass(block[j], "t-selected");
						}
						JABB.Utils.addClass(this, "t-selected");
						rel = this.getAttribute("rel");
					}
					
					if (typeof rel != 'undefined') {
						var qs = ["&cid=", self.options.calendar_id].join(""), 
							post;
						if (rel.indexOf("_") !== 0) {
							post = ["date=", self.date, "&start_ts=", rel.split("|")[0], "&end_ts=", rel.split("|")[1], "&service_id=", rel.split("|")[2], "&employee_id=", rel.split("|")[3]].join("");
							JABB.Ajax.sendRequest(self.options.get_add_cart_url + qs, function (req) {
								if (self.options.checkout == 0) {
									var form = document.getElementById("frmUpdateBooking");
									if (form) {
										form.submit();
										$.blockUI({
											theme: true,
											title: $("#dialogBlockUI").attr("title"),
											message: $("#dialogBlockUI").html()
										}); 
									}
								}
							}, post);
						} else {
							post = ["date=", self.date, "&service_id=", rel.split("|")[2]].join("");
							JABB.Ajax.sendRequest(self.options.get_remove_cart_url + qs, function (req) {
								
							}, post);
						}
						self.loadServices();
						self.loadCart();
					}
					
					return false;
				};
			}
		},
		
		bindCart: function () {
			var i, len, arr, btn, self = this;		
			// Add "click", "mouseover" and "mouseout" event to rows in Basket(Cart)
			var arr = JABB.Utils.getElementsByClass("SBScript_Slot", document.getElementById(self.options.container_basket), "input");
			for (i = 0, len = arr.length; i < len; i++) {
				arr[i].onclick = function () {
					var rel = this.getAttribute("rel").split("|");
					JABB.Ajax.sendRequest(self.options.get_remove_cart_url + "&cid=" + self.options.calendar_id, function(req){
						//self.loadBookingForm(JABB.Utils.serialize(document.forms[self.options.booking_form_name]));
						self.loadCart();
					}, ["date=" + rel[0], "&service_id=", rel[1]].join(""));				
				};
			}
			
			btn = JABB.Utils.getElementsByClass('SBScript_Button_Proceed', self.basket, "input");
			for (i = 0, len = btn.length; i < len; i++) {
				btn[i].onclick = function () {
					this.disabled = true;
					//self.triggerLoading('message_5', self.options.container_services);
					self.triggerPreload();
					self.loadBookingForm();
				};
			}
		},
		
		bindClose: function () {
			var i, len, self = this,
			btnClose = JABB.Utils.getElementsByClass(this.options.events_close, document.getElementById(this.options.container_services), "a");
			// Add "click" event to Close link
			for (i = 0, len = btnClose.length; i < len; i++) {
				btnClose[i].onclick = function () {
					//self.triggerLoading('message_1', self.options.container_services);
					self.triggerPreload();
					self.loadServices();
					return false;
				};
			}
		},
		
		bindBookingForm: function () {
			var self = this;
			// Add "change" event to Payment method combo box
			if (document.forms[self.options.booking_form_name] && document.forms[self.options.booking_form_name][self.options.booking_form_payment_method]) {
				JABB.Utils.addEvent(document.forms[self.options.booking_form_name][self.options.booking_form_payment_method], "change", function () {
					// if there will be any credit card option...
					if (self.options.cc_data_flag) {
						var bookingForm = document.forms[self.options.booking_form_name],
							$ccData = JABB.Utils.getElementsByClass(self.options.cc_data_wrapper, bookingForm, null),
							$related = JABB.Utils.getElementsByClass("SBScript_PriceRelated", bookingForm, null),
							$value = this.options[this.selectedIndex].value;
						
						if ($value == 'creditcard') {
							// show the credit cards fields
							for (var i = 0, len = $ccData.length; i < len; i++) {
								$ccData[i].style.display = "";
							}
							for (var i = 0, len = $related.length; i < len; i++) {
								$related[i].style.display = "";
							}
							
							// for each field add a requered class name
							for (var i = 0, len = self.options.cc_data_names.length; i < len; i++) {
								JABB.Utils.addClass(document.forms[self.options.booking_form_name][self.options.cc_data_names[i]], 'SBScript_Required');
							}
						} else {
							// hide the credit cards fields
							for (var i = 0, len = $ccData.length; i < len; i++) {
								$ccData[i].style.display = "none";
							}
							
							// for each field remove the requered class name
							for (var i = 0, len = self.options.cc_data_names.length; i < len; i++) {
								JABB.Utils.removeClass(document.forms[self.options.booking_form_name][self.options.cc_data_names[i]], 'SBScript_Required');
							}
						}
					}
				});
			}
			
			//Add "click" event to Submit button
			if (document.forms[self.options.booking_form_name] && document.forms[self.options.booking_form_name][self.options.booking_form_submit_name]) {
				JABB.Utils.addEvent(document.forms[self.options.booking_form_name][self.options.booking_form_submit_name], "click", function (event) {
					var $this = this;
					$this.disabled = true;
					if (!self.validateBookingForm($this)) {
						return;
					}
					if ($this.form.captcha) {
						JABB.Ajax.getJSON(self.options.get_booking_captcha_url + "&captcha=" + $this.form.captcha.value, function (json) {
							switch (json.code) {
							case 100:
								self.errorHandler('\n' + self.options.validation.error_captcha);
								$this.disabled = false;
								break;
							case 200:
								self.loadBookingSummary(JABB.Utils.serialize(document.forms[self.options.booking_form_name]));
								self.triggerPreload();
								//self.triggerLoading('message_4', self.options.container_services);							
								break;
							}
						});
					} else {
						self.loadBookingSummary(JABB.Utils.serialize(document.forms[self.options.booking_form_name]));
						self.triggerPreload();
						//self.triggerLoading('message_4', self.options.container_services);					
					}				
				});
			}
			//Add "click" event to Cancel button
			if (document.forms[self.options.booking_form_name] && document.forms[self.options.booking_form_name][self.options.booking_form_cancel_name]) {
				JABB.Utils.addEvent(document.forms[self.options.booking_form_name][self.options.booking_form_cancel_name], "click", function (event) {
					this.disabled = true;
					//self.triggerLoading('message_1', self.options.container_services);
					self.triggerPreload();
					self.loadServices();				
				});
			}
		},
		
		bindBookingSummary: function (post) {
			var self = this;
			//Add "click" event to Submit button
			if (document.forms[self.options.booking_summary_name] && document.forms[self.options.booking_summary_name][self.options.booking_summary_submit_name]) {
				JABB.Utils.addEvent(document.forms[self.options.booking_summary_name][self.options.booking_summary_submit_name], "click", function (event) {
					var $this = this;
					$this.disabled = true;
					if (!self.validateBookingSummary($this)) {
						return;
					}
					JABB.Ajax.postJSON(self.options.get_booking_save_url + "&cid=" + self.options.calendar_id, function (json) {
						switch (json.code) {
						case 100:
							self.errorHandler('\n' + self.options.message_8);
							$this.disabled = false;
							break;
						case 200:
							self.loadPaymentForm(json);
							break;
						}																								
					}, post + "&" + JABB.Utils.serialize(document.forms[self.options.booking_summary_name]) + "&calendar_id=" + self.options.calendar_id);
				});
			}
			//Add "click" event to Cancel button
			if (document.forms[self.options.booking_summary_name] && document.forms[self.options.booking_summary_name][self.options.booking_summary_cancel_name]) {
				JABB.Utils.addEvent(document.forms[self.options.booking_summary_name][self.options.booking_summary_cancel_name], "click", function (event) {
					this.disabled = true;
					//self.triggerLoading('message_2', self.options.container_services);
					self.triggerPreload();
					self.loadBookingForm(post);
				});
			}
		},
		
		loadServices: function (opts) {
			var o = {};
			if (typeof opts !== "undefined") {
				o = opts;
			}
			if (o.date) {
				this.date = o.date;
			}
			var self = this,
				qs = ["&cid=", self.options.calendar_id, "&date=", self.date].join("");
			JABB.Ajax.sendRequest(this.options.get_services_url + qs, function (req) {
				self.services.innerHTML = "";
				self.preload.style.display = 'none';
				self.services.innerHTML = req.responseText;
				self.bindServices();
			});
		},
		
		loadService: function (service_id) {
			var self = this,
				qs = ["&cid=", self.options.calendar_id, "&date=", self.date, "&service_id=", service_id].join("");
			JABB.Ajax.sendRequest(self.options.get_service_url + qs, function (req) {
				self.services.innerHTML = "";
				self.preload.style.display = 'none';
				self.services.innerHTML = req.responseText;
				self.bindService();
			});
		},
		
		loadBookingForm: function (post) {
			var self = this,
				qs = ["&cid=", self.options.calendar_id].join("");
			JABB.Ajax.sendRequest(self.options.get_booking_form_url + qs, function (req) {
				self.services.innerHTML = '';
				self.preload.style.display = 'none';
				self.services.innerHTML = req.responseText;
				self.bindBookingForm();
				self.bindClose();
				self.loadCart();
			}, post);
		},
		
		loadCart: function () {
			var self = this;
			JABB.Ajax.sendRequest([self.options.get_cart_url, "&cid=", self.options.calendar_id, "&checkout=", self.options.checkout].join(""), function (req) {
				document.getElementById(self.options.container_basket).innerHTML = req.responseText;
				self.bindCart();
				self.bindClose();						
			});
		},
		
		loadBookingSummary: function (post) {
			var self = this,
				qs = ["&cid=", self.options.calendar_id].join("");
			JABB.Ajax.sendRequest(self.options.get_booking_summary_url + qs, function (req) {
				self.services.innerHTML = '';
				self.preload.style.display = 'none';
				self.services.innerHTML = req.responseText;
				self.bindBookingSummary(post);
				self.bindClose();
			}, post);
		},
		
		loadPaymentForm: function (obj) {
			var self = this,
			qs = "&cid=" + self.options.calendar_id,
			bs, div;
			JABB.Ajax.sendRequest(self.options.get_payment_form_url + qs, function (req) {
				bs = document.forms[self.options.booking_summary_name];
				if (bs && bs.parentNode) {
					div = document.createElement("div");
					div.innerHTML = req.responseText;
					bs.parentNode.insertBefore(div, bs);
					
					if (typeof document.forms[self.options.payment[obj.payment]] != 'undefined') {
						document.forms[self.options.payment[obj.payment]].submit();
					} else {
						self.triggerLoading('message_7', self.options.container_services);
					}
				}
				
			}, "id=" + obj.booking_id);
		},
		
		validateBookingForm: function (btn) {
			var re = /([0-9a-zA-Z\.\-\_]+)@([0-9a-zA-Z\.\-\_]+)\.([0-9a-zA-Z\.\-\_]+)/,
				message = "";
			
			var frm = document.forms[this.options.booking_form_name];
			for (var i = 0, len = frm.elements.length; i < len; i++) {
				var cls = frm.elements[i].className;
				if (cls.indexOf("SBScript_Required") !== -1 && frm.elements[i].disabled === false) {
					switch (frm.elements[i].nodeName) {
					case "INPUT":
						switch (frm.elements[i].type) {
						case "checkbox":
						case "radio":
							if (!frm.elements[i].checked && frm.elements[i].getAttribute("rev")) {
								message += "\n - " + frm.elements[i].getAttribute("rev"); 
							}
							break;
						default:
							if (frm.elements[i].value.length === 0 && frm.elements[i].getAttribute("rev")) {
								message += "\n - " + frm.elements[i].getAttribute("rev");
							}
							break;
						}
						break;
					case "TEXTAREA":
						if (frm.elements[i].value.length === 0 && frm.elements[i].getAttribute("rev")) {						
							message += "\n - " + frm.elements[i].getAttribute("rev");
						}
						break;
					case "SELECT":
						switch (frm.elements[i].type) {
						case 'select-one':
							if (frm.elements[i].value.length === 0 && frm.elements[i].getAttribute("rev")) {
								message += "\n - " + frm.elements[i].getAttribute("rev"); 
							}
							break;
						case 'select-multiple':
							var has = false;
							for (j = frm.elements[i].options.length - 1; j >= 0; j = j - 1) {
								if (frm.elements[i].options[j].selected) {
									has = true;
									break;
								}
							}
							if (!has && frm.elements[i].getAttribute("rev")) {
								message += "\n - " + frm.elements[i].getAttribute("rev");
							}
							break;
						}
						break;
					default:
						break;
					}
				}
				if (cls.indexOf("SBScript_Email") !== -1) {
					if (frm.elements[i].nodeName === "INPUT" && frm.elements[i].value.length > 0 && frm.elements[i].value.match(re) == null) {
						message += "\n - " + this.options.validation.error_email;
					}
				}
			}
			
			if (JABB.Utils.getElementsByClass("SBScript_Slot", document.getElementById(this.options.container_slots), "INPUT").length === 0) {
				message += "\n - " + this.options.validation.error_min;
			}
			
			if (message.length === 0) {
				return true;
			} else {
				this.errorHandler(message);		
				btn.disabled = false;
				return false;
			}
		},
		
		validateBookingSummary: function (btn) {
			var pass = true,
				message = "\n" + this.options.validation.error_payment;
			
			if (pass) {
				return true;
			} else {
				this.errorHandler(message);		
				btn.disabled = false;
				return false;
			}
		},
	
		errorHandler: function (message) {
			var err = JABB.Utils.getElementsByClass("SBScript_Error", document.forms[this.options.booking_form_name], "P");
			if (err[0]) {
				err[0].innerHTML = '<span></span>' + this.options.validation.error_title + message.replace(/\n/g, "<br />");
				err[0].style.display = '';
			} else {
				alert(this.options.validation.error_title + message);
			}
		},
		
		triggerLoading: function (message, container) {
			document.getElementById(container).innerHTML = this.options[message];
		},
		
		triggerPreload: function () {
			var self = this;
			self.preload.style.height = self.services.scrollHeight + "px";
			self.services.innerHTML = '';
			self.preload.style.display = '';
		},
	
		init: function (calObj) {
			var self = this;
			self.options = calObj;
			self.date = self.options.today;
			self.preload = document.getElementById(self.options.preload);
			self.services = document.getElementById(self.options.container_services);
			self.basket = document.getElementById(self.options.container_basket);
			
			var calendar = new Calendar({
				inline: true,
				element: self.options.datepicker,
				startDay: self.options.start_day,
				dateFormat: self.options.date_format,
				onSelect: function (elem, formated, date, cell) {
					self.date = formated;
					self.triggerPreload();
					self.loadServices();
				}
			});
			
			self.loadServices();
			self.loadCart();
		}
	}
	return (window.StivaApp = StivaApp);
})(window);