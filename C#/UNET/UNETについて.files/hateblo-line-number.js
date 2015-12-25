// Ref: http://7cc.hatenadiary.jp/entry/hatena-source-line

(function(document) {
	var pres = document.getElementsByTagName('pre');

	for (var i = pres.length; i--;) {
		var elem = makeLineNumber(pres[i]);
		pres[i].appendChild(elem);
	}

	function makeLineNumber(pre) {
		var ol = document.createElement('ol'),
				li = document.createElement('li'),
				df = document.createDocumentFragment(),
				br = pre.innerHTML.match(/\n/g);

		if (br) {
			ol.className = 'preLine';
			ol.setAttribute('role', 'presentation');

			for (var i = br.length; i--;) {
				var cloned_li = li.cloneNode(true);
				df.appendChild(cloned_li);
			}

			ol.appendChild(df);
		}
		return ol;
	}
})(document);
