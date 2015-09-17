function sfcheck(formname){
	if (document.forms[formname].vs[0].checked){
		document.forms[formname].action = 'http://match.seesaa.jp/ot_listing.pl';
		document.forms[formname].keyword.name = 'k';
	}
	if (document.forms[formname].vs[1].checked){
		var actionurl = article_search_blogurl + 'pages/user/search/';
		document.forms[formname].action = actionurl;
		document.forms[formname].keyword.name = 'keyword';
	}
return true;
}
