(
	function()
	{
		// -------------------------------------------------------------------- data
		var $data = {
			// PHP のURL
			url		: "http://vote1.fc2.com/poll.php",
			
			// error -> 1, 通常は空
			error	: "",
			
			// user id
			uid		: "12029851",
			
			// 投票タイトル
			title	: "",
			
			// ユーザのニックネーム
			nickname: "yohshiy",
			
			// ユーザ指定の色名
			clr		: "blue", // 不要
			
			// ユーザ指定の背景画像
			bgimg	: "",
			
			// 質問番号 : 質問毎のユニークなインデックス番号
			qnum 	: "3",
			
			// 質問
			quest 	: "使っている JavaScript 系新(変換)言語は ?",
			
			// 投票終了時刻	: UTC ( 0は未設定 )
			period	: "0"-0,
			
			// 結果閲覧制限	: 0-> いつでも可、1->未投票時は不可
			vrest	: "0"-0,
			
			// コメント取得 : 0-> ナシ、1-> 任意, 2->必須
			cget	: "1"-0,
			
			// コメント最大文字数
			cleng	: "25"-0,
			
			// コメント閲覧制限	: 0-> 誰でも可, 1-> 管理者のみ
			crest	: "0"-0,
			
			// 連続投稿禁止 : 0->No, 1->Yes
			pkick	: "1"-0,
			
			// 連続投稿禁止期間 ( 秒.  0は未設定 )
			kval	: "43200"-0,
			
			// 投票は一人一回 :true->Yes, false->No
			onetime : !!("43200"-0 && !"1"),
			
			// 投票状況 : 0-> 進行中, 1->削除済み, 2-> 終了
			ing		: "0"-0,
			
			// 選択肢の追加が許可されている場合
			// 追加できる選択肢の最大値が入る。
			// env.choice[N].extra をカウントし、追加可能か否か判定できる。
			// 許可されていない場合の値は 0.
			usraddlen	: "0"-0,
			
			// 選択肢の追加に関する値
			/* radio 部品
			 * <input type=radio name=env.extra_radioname value=env.extra_radioval
			 * 追加入力フォーム
			 * <input type=text name=env.extra_textname
			 */
			extra_radioname : "poll",
			extra_radioval 	: "0",
			extra_textname 	: "usrsel",
			
			// コメント入力フォーム name 属性の値
			comment_textname: "com",
			
			// 各選択肢とそれに関する値
			/* <img src=env.choice[N].img >
			 * <input type=radio name=env.choice[N].name value=env.choice[N].value >
			 *
			 * env.choice[N].text : 質問文
			 * env.choice[N].extra : 0-> 管理者の用意した選択肢、 1->追加された選択肢
			 */
			choice : [
								{
					name : "poll",
					value: "1",
					text : "Dart",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "2",
					text : "TypeScript",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "3",
					text : "CoffeeScript",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "4",
					text : "JSX",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "5",
					text : "Haxe",
					img  : "",
					extra: "0"-0
				},
								{
					name : "poll",
					value: "6",
					text : "その他",
					img  : "",
					extra: "0"-0
				},
								null
			],
			// hidden 用パラメータ 
			// <input type=hidden name=env.hides[N].name value=env.hides[N].value
			hides 	: [
								{
					name : "uid",
					value: "12029851"
				},
								{
					name : "no",
					value: "3"
				},
								{
					name : "mode",
					value: "on"
				},
								{
					name : "charset",
					value: document.charset || document.characterSet
				}
			]
		};
		var $style_title_fg = '#ffffff';
		var $style_title_bg = '#669999';
		var $style_main_fg = '#000000';
		var $style_main_bg = '#ffffff';
		var $style_transparent = '';
		var $style_border = '#000000';
		var $width = 180;
		// ------------------------------------------------------------------- /data
		// -------------------------------------------------------------------- item
		// ID プリフィクス
		var $id_base = 'fc2_vote_' + $data['uid'] + '_' + $data['qnum'];
		// 追加済選択肢数
		var $count_extra = 0;
		// 選択肢 (規定および追加済)
		var $items = [];
		for (var $i=0; $i<$data['choice'].length; $i++)
		{
			var $obj = $data['choice'][$i];
			if (! $obj)
			{
				continue;
			}
			var $id = $id_base + '_' + $obj['value'];
			var $img = $obj['img'] ? '<img src="' + $obj['img']
				+ '" style="vertical-align:middle;margin:0px 3px;"/>' : '';
			$items.push(
				'<input type="radio" name="' + $obj['name']
					+ '" value="' + $obj['value'] + '" id="' + $id + '" />' + $img
					+ '<label for="' + $id + '" style="cursor:pointer;">'
					+ $obj['text'] + '</label>'
			);
			$count_extra += $obj['extra'];
		}
		// 選択肢 (追加フォーム)
		var $extra_id = [
			$id_base + '_0',
			$id_base + '_0_' + $data['comment_textname']
		];
		if ($count_extra < $data['usraddlen'])
		{
			$items.push(
				'<input type="radio" name="' + $data['extra_radioname']
					+ '" value="' + $data['extra_radioval']
					+ '" id="' + $extra_id[0] + '" />'
					+ '<input type="text" name="' + $data['extra_textname']
					+ '" id="' + $extra_id[1] + '" style="width:120px;height:20px;" />'
			);
		}
		// 選択肢テーブル化
		var $style = [
			"font:12px/13px 'MS UI Gothic','Osaka'",
			'color:' + $style_main_fg,
			'text-align:left',
			'overflow:hidden'
		];
		$style = $implode(';', $style);
		var $txt = '';
		for (var $i=0; $i<$items.length; $i++)
		{
			$txt += '<tr><td style="' + $style + '">' + $items[$i] + '</td></tr>';
		}
		$items = $txt;
		var $style = [
			'border:0px',
			'border-spacing:0px',
			'border-collapse:collapse',
			'margin:auto',
			'width:' + ($width-6) + 'px',
			'table-layout:fixed'
		];
		$style = $implode(';', $style);
		$items = '<table style="' + $style + '">' + $items + '</table>';
		// 不可視属性パラメータ
		for (var $i=0; $i<$data['hides'].length; $i++)
		{
			var $obj = $data['hides'][$i];
			if (! $obj)
			{
				continue;
			}
			$items += '<input type="hidden" name="' + $obj['name']
				+ '" value="' + $obj['value'] + '" />';
		}
		// ------------------------------------------------------------------- /item
		// -------------------------------------------------------------------- html
		var $html = '';
		// ヘッダ
		var $style = [
			'color:' + $style_title_fg,
			'text-decoration:none'
		];
		$txt = '<a href="http://vote.fc2.com/" target="_blank" style="'
			+ $implode(';', $style) +'">FC2無料投票レンタル</a>'
		var $style = [
			'color:' + $style_title_fg,
			'background-color:' + $style_title_bg,
			'padding:3px 0px 3px 4px'
		];
		$html += $div(
			$txt,
			$style
		);
		// 質問
		var $style = [
			'background-color:#eeeeee',
			'padding:4px 2px 4px 4px'
		];
		$html += $div($data['quest'], $style);
		// アイテム
		var $style = [
			'border-top:1px solid ' + $style_border,
			'padding:2px 2px 6px 2px',
			'text-align:center'
		];
		if ($data['bgimg'])
		{
			$style.push('background-image:url(' + $data['bgimg'] +')');
		}
		if (! $style_transparent)
		{
			$style.push('background-color:' + $style_main_bg);
		}
		$html += $div($items, $style);
		// コメント
		var $comment = '';
		if ($data['cget'])
		{
			var $style = [
				'padding:1px 0px 0px 6px',
				'text-align:left'
			];
			$comment += $div(
				$data['cleng'] + '文字以内のコメント'
					+ ($data['cget']>1 ? ' （必須）' : ''),
				$style
			);
			var $style = [
				'padding:0px 0px 2px 8px',
				'text-align:left'
			];
			$comment += $div(
				'<input type="text" name="' + $data['comment_textname']
					+ '" id="' + $id_base + $data['comment_textname']
					+ '" style="width:' + ($width-16) + 'px;height:20px;" />',
				$style
			);
		}
		// 投票結果リンク
		var $result_link = '';
		if (! $data['vrest'])
		{
			var $style = [
				'padding:8px 0px 0px 8px',
				'float:left'
			];
			$result_link = $div(
				'<a href="' + $data['url']
					+ '?mode=result&amp;uid=' + $data['uid'] + '&amp;no=' + $data['qnum']
					+ '" style="color:#777777;" target="_blank">投票結果</a>',
				$style
			);
		}
		// 送信ボタン
		var $style = [
			'border-top:1px solid ' + $style_border,
			'background-color:#eeeeee',
			'padding:4px 4px 4px 0px',
			'text-align:right'
		];
		$html += $div(
			$comment + $result_link + '<input type="button" value="投票" id="'
				+ $id_base + '_button" style="height:23px;" />',
			$style
		);
		// 外枠
		var $style = [
			'color:#000000',
			"font:12px/13px 'MS UI Gothic','Osaka'",
			'border:1px solid ' + $style_border,
			'text-align:left',
			'width:' + $width + 'px'
		];
		$html = $div($html, $style);
		
		// PR枠
		var $style = [
			'background-color:#eeeeee',
			'color:#000000',
			"font:12px/13px 'MS UI Gothic','Osaka'",
			'border-bottom:1px solid ' + $style_border,
			'border-left:1px solid ' + $style_border,
			'border-right:1px solid ' + $style_border,
			'text-align:left',
			'width:' + $width + 'px'
		];
		//$txt = '<script type="text/javascript" src="http://ad.pitta.ne.jp/ads/a38e375901ad047bed132971cbe2fc7fd6a83799"></script>'
		//$html += $div(
		//	$txt,
		//	$style
		//);
		
		// <form>
		$html = '<form method="post" name="'+ $id_base
			+ '" action="' + $data['url']
			+ '" target="_blank" style="margin:0px;">' + $html + '</form><img src="http://media.fc2.com/counter_img.php?id=715" width="1" height="1">';
		document.write($html);
		// ------------------------------------------------------------------- /html
		// ------------------------------------------------------------------- event
		// disable
		var $button = $($id_base + '_button');
		$button.disabled = true;
		// radio
		var $radios = document[$id_base].poll;
		for (var $i=0; $i<$radios.length; $i++)
		{
			$radios[$i].onclick = function()
			{
				$button.disabled = false;
			};
		}
		// extra
		var $obj = $($extra_id[1]);
		if ($obj)
		{
			$obj.onclick = function()
			{
				$($extra_id[0]).checked = true;
				$button.disabled = false;
			};
		}
		// submit
		$button.onclick = function()
		{
			var $checked = false;
			for (var $i=0; $i<$radios.length; $i++)
			{
				if ($radios[$i].checked)
				{
					$checked = $radios[$i].id;
					break;
				}
			}
			if ($checked==$extra_id[0] && ! $($extra_id[1]).value.length)
			{
				alert('追加する選択肢の名前がカラッポ！');
				return;
			}
			if ($data['cget']>1)
			{
				if (! $($id_base + $data['comment_textname']).value.length)
				{
					alert('コメントを入れて');
					return;
				}
			}
			$button.disabled = true;
			document[$id_base].submit();
		};
		// ------------------------------------------------------------------ /event
		// ------------------------------------------------------------------ common
		// implode()
		function $implode($sep, $src)
		{
			if (! $src || ! $src.length)
			{
				return '';
			}
			var $dst = $src.shift();
			while ($src.length)
			{
				var $txt = $src.shift();
				if ($txt)
				{
					$dst += $sep + $txt;
				}
			}
			return $dst;
		}
		// div()
		function $div($val, $style)
		{
			$style = $implode(';', $style);
			return '<div style="' + $style + '">' + $val + '</div>';
		}
		// $()
		function $($id)
		{
			return document.getElementById($id);
		}
		// ----------------------------------------------------------------- /common
	}
)();