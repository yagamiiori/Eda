using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ComboBoxClass : MonoBehaviour 
{
    /// <summary>マネージャーコンポ</summary>
    private GameManager gameManager;
    /// <summary>ユニットIDを表示しているTextコンポ（インスペクタからのみ設定する）</summary>
    [SerializeField]
    private Text text_UnitID;
    /// <summary>コンボボックスを開いた時のコンボボックスの画像</summary>
    public Sprite Sprite_UISprite;
    /// <summary>コンボボックスを開いた時のコンボボックスの背景画像</summary>
    public Sprite Sprite_Background;
    /// <summary>コンボボックスから特定のボタンを選択した時のdelegate</summary>
    public Action<int> OnSelectionChanged;

    [SerializeField]
	private bool _isComboBoxEnable = true;
    /// <summary>プルダウンボタンを押した時にプルダウンメニューを表示するか否か</summary>
    public bool IsComboBoxEnable
	{
		get
		{
			return _isComboBoxEnable;
		}
		set
		{
            // フィールドに値を設定する
			_isComboBoxEnable = value;

            // 子のButtonコンポを取得し、有無フラグに入力された値を設定する
			var button = comboButtonRectTransform.GetComponent<Button>();
			button.interactable = _isComboBoxEnable;

            // 子のImageコンポを取得し、カラーおよびスプライトを設定
			var image = comboImageRectTransform.GetComponent<Image>();
			image.color = image.sprite == null ? new Color(1.0f, 1.0f, 1.0f, 0.0f) : _isComboBoxEnable ? button.colors.normalColor : button.colors.disabledColor;

            // プレイヤーが再生可能でない場合はセッターを抜ける
            if (!Application.isPlaying) return;
				
            // 入力された値がfalseかつオーバーレイComboBoxがアクティブ状態の場合はコンボボックスを
			if (!_isComboBoxEnable && overlayGO.activeSelf)
				ToggleComboBox(false);
		}
	}

    [SerializeField]
	private int _itemsToDisplay = 4;
    /// <summary>プルダウンボタンを押した時にプルダウンメニューに一度に表示されるアイテム数</summary>
    public int ItemsToDisplay
	{
		get
		{
			return _itemsToDisplay;
		}
		set
		{
			if (_itemsToDisplay == value)
				return;
			_itemsToDisplay = value;
			Refresh();
		}
	}

	[SerializeField]
	private bool _hideFirstItem;
    /// <summary>プルダウンメニューで一番上のアイテムを表示するか否か</summary>
    public bool HideFirstItem
	{
		get
		{
			return _hideFirstItem;
		}
		set
		{
			if (value)
                // trueの場合はオフセット位置をデクリメントして一番上のアイテムを非表示にする
				scrollOffset--;
			else
                // falseの場合はオフセット位置をインクリメントして一番上のアイテムを表示する
                scrollOffset++;
			_hideFirstItem = value;
			Refresh();
		}
	}

	[SerializeField]
    private int _selectedClass = 0;
    /// <summary>プルダウンメニューで選択されたクラスのID</summary>
    public int SelectedClass
	{
		get 
		{
			return _selectedClass;
		}
		set
		{
			if (_selectedClass == value)
                // 現IDと選択されたIDが同じなら処理する必要なし
				return;

			if (value > -1 && value < ClassList.Length)
			{
                // 選択されたIDが正常範囲内であればフィールドに設定する
				_selectedClass = value;

                // クラスID設定メソッドをコールし、選択されたクラスIDを設定する
                SetClassIDtoManager(value);

				RefreshSelected();
			}
		}
	}

	[SerializeField]
	private ComboBoxItem[] _classList;
    /// <summary>プルダウンメニューを開いた時に表示されるアイテムの総数</summary>
    public ComboBoxItem[] ClassList
	{
		get
		{
			if (_classList == null)
                // 存在しなければ空の配列を生成する
				_classList = new ComboBoxItem[0];
			return _classList;
		}
		set
		{
			_classList = value;
			Refresh();
		}
	}

    /// <summary>ボタンをマスクするためのオーバーレイComboBoxオブジェクト</summary>
    private GameObject overlayGO;
	private int scrollOffset;
    /// <summary>ComboBoxのスクロールバー横幅のサイズ</summary>
    [SerializeField]
    private float _scrollbarWidth = 10.0f;

	private RectTransform _rectTransform;
    /// <summary>プルダウンメニューで一番上のアイテムを表示するか否か</summary>
    private RectTransform rectTransform
	{
		get
		{
			if (_rectTransform == null)
                // 持っていなければ取得する
                _rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
		set
		{
			_rectTransform = value;
		}
	}

	private RectTransform _buttonRectTransform;
    /// <summary>プルダウンボタン（最初から表示されてるボタン）のRectTransformコンポ</summary>
    private RectTransform buttonRectTransform
	{
		get
		{
			if (_buttonRectTransform == null)
                // 持っていなければ取得する
				_buttonRectTransform = rectTransform.Find("Button").GetComponent<RectTransform>();
			return _buttonRectTransform;
		}
		set
		{
			_buttonRectTransform = value;
		}
	}

	private RectTransform _comboButtonRectTransform;
    /// <summary>コンボボタン（プルダウンメニュー内のボタン）のRectTransformコンポ</summary>
    private RectTransform comboButtonRectTransform
	{
		get
		{
			if (_comboButtonRectTransform == null)
                // 持っていなければ取得する
                _comboButtonRectTransform = buttonRectTransform.Find("ComboButton").GetComponent<RectTransform>();
			return _comboButtonRectTransform;
		}
		set
		{
			_comboButtonRectTransform = value;
		}
	}

	private RectTransform _comboImageRectTransform;
    /// <summary>コンボボタン（プルダウンメニュー内のボタン）のImageオブジェクトのRectTransformコンポ</summary>
    private RectTransform comboImageRectTransform
	{
		get
		{
			if (_comboImageRectTransform == null)
                // 持っていなければ取得する
                _comboImageRectTransform = comboButtonRectTransform.Find("Image").GetComponent<RectTransform>();
			return _comboImageRectTransform;
		}
		set
		{
			_comboImageRectTransform = value;
		}
	}

	private RectTransform _comboTextRectTransform;
	private RectTransform comboTextRectTransform
	{
		get
		{
			if (_comboTextRectTransform == null)
				_comboTextRectTransform = comboButtonRectTransform.Find("Text").GetComponent<RectTransform>();
			return _comboTextRectTransform;
		}
		set
		{
			_comboTextRectTransform = value;
		}
	}

	private RectTransform _comboArrowRectTransform;
	private RectTransform comboArrowRectTransform
	{
		get
		{
			if (_comboArrowRectTransform == null)
				_comboArrowRectTransform = buttonRectTransform.Find("Arrow").GetComponent<RectTransform>();
			return _comboArrowRectTransform;
		}
		set
		{
			_comboArrowRectTransform = value;
		}
	}

	private RectTransform _scrollPanelRectTransfrom;
	private RectTransform scrollPanelRectTransfrom
	{
		get
		{
			if (_scrollPanelRectTransfrom == null)
				_scrollPanelRectTransfrom = rectTransform.Find("Overlay/ScrollPanel").GetComponent<RectTransform>();
			return _scrollPanelRectTransfrom;
		}
		set
		{
			_scrollPanelRectTransfrom = value;
		}
	}

	private RectTransform _itemsRectTransfrom;
	private RectTransform itemsRectTransfrom
	{
		get
		{
			if (_itemsRectTransfrom == null)
				_itemsRectTransfrom = scrollPanelRectTransfrom.Find("Items").GetComponent<RectTransform>();
			return _itemsRectTransfrom;
		}
		set
		{
			_itemsRectTransfrom = value;
		}
	}

	private RectTransform _scrollbarRectTransfrom;
	private RectTransform scrollbarRectTransfrom
	{
		get
		{
			if (_scrollbarRectTransfrom == null)
				_scrollbarRectTransfrom = scrollPanelRectTransfrom.Find("Scrollbar").GetComponent<RectTransform>();
			return _scrollbarRectTransfrom;
		}
		set
		{
			_scrollbarRectTransfrom = value;
		}
	}

	private RectTransform _slidingAreaRectTransform;
	private RectTransform slidingAreaRectTransform
	{
		get
		{
			if (_slidingAreaRectTransform == null)
				_slidingAreaRectTransform = scrollbarRectTransfrom.Find("SlidingArea").GetComponent<RectTransform>();
			return _slidingAreaRectTransform;
		}
		set
		{
			_slidingAreaRectTransform = value;
		}
	}

	private RectTransform _handleRectTransfrom;
	private RectTransform handleRectTransfrom
	{
		get
		{
			if (_handleRectTransfrom == null)
				_handleRectTransfrom = slidingAreaRectTransform.Find("Handle").GetComponent<RectTransform>();
			return _handleRectTransfrom;
		}
		set
		{
			_handleRectTransfrom = value;
		}
	}

	private void Awake()
	{
        // マネージャコンポ取得
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // オブジェクト生成および初期化メソッドをコール
        InitControl();
    }

    /// <summary>
    /// クラスID設定メソッド
    /// <para>　このプルダウンメニューが担当するユニットのIDを</para>
    /// <para>　親オブジェクトのTextコンポの文字列より取得し、それを元に</para>
    /// <para>　そのユニットIDのクラスフィールドへ選択されたクラスIDを設定する。</para>
    /// </summary>
    private void SetClassIDtoManager(int setterValue)
    {
        // ユニットIDのTextからユニットIDである最後の1文字(または2文字)を抜き出して定数リテラルに変換する
        int unitID = 0;
        if (4 == text_UnitID.text.Length)
        {
            // IDが1桁の場合は末尾1文字を抽出
            unitID = int.Parse(text_UnitID.text.Substring(text_UnitID.text.Length - 1, 1));
        }
        else
        {
            // IDが2桁の場合は末尾2文字を抽出
            unitID = int.Parse(text_UnitID.text.Substring(text_UnitID.text.Length - 2, 2));
        }

        // 設定されたクラスIDをマネージャーコンポに設定
        // TODO クラスIDやエレメントIDは0から始める？+1補正する処理が鬱陶しい
        gameManager.unitStateList[unitID - 1].classType = setterValue + 1;

        // 表示しているユニット画像を更新
//        var unitSpriteSet = new NameSelect();
//        unitSpriteSet.UnitSpriteSet(gameManager);
    }

	public void OnItemClicked(int index)
	{
		var selectionChanged = index != SelectedClass;
		SelectedClass = index;
		ToggleComboBox(true);
		if (selectionChanged && OnSelectionChanged != null)
			OnSelectionChanged(index);
	}

    /// <summary>
    /// プルダウンメニューアイテム追加メソッド
    /// <para>　スクリプトからプルダウンメニューアイテムを追加する場合に</para>
    /// <para>　コールし、アイテムを生成する。</para>
    /// </summary>
    public void AddItems(params object[] list)
	{
		var cbItems = new List<ComboBoxItem>();

		foreach (var obj in list)
		{
			if (obj is ComboBoxItem)
			{
				var item = (ComboBoxItem)obj;
				cbItems.Add(item);
				continue;
			}
			if (obj is string)
			{
				var item = new ComboBoxItem((string)obj, null, false, null);
				cbItems.Add(item);
				continue;
			}
			if (obj is Sprite)
			{
				var item = new ComboBoxItem(null, (Sprite)obj, false, null);
				cbItems.Add(item);
				continue;
			}
			throw new Exception("Only ComboBoxItem, string and Sprite types are allowed");
		}
		var newItems = new ComboBoxItem[ClassList.Length + cbItems.Count];
		ClassList.CopyTo(newItems, 0);
		cbItems.ToArray().CopyTo(newItems, ClassList.Length);
		Refresh();
		ClassList = newItems;
	}

    /// <summary>
    /// 全プルダウンメニュークリアメソッド
    /// </summary>
    public void ClearItems()
	{
		ClassList = new ComboBoxItem[0];
	}

    /// <summary>
    /// プルダウンメニュー内オブジェクト生成メソッド
    /// </summary>
    public void CreateControl()
	{
		rectTransform = GetComponent<RectTransform>();

        // プルダウンメニュー内のButtonオブジェクト生成
		var buttonGO = new GameObject("Button");
		buttonGO.transform.SetParent(transform, false);
		buttonRectTransform = buttonGO.AddComponent<RectTransform>();
		buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
		buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.sizeDelta.y);
		buttonRectTransform.anchoredPosition = Vector2.zero;

        // ComboButtonオブジェクト生成
        var comboButtonGO = new GameObject("ComboButton");
		comboButtonGO.transform.SetParent(buttonRectTransform, false);
		comboButtonRectTransform = comboButtonGO.AddComponent<RectTransform>();
		comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonRectTransform.sizeDelta.x);
		comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonRectTransform.sizeDelta.y);
		comboButtonRectTransform.anchoredPosition = Vector2.zero;

        // ComboButtonオブジェクトにImageコンポを追加し画像および画像タイプ（Sliced）を設定
        var comboButtonImage = comboButtonGO.AddComponent<Image>();
		comboButtonImage.sprite = Sprite_UISprite;
		comboButtonImage.type = Image.Type.Sliced;


        // ComboButtonオブジェクトの画像を設定
        var comboButtonButton = comboButtonGO.AddComponent<Button>();
		comboButtonButton.targetGraphic = comboButtonImage;

        // ComboButtonオブジェクトのカラー設定
		var comboButtonColors = new ColorBlock();
		comboButtonColors.normalColor = new Color32(255, 255, 255, 255);
		comboButtonColors.highlightedColor = new Color32(245, 245, 245, 255);
		comboButtonColors.pressedColor = new Color32(200, 200, 200, 255);
		comboButtonColors.disabledColor = new Color32(200, 200, 200, 128);
		comboButtonColors.colorMultiplier = 1.0f;
		comboButtonColors.fadeDuration = 0.1f;
		comboButtonButton.colors = comboButtonColors;

        // 矢印オブジェクト生成
        var comboArrowGO = new GameObject("Arrow");
		comboArrowGO.transform.SetParent(buttonRectTransform, false);
		var comboArrowText = comboArrowGO.AddComponent<Text>();
		comboArrowText.color = new Color32(0, 0, 0, 255);
        comboArrowText.alignment = TextAnchor.MiddleRight;
		comboArrowText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		comboArrowText.text = "▼";
		comboArrowRectTransform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
		comboArrowRectTransform.pivot = new Vector2(1.0f, 0.5f);
		comboArrowRectTransform.anchorMin = Vector2.right;
		comboArrowRectTransform.anchorMax = Vector2.one;
		comboArrowRectTransform.anchoredPosition = Vector2.zero;
		comboArrowRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboButtonRectTransform.sizeDelta.y);
		comboArrowRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, comboButtonRectTransform.sizeDelta.y);
		var comboArrowCanvasGroup = comboArrowGO.AddComponent<CanvasGroup>();
		comboArrowCanvasGroup.interactable = false;
		comboArrowCanvasGroup.blocksRaycasts = false;

        // Imageオブジェクト生成
        var comboImageGO = new GameObject("Image");
		comboImageGO.transform.SetParent(comboButtonRectTransform, false);
		var comboImageImage = comboImageGO.AddComponent<Image>();
		comboImageImage.color = new Color32(255, 255, 255, 0);
		comboImageRectTransform.pivot = Vector2.up;
		comboImageRectTransform.anchorMin = Vector2.zero;
		comboImageRectTransform.anchorMax = Vector2.up;
		comboImageRectTransform.anchoredPosition = new Vector2(4.0f, -4.0f);
		comboImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboButtonRectTransform.sizeDelta.y - 8.0f);
		comboImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, comboButtonRectTransform.sizeDelta.y - 8.0f);

        // Textオブジェクト生成
        var comboTextGO = new GameObject("Text");
		comboTextGO.transform.SetParent(comboButtonRectTransform, false);
		var comboTextText = comboTextGO.AddComponent<Text>();
		comboTextText.color = new Color32(0, 0, 0, 255);
		comboTextText.alignment = TextAnchor.MiddleLeft;
		comboTextText.lineSpacing = 1.2f;
		comboTextText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		comboTextRectTransform.pivot = Vector2.up;
		comboTextRectTransform.anchorMin = Vector2.zero;
		comboTextRectTransform.anchorMax = Vector2.one;
		comboTextRectTransform.anchoredPosition = new Vector2(10.0f, 0.0f);
		comboTextRectTransform.offsetMax = new Vector2(4.0f, 0.0f);
		comboTextRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, comboButtonRectTransform.sizeDelta.y);
	}

    /// <summary>
    /// オブジェクト生成および初期化メソッド
    /// </summary>
    public void InitControl()
	{
		var cbi = transform.Find("Button/ComboButton/Image");
		var cbt = transform.Find("Button/ComboButton/Text");
		var cba = transform.Find("Button/Arrow");
		if (cbi == null || cbt == null || cba == null)
		{
			foreach (Transform child in transform)
				Destroy(child);
			CreateControl();
		}

		comboButtonRectTransform.GetComponent<Button>().onClick.AddListener(() => { ToggleComboBox(false); });
		var dropdownHeight = comboButtonRectTransform.sizeDelta.y *  Mathf.Min(ItemsToDisplay, ClassList.Length - (HideFirstItem ? 1 : 0));

        // ボタンをマスクするためのオーバーレイComboBoxオブジェクトを生成
        overlayGO = new GameObject("Overlay");
		overlayGO.SetActive(false);

        // オーバーレイComboBoxにImageコンポを追加
        var overlayImage = overlayGO.AddComponent<Image>();

        // カラーを設定
		overlayImage.color = new Color32(0, 0, 0, 0);

        // Canvasコンポをゲトコンできるまで親オブジェクトをループ
		var canvasTransform = transform;
        while (canvasTransform.GetComponent<Canvas>() == null)
        {
            canvasTransform = canvasTransform.parent;
        }
        // オーバーレイComboBoxオブジェクトをCanvasコンポを持つオブジェクトの子にする
		overlayGO.transform.SetParent(canvasTransform, false);

        // オーバーレイComboBoxにRectTransformコンポを追加
        var overlayRectTransform = overlayGO.GetComponent<RectTransform>();

        // オーバーレイComboBoxのRect系の設定
		overlayRectTransform.anchorMin = Vector2.zero;
		overlayRectTransform.anchorMax = Vector2.one;
		overlayRectTransform.offsetMin = Vector2.zero;
		overlayRectTransform.offsetMax = Vector2.zero;
		overlayGO.transform.SetParent(transform, false);

        // オーバーレイComboBoxにButtonコンポを追加してアルファ値0の透過Imageを設定
        var overlayButton = overlayGO.AddComponent<Button>();
		overlayButton.targetGraphic = overlayImage;

        // なんかを追加してるっぽい
		overlayButton.onClick.AddListener(() => { ToggleComboBox(false); });

        // オーバーレイComboBoxオブジェクトに実装するスクロールバーを生成
        var scrollPanelGO = new GameObject("ScrollPanel");

        // スクロールバーにImageコンポを追加し画像および画像タイプ（Sliced）を設定
        var scrollPanelImage = scrollPanelGO.AddComponent<Image>();
		scrollPanelImage.sprite = Sprite_UISprite;
		scrollPanelImage.type = Image.Type.Sliced;

        // スクロールバーをオーバーレイComboBoxオブジェクトの子に設定
		scrollPanelGO.transform.SetParent(overlayGO.transform, false);

        // スクロールバーのRect系の設定
        scrollPanelRectTransfrom.pivot = new Vector2(0.5f, 1.0f);
		scrollPanelRectTransfrom.anchorMin = Vector2.zero;
		scrollPanelRectTransfrom.anchorMax = Vector2.one;

		scrollPanelGO.transform.SetParent(transform, false);
		scrollPanelRectTransfrom.anchoredPosition = new Vector2(0.0f, -comboButtonRectTransform.sizeDelta.y);
		scrollPanelGO.transform.SetParent(overlayGO.transform, false);

		scrollPanelRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboButtonRectTransform.sizeDelta.x);
		scrollPanelRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);

		var scrollPanelScrollRect = scrollPanelGO.AddComponent<ScrollRect>();
		scrollPanelScrollRect.horizontal = false;
		scrollPanelScrollRect.elasticity = 0.0f;
		scrollPanelScrollRect.movementType = ScrollRect.MovementType.Clamped;
		scrollPanelScrollRect.inertia = false;
		scrollPanelScrollRect.scrollSensitivity = comboButtonRectTransform.sizeDelta.y;
		scrollPanelGO.AddComponent<Mask>();

        // スクロールバーの横幅を決定？
        var scrollbarWidth = ClassList.Length - (HideFirstItem ? 1 : 0) > _itemsToDisplay ? _scrollbarWidth : 0.0f;

        // Itemsオブジェクト（全てのコンボボタンの親オブジェクト）を生成
		var itemsGO = new GameObject("Items");
		itemsGO.transform.SetParent(scrollPanelGO.transform, false);
		itemsRectTransfrom = itemsGO.AddComponent<RectTransform>();
		itemsRectTransfrom.pivot = Vector2.up;
		itemsRectTransfrom.anchorMin = Vector2.up;
		itemsRectTransfrom.anchorMax = Vector2.one;
		itemsRectTransfrom.anchoredPosition = Vector2.right;
		itemsRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollPanelRectTransfrom.sizeDelta.x - scrollbarWidth);
		var itemsContentSizeFitter = itemsGO.AddComponent<ContentSizeFitter>();
		itemsContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		itemsContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		var itemsGridLayoutGroup = itemsGO.AddComponent<GridLayoutGroup>();
		itemsGridLayoutGroup.cellSize = new Vector2(comboButtonRectTransform.sizeDelta.x - scrollbarWidth, comboButtonRectTransform.sizeDelta.y);
		itemsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		itemsGridLayoutGroup.constraintCount = 1;
		scrollPanelScrollRect.content = itemsRectTransfrom;

        // Scrollbarオブジェクトを生成
		var scrollbarGO = new GameObject("Scrollbar");
		scrollbarGO.transform.SetParent(scrollPanelGO.transform, false);
		var scrollbarImage = scrollbarGO.AddComponent<Image>();
		scrollbarImage.sprite = Sprite_Background;
		scrollbarImage.type = Image.Type.Sliced;
		var scrollbarScrollbar = scrollbarGO.AddComponent<Scrollbar>();
		var scrollbarColors = new ColorBlock();
		scrollbarColors.normalColor = new Color32(128, 128, 128, 128);
		scrollbarColors.highlightedColor = new Color32(128, 128, 128, 178);
		scrollbarColors.pressedColor = new Color32(88, 88, 88, 178);
		scrollbarColors.disabledColor = new Color32(64, 64, 64, 128);
		scrollbarColors.colorMultiplier = 2.0f;
		scrollbarColors.fadeDuration = 0.1f;
		scrollbarScrollbar.colors = scrollbarColors;
		scrollPanelScrollRect.verticalScrollbar = scrollbarScrollbar;
		scrollbarScrollbar.direction = Scrollbar.Direction.BottomToTop;
		scrollbarRectTransfrom.pivot = Vector2.one;
		scrollbarRectTransfrom.anchorMin = Vector2.one;
		scrollbarRectTransfrom.anchorMax = Vector2.one;
		scrollbarRectTransfrom.anchoredPosition = Vector2.zero;
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollbarWidth);
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);

		var slidingAreaGO = new GameObject("SlidingArea");
		slidingAreaGO.transform.SetParent(scrollbarGO.transform, false);
		slidingAreaRectTransform = slidingAreaGO.AddComponent<RectTransform>();
		slidingAreaRectTransform.anchoredPosition = Vector2.zero;
		slidingAreaRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
		slidingAreaRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight - scrollbarRectTransfrom.sizeDelta.x);

		var handleGO = new GameObject("Handle");
		handleGO.transform.SetParent(slidingAreaGO.transform, false);
		var handleImage = handleGO.AddComponent<Image>();
		handleImage.sprite = Sprite_UISprite;
		handleImage.type = Image.Type.Sliced;
		handleImage.color = new Color32(255, 255, 255, 150);
		scrollbarScrollbar.targetGraphic = handleImage;
		scrollbarScrollbar.handleRect = handleRectTransfrom;
		handleRectTransfrom.pivot = new Vector2(0.5f, 0.5f);
		handleRectTransfrom.anchorMin = new Vector2(0.5f, 0.5f);
		handleRectTransfrom.anchorMax = new Vector2(0.5f, 0.5f);
		handleRectTransfrom.anchoredPosition = Vector2.zero;
		handleRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollbarWidth);
		handleRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollbarWidth);

		IsComboBoxEnable = IsComboBoxEnable;

		if (ClassList.Length < 1)
			return;
		Refresh();
	}

	public void Refresh()
	{
		var itemsGridLayoutGroup = itemsRectTransfrom.GetComponent<GridLayoutGroup>();
		var itemsLength = ClassList.Length - (HideFirstItem ? 1 : 0);
		var dropdownHeight = comboButtonRectTransform.sizeDelta.y *  Mathf.Min(_itemsToDisplay, itemsLength);
		var scrollbarWidth = itemsLength > ItemsToDisplay ? _scrollbarWidth : 0.0f;
		scrollPanelRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollbarWidth);
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);
		slidingAreaRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight - scrollbarRectTransfrom.sizeDelta.x);
		itemsRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollPanelRectTransfrom.sizeDelta.x - scrollbarWidth);
		itemsGridLayoutGroup.cellSize = new Vector2(comboButtonRectTransform.sizeDelta.x - scrollbarWidth, comboButtonRectTransform.sizeDelta.y);
		for (var i = itemsRectTransfrom.childCount - 1; i > -1; i--)
			DestroyImmediate(itemsRectTransfrom.GetChild(0).gameObject);

        // 指定された数分のプルダウンメニュー内ボタンについて表示名と表示画像を設定
		for (var i = 0; i < ClassList.Length; i++)
		{
			if (HideFirstItem && i == 0)
				continue;

			var item = ClassList[i];    // フィールド載せ替え
			item.OnUpdate = Refresh;
			var itemTransform = Instantiate(comboButtonRectTransform) as Transform;
			itemTransform.SetParent(itemsRectTransfrom, false);
			itemTransform.GetComponent<Image>().sprite = null;

            // プルダウンメニュー内のボタン表示名Textを設定
			var itemText = itemTransform.Find("Text").GetComponent<Text>();
			itemText.text = item.ClassName;

            // プルダウンメニュー内のボタン表示名Textのカラーを設定
            if (item.IsDisabled)
				itemText.color = new Color32(174, 174, 174, 255);

            // プルダウンメニュー内のボタン表示画像を設定
			var itemImage = itemTransform.Find("Image").GetComponent<Image>();
			itemImage.sprite = item.Image;

            // プルダウンメニュー内のボタン表示画像のカラーを設定
            itemImage.color = item.Image == null ? new Color32(255, 255, 255, 0) : item.IsDisabled ? new Color32(255, 255, 255, 147) : new Color32(255, 255, 255, 255);

            // プルダウンメニュー内のButtonコンポを取得
			var itemButton = itemTransform.GetComponent<Button>();
			itemButton.interactable = !item.IsDisabled;

			var index = i;
			itemButton.onClick.AddListener(
				delegate()
				{
					OnItemClicked(index);
                    // インスペクタに表示するプルダウンメニュー内ボタンの設定項目を生成？
					if (item.OnSelect != null)
						item.OnSelect();
				}
			);
		}
		RefreshSelected();
		UpdateComboBoxImages();
		UpdateGraphics();
		FixScrollOffset();
	}

	public void RefreshSelected()
	{
		var comboButtonImage = comboImageRectTransform.GetComponent<Image>();
		var item = SelectedClass > -1 && SelectedClass < ClassList.Length ? ClassList[SelectedClass] : null;
		var includeImage = item != null && item.Image != null;
		comboButtonImage.sprite = includeImage ? item.Image : null;
		var comboButtonButton = comboButtonRectTransform.GetComponent<Button>();
		comboButtonImage.color = includeImage ? (IsComboBoxEnable ? comboButtonButton.colors.normalColor : comboButtonButton.colors.disabledColor) : new Color(1.0f, 1.0f, 1.0f, 0);
		UpdateComboBoxImage(comboButtonRectTransform, includeImage);
		comboTextRectTransform.GetComponent<Text>().text = item != null ? item.ClassName : "";

		if (!Application.isPlaying)
            // Unityプレイヤーで再生不可能な場合は終了
            return;

        // 全ての子オブジェクトのImageコンポを取得し、カラーを設定
		var i = 0;
		foreach (Transform child in itemsRectTransfrom)
		{
			comboButtonImage = child.GetComponent<Image>();
			comboButtonImage.color = SelectedClass == i + (HideFirstItem ? 1 : 0) ? comboButtonButton.colors.highlightedColor : comboButtonButton.colors.normalColor;
			i++;
		}
	}

	private void UpdateComboBoxImages()
	{
		var includeImages = false;
		foreach (var item in ClassList)
		{
			if (item.Image != null)
			{
				includeImages = true;
				break;
			}
		}
		foreach (Transform child in itemsRectTransfrom)
			UpdateComboBoxImage(child, includeImages);
	}

	private void UpdateComboBoxImage(Transform comboButton, bool includeImage)
	{
		comboButton.Find("Text").GetComponent<RectTransform>().offsetMin = Vector2.right * (includeImage ? comboImageRectTransform.rect.width + 8.0f : 10.0f);
	}

	private void FixScrollOffset()
	{
		var selectedIndex = SelectedClass + (HideFirstItem ? 1 : 0);
		if (selectedIndex < scrollOffset)
			scrollOffset = selectedIndex;
		else
			if (selectedIndex > scrollOffset + ItemsToDisplay - 1)
				scrollOffset = selectedIndex - ItemsToDisplay + 1;
		var itemsCount = ClassList.Length - (HideFirstItem ? 1 : 0);
		if (scrollOffset > itemsCount - ItemsToDisplay)
			scrollOffset = itemsCount - ItemsToDisplay;
		if (scrollOffset < 0)
			scrollOffset = 0;
		itemsRectTransfrom.anchoredPosition = new Vector2(0.0f, scrollOffset * rectTransform.sizeDelta.y);
	}
	
	private void ToggleComboBox(bool directClick)
	{
		overlayGO.SetActive(!overlayGO.activeSelf);

        // コンボボックスがアクティブの場合
        if (overlayGO.activeSelf)
        {
            var curTransform = transform;
            do
            {
                curTransform.SetAsLastSibling();
            }
            while ((curTransform = curTransform.parent) != null);
            FixScrollOffset();
        }
        // コンボボックスが非アクティブの場合
        else
        {
            if (directClick)
            {
                scrollOffset = (int)Mathf.Round(itemsRectTransfrom.anchoredPosition.y / rectTransform.sizeDelta.y);
            }
        }
    }

    /// <summary>
    ///  プルダウンメニュー再構築メソッド？
    ///  <para>　プルダウンメニューのサイズや矢印オブジェクトの配置を決めてるっぽい</para>
    /// </summary>
	public void UpdateGraphics()
	{
		if (overlayGO != null)
		{
			var scrollbarWidth = ClassList.Length - (HideFirstItem ? 1 : 0) > ItemsToDisplay ? _scrollbarWidth : 0.0f;
			handleRectTransfrom.offsetMin = -scrollbarWidth / 2 * Vector2.one;
			handleRectTransfrom.offsetMax = scrollbarWidth / 2 * Vector2.one;
		}
		if (rectTransform.sizeDelta != buttonRectTransform.sizeDelta && buttonRectTransform.sizeDelta == comboButtonRectTransform.sizeDelta)
		{
			buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
			buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.sizeDelta.y);
			comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
			comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.sizeDelta.y);
			comboArrowRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.y);
			comboImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboImageRectTransform.rect.height);
			comboTextRectTransform.offsetMax = new Vector2(4.0f, 0.0f);
			if (overlayGO == null)
				return;
			scrollPanelRectTransfrom.SetParent(transform, false);
			scrollPanelRectTransfrom.anchoredPosition = new Vector2(0.0f, -comboButtonRectTransform.sizeDelta.y);
			scrollPanelRectTransfrom.SetParent(overlayGO.transform, false);
			scrollPanelRectTransfrom.GetComponent<ScrollRect>().scrollSensitivity = comboButtonRectTransform.sizeDelta.y;
			UpdateComboBoxImage(comboButtonRectTransform, ClassList[SelectedClass].Image != null);
			Refresh();
		}
	}
}