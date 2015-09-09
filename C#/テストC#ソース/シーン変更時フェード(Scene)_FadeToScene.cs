using UnityEngine;
using System.Collections;

////////////////////////////////////////////////////////////////////////////////////////
//　関数名：フェードクラス
//　機能：シーン切り替え時にフェードイン / フェードアウトさせる
//　継承：MonoBehaviour
//　種別：通常クラス
//　アタッチ先：メインカメラオブジェクト
//　保持メソッド：
//　リダイレクト：なし
//
//　詳細：
//　　　　シーン切り替え時にフェードイン / フェードアウトさせる
//
//  呼び出し例：
//  　　　　　　Scene遷移実施
//  　　　　　　ﾌｪｰﾄﾞｱｳﾄ時間、ﾌｪｰﾄﾞ中待機時間、ﾌｪｰﾄﾞｲﾝ時間、ｶﾗｰ、遷移先Pos情報(Vector3)、遷移先ｼｰﾝ
//            　this.GetComponent<FadeToPos>().FadeOut(0.3f, 0.2f, 0.3f, Color.black, nextScene);
//
//　履歴：
//　　　　14.12.12 初版
//　　　　14.12.13 衝突による再判定バグのため改修
//
////////////////////////////////////////////////////////////////////////////////////////
public class FadeToScene : MonoBehaviour
{

    // フェード関係テクスチャ変数～ここから～
    private Texture2D texture;
    private string sequence = null;
    private Color from;
    private Color to;
    private Color now;
    private float time;
    private float fadewait;
    private float fadeinTime;
    private string sceneName;
    // フェード関係テクスチャ変数～ここまで～

    // =====================================================
    // GUI描画時に呼ばれる
    // =====================================================
    void OnGUI()
    {
        if (now.a != 0)
        {
            GUI.color = now;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        }
    }

    // =====================================================
    // テクスチャ描画メソッド
    // =====================================================
    public void TextureLoad()
    {

        // フェード関係テクスチャ
        texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
    }

    // =====================================================
    // ①
    // フェードアウト開始メソッド
    // フェードアウト時間、フェード中待機時間、フェードイン時間、フェードカラー、遷移先シーン名
    // =====================================================
    public void FadeOut(float t_time, float f_wait, float a_time, Color t_color, string toScene)
    {
        TextureLoad();
        to = from = t_color;
        from.a = 0;
        time = t_time;
        fadewait = f_wait;
        sceneName = toScene;
        fadeinTime = a_time;
        StartSequence("FadeUpdate");
    }

    // =====================================================
    // ②
    // フェードイン,アウトコルーチン起動メソッド
    // フェードイン / フェードアウトのどちらかの
    // メソッドを起動する。
    // どちらを起動するかは引数の文字列により分岐する。
    // =====================================================
    public void StartSequence(string function_name)
    {
        if (sequence != null)
        {
            StopCoroutine(sequence);
            sequence = null;
        }
        sequence = function_name;
        StartCoroutine(sequence);
    }

    // =====================================================
    // ③
    // フェードアウト実施コルーチン
    // フェードアウトを実施する
    // =====================================================
    public IEnumerator FadeUpdate()
    {
        float now_time = 0;
        while (0 < time && now_time < time)
        {
            now_time += Time.deltaTime;
            now = Color.Lerp(from, to, now_time / time);
            yield return 0;
        }

        // フェードアウト完了時のカラーを現カラーに設定
        now = to;

        // フェードイン前の一時停止メソッドをコール
        StartCoroutine(BeforeFadeIn(fadewait, fadeinTime, sceneName));
    }

    // =================================================
    // ④
    // フェードイン前一時停止メソッド
    // フェード中停止時間、フェードイン時間、遷移先シーン
    // =================================================
    public IEnumerator BeforeFadeIn(float waittime, float fadein, string scene)
    {
        // 暗転完了後、一時停止
        yield return new WaitForSeconds(waittime);

        // シーン遷移実施
        Application.LoadLevel(scene);

        // 一時停止
        yield return new WaitForSeconds(0.1f);

        // フェードアウト後はフェードインするため
        // フェードインメソッドをコール
        FadeIn(fadein, Color.black);
    }

    // =====================================================
    // ⑤
    // フェードイン開始メソッド
    // コール時に指定したフェードイン時間, フェードカラー
    // を設定し、FadeUpdateFromFadeInメソッドを
    // StartSequenceメソッドからコールする
    // =====================================================
    public void FadeIn(float t_time, Color t_color)
    {
        // 指定したフェードカラーとフェード時間を設定
        to = from = t_color;
        to.a = 0;
        time = t_time;
        // フェードインメソッドをコール
        StartSequence("FadeUpdateFromFadeIn");
        return;

    }

    // =====================================================
    // ⑥
    // フェードイン実施コルーチン
    // フェードインを実施する
    // =====================================================
    public IEnumerator FadeUpdateFromFadeIn()
    {
        float now_time = 0;
        while (0 < time && now_time < time)
        {
            now_time += Time.deltaTime;
            now = Color.Lerp(from, to, now_time / time);
            yield return 0;
        }

        // フェードアウト完了時のカラーを現カラーに設定
        now = to;
    }
}

