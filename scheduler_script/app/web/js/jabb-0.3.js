var JABB = JABB || {};
JABB.version = "0.3";
JABB.Ajax = {
	onStart: null,
	onStop: null,
	onError: null,
	XMLHttpFactories: [
		function () {return new XMLHttpRequest()},
		function () {return new ActiveXObject("Msxml2.XMLHTTP")},
		function () {return new ActiveXObject("Msxml3.XMLHTTP")},
		function () {return new ActiveXObject("Microsoft.XMLHTTP")}
	],
	sendRequest: function (url, callback, postData) {
		var req = this.createXMLHTTPObject();
		if (!req) {
			return;
		}
		var method = (postData) ? "POST" : "GET";
		var calledOnce = false;
		req.open(method, url, true);
		//Refused to set unsafe header "User-Agent"
		//req.setRequestHeader('User-Agent', 'XMLHTTP/1.0');
		req.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
		if (postData) {
			req.setRequestHeader('Content-type','application/x-www-form-urlencoded');
		}
		req.onreadystatechange = function () {
			switch (req.readyState) {
			case 1:
				if (!calledOnce) {
					JABB.Ajax.onAjaxStart();
					calledOnce = true;
				}
			break;
			case 2:
				return;
			break;
			case 3:
				return;
			break;
			case 4:
				JABB.Ajax.onAjaxStop();
				if (req.status == 200) {
					callback(req);
				} else {
					JABB.Ajax.onAjaxError();
				}
				delete req;
			break;
			}/*
			if (req.readyState != 4) {
				return;
			}
			if (req.status != 200 && req.status != 304) {
				return;
			}
			callback(req);*/
		};
		if (req.readyState == 4) {
			return;
		}
		req.send(postData);
	},
	onAjaxStart: function () {
		if (typeof this.onStart == 'function') {
			this.onStart();
		}
	},
	onAjaxStop: function () {
		if (typeof this.onStop == 'function') {
			this.onStop();
		}
	},
	onAjaxError: function () {
		if (typeof this.onError == 'function') {
			this.onError();
		}
	},
	createXMLHTTPObject: function () {
		var xmlhttp = false;
		for (var i = 0; i < this.XMLHttpFactories.length; i++) {
			try {
				xmlhttp = this.XMLHttpFactories[i]();
			}
			catch (e) {
				continue;
			}
			break;
		}
		return xmlhttp;
	},
	getJSON: function (url, callback) {
		this.sendRequest(url, function (req) {
			callback(eval("(" + req.responseText + ")"));
		});
	},
	postJSON: function (url, callback, postData) {
		this.sendRequest(url, function (req) {
			callback(eval("(" + req.responseText + ")"));
		}, postData);
	}, 
	get: function (url, container_id) {
		this.sendRequest(url, function (req) {
			document.getElementById(container_id).innerHTML = JABB.Utils.parseScript(req.responseText);
		});
	},
	post: function (url, container_id, postData) {
		this.sendRequest(url, function (req) {
			document.getElementById(container_id).innerHTML = JABB.Utils.parseScript(req.responseText);
		}, postData);
	}
};
JABB.Utils = {
	addClass: function (ele, cls) {
		if (!this.hasClass(ele, cls)) {
			ele.className += " " + cls;
		}
	},
	hasClass: function (ele, cls) {
		return ele.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
	},
	removeClass: function (ele, cls) {
		if (this.hasClass(ele, cls)) {
			var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
			ele.className = ele.className.replace(reg, ' ');
		}
	},
	importCss: function (cssFile) {
		if (document.createStyleSheet) {
			document.createStyleSheet(cssFile);
		} else {
			var styles = "@import url(" + cssFile + ");";
			var newSS = document.createElement('link');
			newSS.rel = 'stylesheet';
			newSS.href = 'data:text/css,' + escape(styles);
			document.getElementsByTagName("head")[0].appendChild(newSS);
		}
	},
	importJs: function (jsFile) {
		var d = window.document;
		if (d.createElement) {
			var js = d.createElement("script");
			js.type = "text/javascript";
			js.src = jsFile;
			if (js) {
				d.getElementsByTagName("head")[0].appendChild(js);
			}
		}
	},
	getElementsByClass: function (searchClass, node, tag) {
		var classElements = new Array();
		if (node == null) {
			node = document;
		}
		if (tag == null) {
			tag = '*';
		}
		var els = node.getElementsByTagName(tag);
		var elsLen = els.length;
		var pattern = new RegExp("(^|\\s)"+searchClass+"(\\s|$)");
		for (var i = 0, j = 0; i < elsLen; i++) {
			if (pattern.test(els[i].className)) {
				classElements[j] = els[i];
				j++;
			}
		}
		return classElements;
	},
	addEvent: function (obj, type, fn) {
		if (obj.addEventListener) {
			obj.addEventListener(type, fn, false);
		} else if (obj.attachEvent) {
			obj["e" + type + fn] = fn;
			obj[type + fn] = function() { obj["e" + type + fn](window.event); };
			obj.attachEvent("on" + type, obj[type + fn]);
		} else {
			obj["on" + type] = obj["e" + type + fn];
		}
	},
	fireEvent: function (element, event) {
		if (!element) return false;
		if (document.createEventObject) {
			// dispatch for IE
			var evt = document.createEventObject();
			return element.fireEvent('on' + event, evt);
		} else {
			// dispatch for firefox + others
			var evt = document.createEvent("HTMLEvents");
			evt.initEvent(event, true, true); // event type,bubbling,cancelable
			return !element.dispatchEvent(evt);
		}
	},
	serialize: function (form) {
		if (!form || form.nodeName !== "FORM") {
			return undefined;
		}
		var i, j, q = [];
		for (i = form.elements.length - 1; i >= 0; i = i - 1) {
			if (form.elements[i].name === "") {
				continue;
			}
			switch (form.elements[i].nodeName) {
			case 'INPUT':
				switch (form.elements[i].type) {
				case 'text':
				case 'hidden':
				case 'password':
				case 'button':
				case 'reset':
				case 'submit':
					q.push(form.elements[i].name + "=" + encodeURIComponent(form.elements[i].value));
					break;
				case 'checkbox':
				case 'radio':
					if (form.elements[i].checked) {
						q.push(form.elements[i].name + "=" + encodeURIComponent(form.elements[i].value));
					}						
					break;
				case 'file':
					break;
				}
				break; 
			case 'TEXTAREA':
				q.push(form.elements[i].name + "=" + encodeURIComponent(form.elements[i].value));
				break;
			case 'SELECT':
				switch (form.elements[i].type) {
				case 'select-one':
					q.push(form.elements[i].name + "=" + encodeURIComponent(form.elements[i].options[form.elements[i].selectedIndex].value));
					break;
				case 'select-multiple':
					for (j = form.elements[i].options.length - 1; j >= 0; j = j - 1) {
						if (form.elements[i].options[j].selected) {
							q.push(form.elements[i].name + "=" + encodeURIComponent(form.elements[i].options[j].value));
						}
					}
					break;
				}
				break;
			case 'BUTTON':
				switch (form.elements[i].type) {
				case 'reset':
				case 'submit':
				case 'button':
					q.push(form.elements[i].name + "=" + encodeURIComponent(form.elements[i].value));
					break;
				}
				break;
			}
		}
		return q.join("&");
	},
	extend: function (obj, args) {
		var i;
		for (i in args) {
			obj[i] = args[i];
		}
		return obj;
	},
	createElement: function (element) {
		if (typeof document.createElementNS != 'undefined') {
			return document.createElementNS('http://www.w3.org/1999/xhtml', element);
		}
		if (typeof document.createElement != 'undefined') {
			return document.createElement(element);
		}
		return false;
	},
	getEventTarget: function (e) {
		var targ;
		if (!e) {
			e = window.event;
		}
		if (e.target) {
			targ = e.target;
		} else if (e.srcElement) {
			targ = e.srcElement;
		}
		if (targ.nodeType == 3) {
			targ = targ.parentNode;
		}	
		return targ;
	},
	parseScript: function (_source) {
		var source = _source,
			scripts = [];
		while (source.indexOf("<script") > -1 || source.indexOf("</script") > -1) {
			var s = source.indexOf("<script");
			var s_e = source.indexOf(">", s);
			var e = source.indexOf("</script", s);
			var e_e = source.indexOf(">", e);
			scripts.push(source.substring(s_e+1, e));
			source = source.substring(0, s) + source.substring(e_e+1);
		}
		for (var i = 0; i < scripts.length; i++) {
			try {
				eval(scripts[i]);
			} catch(ex) {
				// do what you want here when a script fails
			}
		}
		return source;
	},
	getViewportHeight: function () {
		if (window.innerHeight != window.undefined) {
			return window.innerHeight;
		}
		if (document.compatMode == 'CSS1Compat') {
			return document.documentElement.clientHeight;
		}
		if (document.body) {
			return document.body.clientHeight;
		} 
		return window.undefined; 
	},
	getViewportWidth: function () {
		if (window.innerWidth != window.undefined) {
			return window.innerWidth;
		} 
		if (document.compatMode == 'CSS1Compat') {
			return document.documentElement.clientWidth;
		} 
		if (document.body) {
			return document.body.clientWidth;
		} 
	},
	getScrollTop: function () {
		if (self.pageYOffset) {// all except Explorer
			return self.pageYOffset;
		} else if (document.documentElement && document.documentElement.scrollTop) {// Explorer 6 Strict
			return document.documentElement.scrollTop;
		} else if (document.body) {// all other Explorers
			return document.body.scrollTop;
		}
	},
	getScrollLeft: function () {
		if (self.pageXOffset) {// all except Explorer
			return self.pageXOffset;
		} else if (document.documentElement && document.documentElement.scrollLeft) { // Explorer 6 Strict
			return document.documentElement.scrollLeft;
		} else if (document.body) { // all other Explorers
			return document.body.scrollLeft;
		}
	}
};
JABB.Cookie = {
	set: function (name, value, days) {
		var expires = "";
		if (days) {
			var date = new Date();
			date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
			expires = "; expires=" + date.toGMTString();
		}
		document.cookie = name + "=" + value + expires + "; path=/";
	},
	get: function (name) {
		var nameEQ = name + "=",
			ca = document.cookie.split(';'),
			c;
		for (var i = 0, len = ca.length; i < len; i++) {
			c = ca[i];
			while (c.charAt(0) == ' ') {
				c = c.substring(1, c.length);
			}
			if (c.indexOf(nameEQ) == 0) {
				return c.substring(nameEQ.length, c.length);
			}
		}
		return null;
	},
	erase: function (name) {
		this.set(name, "", -1);
	}
};