(function(d) {
  var yad_host = 'y.kau.li';
  if (typeof(__kauli_yad_js_exec__) != 'undefined') { return; }
  if (location.host.indexOf(yad_host) >= 0) { return; }
  var tag_cnt = 1;
  var search_cnt = 0;
  var search_max = 100;
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

  function findTags() {
    if (search_cnt++ > search_max) { return; }
    var d = document;
    for (var i = tag_cnt; i <= 12; i++) {
      var yadelm = d.getElementById('kauli_yad_' + i);
      if (!yadelm) { break; }
      tag_cnt++;
      if (yadelm.firstChild.nodeType != 8) { break; }
      var spot_id = parseInt(yadelm.innerHTML.replace(/^<!--|-->$/g, "").split(":")[0]);
      if (spot_id < 9000) {
        break;
      }
      var w = yadelm.style.width.replace(/px$/, "");
      var h = yadelm.style.height.replace(/px$/, "");
      var p = pos(yadelm);
      var u = urls();
      var query = 'u=' + encodeURIComponent(u.url) + '&r='+ encodeURIComponent(u.rurl) + '&s=' + spot_id + '&z=' + w + 'x' + h + '&c=' + i + '&cs=' + charset + '&p=' + p.x + '-' + p.y + '&t=' + t;
      if (typeof(__kauli_motime__) != 'undefined') {
        query += '&motime=' + parseInt(__kauli_motime__, 10);
      }
      var iframe_src = 'http://' + spot_id + '.' + yad_host + '/?' + query;
      yadelm.innerHTML = '<iframe name="' + t + '" id="kauli_s_' + spot_id + '" src="' + iframe_src + '" width="' + w + '" height="' + h + '" scrolling="no" frameborder="0" allowtransparency="true" onload="this.parentNode.style.visibility=\'visible\'"></iframe>';
    }
    setTimeout(findTags, 300);
  }

  var FloatAd = function(container) {
    var self = this;
    var built = false;

    this.build = function() {
      var makeFloat = function(el, zindex) {
        el.style.position = 'absolute';
        el.style.display = 'block';
        el.style.top = el.style.left = 0;
        el.style.zIndex = zindex;
        return el.firstChild;
      };
      var iframe = function(id, zindex) {
        var el = document.createElement('iframe');
        el.id = id;
        el.width = 0;
        el.height = 0;
        el.frameBorder = 0;
        el.scrolling = 'no';
        return el;
      };
      self.adFrame = makeFloat(container, 10002);
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
      self.setFrame(self.adFrame, src);
      self.bindEvents();
    };

    this.setFrame = function(target, src) {
      if (src) { 
        target.addEventListener('load', function(event) {
          self.adjust();
        }, false);
        target.src = src;
      } else {
        self.adjust();
      }
    };

    this.adjust = function() {
      var width = window.innerWidth - 4;
      if (UA.isLandscape()) {
        width /= 1.5;
      }
      var height = width/320 * 50;
      container.style.width  = width + 'px';
      container.style.height = height + 'px';
      self.adFrame.width  = width;
      self.adFrame.height = height;

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
      var target = self.adFrame.contentWindow;
      if (target) {
        target.postMessage({'width':width, 'height':height, 'orientation':UA.isLandscape()}, '*');
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

  if (document.getElementById('kauli_yad_1')) {
    findTags();
  } else {
    setTimeout(findTags, 200);
  }

  if (UA.canFloat()) {
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

    window.addEventListener('message', function(event) {
      var suffix = '.kau.li',
          origin = event.origin;
          subp = origin.length - suffix.length;
      if (!((subp >= 0) && (origin.lastIndexOf(suffix) == subp))) {
        return;
      }
      if (!hasFloat && event.data.overlay) {
        var yadelm_id = 'kauli_s_' + event.data.spot_id,
            yadelm = document.getElementById(yadelm_id).parentNode,
            floatad = new FloatAd(yadelm);
        floatad.build();
        floatad.showAdFrame();
        floatad.showCloseBtn();
        hasFloat = true;
        yadelm.style.display = 'block';
      }
    }, false);
  }

})(document);
var __kauli_yad_js_exec__ = true;
