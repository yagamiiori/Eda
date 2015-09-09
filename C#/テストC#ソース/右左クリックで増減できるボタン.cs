using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

public class UnitSelectButtonSol : 
    MonoBehaviour,
    IUnitSelect,                            //  ユニットセレクトIF
    IPointerEnterHandler,
    IPointerExitHandler
{
    private GameManager gameManager;        // マネージャコンポ
    public int mouseOverJug = 0;            // マウスオーバー判定フラグ

    // ----------------------------------------
    // Startメソッド
    // ----------------------------------------
    void Start()
    {
        // マネージャコンポ取得
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // -----------------------------------
    // カーソルエントリーメソッド
    // オブジェクト上にマウスカーソルがオーバーした時にコールされ、
    // マウスボタンの左右を判定するメソッドをコルーチンでコールする。
    // -----------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        // マウスオーバー判定フラグをON
        mouseOverJug = 1;

        // マウスクリック用イベントハンドラをコール
        StartCoroutine("MouseClickHandler");
    }

    // -----------------------------------
    // カーソルエスケープメソッド
    // オブジェクト上からマウスカーソルがリリースされた時にコールされ、
    // 起動していたコルーチンを停止させる。
    // -----------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        // マウスオーバー判定フラグをOFF
        mouseOverJug = 0;

        // マウスクリック用イベントハンドラを停止
        StopCoroutine("MouseClickHandler");
    }

    // -----------------------------------
    // マウスクリック判定メソッド
    // オブジェクト上にマウスオーバーされている時にコールされ、
    // マウスの右クリックと左クリックで別の処理を行う。
    // -----------------------------------
    public IEnumerator MouseClickHandler()
    {
        // 永続ループ（ただし、マウスオーバーを抜けたらreturnする）
        while (1 == mouseOverJug)
        {
            // マウス左クリックされた場合
            if (Input.GetMouseButtonDown(0))
            {
				// 処理
            }
            // マウス右クリックされた場合
            else if (Input.GetMouseButtonDown(1))
            {
				// 処理
            }

            // コルーチンを抜ける
            yield return null;
        }
    }
}
