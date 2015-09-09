function pagenavi(w)
{
/** ページナビ for FC2ブログ
 * /page-1.html
 * /category2-1.html
 * /blog-date-200807-1.html
 * /?tag=%A5%D1%A5%F3%A5%C0&page=1
 * /?q=%A5%D1%A5%F3%A5%C0&page=1
 */
  if (n == '')
    return;
  url = nextpageurl;
  add = -1;
  if (url == '') {
    url = prevpageurl;
    add = 1;
  }
  ext = '.html';
  if ((i = url.indexOf('/page-')) != -1) {
    c = url.substring(i + 6, url.length - 5);
    base = url.substring(0, i + 6);
  } else if (url.indexOf('/category') != -1
    || url.indexOf('/blog-date-') != -1) {
    i = url.lastIndexOf('-');
    c = url.substring(i + 1, url.length - 5);
    base = url.substring(0, i + 1);
  } else if ((i = url.indexOf('page=')) != -1) {
    c = url.substring(i + 5);
    base = url.substring(0, i + 5);
    ext = '';
  } else {
    c = 0;
    add = 0;
    base = '';
    ext = '';
  }
  n = Number(n);
  if (n < 1) n = 1;
  c = Number(c) + add + 1;
  if (c < 1) c = 1;
  if (c > n) c = n;
  if (w < 0) w = 0;
  ww = 2 * w + 1;
  for (i = 1; i <= n; i++) {
    dot = ' <a href=\"' + base + (i - 1) + ext + '\" title=\"' + i + '\">..</a> '
    if ((c - w <= i && i <= c + w) || i == 1 || i == n
      || (i == 2 && c - w - 1 == i) || (i == n - 1 && c + w + 1 == i)) {
      if (i == c) {
        document.write(' <b> ' + i + ' </b> ');
      } else {
        document.write(' <a href=\"' + base + (i - 1) + ext + '\">' + i + '</a> ');
      }
    } else if (i < c - w) {
      if (i - 1 <= (c - w - 2) % ww) {
        if (i - 1 == Math.floor(((c - w - 2) % ww + 1) / 2))
          document.write(dot);
      } else if ((c - i) % ww == 0)
        document.write(dot);
    } else if (i > c + w) {
      if (n - i <= (n - c + w) % ww) {
        if (n - i == Math.floor(((n - c + w) % ww + 1) / 2))
          document.write(dot);
      } else if ((i - c) % ww == 0)
        document.write(dot);
    }
  }
}
