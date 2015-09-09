/*--------------------------------------------------------------------------*
 *  
 *  SmoothScroll JavaScript Library V2
 *  
 *  MIT-style license. 
 *  
 *  2007-2011 Kazuma Nishihata 
 *  http://www.to-r.net
 *  
 *--------------------------------------------------------------------------*/
 
new function(){

	var attr ="data-tor-smoothScroll";//for html5 , if you can't use html5 , this value change "class"
	var attrPatt = /noSmooth/;
	var d = document;//document short cut

    window.requestAnimationFrame = (function(){
        return window.requestAnimationFrame		||
            window.webkitRequestAnimationFrame	||
            window.mozRequestAnimationFrame		||
            window.oRequestAnimationFrame		||
            window.msRequestAnimationFrame		||
            function(callback, element){
                window.setTimeout(callback, 1000 / 60);
            };
    })();
	
	/*
	 *add Event
	  -------------------------------------------------*/
	function addEvent(elm,listener,fn){
		try{ // IE
			elm.addEventListener(listener,fn,false);
		}catch(e){
			elm.attachEvent(
				"on"+listener
				,function(){
					fn.apply(elm,arguments)
				}
			);
		}
	}

	/*
	 *Start SmoothScroll
	  -------------------------------------------------*/
	function SmoothScroll(a){
        console.log(a.rel);
        var e = d.getElementById(a.rel.replace(/.*\#/,""));
		if(!e){
			return;
		}
		
		//Move point
		var docHeight = d.documentElement.scrollHeight,
		    winHeight = window.innerHeight || d.documentElement.clientHeight,
            tmpHeight = docHeight - winHeight;
            end = 0;

        do {end += e.offsetTop;} while (e = e.offsetParent);

		end = tmpHeight < end ? tmpHeight : end;
		
		//Current Point
		var start = window.pageYOffset || d.documentElement.scrollTop || d.body.scrollTop || 0;
		
		var isUp = end < start ? true : false;

        (function(start, end, isUp){
            var i = 10,
                loop = function(){
                    if(isUp && start >= end) {
                        start = Math.floor(start - (start - end) / i - 1);
                        window.scrollTo(0, start);
                    } else if(!isUp && start <= end) {
                        start = Math.floor(start + (end - start) / i + 1);
                        window.scrollTo(0, start);
                    } else {
                        scrollTo(0, end);
                        isGoing = false;
                    }

                    return;
                },
                isGoing = true;

            requestAnimationFrame(function(){
                loop();

                if (isGoing) {
                    requestAnimationFrame(arguments.callee);
                }
            });
        }(start, end, isUp));
	}

	/*
	 *Add SmoothScroll
	  -------------------------------------------------*/
	addEvent(window,"load",function(){
		var anchors = d.getElementsByTagName("a");
		for(var i = 0 ,len=anchors.length; i<len ; i++){
			if(!anchors[i].rel && !attrPatt.test(anchors[i].getAttribute(attr)) && 
				anchors[i].href.replace(/\#[a-zA-Z0-9_]+/,"") == location.href.replace(/\#[a-zA-Z0-9_]+/,"")){
				anchors[i].rel = anchors[i].href;
				anchors[i].href = "javascript:void(0)";
				anchors[i].onclick=function(){SmoothScroll(this)}
			}
		}
	});

}
