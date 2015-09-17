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
public class FadeTextureDisplay : MonoBehaviour
{
    /// <summary>フェードテクスチャ（インスペクタから画像を指定）</summary>
    [SerializeField]
    private Texture2D texture;
    /// <summary>カラー（初期値）</summary>
    [SerializeField]
    private Color initialVal;
    /// <summary>カラー（到達値）</summary>
    [SerializeField]
    private Color ReachingVal;
    /// <summary>現在のカラー</summary>
    private Color nowColorVal = Color.white;
    /// <summary>フェードアウト処理を行う秒</summary>
    [SerializeField]
    private float fadeoutSec;
    /// <summary>フェードアウト処理後にその状態を維持する秒</summary>
    [SerializeField]
    private float fadeoutKeepSec;
    /// <summary>フェードイン処理を行う秒</summary>
    [SerializeField]
    private float fadeinSec;
    /// <summary>フェードイン処理後にその状態を維持する秒</summary>
    [SerializeField]
    private float fadeinKeepSec;
    /// <summary>経過した秒</summary>
    private float elapsedSec = 0;
    /// <summary>フェード有無</summary>
    private bool isFading = true;

    // =====================================================
    // GUI描画時に呼ばれる
    // この中のものは最手前に表示される。
    // =====================================================
    void OnGUI()
    {
        // 描画位置、描画サイズ、スケーリングモードを指定してフェード画像を描画する
        GUI.color = nowColorVal;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.ScaleToFit);
    }

    void Update()
    {
        if (isFading)
        {
            if (null == texture)
            {
                // Textureをアタッチしていない場合はフェード不可のため本スクリプトを停止する
                this.enabled = false;
            }
            if (0 >= fadeoutSec || 0 >= fadeinSec)
            {
                // フェードアウト及びフェードインを行う秒の設定が不正な場合はフェード不可のため本スクリプトを停止する
                this.enabled = false;
            }

            // 経過時間を測定
            elapsedSec += Time.deltaTime;

            if (elapsedSec < fadeoutSec)
            {
                // フェードアウト時間中はtoColorへと徐々に変化させる
                nowColorVal = Color.Lerp(initialVal, ReachingVal, elapsedSec / fadeoutSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec)
            {
                // フェードアウト維持時間中はtoColorの色を維持する
                nowColorVal = ReachingVal;
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec)
            {
                // フェードイン時間中はfromColorへと徐々に変化させる
                nowColorVal = Color.Lerp(ReachingVal, initialVal, (elapsedSec - fadeoutSec - fadeoutKeepSec) / fadeinSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec + fadeinKeepSec)
            {
                // フェードイン維持時間中はfromColorの色を維持する
                nowColorVal = initialVal;
            }
            else
            {
                // 全ての処理が完了したら経過時間を0に戻してフェードアウトから再度実行する
                elapsedSec = 0;
            }
        }
    }
}

