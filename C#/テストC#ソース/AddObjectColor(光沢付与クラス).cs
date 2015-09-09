using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//　関数名：オブジェクトカラーPalfxクラス
//　機能：オブジェクトのカラーを変更するmugenで言うPalfx
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
public class AddObjectColor : MonoBehaviour
{
    private Texture2D texture;
    private string sequence = null;
    private Color from = Color.white;
    private Color to;
    private Color now;
    private Color setCloro;
    private float time;
    private float fadewait;
    private float fadeinTime;
    private GameObject targetObject;
    private Image targetImage;

    public void Update()
    {
        if (targetImage) targetImage.color = now;   // ターゲットオブジェクトのカラーをPalfx
    }

    // =====================================================
    // ①
    // Palfx開始メソッド
    // フェードアウト時間、フェード中待機時間、フェードイン時間、フェードカラー、対象のGameObject
    // =====================================================
    public void PalfxStart(float t_time, float f_wait, float a_time, Color t_color, GameObject go)
    {
        to = setCloro = t_color;
//        from.a = 0;
        time = t_time;
        fadewait = f_wait;
        targetObject = go;
        targetImage = targetObject.GetComponent<Image>();
        fadeinTime = a_time;
        StartCoroutine("PalfxBegin");
    }

    // =====================================================
    // ③
    // 光沢増加コルーチン
    // 光沢増加処理を実施する
    // =====================================================
    public IEnumerator PalfxBegin()
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
        StartCoroutine(BeforePalfx(fadewait, fadeinTime, targetObject));
    }

    // =================================================
    // ④
    // フェードイン前一時停止メソッド
    // フェード中停止時間、フェードイン時間、遷移先シーン
    // =================================================
    public IEnumerator BeforePalfx(float waittime, float fadein, GameObject go)
    {
        // 暗転完了後、一時停止
        yield return new WaitForSeconds(waittime);

        // 一時停止
        yield return new WaitForSeconds(0.1f);

        // フェードアウト後はフェードインするため
        // フェードインメソッドをコール
        BeforePalfx(fadein, setCloro);
    }

    // =====================================================
    // ⑤
    // フェードイン開始メソッド
    // コール時に指定したフェードイン時間, フェードカラー
    // を設定し、FadeUpdateFromFadeInメソッドを
    // StartSequenceメソッドからコールする
    // =====================================================
    public void BeforePalfx(float t_time, Color t_color)
    {
        // 指定したフェードカラーとフェード時間を設定
        to = t_color;
//        to.a = 0;
        time = t_time;
        // フェードインメソッドをコール
        StartCoroutine("Palfx");
        return;
    }

    // =====================================================
    // ⑥
    // フェードイン実施コルーチン
    // フェードインを実施する
    // =====================================================
    public IEnumerator Palfx()
    {
        float now_time = 0;
        while (0 < time && now_time < time)
        {
            now_time += Time.deltaTime;
            now = Color.Lerp(to, from, now_time / time);
            yield return 0;
        }

        // フェードアウト完了時のカラーを現カラーに設定
        now = from;
    }

    // =====================================================
    // オブジェクトカラー変更メソッド
    // 入力されたオブジェクトのカラーを変更する
    // とりあえず使わない
    // =====================================================
    private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    {

        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            foreach (Material material in targetRenderer.materials)
            {
                material.color = color;
            }
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }

    }
}

