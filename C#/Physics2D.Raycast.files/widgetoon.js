var widgetoon_iseq = 0;

function widgetoon_attrs2query(elm)
{
  q = "";
  f = 0;
  ea = elm.attributes;
  for (var i=0;i<ea.length;i++) {
    if (ea[i].name.slice(0,5) == "data-") {
      vname = ea[i].name.slice(5);
      q += vname + "=" + encodeURIComponent(ea[i].value) + "&";
      switch(true) {
        case (vname == 'size' && ea[i].value.slice(0,1)== 'l'):
          f|=1;
          break;
        case (vname == 'count' && ea[i].value.slice(0,1)== 'v'):
          f|=2;
          break;
        case (vname == 'lang' && ea[i].value.slice(0,1)== 'j'):
          f|=4
          break;
      }
    }
  }
  w=(f&2 ? 62:(f&1 ? 122:101))+(f&4 ? 14:0)+(f==5 ? 10:0);
  h=(f&2 ? 61:(f&1 ? 33:28));
  style = "width: "+ w +"px; height: "+ h +"px;";
  return [q,style];
}

function widgetoon_ifremplacer(telem)
{
  telem.innerHTML = 'ifrem';
  ifq = widgetoon_attrs2query(telem);
  eif = document.createElement('iframe');
  eif.setAttribute('id', 'twitter-widgetoon-' + (widgetoon_iseq++));
  eif.setAttribute('scrolling', 'no');
  eif.setAttribute('frameborder', 'no');
  eif.setAttribute('allowtransparency', 'true');
  eif.setAttribute('style', "position: static; visibility: visible; "+ ifq[1]);
  eif.setAttribute('src', "http://jsoon.digitiminimi.com/tweet_button.html#" + ifq[0]);
  telem.appendChild(eif);
  telem.removeChild(telem.childNodes[0]);
}

function widgetoon_main()
{
  var cn = [];
  tsbs = document.getElementsByClassName('twitter-share-buttoon');
  for (i=0;i<tsbs.length;i++) {
    widgetoon_ifremplacer(tsbs[i]);
  }
}
