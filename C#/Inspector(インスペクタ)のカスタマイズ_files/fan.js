if(typeof(seesaaFanAddEventDone) === "undefined")
  var seesaaFanAddEventDone = {};
(function () {
  var links = document.links;
  for(i=0;i < links.length;i++){
    ele = links[i];
    if(ele.id.indexOf("seesaaFanBookmarkLink") > -1 && ! seesaaFanAddEventDone[ele.id]){
      var href  = ele.href;
      var title = ele.title;

      var funcIE = function () { window.external.AddFavorite(href, title) };
      var funcFF = function () { window.sidebar.addPanel(title, href, '') };
      var funcNE = function () { window.sidebar.addPanel(title, href) };

      function _addEventListener (ele, func) {
	if(ele.addEventListener){
	  ele.addEventListener("click", func, false);
	}
	else if(ele.attachEvent){
	  ele.attachEvent("onclick", func);
	}
      }

      if(navigator.userAgent.indexOf("Opera") > -1){
	ele.rel = 'sidebar';
      }
      else {
	if(navigator.userAgent.indexOf("MSIE") > -1)
	  _addEventListener(ele, funcIE);
	if(navigator.userAgent.indexOf("Firefox") > -1)
	  _addEventListener(ele, funcFF);
	if(navigator.userAgent.indexOf("Netscape") > -1)
	  _addEventListener(ele, funcNE);
	ele.href='#'; // do nothing.
	seesaaFanAddEventDone[ele.id] = true;
      }
    }
  }
})();
