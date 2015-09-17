(function(){
var protocol = document.location.protocol;


var urlPrefix = (protocol == "https:") ? "https://a248.e.akamai.net/f/248/45380/24h/img.rc.impact-ad.jp/rc/dn/doko/" : "http://a248.e.akamai.net/f/248/45380/24h/img.rc.impact-ad.jp/rc/dn/doko/";
var defaultFormatCssUrlBase = urlPrefix + "css/";
var defaultFormatJsUrlBase = urlPrefix + "js/";
var adServerUrl = protocol + "//nw.ads.doko.jp/adparts/ad";
var dummyHtml = urlPrefix + "js/dummy.html";


var dokoAdParams = {};
var params = [];

var ua = navigator.userAgent;
if(ua.indexOf('Gecko') > -1 && ua.indexOf('KHTML') === -1){
    dokoAdParams["browser"] = "gecko";
}
else{
    dokoAdParams["browser"] = "others";
}


dokoAdParams["doko_url_prefix"] = urlPrefix;

if (document.compatMode == "BackCompat") {
    dokoAdParams["compatible_flg"] = true;
}
else {

    dokoAdParams["compatible_flg"] = false;
}


if(typeof document.documentElement.style.maxHeight != "undefined"){
    dokoAdParams["modern_browser_flg"] = true;
}
else{

    dokoAdParams["modern_browser_flg"] = false;
}





if(typeof doko_ad_siteid != "undefined" && doko_ad_siteid != null){
    dokoAdParams["doko_ad_siteid"] = doko_ad_siteid;
    params.push("sid=" + dokoAdParams["doko_ad_siteid"]);
}


params.push("ua=" + encodeURIComponent(navigator.userAgent));


if(typeof doko_ad_keyword != "undefined" && doko_ad_keyword != null){
    dokoAdParams["doko_ad_keyword"] = doko_ad_keyword;
    params.push("kwd=" + encodeURIComponent(dokoAdParams["doko_ad_keyword"]));
}

if(typeof doko_ad_prj != "undefined" && doko_ad_prj != null){
    dokoAdParams["doko_ad_prj"] = doko_ad_prj;
}
else{
    dokoAdParams["doko_ad_prj"] = "j";
}
params.push("prj=" + dokoAdParams["doko_ad_prj"]);


if(typeof doko_ad_lon != "undefined" && doko_ad_lon != null){
    dokoAdParams["doko_ad_lon"] = doko_ad_lon;
    params.push("lon=" + dokoAdParams["doko_ad_lon"]);
}
if(typeof doko_ad_lat != "undefined" && doko_ad_lat != null){
    dokoAdParams["doko_ad_lat"] = doko_ad_lat;
    params.push("lat=" + dokoAdParams["doko_ad_lat"]);
}


if(typeof doko_ad_addr != "undefined" && doko_ad_addr != null){
    dokoAdParams["doko_ad_addr"] = doko_ad_addr;
    params.push("adr=" + encodeURIComponent(dokoAdParams["doko_ad_addr"]));
}


if(typeof doko_ad_channel != "undefined" && doko_ad_channel != null){

    dokoAdParams["doko_ad_channel"] = encodeURIComponent(doko_ad_channel);

    params.push("chn=" + dokoAdParams["doko_ad_channel"]);
}


if(protocol == "https:"){
    dokoAdParams["ssl"] = '1';
    params.push("ssl=1");
}
else{
    dokoAdParams["ssl"] = '0';
}


if(typeof doko_ad_random != "undefined" && doko_ad_random != null){
    dokoAdParams["doko_ad_random"] = doko_ad_random;
}
else{
    dokoAdParams["doko_ad_random"] = "1";
}
params.push("rnd=" + dokoAdParams["doko_ad_random"]);



if(typeof doko_ad_format != "undefined" && doko_ad_format != null){
    dokoAdParams["doko_ad_format"] = doko_ad_format;
}
else{
    dokoAdParams["doko_ad_format"] = "728x90";
}

params.push("fmt=" + dokoAdParams["doko_ad_format"].replace("x", "X"));


if(typeof doko_ad_dximgsize != "undefined" && doko_ad_dximgsize != null){
    dokoAdParams["doko_ad_dximgsize"] = doko_ad_dximgsize;
}
else{
    dokoAdParams["doko_ad_dximgsize"] = "a";
}
if(dokoAdParams["doko_ad_format"] == "free"){
    params.push("dis=" + dokoAdParams["doko_ad_dximgsize"]);
}


if(typeof doko_color_border != "undefined" && doko_color_border != null){
    dokoAdParams["doko_color_border"] = doko_color_border;
}
else{
    dokoAdParams["doko_color_border"] = null;
}

if(typeof doko_color_bg != "undefined" && doko_color_bg != null){
    dokoAdParams["doko_color_bg"] = doko_color_bg;
}
else{
    dokoAdParams["doko_color_bg"] = null;
}


if(typeof doko_color_link != "undefined" && doko_color_link != null){
    dokoAdParams["doko_color_link"] = doko_color_link;
}
else{

    dokoAdParams["doko_color_link"] = null;
}

if(typeof doko_color_text != "undefined" && doko_color_text != null){
    dokoAdParams["doko_color_text"] = doko_color_text;
}
else{

    dokoAdParams["doko_color_text"] = null;
}


var tmp_ad_logocolor = null;
dokoAdParams["doko_logo_img"] = "logo_01.png";

if(typeof doko_ad_logocolor != "undefined" && doko_ad_logocolor != null && dokoAdParams["doko_ad_format"] == "free"){
    dokoAdParams["doko_ad_logocolor"] = doko_ad_logocolor;
    tmp_ad_logocolor = dokoAdParams["doko_ad_logocolor"];
}else if(dokoAdParams["doko_color_border"] != null && new String(dokoAdParams["doko_color_border"]).length == 7 && dokoAdParams["doko_color_border"].substring(0,1)=="#" ){
    tmp_ad_logocolor = dokoAdParams["doko_color_border"];
}

if(tmp_ad_logocolor != null && new String(tmp_ad_logocolor).length == 7 && tmp_ad_logocolor.substring(0,1)=="#" ){
    var sR = tmp_ad_logocolor.substring(1, 3);  
    var sG = tmp_ad_logocolor.substring(3, 5);  
    var sB = tmp_ad_logocolor.substring(5, 7);  

    var ir = parseInt(sR, 16);  
    var ig = parseInt(sG, 16);  
    var ib = parseInt(sB, 16);  
    var r = Math.pow(ir/255, 2.2);  
    var g = Math.pow(ig/255, 2.2);  
    var b = Math.pow(ib/255, 2.2);  

    var y = 0.21267*r+0.71516*g+0.072169*b;  

    if ( y > 0.008856 ){  
        y = Math.pow(y, 0.33333333333333333);  
    }else{  
        y = ( 7.787 * y ) + ( 16 / 116 );  
    }  

    var l = 116*y-16;
    if (l > 70){ //use black logo image when bright color  
           dokoAdParams["doko_logo_img"] = "logo_02.png";  
    }  
}  




if(dokoAdParams["doko_ad_format"] == "free"){
    params.push("lcl=" + encodeURIComponent(dokoAdParams["doko_ad_logocolor"]));
}


if(typeof doko_ad_width != "undefined" && doko_ad_width != null){
    dokoAdParams["doko_ad_width"] = doko_ad_width;
}
else{
    dokoAdParams["doko_ad_width"] = dokoAdParams["doko_ad_format"].split("x")[0];
}


if(typeof doko_ad_height != "undefined" && doko_ad_height != null){
    dokoAdParams["doko_ad_height"] = doko_ad_height;
}
else{
    dokoAdParams["doko_ad_height"] = dokoAdParams["doko_ad_format"].split("x")[1];
}




if(typeof doko_ad_cssurl != "undefined" && doko_ad_cssurl != null){
    dokoAdParams["doko_ad_cssurl"] = doko_ad_cssurl;
}
else{
    dokoAdParams["doko_ad_cssurl"] = defaultFormatCssUrlBase + "format_" + dokoAdParams["doko_ad_format"] + ".css";
}
if(dokoAdParams["doko_ad_format"] == "free"){
    params.push("css=" + encodeURIComponent(dokoAdParams["doko_ad_cssurl"]));
}





if(typeof doko_ad_dxadnum != "undefined" && doko_ad_dxadnum != null){
    dokoAdParams["doko_ad_dxadnum"] = String(doko_ad_dxadnum);
    params.push("dxn=" + dokoAdParams["doko_ad_dxadnum"]);
}


if(typeof doko_ad_txtadnum != "undefined" && doko_ad_txtadnum != null){
    dokoAdParams["doko_ad_txtadnum"] = String(doko_ad_txtadnum);
    params.push("txn=" + dokoAdParams["doko_ad_txtadnum"]);
}

if(typeof doko_ad_miniflash_max != "undefined" && doko_ad_miniflash_max != null){
    dokoAdParams["doko_ad_miniflash_max"] = String(doko_ad_miniflash_max);
}
else{
    dokoAdParams["doko_ad_miniflash_max"] = 0;
}
params.push("mfm=" + dokoAdParams["doko_ad_miniflash_max"]);


if(typeof doko_ad_iarea != "undefined" && doko_ad_iarea != null){
    dokoAdParams["doko_ad_iarea"] = doko_ad_iarea;
    params.push("iarea=" + dokoAdParams["doko_ad_iarea"]);
}


if(typeof doko_ad_geot != "undefined" && doko_ad_geot != null){
    dokoAdParams["doko_ad_geot"] = doko_ad_geot;
}
else{
    dokoAdParams["doko_ad_geot"] = "0";
}
params.push("geo=" + dokoAdParams["doko_ad_geot"]);



if(typeof doko_ad_banner_flg != "undefined" && doko_ad_banner_flg != null){
    dokoAdParams["doko_ad_banner_flg"] = doko_ad_banner_flg;
}
else{
    dokoAdParams["doko_ad_banner_flg"] = "4";
}
params.push("bf=" + dokoAdParams["doko_ad_banner_flg"]);


if(typeof doko_nolog != "undefined" && doko_nolog != null){
    dokoAdParams["doko_nolog"] = doko_nolog;
}
else{
    dokoAdParams["doko_nolog"] = '0';
}
params.push("nolog=" + dokoAdParams["doko_nolog"]);
params.push("zyxrqp=" + protocol.length );


if(typeof doko_ad_client != "undefined" && doko_ad_client != null){
    dokoAdParams["doko_ad_client"] = doko_ad_client;
}


dokoAdParams["flashVersion"] = (function(){
    var version='0.0.0';
    if(navigator.plugins && navigator.mimeTypes['application/x-shockwave-flash']){
        var plugin = navigator.mimeTypes['application/x-shockwave-flash'].enabledPlugin;
        if(plugin && plugin.description){
            version = plugin.description.replace(/^[A-Za-z\s]+/, '').replace(/(\s+r|\s+b[0-9]+)/, ".");
        }
    }

    else{
        var ieVersion = '';
        try{
            var a = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
            ieVersion = a.GetVariable("$version");
        }
        catch(e){
            try{
                a = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");
                ieVersion = "WIN 6,0,21,0";
                a.AllowScriptAccess = "always";
                ieVersion = a.GetVariable("$version");
            }
            catch(e){
                if(!ieVersion.match(/^WIN/)){
                    try{
                        a = null;
                        a = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
                        ieVersion = a.GetVariable("$version");
                    }
                    catch(e){
                        if(a){
                            ieVersion = "WIN 3,0,18,0";
                        }
                        else{
                            try{
                                a = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
                                ieVersion = "WIN 2,0,0,11";
                            }
                            catch(e){
                                ieVersion = "WIN 0,0,0,0";
                            }
                        }
                    }
                }
            }
        }
        version = ieVersion.replace(/^WIN /,'').replace(/,[0-9]+$/,'').replace(/,/g,'.');
    }
    
    if(version.match(/^[0-9]+\.[0-9]+\.[0-9]+$/)){
        return version;
    }
    else{
        return '0.0.0';
    }
})();


function createId(){
    var seed = 'abcdefghijklmnopqrstuvwxyz';
    seed = seed.split('');
    var id = "";
    for (var i = 0; i < 20; i++) {
        id += seed[Math.floor(Math.random() * seed.length)];
    }
    return id;
}

var adId = createId();
params.push("callback=" + adId + 'call');



var widthTagVal = dokoAdParams["doko_ad_width"];
var heightTagVal = dokoAdParams["doko_ad_height"];
var divStyle = 'style="width:' + widthTagVal + 'px;' + 
               'height:' + heightTagVal +'px;' + 
               'margin:0; padding:0; border:0; font-weight:inherit; font-style:inherit;' + 
               'font-size:100%; font-family:inherit; vertical-align:baseline;"'



	if(navigator.userAgent.indexOf("MSIE 6")!= -1 && dokoAdParams["ssl"] == 1){

		var js_params = "?params=" + encodeURIComponent(adId)  + "," + encodeURIComponent(adServerUrl)  + "," + encodeURIComponent(defaultFormatCssUrlBase)  + "," + encodeURIComponent(defaultFormatJsUrlBase)  + "," + encodeURIComponent(dokoAdParams['doko_url_prefix'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_siteid'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_keyword'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_prj'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_lon'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_lat'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_addr'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_channel'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_random'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_format'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_dximgsize'])  + "," + encodeURIComponent(dokoAdParams['doko_logo_img'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_cssurl'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_dxadnum'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_txtadnum'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_miniflash_max'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_iarea'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_geot'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_banner_flg'])  + "," + encodeURIComponent(dokoAdParams['doko_nolog'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_client'])  + "," + encodeURIComponent(dokoAdParams['flashVersion'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_width'])  + "," + encodeURIComponent(dokoAdParams['doko_ad_height'])  + "," + encodeURIComponent(dokoAdParams['doko_color_border']) + "," + encodeURIComponent(dokoAdParams['doko_ad_logocolor']) + "," + encodeURIComponent(dokoAdParams['doko_color_bg'])  + "," + encodeURIComponent(dokoAdParams['doko_color_link'])  + "," + encodeURIComponent(dokoAdParams['doko_color_text']);

		document.write('<iframe src="' + dummyHtml + js_params + ' " id="' + adId + '" frameborder="0" scrolling="no" width="' + dokoAdParams["doko_ad_width"] + '" height="' + dokoAdParams["doko_ad_height"] + '" marginwidth="0" marginheight="0" >'+'</iframe>');
		
		return;
	}

	document.write('<iframe id="' + adId + '" frameborder="0" scrolling="no" '+ 'width="' + dokoAdParams["doko_ad_width"] + '" '+'height="' + dokoAdParams["doko_ad_height"] + '" >'+'</iframe>');



var adBlock = document.getElementById(adId);



var strAdIFrame = [
    '<iframe frameborder="0" scrolling="no" ',
             'width="' + dokoAdParams["doko_ad_width"] + '" ',
             'height="' + dokoAdParams["doko_ad_height"] + '" >',
    '</iframe>'
].join("");


if(document.getElementById(adId)){
var adIFrame = document.getElementById(adId).contentWindow;

        

var adIFrameDoc = adIFrame.document;

adBlock.adIFrame = adIFrame;
adBlock.adIFrameDoc = adIFrameDoc;
}

dokoAdParams["doko_ad_width"] = String(Number(dokoAdParams["doko_ad_width"]) - 2);
dokoAdParams["doko_ad_height"] = String(Number(dokoAdParams["doko_ad_height"]) - 2);

function loadFormatScript(_url, _callBack){
    var objScript = document.createElement("script");
    objScript.src = _url;
    objScript.type = 'text/javascript';
    objScript.charset = "UTF-8";
    
    var callBack = _callBack || (function(){});

    document.getElementsByTagName("head")[0].appendChild(objScript);
    
    var timeoutCounter = 50;
    var timer;
    var callBackFunctionName = adId + 'call';
    var observe = function(){
        if(timeoutCounter > 0){
            timeoutCounter--;
        }
        else{
            return;
        }
        if(window[callBackFunctionName]){
            clearTimeout(timer);
            callBack();
        }
        else{
            setTimeout(observe, 100);
        }
    };
    timer = setTimeout(observe, 100);
}

function loadAdJSON(_url, _callBack){
    var objScript = document.createElement("script");
    objScript.src = _url;
    objScript.type = 'text/javascript';
    objScript.charset = "UTF-8";
    
    var callBack = _callBack || (function(){});

    document.getElementsByTagName("head")[0].appendChild(objScript);
}
var iframeCounter=10;
var timerIframe
var createIframe = function(){
    if(iframeCounter > 0){
       iframeCounter--;
    }
    else{
        return;
    }
    if(adBlock!=null){
        clearTimeout(timerIframe);
dokodokodoko.push({
    "adBlock"      : adBlock,
    "formatScript" : 'format_' + dokoAdParams["doko_ad_format"] + '.js',
    "dokoAdParams" : dokoAdParams,
    "loadFlg"      : false
});

loadFormatScript(defaultFormatJsUrlBase + 'format_' + dokoAdParams["doko_ad_format"] + '.js', function(){
    loadAdJSON(adServerUrl + '?' + params.join('&'));
});
    }
    else{
       adBlock = document.getElementById(adId);
       timerIrame=setTimeout(createIframe, 50);
    }
};
createIframe();

})();
