using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// ディスプレイカラー変更クラス
/// <para>　BattleStart時に画面を黄色にする</para>
/// </summary>
public class DisplayColorGO : MonoBehaviour 
{
    /// <summary>画面カラーのフェード時間</summary>
    public float fadeTime = 0;
    /// <summary>経過した秒</summary>
    private float elapsedSec = 0;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private DisplayColorGO() { }

    public void ColorChange()
    {
        // 画面カラーのフェード時間を初期化
        fadeTime = 0.6f;

        // 初期カラーと到達カラーを設定(255で割る)
        var imageCompo = this.gameObject.GetComponent<Image>();
        Color startColor = new Color(0.51f, 0.41f, 0.13f, 0);
        Color endColor = new Color(0.51f, 0.41f, 0.13f, 0.601f);

        StartCoroutine(ColorCor(startColor, endColor, imageCompo));
    }

    private IEnumerator ColorCor(Color a, Color b, Image imageCompo)
    {
        while (true)
        {
            elapsedSec += Time.deltaTime;
            if (20.0f < elapsedSec)
            {
                // Lerp処理が終了したらループを抜ける
                break;
            }
            // アルファ値をLerp
            imageCompo.color = Color.Lerp(a, b, elapsedSec * fadeTime);
            yield return null;
        }
    }

    /// <summary>
    /// DisplayColorGO消去メソッド
    /// <para>　本オブジェクトを消去する。</para>
    /// </summary>
    public void Destroy()
    {
        Destroy(this);
    }
}
