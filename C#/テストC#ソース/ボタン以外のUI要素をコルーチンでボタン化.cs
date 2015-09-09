using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

public class UnitSelectButtonSol : 
    MonoBehaviour,
    IPointerEnterHandler,					// イベントハンドラIF
    IPointerExitHandler						// イベントハンドラIF
{
    public int mouseOverJug = 0;            // マウスオーバー判定フラグ

    // -----------------------------------
    // カーソルエントリーメソッド
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
    // -----------------------------------
    public IEnumerator MouseClickHandler()
    {
        // 永続ループ（ただし、マウスオーバーを抜けたらreturnする）
        while (1 == mouseOverJug)
        {
            // マウス左クリックされた場合
            if (Input.GetMouseButtonDown(0))
            {
            }
            // マウス右クリックされた場合
            else if (Input.GetMouseButtonDown(1))
            {
            }

            // コルーチンを抜ける
            yield return null;
        }
    }
}
