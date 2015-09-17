var blBookmarks;
blBookmarks = (function() {
	var blBookmarks, blinklistBookmarks, deliciousBookmarks, diggBookmarks, diigoBookmarks, facebookBookmarks, facebookHeightBookmarks, hatenaBookmarks, hatenaHeightBookmarks, newsingBookmarks, outputBookMarks, redditBookmarks, stumbleuponBookmarks, twitterBookmarks, twitterHeightBookmarks, twitthisBookmarks, yahooBookmarks, greeiineBookmarks, biziqBookmarks, mixicheckBookmarks, googleplusoneBookmarks, mixiiine2Bookmarks;
	var __merge, __slice, __bind, __uncurry_this, __async_call;
	var baseDetectingBrowser, ieDetectingBrowser, isIE;
	var __hasProp = Object.prototype.hasOwnProperty, __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor; child.__super__ = parent.prototype; return child; }, __indexOf = Array.prototype.indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (__hasProp.call(this, i) && this[i] === item) return i; } return -1; };

	__slice = Array.prototype.slice;

	__bind = Function.prototype.bind || function(_obj){
	    var _this, _args;
	    _this = this;
	    _args = __slice.call(arguments, 1);
	    return function(){
		var _arg;
		_arg = __slice.call(arguments);
		return _this.apply(_obj, _args.concat(_arg));
	    };
	};

	__uncurry_this = __bind.call(__bind, Function.prototype.call);

	__async_call = function(){
	    var _slice, _bind, _apply;
	    _slice = __uncurry_this(__slice);
	    _bind = __uncurry_this(__bind);
	    _apply = __uncurry_this(Function.prototype.apply);

	    return typeof setImmediate === "function"? function(){
		var _args;
		_args = _slice(arguments);
		setImmediate( _apply( _bind, null, _args));
	    }: typeof MessageChannel === "function"? function(){
		var ch, queue;
		ch = new MessageChannel();
		queue = [];
		ch.port1.onmessage = function(){ queue.shift()(); };
		return function(){
		    var _args;
		    _args = _slice(arguments);
		    queue.push(_apply(_bind, null, _args));
		    ch.port2.postMessage(0);
		};
	    }(): function(){
		var _args;
		_args = _slice(arguments);
		setTimeout(_apply(_bind, null, _args), 0);
	    };
	}();

	/*
	  base detecting browser::super
	*/
	baseDetectingBrowser = (function() {
		function baseDetectingBrowser(){
		    // constructor
		    var _this;
		        
		    this.isBrowser = false;
		    this.version = null;
		    this.options = {
			navigator:null,
			browser_name:null,
			version_scrap_string:null
		    };

		    _this = this;
		    _this._set_navigator();
		    _this._set_browser();
		    _this.set_version();

		}

		baseDetectingBrowser.prototype._set_navigator = function(){
		    this.options.navigator = window.navigator;
		};
		
		/*
		  override
		  default::lower case
		*/
		
		baseDetectingBrowser.prototype.set_browser_name = function(){
		    this.options.browser_name = null;
		};
		
		/*
		  overide
		  default::lower case
		*/
		baseDetectingBrowser.prototype._get_ua = function(){
		    return this.options.navigator.userAgent.toLowerCase();
		};
		
		baseDetectingBrowser.prototype._set_browser = function(){
		    var _browser_name, _this, _options, ua;
		        
		    _this = this;
		    _options = _this.options;
		    _this.set_browser_name();
		    _browser_name = _options.browser_name;

		    ua = _this._get_ua();
		        
		    if( _browser_name !== null){
			if( ua.indexOf( _browser_name ) !== -1 ){
			    this.isBrowser = true;
			}
		    }
		};

		/*
		  override
		*/
		baseDetectingBrowser.prototype.set_version = function(){
		    var _options, ua, _scrap_string, match, _this;
		    _this = this;
		    if(_this.isBrowser === true){
			_this.set_version_scrap_string();
			_options = _this.options;
			_scrap_string = _options.version_scrap_string;
			if( _scrap_string !== null){
			    ua = _this._get_ua();
			    match = ua.match(_scrap_string);
			    if(match !== null){
				this.version = parseFloat(match[1].replace("_", "."));
			    }
			}
		    }
		};
		
		/*
		  override
		*/
		baseDetectingBrowser.prototype.set_version_scrap_string = function(){
		    this.options.version_scrap_string = null;
		};

		return baseDetectingBrowser;
	    })();

	/*
	  ie
	*/
	ieDetectingBrowser = (function() {
		__extends(ieDetectingBrowser, baseDetectingBrowser);
		
		function ieDetectingBrowser() {
		    ieDetectingBrowser.__super__.constructor.apply(this, arguments);
		}

		ieDetectingBrowser.prototype.set_browser_name = function(){
		    this.options.browser_name = "msie";
		};

		ieDetectingBrowser.prototype.set_version_scrap_string = function(){
		    this.options.version_scrap_string = /msie\s([^\s]*)/;
		};
		return ieDetectingBrowser;
	    })();

	isIE = new ieDetectingBrowser();

	__merge = function(destination, source) {
	    var key, value;
	    for (key in source) {
		value = source[key];
		if (source.hasOwnProperty(key) === true) destination[key] = source[key];
	    }
	    return destination;
	};

	/*
	  # ブックマーク表示処理クラス:super
	*/

	outputBookMarks = (function() {
		function outputBookMarks() {
		    this.opts = {};
		    this.script_options = {};
		    this.api_id = null;
		}

		outputBookMarks.prototype.default_options = {
		    base_url: "",
		    index: null,
		    element: null,
		    media_key: "",
		    url: "",
		    subject: "",
		    blog_url: ""
		};

		outputBookMarks.prototype.default_scrpipt_options = {
		    id: null,
		    element: "script",
		    type: "text/javascript",
		    async: true,
		    charaset: "utf-8",
		    src: null,
		    style_tag: null
		};

		/*
		  # 出力用オブション値の初期値を設定::オーバライド
		*/

		outputBookMarks.prototype.get_html_options = function() {
		    var _this, _opts, _img;
		    _this = this;
		    _opts = _this.opts;
		    _img = _this.get_icon_img();
		    return {
			url: _opts.url,
			    subject: _opts.subject,
			    img: _img,
			    title: ""
			    };
		};

		/*
		  # 出力用HTMLを設定::オーバライド
		*/

		outputBookMarks.prototype.get_html = function(html_options) {
		    var _this, img, title, url;
		    _this = this;
		    url = _this.escape_html(html_options.url);
		    img = _this.escape_html(html_options.img);
		    title = _this.escape_html(html_options.title);
		    return "<a href=\"" + url + "\" target=\"_blank\"><img src=\"" + img + "\" alt=\"" + title + "\" title=\"" + title + "\" border=\"0\" hspace=\"1\"></a>";
		};

		/*
		  # Script用設定::オーバーライド
		*/

		outputBookMarks.prototype.set_script_options = function() {
		    this.script_options.src = null;
		};

		/*
		  # Api ID用設定::オーバーライド
		*/
		outputBookMarks.prototype.set_api_id = function() {
		    this.api_id = null;
		};

		outputBookMarks.prototype.get_script_options = function() {
		    var _this;
		    _this = this;
		    _this.set_api_id();
		    _this.set_script_options();
		    return __merge( _this.default_scrpipt_options, _this.script_options);
		};

		outputBookMarks.prototype.add_script = function() {
		    var _this, id_element, s, script, script_options;
		    _this = this;
		    script_options = _this.get_script_options();
		    id_element = document.getElementById(script_options.id);
		    if ((id_element === null) && (script_options.src !== null)) {
			script = document.createElement(script_options.element);
			script.type = script_options.type;
			script.async = script_options.async;
			script.charaset = script_options.charaset;
			script.src = script_options.src;
			script.setAttribute("id", script_options.id);
			s = document.getElementsByTagName('script')[0];
			s.parentNode.insertBefore(script, s);
			if (script_options.hasOwnProperty("style_tag") === true && script_options.style_tag !== "" && script_options.style_tag !== null) {
			    _this.add_style(script_options.id, script_options.style_tag);
			}
		    }
		};

		outputBookMarks.prototype.reload_script = function() {
		    var _this, id_element, script_options;
		    _this = this;
		    script_options = _this.get_script_options();
		    id_element = document.getElementById(script_options.id);
		    if ( id_element === null) {
			_this.add_script();
		    } else {
			_this.remove_script(id_element);
			_this.add_script();
		    }
		};

		outputBookMarks.prototype.remove_script = function(obj) {
		    var obj_parent;
		    obj_parent = obj.parentNode;
		    obj_parent.removeChild(obj);
		};

		outputBookMarks.prototype.add_style = function(script_id, tags) {
		    var id_element, sheet, style, style_id, tags_list;
		    if( isIE.isBrowser === true ){
			sheet = document.createStyleSheet();
			tags_list = tags.split("{").join(",").split("}").join("").split(",");
			sheet.addRule(tags_list[0], tags_list[1]);
		    }else{
			style_id = "" + script_id + "_style";
			id_element = document.getElementById(style_id);
			if (id_element !== null) {
			    style = document.createElement("style");
			    style.setAttribute("type", "text/css");
			    style.setAttribute("id", style_id);
			    document.getElementsByTagName("head")[0].appendChild(style);
			    style.sheet.insertRule(tags, style.sheet.cssRules.length);
			}
		    }
		};

		outputBookMarks.prototype.get_icon_img = function() {
		    var _opts, img;
		    _opts = this.opts;
		    img = "" + _opts.blog_url + "/img/bookmark/" + _opts.media_key + "_ico.gif";
		    return img;
		};

		outputBookMarks.prototype.escape_html = function(str) {
		    var obj;
		    obj = document.createElement("pre");
		    if (typeof obj.textContent !== "undefined") {
			obj.textContent = str;
		    } else {
			obj.innerText = str;
		    }
		    return obj.innerHTML;
		};

		outputBookMarks.prototype.get = function(options) {
		    var _this, _html_options, html;
		    _this = this;
		    if (options === null) options = {};
		    this.opts = __merge(_this.default_options, options);
		    _html_options = _this.get_html_options();
		    html = _this.get_html(_html_options);
		    return html;
		};

		outputBookMarks.prototype.add_external_script = function(src) {
		    var script;
		    script = document.createElement("script");
		    script.type = "text/javascript";
		    script.src = src;
		    document.getElementsByTagName("head")[0].appendChild(script);
		};

		return outputBookMarks;

	    })();

	/*
	  # hatena
	*/

	hatenaBookmarks = (function() {

		__extends(hatenaBookmarks, outputBookMarks);

		function hatenaBookmarks() {
		    hatenaBookmarks.__super__.constructor.apply(this, arguments);
		}

		hatenaBookmarks.prototype.get_html_options = function() {
		    var _opts;
		    _opts = this.opts;
		    return {
			url: _opts.url,
			    subject: _opts.subject,
			    img: null,
			    title: "このエントリーをはてなブックマークに追加"
			    };
		};

		hatenaBookmarks.prototype.get_html = function(html_options) {
		    return '<a href="http://b.hatena.ne.jp/entry/' + html_options.url + '" class="hatena-bookmark-button" data-hatena-bookmark-title="' + html_options.subject + '" data-hatena-bookmark-layout="standard-balloon" data-hatena-bookmark-lang="ja" title="' + html_options.title + '"><img src="http://b.st-hatena.com/images/entry-button/button-only@2x.png" alt="' + html_options.title + '" width="20" height="20" style="border: none;" /></a>';
		};

		hatenaBookmarks.prototype.set_script_options = function() {
		    this.script_options = {
			id: "hatena-js",
			src: "http://b.st-hatena.com/js/bookmark_button.js",
			charset: "utf-8",
			async: "async"
		    };
		};

		return hatenaBookmarks;

	    })();

	/*
	  # hatena type height.
	*/

	hatenaHeightBookmarks = (function() {

		__extends(hatenaHeightBookmarks, outputBookMarks);

		function hatenaHeightBookmarks() {
		    hatenaHeightBookmarks.__super__.constructor.apply(this, arguments);
		}

		hatenaHeightBookmarks.prototype.get_html_options = function() {
		    var _opts;
		    _opts = this.opts;
		    return {
			url: _opts.url,
			    subject: _opts.subject,
			    img: null,
			    title: "このエントリーをはてなブックマークに追加"
			    };
		};

		hatenaHeightBookmarks.prototype.get_html = function(html_options) {
		    return '<a href="http://b.hatena.ne.jp/entry/' + html_options.url + '" class="hatena-bookmark-button" data-hatena-bookmark-title="' + html_options.subject + '" data-hatena-bookmark-layout="vertical-balloon" data-hatena-bookmark-lang="ja" title="' + html_options.title + '"><img src="http://b.st-hatena.com/images/entry-button/button-only@2x.png" alt="'+ html_options.title +'" width="20" height="20" style="border: none;" /></a>';
		};

		hatenaHeightBookmarks.prototype.set_script_options = function() {
		    this.script_options = {
			id: "hatena-js",
			src: "http://b.st-hatena.com/js/bookmark_button.js",
			charset: "utf-8",
			async: "async"
		    };
		};

		return hatenaHeightBookmarks;

	    })();

	/*
	  # facebook type width.
	*/

	facebookBookmarks = (function() {

		__extends(facebookBookmarks, outputBookMarks);

		function facebookBookmarks() {
		    facebookBookmarks.__super__.constructor.apply(this, arguments);
		}

		facebookBookmarks.prototype.get_html_options = function() {
		    return {
			url: this.opts.url,
			subject: null,
			img: null,
			title: null
		    };
		};

		facebookBookmarks.prototype.get_html = function(html_options) {
		    return "<div class=\"fb-like\" layout=\"button_count\" data-href=\"" + html_options.url + "\" data-send=\"false\" data-width=\"120\" data-show-faces=\"false\"></div>";
		};

		facebookBookmarks.prototype.set_api_id = function() {
		    this.api_id = 216026118496977;
		};

		facebookBookmarks.prototype.set_script_options = function() {
		    var src;
		    src= "//connect.facebook.net/ja_JP/sdk.js#xfbml=1&version=v2.0&appId=" + this.api_id;
		    this.script_options = {
			id: "facebook-jssdk",
			src: src
		    };
		};
		return facebookBookmarks;

	    })();

	/*
	  # facebook type height.
	*/

	facebookHeightBookmarks = (function() {

		__extends(facebookHeightBookmarks, facebookBookmarks);

		function facebookHeightBookmarks() {
		    facebookHeightBookmarks.__super__.constructor.apply(this, arguments);
		}

		facebookHeightBookmarks.prototype.get_html = function(html_options) {
		    return "<div class=\"fb-like\" layout=\"box_count\" data-href=\"" + html_options.url + "\" data-send=\"false\" data-width=\"120\" data-show-faces=\"false\"></div>";
		};

		return facebookHeightBookmarks;

	    })();

	/*
	  # twitter
	*/

	twitterBookmarks = (function() {

		__extends(twitterBookmarks, outputBookMarks);

		function twitterBookmarks() {
		    twitterBookmarks.__super__.constructor.apply(this, arguments);
		}

		twitterBookmarks.prototype.get_html_options = function() {
		    var _opts;
		    _opts = this.opts;
		    return {
			url: _opts.url,
			    subject: _opts.subject,
			    img: null,
			    title: null
			    };
		};

		twitterBookmarks.prototype.get_html = function(html_options) {
		    return "<a href=\"https://twitter.com/share\" class=\"twitter-share-button twitter-share-button-width\" data-url=\"" + html_options.url + "\" data-lang=\"ja\" data-text=\"" + html_options.subject + "\"></a>";
		};

		twitterBookmarks.prototype.set_script_options = function() {
		    var protocol, src;
		    protocol = /^http:/.test(document.location)?"http":"https";
		    src = protocol + "://platform.twitter.com/widgets.js";
		    this.script_options = {
			id: "twitter-wjs",
			src: src
			//style_tag: "iframe.twitter-share-button-width { width: 110px!important;padding-left:2px;}"
		    };
		};

		return twitterBookmarks;

	    })();

	/*
	  # twitter type height.
	*/

	twitterHeightBookmarks = (function() {

		__extends(twitterHeightBookmarks, twitterBookmarks);

		function twitterHeightBookmarks() {
		    twitterHeightBookmarks.__super__.constructor.apply(this, arguments);
		}

		twitterHeightBookmarks.prototype.get_html = function(html_options) {
		    return "<a href=\"https://twitter.com/share\" class=\"twitter-share-button twitter-share-button-height\" data-url=\"" + html_options.url + "\" data-lang=\"ja\" data-text=\"" + html_options.subject + "\" data-count=\"vertical\"></a>";
		};

		return twitterHeightBookmarks;

	    })();

	/*
	  # yahoo
	*/

	yahooBookmarks = (function() {

		__extends(yahooBookmarks, outputBookMarks);

		function yahooBookmarks() {
		    yahooBookmarks.__super__.constructor.apply(this, arguments);
		}

		yahooBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, subject, url, img;
		    _this = this;
		    _opts = _this.opts;
		    subject = encodeURIComponent(this.opts.subject);
		    url = "http://bookmarks.yahoo.co.jp/action/bookmark?t=" + subject + "&u=" + _opts.url + "&r=my&fr=ybm_netallica";
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "Yahoo!ブックマーク"
			    };
		};

		return yahooBookmarks;

	    })();

	/*
	  # delicious
	*/

	deliciousBookmarks = (function() {

		__extends(deliciousBookmarks, outputBookMarks);

		function deliciousBookmarks() {
		    deliciousBookmarks.__super__.constructor.apply(this, arguments);
		}

		deliciousBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, title, url, img;
		    _this = this;
		    _opts = _this.opts;
		    title = encodeURIComponent(_opts.subject);
		    url = "http://del.icio.us/post?url=" + _opts.url + "&title=" + title;
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "エントリをDeliciousに追加する"
			    };
		};

		return deliciousBookmarks;

	    })();

	/*
	  # newsing
	*/

	newsingBookmarks = (function() {

		__extends(newsingBookmarks, outputBookMarks);

		function newsingBookmarks() {
		    newsingBookmarks.__super__.constructor.apply(this, arguments);
		}

		newsingBookmarks.prototype.get_html_options = function() {
		    var _this, url, img;
		    _this = this;
		    url = "http://newsing.jp/add?url=" + _this.opts.url;
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "Newsing It!"
			    };
		};

		return newsingBookmarks;

	    })();

	/*
	  # reddit
	*/

	redditBookmarks = (function() {

		__extends(redditBookmarks, outputBookMarks);

		function redditBookmarks() {
		    redditBookmarks.__super__.constructor.apply(this, arguments);
		}

		redditBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, title, url, img;
		    _this = this;
		    _opts = _this.opts;
		    title = encodeURIComponent(_opts.subject);
		    url = "http://reddit.com/submit?url=" + _opts.url + "&title=" + title;
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "reddit"
			    };
		};

		return redditBookmarks;

	    })();


	/*
	  # blinklist
	*/

	blinklistBookmarks = (function() {

		__extends(blinklistBookmarks, outputBookMarks);

		function blinklistBookmarks() {
		    blinklistBookmarks.__super__.constructor.apply(this, arguments);
		}

		blinklistBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, title, url, img;
		    _this = this;
		    _opts = _this.opts;
		    title = encodeURIComponent(_opts.subject);
		    url = "http://www.blinklist.com/index.php?Action=Blink/addblink.php&Url=" + _opts.url + "&Title=" + title;
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "blinklist"
			    };
		};

		return blinklistBookmarks;

	    })();

	/*
	  # digg
	*/

	diggBookmarks = (function() {

		__extends(diggBookmarks, outputBookMarks);

		function diggBookmarks() {
		    diggBookmarks.__super__.constructor.apply(this, arguments);
		}

		diggBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, title, url, img;
		    _this = this;
		    _opts = _this.opts;
		    title = encodeURIComponent(_opts.subject);
		    url = "http://digg.com/submit?phase=2&url=" + _opts.url + "&title=" + title;
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "Digg"
			    };
		};

		return diggBookmarks;

	    })();

	/*
	  # twitthis
	*/

	twitthisBookmarks = (function() {

		__extends(twitthisBookmarks, outputBookMarks);

		function twitthisBookmarks() {
		    twitthisBookmarks.__super__.constructor.apply(this, arguments);
		}

		twitthisBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, title, url, img;
		    _this = this;
		    _opts = _this.opts;
		    title = encodeURIComponent(_opts.subject);
		    url = "http://twitthis.com/twit?url=" + _opts.url + "&title=" + title;
		    img = _this.get_icon_img();
		    return {
			url: url,
			    subject: null,
			    img: img,
			    title: "Twit This"
			    };
		};

		return twitthisBookmarks;

	    })();

	/*
	  # diigo
	*/

	diigoBookmarks = (function() {

		__extends(diigoBookmarks, outputBookMarks);

		function diigoBookmarks() {
		    diigoBookmarks.__super__.constructor.apply(this, arguments);
		}

		diigoBookmarks.prototype.get_html_options = function() {
		    var _this, _opts, title, url;
		    _this = this;
		    _opts = _this.opts;
		    title = encodeURIComponent(this.opts.subject);
		    url = "http://www.diigo.com/post?url=" + _opts.url + "&title=" + title;
		    return {
			url: url,
			    subject: null,
			    img: "http://www.diigo.com/images/ii_blue.gif",
			    title: "Add to diigo"
			    };
		};

		return diigoBookmarks;

	    })();

	/*
	  # greeiine
	*/

	greeiineBookmarks = (function() {

		__extends(greeiineBookmarks, outputBookMarks);

		function greeiineBookmarks() {
		    greeiineBookmarks.__super__.constructor.apply(this, arguments);
		}

		greeiineBookmarks.prototype.get_html_options = function() {
		    var url;
		    url = encodeURIComponent(this.opts.url);
		    return {
			url: url,
			    subject: null,
			    img: null,
			    title: null
			    };
		};

		greeiineBookmarks.prototype.get_html = function(html_options) {
		    return "<iframe src=\"http://share.gree.jp/share?url=" + html_options.url + "&type=0&height=16\" scrolling=\"no\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\" style=\"border:none; overflow:hidden; width:62px; height:16px;\" allowTransparency=\"true\"></iframe>";
		};

		return greeiineBookmarks;

	    })();

	/*
	  # biz-iq
	*/

	biziqBookmarks = (function() {

		__extends(biziqBookmarks, outputBookMarks);

		function biziqBookmarks() {
		    biziqBookmarks.__super__.constructor.apply(this, arguments);
		}

		biziqBookmarks.prototype.get_html_options = function() {
		    return {
			url: this.opts.url,
			subject: null,
			img: null,
			title: null
		    };
		};

		biziqBookmarks.prototype.get_html = function(html_options) {
		    return "<a href=\"javascript:void(0)\" onclick=\"window.open('http://platform.biz-iq.jp/connect/share?ft=check&u=" + html_options.url + "', '_blank', 'width=480, height=440, resizable=no,toolbar=no,menubar=no,scrollbars=no,location=yes,status=no')\">\n        <img src=\"https://static.biz-iq.jp/3662/img/connect.gif\" border=\"0\" hspace=\"1\">\n</a>";
		};

		return biziqBookmarks;

	    })();


	/*
	  # mixicheck
	*/

	mixicheckBookmarks = (function() {

		__extends(mixicheckBookmarks, outputBookMarks);

		function mixicheckBookmarks() {
		    mixicheckBookmarks.__super__.constructor.apply(this, arguments);
		}

		mixicheckBookmarks.prototype.get_html_options = function() {
		    var _opts, url, subject;
		    _opts = this.opts;
		    url = _opts.url;
		    subject = _opts.subject;
		    return {
			url: url,
			    subject: subject,
			    img: null,
			    title: null
			    };
		};

		mixicheckBookmarks.prototype.get_html = function(html_options) {
		    return "<iframe src=\"" + this.opts.blog_url + "/bookmarks_iframe.html#url=" + html_options.url + "#media=mixicheck\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:60px; height:21px;\" allowTransparency=\"true\"></iframe>";
		};
		return mixicheckBookmarks;

	    })();

	/*
	  # stumbleupon
	*/

	stumbleuponBookmarks = (function() {

		__extends(stumbleuponBookmarks, outputBookMarks);

		function stumbleuponBookmarks() {
		    stumbleuponBookmarks.__super__.constructor.apply(this, arguments);
		}

		stumbleuponBookmarks.prototype.get_html_options = function() {
		    return {
			url: this.opts.url,
			subject: null,
			img: null,
			title: null
		    };
		};

		stumbleuponBookmarks.prototype.get_html = function(html_options) {
		    return "<iframe src=\"" + this.opts.blog_url + "/bookmarks_iframe.html#url=" + html_options.url + "#media=stumbleupon\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:21px; height:21px;\" allowTransparency=\"true\"></iframe>";
		};

		return stumbleuponBookmarks;

	    })();

	/*
	  # googleplusone
	*/

	googleplusoneBookmarks = (function() {

		__extends(googleplusoneBookmarks, outputBookMarks);

		function googleplusoneBookmarks() {
		    googleplusoneBookmarks.__super__.constructor.apply(this, arguments);
		}

		googleplusoneBookmarks.prototype.get_html_options = function() {
		    return {
			url: this.opts.url,
			subject: null,
			img: null,
			title: null
		    };
		};

		googleplusoneBookmarks.prototype.get_html = function(html_options) {
		    return "<iframe src=\"" + this.opts.blog_url + "/bookmarks_iframe.html#url=" + html_options.url + "#media=googleplusone\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:60px; height:21px;\" allowTransparency=\"true\"></iframe>";
		};

		return googleplusoneBookmarks;

	    })();


	mixiiine2Bookmarks = (function() {

		__extends(mixiiine2Bookmarks, outputBookMarks);

		function mixiiine2Bookmarks() {
		    mixiiine2Bookmarks.__super__.constructor.apply(this, arguments);
		}

		mixiiine2Bookmarks.prototype.get_html_options = function() {
		    return {
			url: this.opts.url,
			subject: null,
			img: null,
			title: null
		    };
		};

		mixiiine2Bookmarks.prototype.get_html = function(html_options) {
		    return "<iframe src=\"" + this.opts.blog_url + "/bookmarks_iframe.html#url=" + html_options.url + "#media=mixiiine2\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:140px; height:21px;\" allowTransparency=\"true\"></iframe>";
		};

		return mixiiine2Bookmarks;

	    })();


	function blBookmarks(options) {
	    var _opts;
	    if (options === null) options = {};
	    this.opts = _opts = __merge(this.default_options, options);
	    this.appendBalloonBox();
	    this.opts.bookmark_button_img = _opts.blog_url + _opts.bookmark_button_img;
	    this.opts.bookmark_services = this.conversion_bookmark_services_string(_opts.bookmark_services, _opts.conversion_bookmark_services_string_list);
	}

	blBookmarks.prototype.default_options = {
	    blog_url: "",
	    bookmark_services: [],
	    order_bookmark_services: ["hatena", "hatenaHeight", "facebook", "facebookHeight", "twitter", "twitterHeight", "yahoo", "livedoor", "delicious", "newsing", "reddit", "blinklist", "digg", "twitthis", "stumbleupon", "diigo", "mixicheck", "greeiine", "googleplusone", "bi", "mixiiine2", "mixiiine1"],
	    conversion_bookmark_services_string_list: {
		"ha": "hatena",
		"hah": "hatenaHeight",
		"ya": "yahoo",
		"de": "delicious",
		"ne": "newsing",
		"re": "reddit",
		"bl": "blinklist",
		"dig": "digg",
		"twth": "twitthis",
		"st": "stumbleupon",
		"dii": "diigo",
		"tw": "twitter",
		"twh": "twitterHeight",
		"fb": "facebook",
		"fbh": "facebookHeight",
		"go": "googleplusone",
		"mi2": "mixiiine2",
		"gr": "greeiine",
		"mi": "mixicheck"
	    },
	    sns_box_class: "sns_box",
	    bookmark_button_class:"seesaaBookmarks",
	    now_push_elem: null,
	    init_sns_list: ["hatena", "hatenaHeight", "facebook", "facebookHeight", "twitter", "twitterHeight"],
	    height_sns_list: ["hatenaHeight", "facebookHeight", "twitterHeight"],
	    add_init_script_list: ["hatena", "hatenaHeight", "facebook", "facebookHeight", "twitter", "twitterHeight", "stumbleupon", "mixicheck", "googleplusone"],
	    balloon_elem: "balloonBox",
	    balloon_close_elem: "balloonClose",
	    bookmark_button_elem: "bookmark_button",
	    bookmark_button_img: "/img/bookmark/bookmark.gif",
	    base_tag: "div",
	    base_elem: "bookmark",
	    data_url: "data-url",
	    data_subject: "data-subject",
	    balloon:null,
	    timer:null,
	    stop_time: 5000,
	    sns_list: {
		hatena: hatenaBookmarks,
		hatenaHeight: hatenaHeightBookmarks,
		facebook: facebookBookmarks,
		facebookHeight: facebookHeightBookmarks,
		twitter: twitterBookmarks,
		twitterHeight: twitterHeightBookmarks,
		yahoo: yahooBookmarks,
		delicious: deliciousBookmarks,
		newsing: newsingBookmarks,
		reddit: redditBookmarks,
		blinklist: blinklistBookmarks,
		digg: diggBookmarks,
		twitthis: twitthisBookmarks,
		stumbleupon: stumbleuponBookmarks,
		diigo: diigoBookmarks,
		greeiine: greeiineBookmarks,
		bi: biziqBookmarks,
		mixicheck: mixicheckBookmarks,
		googleplusone: googleplusoneBookmarks,
		mixiiine2: mixiiine2Bookmarks
	    }
	};

	blBookmarks.prototype.exe = function() {
	    var _this, class_list;
	    _this = this;
	    __async_call(function(){
		    if( isIE.isBrowser === true && isIE.version === 6){} else {
			//if (this.userAgent.indexOf("msie") !== -1 && this.appVersion.indexOf("msie 6.") !== -1) {} else {
			class_list = this.get_class_list(this.opts.base_tag, this.opts.base_elem);
			this.add_scripts_init();
			if(this.opts.bookmark_services.length > 0){
			    this.drow_bookmarks(class_list);
			}
		    }
		}, _this);
	};

	blBookmarks.prototype.conversion_bookmark_services_string = function(bookmark_services, conversion_bookmark_services_string_list) {
	    var response_bookmark_services, bookmark_service, conversion_bookmark, i, _len, order_bookmark_services;
	    response_bookmark_services = [];
	    order_bookmark_services = this.opts.order_bookmark_services;
	    for(i = 0, _len = bookmark_services.length; i < _len; i++){
		bookmark_service = bookmark_services[i];
		if( conversion_bookmark_services_string_list.hasOwnProperty(bookmark_service) === true){
		    conversion_bookmark = conversion_bookmark_services_string_list[bookmark_service];
		}else{
		    conversion_bookmark = bookmark_service;
		}
		if( __indexOf.call(order_bookmark_services, conversion_bookmark) >= 0 && __indexOf.call(response_bookmark_services, conversion_bookmark) < 0){
		    response_bookmark_services.push(conversion_bookmark);
		}
	    }
	    return response_bookmark_services;
	};

	blBookmarks.prototype.sort_bookmark_services = function(bookmark_services, order_bookmark_services) {
	    var key, response_bookmark_services, _i, _len;
	    response_bookmark_services = [];
	    for (_i = 0, _len = order_bookmark_services.length; _i < _len; _i++) {
		key = order_bookmark_services[_i];
		if (__indexOf.call(bookmark_services, key) >= 0) {
		    response_bookmark_services.push(key);
		}
	    }
	    return response_bookmark_services;
	};

	blBookmarks.prototype.add_scripts_init = function() {
	    var bookmark_list, bookmark_process, key, _i, _len, _ref;
	    bookmark_list = __merge({}, this.opts.sns_list);
	    _ref = this.opts.add_init_script_list;
	    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
		key = _ref[_i];
		bookmark_process = new bookmark_list[key]();
		if ("add_script" in bookmark_process) bookmark_process.add_script();
	    }
	};

	blBookmarks.prototype.drow_bookmarks = function(class_list) {
	    var bookmark_button_obj, bookmark_services, element, height_obj_height, height_sns_list, init_sns_list, key, opts, sns_obj, subject, url, _i, _j, _len, _len2, _fragment;
	    opts = this.opts;
	    bookmark_services = this.opts.bookmark_services;
	    init_sns_list = this.opts.init_sns_list;
	    height_sns_list = opts.height_sns_list;
	    _fragment = document.createDocumentFragment();
	    for (_i = 0, _len = class_list.length; _i < _len; _i++) {
		element = class_list[_i];
		url = element.getAttribute(opts.data_url);
		subject = element.getAttribute(opts.data_subject);
		height_obj_height = null;
		for (_j = 0, _len2 = init_sns_list.length; _j < _len2; _j++) {
		    key = init_sns_list[_j];
		    if (__indexOf.call(bookmark_services, key) >= 0) {
			sns_obj = this.sns_object(key, url, subject);
			if (sns_obj !== null) {
			    _fragment.appendChild(sns_obj);
			    if (height_obj_height === null) {
				if (__indexOf.call(height_sns_list, key) >= 0) {
				    height_obj_height = sns_obj.offsetHeight;
				}
			    }
			}
		    }
		}

		if(this.get_append_bookmark_flag() === true){
		    bookmark_button_obj = this.bookmark_button();
		    if (height_obj_height !== null) {
			bookmark_button_obj.style.height = "63px";
		    }
		    _fragment.appendChild(bookmark_button_obj);
		}
		element.appendChild(_fragment);
	    }
	};

	blBookmarks.prototype.get_append_bookmark_flag = function(){
	    var init_sns_list, sns_list, bookmark_services, key;
	    init_sns_list = this.opts.init_sns_list;
	    bookmark_services = this.opts.bookmark_services;
	    sns_list = [];
	    for (var _i = 0, _len = bookmark_services.length; _i < _len; _i++) {
		key = bookmark_services[_i];
		if (__indexOf.call(init_sns_list, key) >= 0) {
		} else {
		    return true;
		} 
	    }
	    return false;
	};

	blBookmarks.prototype.sns_object = function(key, url, subject) {
	    var element, html, opts, sns_class, sns_list, sns_obj;
	    opts = this.opts;
	    sns_list = opts.sns_list;
	    if (sns_list.hasOwnProperty(key) === true) {
		sns_obj = new sns_list[key]();
		html = sns_obj.get({
			blog_url: opts.blog_url,
			media_key: key,
			url: url,
			subject: subject
		    });
		if (html !== null) {
		    element = document.createElement("div");
		    sns_class = "" + opts.sns_box_class + " " + key + "Bookmark";
		    if(element.getAttribute("className") !== null){
			element.setAttribute("className", sns_class);
		    } else {
			element.setAttribute("class", sns_class);
		    }
		    element.innerHTML = html;
		    return element;
		} else {
		    return null;
		}

	    } else {
		return null;
	    }
	};

	blBookmarks.prototype.balloon_timer = function(flag) {
	    var opts;
	    opts = this.opts;
	    if (flag === true) {
		this.timerID = setTimeout(function() {
			if (opts.balloon !== null) {
			    opts.balloon.style.display = "none";
			    opts.now_push_elem = null;
			}
		    }, opts.stop_time);
		return;
	    } else {
		clearTimeout(this.timerID);
		return;
	    }
	};

	blBookmarks.prototype.push_bookmark_button = function(self) {
	    var element, init_sns_list, key, obj, opts, parent, selfHeight, selfX, selfY, sns_list, sns_obj, subject, url, _i, _len, _this, bookmark_services, _obj;
	    bookmark_services = this.opts.bookmark_services;
	    this.balloon_timer(false);
	    opts = this.opts;
	    _this = this;
	    parent = self.parentNode;
	    url = parent.getAttribute(opts.data_url);
	    subject = parent.getAttribute(opts.data_subject);
	    if (opts.balloon === null) {
		opts.balloon = document.getElementById(opts.balloon_elem);
	    }
	    opts.balloon.innerHTML = "";
	    if (opts.now_push_elem === self) {
		opts.balloon.style.display = "none";
		opts.now_push_elem = null;
	    } else {
		if(isIE.isBrowser === true && isIE.version < 8){
		    selfY = self.offsetTop - 10;
		    selfX = self.offsetLeft;
		    obj = self;
		    while (obj = obj.offsetParent) {
			selfX += obj.offsetLeft;
			selfY += obj.offsetTop;
			selfHeight += obj.offsetHeight;
		    }
		} else if( isIE.isBrowser === true && isIE.version === 9){
		    obj = self;
		    selfY = self.offsetTop - 10;
		    selfX = self.offsetLeft;
		    while (obj = obj.offsetParent) {
			selfY += obj.offsetTop;
			selfX += obj.offsetLeft;
		    }
		} else {
		    selfX = self.offsetLeft;
		    selfY = self.offsetTop - 10;
		    if(self.offsetParent !== document.body){
			obj = self.offsetParent;
			while(true){
			    selfY += obj.offsetTop;
			    selfX += obj.offsetLeft;
			    if(obj.offsetParent === document.body){
				break;
			    }else{
				obj = obj.offsetParent;
			    }
			}
		    }

		}
		selfHeight = self.offsetHeight;

		element = document.createElement("div");
		element.innerHTML = " <div id=\"" + opts.balloon_close_elem + "\">x</div>";
		init_sns_list = this.opts.init_sns_list;
		sns_list = this.opts.sns_list;
		for (_i = 0, _len = bookmark_services.length; _i < _len; _i++) {
		    key = bookmark_services[_i];
		    if (__indexOf.call(init_sns_list, key) >= 0) {} else {
			if((key === "stumbleupon" || key === "googleplusone") && isIE.isBrowser === true && isIE.version < 8){
			    sns_obj = null;
			} else {
			    sns_obj = this.sns_object(key, url, subject);
			}
			if (sns_obj != null) {
			    element.appendChild(sns_obj);
			    if (sns_list.hasOwnProperty(key) === true) {
				_obj = new sns_list[key]();
				_obj.reload_script();
			    }
			}
		    }
		}
		opts.balloon.appendChild(element);
		opts.balloon.style.top = selfY + selfHeight + "px";
		opts.balloon.style.left = selfX + "px";
		opts.balloon_close = document.getElementById(opts.balloon_close_elem);
		this.balloon_timer(true);
		opts.balloon.style.display = "block";
		opts.now_push_elem = self;
		opts.balloon_close.onclick = function() {
		    _this.push_balloon_close(self);
		    _this.balloon_timer(false);
		};
		opts.balloon.onmouseout = function() {
		    _this.balloon_timer(true);
		};
		opts.balloon.onmouseover = function() {
		    _this.balloon_timer(false);
		};
	    }
	};

	blBookmarks.prototype.push_balloon_close = function(self) {
	    var balloon, opts, _this;
	    opts = this.opts;
	    _this = this;
	    balloon = document.getElementById(opts.balloon_elem);
	    balloon.innerHTML = "";
	    balloon.style.display = "none";
	    self.onclick = function() {
		_this.push_bookmark_button(self);
	    };
	};

	blBookmarks.prototype.bookmark_button = function() {
	    var element, opts, self;
	    self = this;
	    opts = this.opts;
	    element = document.createElement("div");
	    if(isIE.isBrowser === true && isIE.version < 8){
		element.setAttribute("className", opts.bookmark_button_class);
	    } else {
		element.setAttribute("class", opts.bookmark_button_class);
	    }
	    element.onclick = function() {
		self.push_bookmark_button(this);
	    };
	    element.style.position = "relative";
	    //    element.style.height = "80px";
	    element.innerHTML = "<img style=\"position:absolute; bottom:0;\"  src=\"" + opts.bookmark_button_img + "\" alt=\"ブックマークボタン\">";
	    return element;
	};

	blBookmarks.prototype.appendBalloonBox = function() {
	    var element, objBody, opts, balloon_elem;
	    opts = this.opts;
	    balloon_elem = opts.balloon_elem;
	    element = document.getElementById(balloon_elem);
	    if(!element){
		element = document.createElement("div");
		element.id = balloon_elem;
		element.style.display = "none";
		objBody = document.getElementsByTagName("body").item(0);
		objBody.appendChild(element);
	    }
	};

	blBookmarks.prototype.get_class_list = function(tag, elem) {
	    var class_list, element, elements, _i, _len;
	    class_list = [];
	    elements = document.getElementsByTagName(tag);
	    for (_i = 0, _len = elements.length; _i < _len; _i++) {
		element = elements[_i];
		if (element.className === elem) class_list.push(element);
	    }
	    return class_list;
	};

	return blBookmarks;

    })();
