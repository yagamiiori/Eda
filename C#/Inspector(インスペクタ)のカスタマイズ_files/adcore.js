(function(){var j=true,i=false,h="imobile_xid",g=null,k=",";if(!("SPMobile" in window)){var loadBalanceModule={validTimeBand:function(b){var a=1<<(new Date).getHours(),c=(b&a)==a;return c},validHttpOnly:function(){return document.location.protocol=="http:"}},xidModule={expireMillSec:3.6e6,ignoreSpotIDsForXidMediaRestore:"16905,28113,28114,28783,29712,29713,34996".split(k),getJSON:function(){try{if(!window.localStorage)return g;var a=window.localStorage.getItem(h);if(a==g)return g;return xidJSON.toObj(a)}catch(b){return g}},setXidText:function(f){try{if(!window.localStorage)return;var a=xidJSON.toObj(f),b=i;if(a&&a.xid)if(a.xid!=="")b=j;var c=i;if(a)if(a.optout&&a.optout=="true")c=j;var d=b==i&&this.getJSON()==g?-1:(new Date).getTime(),e={xid:b&&!c?a.xid:"",synced:b,lastSyncTime:d,optout:c};window.localStorage.setItem(h,xidJSON.toText(e))}catch(g){}},trySyncToMedia:function(){if(!this.needSyncToMedia())return;var a=this;if(window.attachEvent){window.attachEvent("onmessage",function(b){a.isRightOrigin(b.origin)&&a.setXidText(b.data)});window.attachEvent("onload",function(){a.appendSyncFrame()})}else{window.addEventListener("message",function(b){a.isRightOrigin(b.origin)&&a.setXidText(b.data)},i);window.addEventListener("load",function(){a.appendSyncFrame()},i)}},appendSyncFrame:function(){var a="0",d=document.location.protocol=="http:"?"http":"https",c=d+"://xid.i-mobile.co.jp/RestoreXidToMediaStorage.html",b=document.createElement("iframe");b.src=c;b.name="restoreXidFrame";if(b.style){b.style.visibility="hidden";b.style.padding=a;b.style.display="none"}b.setAttribute("frameBorder",a);b.setAttribute("scrolling","no");b.setAttribute("width",a);b.setAttribute("height",a);b.setAttribute("marginwidth",a);b.setAttribute("marginheight",a);b.setAttribute("vspace",a);b.setAttribute("hspace",a);window.document.body.appendChild(b)},isRightOrigin:function(c){var a="xid.i-mobile.co.jp",b=c.length-a.length,d=b>=0&&c.lastIndexOf(a)===b;return d},expireXid:function(b){if(!b)return j;var a=parseInt(b.lastSyncTime,10)+this.expireMillSec;if(!a)return j;return a<(new Date).getTime()},needSyncToMedia:function(){if(this.ignoreSpotForXidMediaRestore())return i;var a=i;if(window.localStorage&&window.postMessage)a=j;var b=this.getJSON();return a&&(b==g||this.expireXid(b))},ignoreSpotForXidMediaRestore:function(){if(!window.imobile_asid)return i;var a=g;try{a=imobile_asid}catch(d){}if(a==g)return i;for(var c=this.ignoreSpotIDsForXidMediaRestore.length,b=0;b<c;b++)if(this.ignoreSpotIDsForXidMediaRestore[b]==a)return j;return i}},xidJSON={},validchars=/^[\],:{}\s]*$/,validescape=/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g,validtokens=/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g,validbraces=/(?:^|:|,)(?:\s*\[)+/g;if(window.JSON&&window.JSON.parse)xidJSON.toObj=function(a){try{return window.JSON.parse(a)}catch(b){return g}};else xidJSON.toObj=function(a){try{if(validchars.test(a.replace(validescape,"@").replace(validtokens,"]").replace(validbraces,""))){var b=new Function("return "+a)();return b}return g}catch(c){return g}};if(window.JSON&&window.JSON.stringify)xidJSON.toText=function(a){try{return window.JSON.stringify(a)}catch(b){return ""}};else xidJSON.toText=function(b){try{var d=[];for(var a in b)if(b.hasOwnProperty(a)){var c=typeof b[a];if(c==="string"||c==="number"||c==="boolean")d.push('"'+a+'":"'+b[a]+'"');else continue}return "{"+d.join(k)+"}"}catch(e){return ""}};try{xidModule.trySyncToMedia()}catch(a){}window.SPMobile={asn:1,NewAsn:function(){return window.SPMobile.asn++},SelectServer:function(servers,rate){for(var svrs=servers.split(k),drts=rate.split(k),total=0,i=0;i<drts.length;i++)total+=eval(drts[i]);for(var score=Math.floor(Math.random()*total),sum=0,idx=0,j=0;j<svrs.length;j++){sum+=eval(drts[j]);if(score<sum){idx=j;break}}return svrs[idx]},SelectServerBySpotId:function(h,g,i){for(var d=h.split(k),c=g.split(k),e=0,b=0;b<c.length;b++)e+=parseInt(c[b]);for(var f=0,j=i%e,a=0;a<d.length;a++){f+=parseInt(c[a]);if(j<f)return d[a]}return d[0]},enableXHRLv2:function(){if(window.XMLHttpRequest){var a=new XMLHttpRequest;return "withCredentials" in a}return i},getAjaxResponse:function(d,c){if(window.SPMobile.enableXHRLv2()){var a=new XMLHttpRequest;a.open("GET",d,j);a.withCredentials="true";a.onreadystatechange=function(){a.readyState==4&&a.status==200&&c(a.responseText)};a.send(g)}else if(window.XDomainRequest){var b=new XDomainRequest;b.open("GET",d);b.onload=function(){c(b.responseText)};b.send(g)}},JsonToObj:function(jsonstr){return eval(jsonstr)},JsonToObjEx:function(a){return xidJSON.toObj(a)},getXid:function(){try{var a=xidModule.getJSON();if(a==g||!a.synced)return g;if(a.optout)if(a.optout==j)return g;if(!a.xid)return g;if(typeof a.xid!=="string")return g;return a.xid}catch(b){return g}},getLoadBalanceModule:function(){return loadBalanceModule}}}})()