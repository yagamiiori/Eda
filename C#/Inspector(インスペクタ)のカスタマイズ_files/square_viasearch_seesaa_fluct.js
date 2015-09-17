/*
 *  Extract Search Query
 *  Copyright (C) Seesaa Inc. 2009
 *  All rights reserved.
 *  Usage: var sq = new seesaaExtractSearchQuery();
 *         var q = sq.ex('http://www.google.com/search?hl=ja&q=%E3%82%B7%E3%83%BC%E3%82%B5%E3%83%BC&btnG=Google+%E6%A4%9C%E7%B4%A2&lr=&aq=f&oq=');
 */

function seesaaExtractSearchQuery () {
    this.initialize.apply(this, arguments);
};

seesaaExtractSearchQuery.prototype = {
    initialize: function (args) {
        if(args){
        }
    },
    getQueryStringParameter: function (paramName, url) {
        /*
          Copyright (c) 2008, Yahoo! Inc. All rights reserved.
          Code licensed under the BSD License:
          http://developer.yahoo.net/yui/license.txt
          version: 2.5.2
        */
        var i, len, idx, queryString, params, tokens, hash = '';

        url = url || top.location.href;

        // add the hash as querystring if any
        idx = url.lastIndexOf("#");
        if ( idx >= 0 ) {
            hash = url.substr(idx + 1);
            url  = url.substr(0, idx);
        }

        idx = url.indexOf("?");
        queryString = idx >= 0 ? url.substr(idx + 1) : '';

        if ( hash.length ) {
            queryString += '&' + hash;
        }

        params = queryString.split("&");

        for (i = 0, len = params.length; i < len; i++) {
            tokens = params[i].split("=");
            if (tokens.length >= 2) {
	        if (tokens[0] === paramName) {
	            //	  return unescape(tokens[1]);
	            return tokens[1];
	        }
            }
        }

        return null;
    },
    getSearchEngine: function (url) {
        var searchEngines = [ {
            name  : 'yahoo_japan',
            regex : '^http://(blog-)?search\.yahoo\.co\.jp/search',
            query : 'p',
            charset_query   : 'ei',
            default_charset : 'euc'
        },{
            name  : 'yahoo_japan',
            regex : '^http://search\.yahoo\.co\.jp/bin/(search|query)',
            query : 'p',
            charset_query   : 'ei',
            default_charset : 'euc'
        },{
            name  : 'google',
            regex : '^https?://www\.google\.(com|co\.jp)/',
            query : 'q',
            charset_query   : 'ie',
            default_charset : ''
        },{
            name  : 'biglobe',
            regex : '^http://cgi\.search\.biglobe\.ne\.jp/cgi-bin/(search2-b|search_bl_top)',
            query : 'q',
            charset_query   : '',
            default_charset : 'euc'
        },{
            name  : 'nifty',
            regex : '^http://(azby\.)?search\.nifty\.com/cgi-bin/search\.cgi',
            query : 'Text',
            charset_query   : '',
            default_charset : 'euc'
        },{
            name  : 'excite',
            regex : '^http://(.*)\.excite\.co\.jp/search\.gw',
            query : 'search',
            charset_query   : '',
            default_charset : 'sjis'
        },{
            name  : 'goo',
            regex : '^http://search\.goo\.ne\.jp/web\.jsp',
            query : 'MT',
            charset_query   : 'IE',
            default_charset : 'euc'
        },{
            name  : 'WindowsLive',
            regex : '^http://search\.live\.com/(sp)?results\.aspx',
            query : 'q',
            charset_query   : '',
            default_charset : 'utf8'
        },{
            name  : 'infoseek',
            regex : '^http://search\.www\.infoseek\.co\.jp/Seek',
            query : 'qt'
        },{
            name  : 'Seesaa',
            regex : '^http://blog\.seesaa\.jp/pages/search/list',
            query : 'q'
        },{
            name  : 'livedoor',
            regex : '^http://(sf|search)\.livedoor\.com/search',
            query : 'q',
            charset_query   : 'ie',
            default_charset : 'utf8'
        },{
            name  : 'Ask.jp',
            regex : '^http://ask\.jp/(blog|web)\.asp',
            query : 'q',
            charset_query   : '',
            default_charset : 'utf8'
        },{
            name  : 'MSN',
            regex : '^http://search\.msn\.co\.jp/(sp)?results\.aspx',
            query : 'q'
        },{
            name  : 'So-net',
            regex : '^http://www\.so-net\.ne\.jp/search/web/',
            query : 'query',
            charset_query   : '',
            default_charset : 'utf8'
        } ];
        for(var i=0;i<searchEngines.length;i++){
            var engine = searchEngines[i];
            if(url.match(engine.regex)){
	        return engine;
            }
        }
    },
    ex: function (url) {
        var engine = this.getSearchEngine(url);
        if(engine){
            var charset = this.convCharsetQuery(this.getQueryStringParameter(engine.charset_query, url))
	            || engine.default_charset
	            || 'utf8';
            return { query: this.getQueryStringParameter(engine.query, url), charset:charset, engine: engine.name };
        }
    },
    convCharsetQuery: function (code) {
        if(code){
            if(code.match(/utf-8/i))     return 'utf8';
            if(code.match(/euc-jp/i))    return 'euc';
            if(code.match(/shift_jis/i)) return 'sjis';
            if(code.match(/x-sjis/i))    return 'sjis';
            return code;
        }
    },
    END:""
};


(function () {
    var sq = new seesaaExtractSearchQuery();
        var search = sq.ex(document.referrer);
        
    if(search){
        document.write('<link type="text/css" href="http://match.seesaa.jp/css/seesaa/viasearch/fluct.css" rel="stylesheet">');
        var tag="";
        tag+="<"+"script type=\"text/javascript\" src=\"http://sh.adingo.jp/?G=1000023660&guid=ON\"></"+"script> \n";
        tag+="<"+"script type=\"text/javascript\"> \n";
        tag+="//<![CDATA[ \n";
        tag+="if(typeof(adingoFluct)!=\"undefined\") adingoFluct.showAd('1000036695'); \n";
        tag+="//]]> \n";
        tag+="</"+"script> \n";
        document.write(tag);
        document.write('</div>');
    }
})();


