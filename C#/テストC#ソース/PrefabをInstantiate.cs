

	// GameObjectに格納しない場合
	Instantiate(Resources.Load("Prefab/PushEffect_GU"), transform.position, Quaternion.identity);


	// GameObjectに格納する場合
	void Start()
	{
	    GameObject obj = Instantiate(Resources.Load("Prefab/PushEffect_GU"), transform.position, Quaternion.identity) as GameObject;
	}


	// ---------------------------------------------------------------
	// Instantiateしたオブジェクトを特定オブジェクトの子にする場合（☆新しい。SetParentを使う）
	// ---------------------------------------------------------------
	// スプライトprefab用フィールド
    GameObject prefab;
    // prefabを表示
    prefab = Instantiate(sprite, vec, Quaternion.identity) as GameObject;
    prefab.transform.SetParent(canVas.transform, false);



	// ---------------------------------------------------------------
	// Instantiateしたオブジェクトを特定オブジェクトの子にする場合（★古い。parentを使う）
	// ---------------------------------------------------------------
	// 親オブジェクトを取得
	GameObject test1 = GameObject.Find("floor");
	// 子オブジェクトを取得
    GameObject test2 = Instantiate(Resources.Load("Prefab/PushEffect_GU"), transform.position, Quaternion.identity) as GameObject;
	// 子オブジェクトのparentフィールドに親オブジェクトを設定
	test2.transform.parent = test1.transform;

