using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//　関数名：フェードクラス
//　機能：シーン切り替え時にフェードイン / フェードアウトさせる
//　継承：MonoBehaviour
//　種別：通常クラス
//　アタッチ先：画面フェード専用Canvas（Canvas_FadeDisplay）
//　保持メソッド：
//　リダイレクト：なし
//
//　詳細：
//　　　　シーン切り替え時にフェードイン / フェードアウトさせる
//
//  呼び出し例：
//
//　履歴：
//　　　　14.12.12 初版
//
////////////////////////////////////////////////////////////////////////////////////////
public class FadeTextureDisplay : MonoBehaviour
{
    /// <summary>使用するフェード画像のオブジェクト名</summary>
    public string chooseImageName;
    /// <summary>フェードアウト処理を行う秒</summary>
    public float fadeoutSec;
    /// <summary>フェードアウト処理後にその状態を維持する秒</summary>
    public float fadeoutKeepSec;
    /// <summary>フェードイン処理を行う秒</summary>
    public float fadeinSec;
    /// <summary>フェードイン処理後にその状態を維持する秒</summary>
    public float fadeinKeepSec;
    /// <summary>フェード画像Imageをアタッチしているオブジェクト配列</summary>
    private GameObject[] allFadeImages;
    /// <summary>フェード画像</summary>
    private Image fadeImage;
    /// <summary>アルファ値（初期値：0）</summary>
    [SerializeField]
    private Color initialAlphaVal;
    /// <summary>アルファ値（到達値：1）</summary>
    [SerializeField]
    private Color reachingAlphaVal;
    /// <summary>現在のアルファ値</summary>
    private Color nowAlphaVal = Color.white;
    /// <summary>経過した秒</summary>
    private float elapsedSec = 0;
    /// <summary>フェード有無</summary>
    private bool isFading = true;

    void Awake()
    {
        // 全シーンで使用する機能のため永続オブジェクトにする
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // ★これだと複数のフェード画像から一つを指定できないからボツ
//      fadeImage = GetComponentInChildren<Image>();

        // 子オブジェクトの中から指定したオブジェクト名のImageコンポを取得
        // ★これだと指定画像以外を非アクティブ化する処理が別途必要だからボツ
//      fadeImage = this.transform.Find(fadeImageName).GetComponentInChildren<Image>();

        // 全てのフェード画像オブジェクトを取得
        allFadeImages = GameObject.FindGameObjectsWithTag("FadeDisplayImages");
        // とりあえず取得した全てのフェード画像オブジェクトを非アクティブ化する
        AllFadeImagesStop();
        // その後、fadeImageNameにより指定されたフェード画像オブジェクトをアクティブ化する
        FadeImageSet();
    }

    /// <summary>
    /// フェード画像設定メソッド
    /// </summary>
    public void FadeImageSet()
    {
        if ("" == chooseImageName)
        {
            // フェード画像名が設定されていない場合のエラーメッセージログを出して処理を停止する
            Debug.Log("Unset ChooseImageName.");
            return;
        }

        foreach (GameObject fadeImageObject in allFadeImages)
        {
            if (chooseImageName == fadeImageObject.name)
            {
                // 指定されたフェード画像名のオブジェクトをアクティブ化しImageコンポを取得する
                fadeImageObject.SetActive(true);
                fadeImage = fadeImageObject.GetComponent<Image>();
                return;
            }
        }
    }

    /// <summary>
    /// フェード画像ランダム設定メソッド
    /// </summary>
    public void FadeImageRandomSet()
    {
        // フェード画像配列から使用するフェード画像をランダムで決定する
        int fadeImagesIndex = Random.Range(0, allFadeImages.Length +1);

        // 決定されたフェード画像オブジェクトをアクティブ化しImageコンポを取得する
        allFadeImages[fadeImagesIndex].SetActive(true);
        fadeImage = allFadeImages[fadeImagesIndex].GetComponent<Image>();
    }

    /// <summary>
    /// 全フェード画像オブジェクト停止メソッド
    /// </summary>
    public void AllFadeImagesStop()
    {
        foreach (GameObject fadeImageObject in allFadeImages)
        {
            // 全てのフェード画像オブジェクトを非アクティブ化する
            fadeImageObject.SetActive(false);

            //こっちはImageコンポのみ停止。コンポだけ止めてオブジェクトを生かしておく理由がないのでボツ。
//          fadeImageObject.GetComponent<Image>().enabled = false;
        }
    }

    void Update()
    {
        if (isFading)
        {
            if (null == fadeImage)
            {
                // Imageをアタッチしていない場合はフェード不可のため本スクリプトを停止する
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
                fadeImage.color = Color.Lerp(initialAlphaVal, reachingAlphaVal, elapsedSec / fadeoutSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec)
            {
                if (fadeImage.color != reachingAlphaVal)
                {
                    // フェードアウト維持時間中に現アルファ値が到達値になっていない場合はアルファ値を到達値にする
                    fadeImage.color = reachingAlphaVal;
                }
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec)
            {
                // フェードイン時間中はfromColorへと徐々に変化させる
                fadeImage.color = Color.Lerp(reachingAlphaVal, initialAlphaVal, (elapsedSec - fadeoutSec - fadeoutKeepSec) / fadeinSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec + fadeinKeepSec)
            {
                if (fadeImage.color != initialAlphaVal)
                {
                    // フェードイン維持時間中に現アルファ値が初期値になっていない場合はアルファ値を初期値にする
                    fadeImage.color = initialAlphaVal;
                }
            }
            else
            {
                // 全ての処理が完了したら経過時間を0に戻してフェードアウトから再度実行する
                elapsedSec = 0;
            }
        }
    }
}
