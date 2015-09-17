function calc_fontSize(count, total, max, min, levels, min_fs, factor) {
    // logic is same as HTML::TagCloud
    if(! levels){ levels = 15 } // allow levels 
    if(! min_fs){ min_fs = 11 } // minimum font size
    if(! factor){ factor = 1  } // default factor

    max = Math.log(max);
    min = Math.log(min);

    if(max - min == 0){
      min = min - levels;
    } else {
      factor = levels / (max - min);
    }     
    if(total < levels) factor *= total / levels;

    return parseInt((Math.log(count) - min) * factor) + min_fs;
}

function tag_cloud(id, levels, min_fs, factor) {
  var parent = document.getElementById(id);
  var child  = parent.getElementsByTagName('a');
  var max   = 0;
  var min   = 0;
  var tags = new Array();
  for (var i = 0; i < child.length; i++) {
    var e = child.item(i);
    if (e.id == 'tag' || e.className == '_tag') {
      c = parseInt(e.title.split('/').pop());
      if (c > max) max = c;
      if (min == 0 || c < min) min = c;
	
      tags.push([ e, c ]);
    }
  }
  for (var i = 0; i < tags.length; i++) {
    var tag = tags[i];
    tag[0].style.fontSize = calc_fontSize(tag[1], tags.length, max, min, levels, min_fs, factor) + 'px';
    tag[0].setAttribute('rel','nofollow');
  }
}
