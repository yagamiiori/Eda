(function(d) {
  var yad_host = 'y.kau.li';
  if (location.host.indexOf(yad_host) >= 0) { return; }
  var charset = (d.all ? d.charset : d.characterSet).toLowerCase();
  var t = (new Date()).getTime();
  var hasFloat = false;

  function pos(elm) {
    var x = 0;
    var y = 0;
    while(elm) {
      x += elm.offsetLeft;
      y += elm.offsetTop;
      elm = typeof(elm.offsetParent) != 'undefined' ? elm.offsetParent : false;
    }
    return ({x : x, y : y});
  }

  function urls() {
    try {
      return {
        url: parent.document.URL,
        rurl: parent.document.referrer
      };
    } catch (e) {
      return {
        url: document.referrer,
        rurl: ''
      };
    }
  }

  function makeYadScriptTag() {
    var kauliAdTag = d.getElementById('kauli_yad_js_' + kauli_yad_js_count);
    var spotId = parseInt(kauliAdTag.innerHTML.replace(/^<!--|-->$/g, "").split(":")[0]);
    if (spotId < 9000) { return; }
    var p = pos(kauliAdTag);
    var u = urls();
    var query = 'u=' + encodeURIComponent(u.url)
              + '&r='+ encodeURIComponent(u.rurl)
              + '&s=' + spotId
              + '&z=' + kauliAdTag.style.width.replace(/px$/, "") + 'x' + kauliAdTag.style.height.replace(/px$/, "")
              + '&c=' + kauli_yad_js_count
              + '&cs=' + charset
              + '&p=' + p.x + '-' + p.y
              + '&t=' + t
              + '&fmt=json';
    if (typeof(__kauli_motime__) != 'undefined') {
      query += '&motime=' + parseInt(__kauli_motime__, 10);
    }
    var src = 'http://' + spotId + '.' + yad_host + '/?' + query;
    d.write("<script type='text/javascript' src='" + src + "'></script>");
  }

  makeYadScriptTag();

  var FloatAd = function(container) {
    var self = this;
    var built = false;

    this.build = function() {
      container.style.position = 'absolute';
      container.style.display = 'block';
      container.style.top = container.style.left = 0;
      container.style.zIndex = 10002;
      self.built = true;
    };

    this.bindEvents = function() {
      document.addEventListener('touchstart', self.hide, false);
      document.addEventListener('touchmove', self.hide, false);
      document.addEventListener('touchend', self.show, false);
      document.addEventListener('scroll', self.show, false);
      window.addEventListener('orientationchange', self.show, false);
    };

    this.showCloseBtn = function() {
      if (self.closeButton) {
        return;
      }
      var btn = document.createElement('button');
      btn.href = '#';
      btn.innerHTML = 'X';
      btn.style.zIndex = 10010;
      btn.style.position = 'absolute';
      btn.style.right = 0;
      btn.style.top = 0;
      btn.style.fontFamily = 'Arial, Helvetica, sans-serif';
      btn.style.fontSize = '20px';
      btn.style.color = '#363636';
      btn.style.padding = '6px 10px';
      btn.style.opacity = 0;
      btn.addEventListener('touchstart', self.close);
      btn.addEventListener('click', self.close);
      container.appendChild(btn);
      setTimeout(function() {
        btn.style.opacity = 0.4;
        self.adjust();
      }, 500);
      self.closeButton = btn;
    };

    this.close = function() {
      container.parentNode.removeChild(container);
    };

    this.hide = function() {
      container.style.opacity = 0;
    };

    this.show = function() {
      self.hide();
      setTimeout(function() { self.adjust(); }, 300);
      setTimeout(function() { container.style.opacity = 1; }, 500);
    };

    this.showAdFrame = function(src) {
      if (hasFloat) { return; }
      self.bindEvents();
    };

    this.adjust = function() {
      var width = window.innerWidth - 4;
      if (UA.isLandscape()) {
        width /= 1.5;
      }
      var height = width/320 * 50;
      container.style.width  = width + 'px';
      container.style.height = height + 'px';

      if (self.closeButton) {
        self.closeButton.style.fontSize = width/320 * 12 + 'px';
      }

      if (window.pageYOffset !== 0 && (document.body.scrollHeight > window.innerHeight) &&
          (document.body.scrollHeight - window.pageYOffset - window.innerHeight < 50)) {
        container.style.top = window.pageYOffset + 'px';
      } else {
        container.style.top = window.pageYOffset + window.innerHeight - height + 'px';
      }
      container.style.left = window.pageXOffset + (window.innerWidth - width)/2 + 'px';

      if (UA.isAndroid()) {
        //container.style.top  = window.pageYOffset + 'px';
        container.style.left = (window.innerWidth - width)/2 + 'px';
      }
      var target = container.getElementsByTagName('iframe')[0];
      if (target && target.id == 'ad') {
        target.width = width + 'px';
        target.height = height + 'px';
        target.contentWindow.postMessage({'width':width, 'height':height}, '*');
      }
    };
  };

  var UA = {
    'canFloat': function() {
      if (UA.hasiOS4() || UA.isAndroid()) {
        return true;
      }
      return false;
    },

    'isiOS': function() {
      if (navigator.userAgent.indexOf('iPad') > -1 ||
          navigator.userAgent.indexOf('iPod') > -1 ||
          navigator.userAgent.indexOf('iPhone') > -1) {
        return true;
      }
      return false;
    },

    'hasiOS4': function() {
      if (!UA.isiOS()) {
        return false;
      }
      navigator.userAgent.match(/OS ([\d_]+)/);
      var version = (RegExp.$1.replace(/_/g, '')+'00').slice(0,3);
      return (version >= 400);
    },

    'isAndroid': function() {
      return (navigator.userAgent.indexOf('Android') != -1);
    },

    'isLandscape': function() {
      if (UA.isAndroid()) {
        return window.screen.width > window.screen.height;
      } else {
        return Math.abs(window.orientation) === 90;
      }
    }
  };

  function makeFloatAd(overlay) {
    if (UA.isAndroid()) {
      var metas = document.getElementsByTagName('meta'),
          viewportFound = false;
      for (var i = 0; i < metas.length; i++) {
        if (metas[i].name == 'viewport') {
            viewportFound = true;
        }
      }
      if (!viewportFound) {
        var head = document.getElementsByTagName('head')[0],
            meta = document.createElement('meta');
        meta.setAttribute('name', 'viewport');
        meta.setAttribute('content', 'width=device-width');
        head.appendChild(meta);
      }
    }

    if (!hasFloat && Number(overlay)) {
      var kauliAdTag = document.getElementById('kauli_yad_js_' + kauli_yad_js_count),
          floatad = new FloatAd(kauliAdTag);
      floatad.build();
      floatad.showAdFrame();
      floatad.showCloseBtn();
      hasFloat = true;
      kauliAdTag.style.display = 'block';
    }
  }

  // JSONP callback function
  window.kauliFillAd = function(data) {
    d.write(data.adcode);
    if (data.cookieSyncHtml) {
      d.write(data.cookieSyncHtml);
    }
    if (UA.canFloat()) {
      makeFloatAd(data.overlay);
    }
  }
})(document);
