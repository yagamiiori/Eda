


(function(){




var scriptName = "format_120x240.js";
var protocol = document.location.protocol;




var dokoAdBlock;
var dokoAdParams;

for(var i = 0; i < dokodokodoko.length; i++){
    if(dokodokodoko[i]["formatScript"] == scriptName && !dokodokodoko[i]["loadFlg"]){
        dokodokodoko[i]["loadFlg"] = true;
        dokoAdBlock = dokodokodoko[i]["adBlock"];
        dokoAdParams = dokodokodoko[i]["dokoAdParams"];
        break;
    }
}



var h3aColor = 'color: #30A7C0;'
var h6Color = 'color: #32AF32;'
var adspa = 'color: #000000;'
var adsh3 = 'color: #30A7C0;'
var adsBg = 'background-color:#FFFFFF;'
var bordColor = 'border: 1px solid #d03939;'
var logoColor = 'background-color:#d03939;'
var logoColor01 = 'background-color:#d03939;'
var logoColor02 = 'background-color:#d03939;'
var logoColor03 = 'background-color:#d03939;'

if(dokoAdParams["doko_color_link"]){
      h3aColor = 'color:' + dokoAdParams["doko_color_link"] + ";"
      adsh3 = 'color:' + dokoAdParams["doko_color_link"] + ";"
}

if(dokoAdParams["doko_color_text"]){ 
      adspa = 'color:' + dokoAdParams["doko_color_text"] + ";"
      h6Color = 'color:' + dokoAdParams["doko_color_text"] + ";"
}

if(dokoAdParams["doko_color_bg"]){ 
      adsBg = 'background-color:' + dokoAdParams["doko_color_bg"] + ";"
}

if(dokoAdParams["doko_color_border"]){ 
      bordColor = 'border: 1px solid ' + dokoAdParams["doko_color_border"] + ";"
      logoColor = 'background-color:' + dokoAdParams["doko_color_border"] + ";"
      logoColor01 = 'background-color:' + dokoAdParams["doko_color_border"] + ";"
      logoColor02 = 'background-color:' + dokoAdParams["doko_color_border"] + ";"
      logoColor03 = 'background-color:' + dokoAdParams["doko_color_border"] + ";"
}




var dokoAdDximgsize = "a";




var adStyle = [
    '.body{',
        'position: absolute;',
        'margin: 0px;',
        'padding: 0px;',
        'left: 0px;',
        'top: 0px;',
        'width: 118px;',
        'height: 238px;',
        bordColor,
        'overflow: hidden;',
        'display:block;',
        adsBg,
    '}\n',
    '.ads{',
        'width: 120px;',
        'height: 113px;',
        'float: left;',
        'background-color:#FFFFFF;',
        'overflow: hidden;',
        adsBg,
    '}\n',
    '.ads_big img{',
        'margin: 0px;',
        'float: left;',
        'padding-left: 0px;',
        'padding-right: 0px;',
        'padding-bottom: 0px;',
        'border: 0px;',
    '}\n',
    '.ads_title{',
        'padding-bottom: 4px;',
    '}\n',
    '.ads h3{',
        'height:28px;',
        'overflow: hidden;',
        'font-size: 12px;',
        adsh3,
        'margin-top: 2px;',
        'margin-right: 3px;',
        'margin-bottom: 1px;',
        'margin-left: 3px;',
        'padding: 0px;',
        'line-height:1.2em;',
        'word-wrap: break-word;',
    '}\n',
    '.ads h3 a{',
        h3aColor,
        'text-decoration: none;',
    '}\n',
    '.ads h3  a:hover{',
        'text-decoration: underline;',
    '}\n',
    '.ads h6{',
        'font-size: 10px;',
        h6Color,
        'padding: 0px;',
        'margin-top: 0px;',
        'margin-right: 3px;',
        'margin-bottom: 3px;',
        'margin-left: 3px;',
        'width:111px;',
        'overflow:hidden;',
    '}\n',
    '.ads h6 a{',
        h6Color,
        'text-decoration: none;',
    '}\n',
    '.ads h6  a:hover{',
        'text-decoration: underline;',
    '}\n',
    '.ads p{',
        'font-size: 10px;',
        'padding: 0px;',
        'margin-top: 0px;',
        'margin-right: 3px;',
        'margin-bottom: 0px;',
        'margin-left: 3px;',
        'line-height:1.1em;',
        'word-wrap: break-word;',
        'height:55px;',
        'overflow:hidden;',
    '}\n',
    '.ads p a{',
        adspa,
        'text-decoration: none;',
    '}\n',
    '.logo{',
        'height: 14px;',
        'width: 95px;',
        'float: right;',
        logoColor,
    '}\n',
    '.logo img{',
        'position:absolute;',
    '}\n',
    '.logo01{',
        'margin-top: 3px;',
        'height: 11px;',
        'width: 1px;',
        'float: right;',
        logoColor01,
    '}\n',
    '.logo02{',
        'margin-top: 2px;',
        'height: 12px;',
        'width: 1px;',
        'float: right;',
        logoColor02,
    '}\n',
    '.logo03{',
        'margin-top: 1px;',
        'height: 13px;',
        'width: 1px;',
        'float: right;',
        logoColor03,
    '}\n',
    '* html div.logo_img{',
        'height: 14px;',
        'width: 95px;',
        'float: right;',
    'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader',
    '(src='+dokoAdParams['doko_url_prefix'] + dokoAdParams['doko_logo_img']+');',
    '}\n',
    '* html div.logo_img img{',
        'display:none;',
    '}\n'
].join("");







var htmlBase = [
    '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">',
    '<html xmlns="http://www.w3.org/1999/xhtml">',
        '<head>',
            '<meta name="Content-Type" content="text/html;charset=UTF-8" />',
            '<title>120x240</title>',
            '<meta http-equiv="Content-Style-Type" content="text/css" />',
            '<meta http-equiv="Content-Script-Type" content="text/javascript" />',
            '<style>',
                '###style###',
            '</style>',
        '</head>',
        '<body>',
            '###content###',
        '</body>',
    '</html>'
].join("");




var templates = {
    'bigFlash'  : ['<div class="ads_big">',
                     '<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0"  width="118" height="238" align="middle">',
                       '<param name="allowScriptAccess" value="sameDomain" />',
                       '<param name="movie" value="###objectUrl###?clickTag=###encodeClickUrl###" />',
                       '<param name="quality" value="high" />',
                       '<param name="bgcolor" value="#ffffff" />',
                       '<param name="scale" value="noscale" />',
                       '<embed src="###objectUrl###?clickTag=###encodeClickUrl###" quality="high" bgcolor="#ffffff"  width="118" height="238" scale="noscale" name="" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />',
                     '</object>',
                   '</div>'].join(""),
                   
                   
    'bigFlashAlt' : [
                      '<div class="ads_big">',
                        '<a href="###clickUrl###"  target="_blank">',
                          '<img src="###subImageUrl###" />',
                        '</a>',
                      '</div>'
                    ].join(""),
                       
    'banner'    : ['<div class="ads_big">',
                     '<a href="###clickUrl###"  target="_blank">',
                       '<img src="###objectUrl###" alt="###adBodyText1###" />',
                     '</a>',
                   '</div>'].join(""),
                   
    'miniFlash' : ['<div class="ads" style="">',
                     '<div class="ads_title">',
                       '<h3><a style="" href="###clickUrl###" target="_blank" >###adTitle###</a></h3>',
                     '</div>',

                         '<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="###dxImageWidth###" height="###dxImageHeight###" align="middle">',
                           '<param name="allowScriptAccess" value="sameDomain" />',
                           '<param name="movie" value="###objectUrl###?clickTag=###encodeClickUrl###" />',
                           '<param name="quality" value="high" />',
                           '<param name="bgcolor" value="#ffffff" />',
                           '<embed src="###objectUrl###?clickTag=###encodeClickUrl###" quality="high" bgcolor="#ffffff" width="###dxImageWidth###" height="###dxImageHeight###"  scale="noscale" name="" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />',
                         '</object>',
                         '<a style="" href="###clickUrl###" target="_blank">',
                           '###adBodyText1###',
                           '###adBodyText2###',
                         '</a>',
                     '<h6><a style="" href="###clickUrl###" target="_blank">###adFooterText###</a></h6>',
                   '</div>'].join(""),
                   
                   
    'miniFlashAlt' : ['<div class="ads" style="" >',
                     '<div class="ads_title" >',
                       '<h3><a style="" href="###clickUrl###" target="_blank" >###adTitle###</a></h3>',
                     '</div>',
                         '<a style="" href="###clickUrl###" target="_blank">',
                           '<img src="###subImageUrl###" />',
                         '</a>',
                       '<p style="">',
                         '<a style="" href="###clickUrl###" target="_blank">',
                           '###adBodyText1###',
                           '###adBodyText2###',
                         '</a>',
                       '</p>',
                     '<h6><a style="" href="###clickUrl###" target="_blank">###adFooterText###</a></h6>',
                   '</div>'].join(""),
                   
                   
    'imageText' :  ['<div class="ads" style="" >',
                      '<div class="ads_title">',
                        '<h3><a style="" href="###clickUrl###" target="_blank">###adTitle###</a></h3>',
                      '</div>',
                          '<a style="" href="###clickUrl###"  target="_blank">',
                            '<img src="###objectUrl###">',
                          '</a>',
                        '<p style="">',
                          '<a style="" href="###clickUrl###" target="_blank" >',
                            '###adBodyText1###',
                            '###adBodyText2###',
                          '</a>',
                        '</p>',
                      '<h6><a style="" href="###clickUrl###" target="_blank" >###adFooterText###</a></h6>',
                    '</div>'].join(""),
    'text'      : ['<div class="ads">',
                     '<div class="ads_title">',
                       '<h3><a href="###clickUrl###" target="_blank">###adTitle###</a></h3>',
                     '</div>',
                       '<p style="">',
                         '<a style="" href="###clickUrl###" target="_blank">',
                           '###adBodyText1###',
                           '###adBodyText2###',
                         '</a>',
                       '</p>',
                     '<h6><a href="###clickUrl###" target="_blank">###adFooterText###</a></h6>',
                   '</div>'].join("")
};





var adLayoutTemplates = {
        0 : ['<div class="body">',
               '<div class="cont">',
                 '###ad1###',
                 '###ad2###',
               '</div>',
               '<div class="footer">',
                 '<div class="logo">',
                   '<div class="logo_img">',
                     '<img height="14" width="95" alt="logo" src="' + dokoAdParams['doko_url_prefix'] + dokoAdParams['doko_logo_img']+'" />',
                   '</div>',
                 '</div>',
                 '<div class="logo03" style=""><img src="' + dokoAdParams['doko_url_prefix'] + 'spacer.gif" width="1" height="1"/></div>',
                 '<div class="logo02" style=""><img src="' + dokoAdParams['doko_url_prefix'] + 'spacer.gif" width="1" height="1"/></div>',
                 '<div class="logo01" style=""><img src="' + dokoAdParams['doko_url_prefix'] + 'spacer.gif" width="1" height="1"/></div>',
               '</div>',
             '</div>'
             ].join(""),
        1 : ['<div class="body">',
               '<div class="cont">',
                 '###ad1###',
               '</div>',
               '<div class="footer">',
                 '<div class="logo">',
                   '<div class="logo_img">',
                     '<img height="14" width="95" alt="logo" src="' + dokoAdParams['doko_url_prefix'] + dokoAdParams['doko_logo_img']+'" />',
                   '</div>',
                 '</div>',
                 '<div class="logo03" style=""><img src="' + dokoAdParams['doko_url_prefix'] + 'spacer.gif" width="1" height="1"/></div>',
                 '<div class="logo02" style=""><img src="' + dokoAdParams['doko_url_prefix'] + 'spacer.gif" width="1" height="1"/></div>',
                 '<div class="logo01" style=""><img src="' + dokoAdParams['doko_url_prefix'] + 'spacer.gif" width="1" height="1"/></div>',
               '</div>',
             '</div>'
             ].join("")
};



var dxImage = {
    "a" : { "width" : "120", "height" : "60"},
    "b" : { "width" : "60", "height" : "60"},
    "d" : { "width" : "100", "height" : "100"}
};


var adId = dokoAdBlock.id;



function AdFrame(_adLayout){
    var ads = [];
    var adLayout = _adLayout;
    this.addAd = function(_ad){
        ads.push(_ad);
    };
    
    this.toHTML = function(){
        var adCount = ads.length;
        var template = adLayoutTemplates[adLayout];
        var strHTML = template;
        for(var i = 0; i < adCount; i++){
            strHTML = strHTML.replace("###ad" + String(i + 1) + "###", ads[i].toHTML);
        }
        
        strHTML = htmlBase.replace("###content###", strHTML);
        strHTML = strHTML.replace("###style###", adStyle);

        return strHTML;
    };
}

function AdBase(_template, _adParams){
    var template = _template;
    var adParams = _adParams;
    this.toHTML = function(){
        
        
        if(dokoAdDximgsize){
            dokoAdParams["doko_ad_dximgsize"] = dokoAdDximgsize;
        }
        
        
        var paramsImageUrl = ""
        var paramsAdParamTitle = "";
        var paramsClickUrl = "";
        var paramsEnCodeClickUrl = "";
        var paramsSubImageUrl = "";
        var paramsWidth = "";
        var paramsHeight = "";
        var paramsAdBodyText1 = "";
        var paramsAdBodyText2 = "";
        var paramsAdFooterText = ""
        var dxImageSize = "";
        if(_adParams != ""){
            paramsImageUrl = adParams["objectUrl"][dokoAdParams["doko_ad_dximgsize"]];
            dxImageSize = dxImage[dokoAdParams["doko_ad_dximgsize"]];
            paramsAdParamTitle = adParams["adTitle"];
            paramsClickUrl = adParams["clickUrl"];
            paramsEnCodeClickUrl = encodeURIComponent(adParams["clickUrl"]);
            paramsSubImageUrl = adParams["subImageUrl"];
            paramsWidth = dxImageSize["width"];
            paramsHeight = dxImageSize["height"];
            paramsAdBodyText1 = adParams["adBodyText1"];
            paramsAdBodyText2 = adParams["adBodyText2"];
            paramsAdFooterText = adParams["adFooterText"];
        }
        var dxImageSize = dxImage[dokoAdParams["doko_ad_dximgsize"]];
        var strHTML = template;
        
        
        strHTML = strHTML.replace(/###adTitle###/g, paramsAdParamTitle);
        
        
        strHTML = strHTML.replace(/###clickUrl###/g, paramsClickUrl);
        strHTML = strHTML.replace(/###encodeClickUrl###/g, paramsClickUrl);
        
        
        
        strHTML = strHTML.replace(/###objectUrl###/g, paramsImageUrl);
        
        
        strHTML = strHTML.replace(/###subImageUrl###/g, paramsSubImageUrl);
        
        
        
        
        strHTML = strHTML.replace(/###dxImageWidth###/g, paramsWidth);
        strHTML = strHTML.replace(/###dxImageHeight###/g, paramsHeight);
        
        
        
        strHTML = strHTML.replace(/###adBodyText1###/g, paramsAdBodyText1);
        strHTML = strHTML.replace(/###adBodyText2###/g, paramsAdBodyText2);
        
        
        
        strHTML = strHTML.replace(/###adFooterText###/g, paramsAdFooterText);
        
        return strHTML;
    }
}
var formater = function(_params){
	
	
	var dispLength = 2;
	
	
	var bigSizeCount = 0;
	
    
    
    var ads = _params["ads"];
    var dobjAdBlock = dokoAdBlock;
    
    
    for(var i = 0; i < ads.length; i++){
		if (ads[i]["imageTypeId"] == "1" || ads[i]["imageTypeId"] == "2"){
                bigSizeCount += 1;
        }
    }

    
    
    var adFrame = new AdFrame(bigSizeCount);
    
    
    for(var i = 0; i < ads.length; i++){
        var template;
        switch(ads[i]["imageTypeId"]){
            case "1" :
                template = "bigFlash";
                if(dokoAdParams["flashVersion"] == "0.0.0"){
                    template = "bigFlashAlt";
                }
            break;
            case "2" :
                template = "banner";
            break;
            case "3" :
                template = "miniFlash";
                if(dokoAdParams["flashVersion"] == "0.0.0"){
                    template = "miniFlashAlt";
                }
            break;
            case "4" :
                template = "imageText";
            break;
            case "5" :
                template = "text";
            break;
        }
        template = templates[template];
        if(!template) continue;
        
        adFrame.addAd(new AdBase(template, ads[i]));
    }

    
    
    if(ads.length < dispLength && bigSizeCount == 0){
        for(var i = 0; i < (dispLength - ads.length); i++){
            adFrame.addAd(new AdBase(templates["text"], ""));
        }
	}

    
    
    if(dokoAdParams["browser"] == "gecko"){
        if(typeof(doko_ad_append)=="undefined"){
            //doc = dobjAdBlock.contentDocument;
            doc = dobjAdBlock.contentWindow.document;
            doc.body.innerHTML = adFrame.toHTML();
        }else{
            dobjAdBlock.contentWindow.document.open();
            dobjAdBlock.contentWindow.document.write(adFrame.toHTML());
            dobjAdBlock.contentWindow.document.close();
            
        }
    }else{
        dobjAdBlock.contentWindow.document.open();
        dobjAdBlock.contentWindow.document.write(adFrame.toHTML());
        dobjAdBlock.contentWindow.document.close();
    }
};


window[adId + "call"] = formater;

})();


