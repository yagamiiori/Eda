
        float timer;			// タイマー
        int waitingTime = 10;		// マージン値

	void Updaste()
	{
	    // タイマー起動
	    timer += Time.deltaTime;

	    // マージン値になった場合
	    if (timer > waitingTime)
	    {
	        // --- やりたい処理 --- //

	        timer = 0; // タイマーをリセット
	    }
	}


