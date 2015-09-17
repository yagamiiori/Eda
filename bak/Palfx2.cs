using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//　関数名：オブジェクトカラーPalfxクラス
//　機能：オブジェクトの持つマテリアルの光沢(輝度)を変更する
//　継承：MonoBehaviour
//　種別：通常クラス
//　アタッチ先：GameObject
//　保持メソッド：
//　リダイレクト：なし
//
//　詳細：
//　　　　
//
//  呼び出し例：
//
//　履歴：
//
////////////////////////////////////////////////////////////////////////////////////////
public class Palfx2 : MonoBehaviour
{
    /// <summary>フェードアウト処理を行う秒</summary>
    public float fadeoutSec;
    /// <summary>フェードアウト処理後にその状態を維持する秒</summary>
    public float fadeoutKeepSec;
    /// <summary>フェードイン処理を行う秒</summary>
    public float fadeinSec;
    /// <summary>フェードイン処理後にその状態を維持する秒</summary>
    public float fadeinKeepSec;
    /// <summary>輝度を変えるMaterial</summary>
    public Material flashingMaterial;
    /// <summary>発光の有無</summary>
    public bool isFlashing = true;
    /// <summary>輝度（初期値）</summary>
    private Color initialVal = new Color(0, 0, 0);
    /// <summary>輝度（到達値）</summary>
    private Color ReachingVal = new Color(0.7f, 0.7f, 0.7f);
    /// <summary>経過した秒</summary>
    private float elapsedSec = 0;

    void Start()
    {
        // 参考：http://dnasoftwares.hatenablog.com/entry/2015/03/19/100108
        flashingMaterial.EnableKeyword("_Emission");
    }

    public void Update()
    {
        if (isFlashing)
        {
            if ("Default UI Material" == flashingMaterial.name)
            {
                // Imageコンポがマテリアルを持っていない場合は発光不可のため本スクリプトを停止する
                this.enabled = false;
            }
            if (0 >= fadeoutSec || 0 >= fadeinSec)
            {
	            // フェードアウト及びフェードインを行う秒の設定が不正な場合は発光不可のため本スクリプトを停止する
                this.enabled = false;
            }

            // 経過時間を測定
            elapsedSec += Time.deltaTime;

            if (elapsedSec < fadeoutSec)
            {
                // フェードアウト時間中はtoColorへと徐々に変化させる
                flashingMaterial.SetColor("_EmissionColor", Color.Lerp(initialVal, ReachingVal, elapsedSec / fadeoutSec));
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec)
            {
                // フェードアウト維持時間中はtoColorの色を維持する
                flashingMaterial.SetColor("_EmissionColor", ReachingVal);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec)
            {
                // フェードイン時間中はfromColorへと徐々に変化させる
                flashingMaterial.SetColor("_EmissionColor", Color.Lerp(ReachingVal, initialVal, (elapsedSec - fadeoutSec - fadeoutKeepSec) / fadeinSec));
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec + fadeinKeepSec)
            {
                // フェードイン維持時間中はfromColorの色を維持する
                flashingMaterial.SetColor("_EmissionColor", initialVal);
            }
            else
            {
                // 全ての処理が完了したら経過時間を0に戻してフェードアウトから再度実行する
                elapsedSec = 0;
            }
        }
    }
}

