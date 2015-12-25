using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// BattleStartクラス
/// <para>　ユニットの初期配置完了後にコールされ、BattleStart処理を実施する。</para>
/// </summary>
public class BattleStart : MonoBehaviour
{
    /// <summary>画面カラーのフェード時間</summary>
    public float fadeTime = 0;
    /// <summary>経過した秒</summary>
    private float elapsedSec = 0;
    /// <summary>フェード実施中判定</summary>
    private bool isFading = false;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private BattleStart() { }

    /// <summary>
    /// BattleStart初期起動メソッド
    /// </summary>
    public void StartingMeth()
    {
        // 画面カラーのフェード時間がインスペクタから設定されてなければ初期化する
        if (0 == fadeTime) fadeTime = 0.7f;

        // 画面カラーゲームオブジェクトとコンポを取得
        var imageGO = GameObject.Find("DisplayColorAtBattleStart");
        var imageCompo = imageGO.GetComponent<Image>();

        // 初期カラーと到達カラーを設定
        Color startColor = new Color(0.51f, 0.41f, 0.13f, 0);
        Color endColor = new Color(0.51f, 0.41f, 0.13f, 0.601f);

        // 画面カラー透明→黄色メソッドをコール
        StartCoroutine(ColorUp(startColor, endColor, imageCompo));

        // 画面カラー黄色→透明メソッドをコール
        StartCoroutine(ColorDown(startColor, endColor, imageCompo));

        // 画面カラーゲームオブジェクトを破棄する
        var displayColorCanv = GameObject.Find("Canvas_DisplayColor");
        if (!isFading) Destroy(displayColorCanv);
    }

    /// <summary>
    /// 画面カラー透明→黄色変更メソッド
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="imageCompo"></param>
    /// <returns></returns>
    private IEnumerator ColorUp(Color a, Color b, Image imageCompo)
    {
        // フェード処理開始を宣言
        isFading = true;

        // 経過時間を初期化
        elapsedSec = 0;
        while (true)
        {
            if (b == imageCompo.color)
            {
                // Lerp処理が終了したらループを抜ける
                break;
            }
            // アルファ値をLerp
            elapsedSec += Time.deltaTime;
            imageCompo.color = Color.Lerp(a, b, elapsedSec * fadeTime);
            yield return null;
        }
    }

    /// <summary>
    /// 画面カラー黄色→透明変更メソッド
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="imageCompo"></param>
    /// <returns></returns>
    private IEnumerator ColorDown(Color a, Color b, Image imageCompo)
    {
        // BattleStart画像表示中は処理を停止する
        yield return new WaitForSeconds(6.0f);

        // 経過時間を初期化
        elapsedSec = 0;
        while (true)
        {
            if (a == imageCompo.color)
            {
                // Lerp処理が終了したらループを抜ける
                yield return new WaitForSeconds(0.6f);
                // フェード終了を宣言
                isFading = false;
                break;
            }
            // アルファ値をLerp
            elapsedSec += Time.deltaTime;
            imageCompo.color = Color.Lerp(b, a, elapsedSec * fadeTime);
            yield return null;
        }
    }
}
