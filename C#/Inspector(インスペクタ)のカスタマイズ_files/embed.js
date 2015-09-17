if (typeof(ARATA_adspots) === 'undefined') {
    var ARATA_adspots = new Array();
}
if (typeof(ARATAControl) === 'undefined') {
    var adBaseUrl = 'https://ad.ad-arata.com/delivery';
    var clickBaseUrl = 'https://ad.ad-arata.com/click';
    var thumbUrl= 'https://ad-arata.s3.amazonaws.com/';
    var kwReg = /[?&](kw|MT|name|p|q|qt|query|search|word)=([^&]+)/;
    ARATAControl = new Object();
    ARATAControl.load = function(idx) {
        var time = new Date().getTime();
        var adspot = ARATA_adspots[idx];
        var match = document.referrer.match(kwReg);
        var kw = match ? match[2] : '';
        var url = adBaseUrl + '?a=' + adspot + '&t=' + time + '&idx=' + idx + '&k=' + kw;
        this.get(url, this.set, idx, time);
    };
    ARATAControl.set = function(obj, idx, time) {
        var adspot = ARATA_adspots[idx];
        var root = document.getElementById('ARATA_AD_' + idx);
        if (obj.thumbnail) {
            root.appendChild(getImgTextDom());
        } else {
            root.appendChild(getBasicTextDom());
        }
        function getBasicTextDom() {
            var span =  document.createElement('span');
            span.id = 'MOredrefncs_ph_' + idx;
            span.className = 'MOredrefncstx text_done';
            var a = document.createElement('a');
            a.className = 'MOredrefncstx';
            a.target = '_blank';
            a.href = clickBaseUrl + '?a=' + adspot + '&m=' + obj.media +
                '&c=' + obj.campaign + '&v=' + obj.advertiser +
                '&d=' + obj.distribution + '&s=' + obj.manuscript +
                '&t=' + time + '&h=' + obj.hash;
            a.appendChild(document.createTextNode(obj.text));
            span.appendChild(a);
            return span;
        }
        function getImgTextDom() {
            var div = document.createElement('div');
            div.className = 'MOredrefncs_thumb_infeed';
            var img =  document.createElement('img');
            img.className = 'MOredrefncs_thumb';
            img.src = thumbUrl + obj.thumbnail;
            div.appendChild(img);
            div.appendChild(getBasicTextDom());
            return div;
        }
    };
    ARATAControl.get = function(url, callback, idx, time) {
        var callbackName = 'callback_' + time;
        this[callbackName] = function(data) {
            callback(data, data.idx, time);
        };
        var script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = url + '&callback=ARATAControl.' + callbackName;
        appendScript();

        function appendScript() {
            if (window.document.body) {
                add();
            } else {
                onload(add);
            }
            function add() {
                document.body.appendChild(script);
            }
        }
        function onload(callback) {
            if (window.addEventListener) {
                window.addEventListener('load', callback, false);
            } else if (window.attachEvent) {
                window.attachEvent('onload', callback);
            } else {
                window.onload = callback;
            }
        }
    };
    ARATAControl.index = 1;
}
(function() {
    var idx = ARATAControl.index++;
    var elmId = 'ARATA_AD_' + idx;

    ARATA_adspots[idx] = ARATA_adspotId;
    document.write('<span id="' + elmId + '"></span>');
    setTimeout('ARATAControl.load(' + idx + ')', 500 + idx * 20);
})();
