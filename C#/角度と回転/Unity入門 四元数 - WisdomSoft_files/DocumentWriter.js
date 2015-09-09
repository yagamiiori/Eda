$(function() {
	//Initialize Social Buttons
	var hatenaButtonHtml = '<a href="http://b.hatena.ne.jp/entry/' + document.URL + '" class="hatena-bookmark-button" title="このエントリーをはてなブックマークに追加">' +
		'<img src="http://b.st-hatena.com/images/entry-button/button-only.gif" alt="このエントリーをはてなブックマークに追加" width="20" height="20" style="border: none;" />' +
		'</a>' +
		'<script type="text/javascript" src="http://b.st-hatena.com/js/bookmark_button.js" charset="utf-8" async="async" ></script>';
	var facebookButtonHtml = GetFrameHtml('//www.facebook.com/plugins/like.php?href=' + document.URL + '&amp;send=false&amp;layout=button_count&amp;width=100&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=21&amp;appId=169570553120681', "100px", "21px");
	var googlePlusButtonHtml = '<div class="g-plusone" data-size="medium" style="display: inline"></div>';

	$(".HatenaButton").html(hatenaButtonHtml);
	$(".FacebookButton").html(facebookButtonHtml);
	$(".GooglePlusButton").html(googlePlusButtonHtml);

	//Initialize PickUp
	var pickUpHtml = '<div><a href="http://go.microsoft.com/?linkid=9773120" target="_blank">' +
				'<img src="http://i.msdn.microsoft.com/hh140859.180x150_claudia(ja-jp,MSDN.10).gif" width="180" height="150" alt="MSDN クラウド 技術解説コミック新登場 クラウド ガール - 窓と雲と碧い空 - " style="border-style:none" />' +
		'</a></div>'
	$(".PickUpItems").html(pickUpHtml);
	
	//Initialize Amazon With Lists
	var ajaxOptions = {
		type: "GET",
		url: "WishList.txt",
		dataType: "text",
		cache: false,
		success: function(data)
		{
			var asimList = data.split('\n');
			var withListHtml = "";
			for(var i = 0 ; i < asimList.length ; i++)
			{
				withListHtml += "<div>" + GetAmazonLink(asimList[i]) + "</div>";
			}
			$(".WishList").html(withListHtml);
		},
		error: function() { $(".WishList").html(GetAmazonVerticalList("プログラミング")); }
	};
	$.ajax(ajaxOptions);

	//Initialize Blogger Frame
	$(".Blogger").html(GetFrameHtml("http://wisdomsoft-blog.blogspot.com/", "100%", "800px"));

	//Initialize LikedIn
	$(".LinkedInProfile").each(function()
	{
		var args = $(this).text();
		var linkedInProfileHtml = '<' + 'script src="http://platform.linkedin.com/in.js" type="text/javascript"></script>' +
			'<' + 'script type="IN/MemberProfile" data-id="' + args + '" data-format="inline" data-related="false">' +
			'<' + '/script>';
		$(this).html(linkedInProfileHtml);
	});

	//Initialize Amazon Link
	$(".AmazonItems").each(function()
	{
		var args = $(this).text().split(',');
		var itemHtml= "";
		for(var i = 0 ; i < args.length ; ++i)
		{
			var asim = args[i].trim();
			itemHtml += GetAmazonLink(asim);
		}
		$(this).html(itemHtml);
	});
	$(".AmazonVerticalList").each(function()
	{
		var args = $(this).text();
		$(this).html(GetAmazonVerticalList(args));
	});
	$(".AmazonHorizontalList").each(function()
	{
		var args = $(this).text();
		$(this).html(GetAmazonHorizontalList(args));
	});
});

var isOldBrowser = !document.createElement('canvas').getContext;

function GetFrameHtml(uri, width, height)
{
	if (isOldBrowser)
	{
		return '<iframe src="' + uri + '" style="display:inline; border:none; overflow:hidden; width:' + width + '; height:' + height + ';" scrolling="no" frameborder="0"></iframe>';
	}
	else
	{
		return '<iframe src="' + uri + '" style="display:inline; border:none; overflow:hidden; padding:0px; margin:0px; width:' + width + '; height:' + height + ';" seamless="seamless"></iframe>';
	}
}

//Inline Frame
function WriteFrame(uri, width, height)
{
	var html = GetFrameHtml(uri, width, height);
	document.write(html);
}

//Amazon Item Link
function GetAmazonLink(asim)
{
	var result = '';
	var imageOnly = true;
	
	if (imageOnly)
	{
		result = '<a href="http://www.amazon.co.jp/gp/product/' + asim + '/ref=as_li_ss_il?ie=UTF8&tag=wisdo-22&linkCode=as2&camp=247&creative=7399&creativeASIN=' + asim +
			'"><img src="http://ws.assoc-amazon.jp/widgets/q?_encoding=UTF8&Format=_SL160_&ASIN=' + asim +
			'&MarketPlace=JP&ID=AsinImage&WS=1&tag=wisdo-22&ServiceVersion=20070822" alt="" style="border:none" /></a><img src="http://www.assoc-amazon.jp/e/ir?t=wisdo-22&l=as2&o=9&a=' + asim +
			'" width="1" height="1" alt="" style="border:none !important; margin:0px !important;" />'
	}
	else
	{	
		if (isOldBrowser)
			result = '<iframe src="http://rcm-jp.amazon.co.jp/e/cm?lt1=_blank&bc1=FFFFFF&IS2=1&nou=1&bg1=FFFFFF&fc1=000000&lc1=0000FF&t=wisdo-22&o=9&p=8&l=as4&m=amazon&f=ifr&ref=ss_til&asins=' + asim +
				'" style="width:120px;height:240px;" scrolling="no" marginwidth="0" marginheight="0" frameborder="0"></iframe>';
		else
			result = '<iframe src="http://rcm-jp.amazon.co.jp/e/cm?lt1=_blank&bc1=FFFFFF&IS2=1&nou=1&bg1=FFFFFF&fc1=000000&lc1=0000FF&t=wisdo-22&o=9&p=8&l=as4&m=amazon&f=ifr&ref=ss_til&asins=' + asim +
				'" style="border:none; overflow:hidden; width:120px; height:240px;" seamless="seamless"></iframe>';
	}

	return result;
}

//Amazon Search List(Live Link) - Vertical
function GetAmazonVerticalList(search)
{
	return GetFrameHtml("http://rcm-jp.amazon.co.jp/e/cm?t=wisdo-22&amp;o=9&amp;p=14&amp;l=st1&amp;mode=books-jp&amp;search=" + search + "&amp;fc1=000000&amp;lt1=_blank&amp;lc1=3366FF&amp;bg1=FFFFFF&amp;f=ifr", "160px", "600px");
}

//Amazon Search List(Live Link) - Horizontal
function GetAmazonHorizontalList(search)
{
	return GetFrameHtml("http://rcm-jp.amazon.co.jp/e/cm?t=wisdo-22&amp;o=9&amp;p=48&amp;l=st1&amp;mode=books-jp&amp;search=" + search + "&amp;fc1=000000&amp;lt1=_blank&amp;lc1=3366FF&amp;bg1=FFFFFF&amp;f=ifr", "728px", "90px");
}