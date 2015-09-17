function renderCmnHeader ( textColor, bgColor, logoImg) {

  var common_header_pr_html = '<scr' + 'ipt src=\'http://static.adlantis.jp/javascripts/AdLantisLoader.js?20090519\' type=\'text/javascript\' charset=\'utf-8\'></scr' + 'ipt>'
    + '<!-- Begin: Adlantis, Zone: [ユーザブログ共通ヘッダーテキスト] -->'
    + '<div class="pr2adlantis"><div class=\'adlantiss_frame zid_yQ96kOGdbqT6C9Cbb9xECQ%3D%3D color_' + textColor + ' container_div autosize\'></div></div>'
    + '<!-- End: Adlantis -->';

  document.write('<link rel="stylesheet" href="http://blog.seesaa.jp/css/common-header.css" type="text/css" />'
		 + '<style type="text/css">'
		 + '#common-header a.seesaa-adLink,'
		 + '#common-header a.adTitle{'
		 + 'color: ' + textColor + ';'
		 + '}'
		 + '#common-header .bgcolor{'
		 + 'background: ' + bgColor + ';'
		 + '}'
		 + '</style>'
		 + '<div id="common-header">'
		 + '<div class="wrap bgcolor">'
		 + '  <div class="leftbox">'
		 + '    <div class="logo">'
		 + '      <a href="http://blog.seesaa.jp"><img src="http://blog.seesaa.jp/img/common_header/logo/' + logoImg + '" alt="Seesaaブログ" border="0" /></a>'
		 + '    </div>'
		 + '    <div class="prbox bgcolor">'
		 + '      <div class="pr1 bgcolor" id="common-header-ads">');
    document.write('<ifr' + 'ame name="maad" src="http://match.seesaa.jp/ot_square.pl?'
                   + 'hid='  + '198'
                   + '&tid=' + 'seesaa_cmn_header'
                   + '&c='   + 1
                   + '&bg_c='     + bgColor.replace('#', '')
                   + '&title_c='  + textColor.replace('#', '')
                   + '&text_c='   + textColor.replace('#', '')
                   + '&border_c=' + bgColor.replace('#', '')
                   + '&link_c='   + '008000'
                   + '&referer='  + encodeURIComponent(document.referrer)
                   + '&url='      + encodeURIComponent(location.href)
                   + '"');
    document.write(' width="'  + 237  + '"' +
                   ' height="' + 18 + '"' +
                   ' scrolling="no" frameborder="no" marginwidth="0" marginheight="0" allowTransparency="true"></ifr' + 'ame>');
    document.write(''
                   + '</div>'
                   + '      <div class="pr2 bgcolor">' + common_header_pr_html + '</div>'
		   + '      <div class="both"></div>'
		   + '    </div>'
		   + '    <div class="both"></div>'
		   + '  </div>'
		   + '  <div class="rightbox bgcolor"><div id="sbContainer" class="seesaaSearchBox"></div>'
		   + '  <script type="text/javascript"><!--\n'
		   + '  var seesaa_sb_keywords    =  ["ダイエット","アフィリエイト","引越し見積もり","アパート探し","マンション投資","新入学","春休み","地震対策","地震予知","iPhoneアプリ","海外旅行","旅行","ペット用品","置き換えダイエット","AKB48","メタボ","自動車免許","FX","iPhone5","コスプレ"];'
		   + '  var seesaa_sb_blog_url    = "http://blog.seesaa.jp"; '
		   + '  var seesaa_sb_tag_url     = "http://blog.seesaa.jp/tag"; '
		   + '  //--></script>'
		   + '  <script type="text/javascript" src="http://blog.seesaa.jp/js/common_header_sb.js" charset="Shift_JIS"></script>'		 
		 + '  </div>'
		   + '  <div class="both"></div>'
		   + '</div>'
		   + '</div>'
		   + '');
}
