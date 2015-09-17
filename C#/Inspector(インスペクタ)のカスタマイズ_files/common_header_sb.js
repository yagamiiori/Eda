var seesaa_sb_keyword = '';
var seesaa_sb_keywords_rate = [0,0,0,1,1,1,1,1,1,1]; 
document.write('  <link rel="stylesheet" href="' + seesaa_sb_blog_url + '/js/common_header_sb.css" type="text/css" />');
if (typeof seesaa_sb_keywords == 'object' && seesaa_sb_keywords.length > 0) seesaa_sb_keyword = seesaa_sb_keywords[(Math.floor( Math.random() * seesaa_sb_keywords.length ))];
if ((seesaa_sb_keywords_rate[(Math.floor( Math.random() * seesaa_sb_keywords_rate.length ))]) == 0) seesaa_sb_keyword = '';
document.getElementById('sbContainer').innerHTML = ''
 + '<form class="form" method="post" action="'
 + seesaa_sb_tag_url 
 + '/pages/search">'
 + '<input value="sjis" name="charset" type="hidden">'
 + '<input value="articles" name="service" type="hidden">'
 + '<input value="photos" name="service" type="hidden">'
 + '<input value="videos" name="service" type="hidden">'
 + '<input value="audios" name="service" type="hidden">'
 + '<input type="text" name="q" value="' 
 + seesaa_sb_keyword
 + '" id="common-header-ads-input" class="input"><button type="submit" class="button"></button><div class="clear"></div></form>';
