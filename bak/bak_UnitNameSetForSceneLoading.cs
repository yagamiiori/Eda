using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

public class UnitNameSetForSceneLoading : MonoBehaviour
{
    /// <summary>クラス名表示用テキストフィールドリスト</summary>
    public List<Text> ClassNameList = new List<Text>();
    /// <summary>ユニット名表示用テキストフィールドリスト</summary>
    public List<Text> UnitNameList = new List<Text>();
    /// <summary>マネージャコンポ</summary>
    private GameManager gameManager;
    /// <summary>Canvas</summary>
    private GameObject canVas;

    /// <summary>コンストラクタ</summary>
    private UnitNameSetForSceneLoading() { }

    void Start()
    {
        // マネージャコンポを取得
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // ゲームオブジェクト"Canvas"取得
        canVas = GameObject.FindWithTag("Canvas");

        // 全ユニット数分のユニット名表示用テキストコンポを取得し、リストに格納
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName0").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName1").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName2").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName3").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName4").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName5").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName6").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName7").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName8").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName9").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName10").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName11").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName12").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName13").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName14").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName15").GetComponent<Text>());

        int unitID = 0;
        foreach (Text t in UnitNameList)
        {
            // ユニット名をユニットネーム表示枠に設定
            t.text = gameManager.unitStateList[unitID].unitName;

            unitID++;
        }
    }
}
