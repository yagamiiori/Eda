
    private AddObjectColor palfxColor;		// 光沢付与クラス
    private GameObject palfxTarget;		    // 光沢付与対象オブジェクト
    float timer;							// 光沢付与用タイマー
    float waitingTime = 3.0f;				// 光沢付与用タイマーマージン値

    void Update()
    {
		// 光沢付与処理
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            // オブジェクトに光沢を付与（※AddObjectColorクラスは同フォルダ内AddObjectColor.csを参照）
            palfxColor.PalfxStart(0.5f, 0.1f, 0.5f, Color.gray, palfxTarget);
            timer = 0; // タイマーリセット
        }
	}

