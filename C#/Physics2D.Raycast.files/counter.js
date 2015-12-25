(function(){
var re  = new RegExp("[<>'\"]", "g");
hatena_counter_screen = screen.width + "x" + screen.height+","+screen.colorDepth;
hatena_counter_ref = hatena_counter_ref.replace(re, "");
if(hatena_counter_mode == null ){var hatena_counter_mode = "default";}
document.write('<scr','ipt type="text/javascript" src="http://counter.hatena.ne.jp/',hatena_counter_name,'/',hatena_counter_id,'?js=1&mode=',hatena_counter_mode,'&c=',hatena_counter_screen,'&r=',hatena_counter_ref,'"></scr','ipt>');
})();
