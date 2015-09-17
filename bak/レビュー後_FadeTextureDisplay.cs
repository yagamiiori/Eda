using UnityEngine;
using UnityEngine.UI;

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
    // TODO; ★fadeImageがあるから不要
    /// <summary>使用するフェード画像のオブジェクト名</summary>
    //public string chooseImageName;
    /// <summary>フェードイン処理を行う秒</summary>
    public float fadeinSec = 1;
    /// <summary>フェードイン処理後にその状態を維持する秒</summary>
    public float fadeinKeepSec = 1;
    /// <summary>フェードアウト処理を行う秒</summary>
    public float fadeoutSec = 1;
    /// <summary>フェードアウト処理後にその状態を維持する秒</summary>
    public float fadeoutKeepSec = 1;
    // TODO: ★フィールドである理由が分からない、わざわざ広域なスコープにしなくてもグローバル変数で十分事が足りる
    /// <summary>フェード画像Imageをアタッチしているオブジェクト配列</summary>
    //private GameObject[] allFadeImages;
    // TODO: ★このImageにフェードしたい画像を設定するのならchooseImageNameは不要
    /// <summary>フェード画像</summary>
    public Image fadeImage;
    // TODO: ★スクリプトのEnable設定で制御するのでこのフラグは不要
    ///// <summary>フェード有無</summary>
    //public bool isFading = true;
    /// <summary>フェード画像のランダム表示有無。<br></br>ランダム表示の場合はインスペクタで設定したfadeImageではなく配下のImageの何れかがランダム選ばれる</summary>
    public bool isRandom = false;
    /// <summary>アルファ値（初期値：0）</summary>
    [SerializeField]
    private Color initialAlphaVal = new Color(1f, 1f, 1f, 0f);
    /// <summary>アルファ値（到達値：1）</summary>
    [SerializeField]
    private Color reachingAlphaVal = new Color(1f, 1f, 1f, 1f);
    // ★使われてないね
    ///// <summary>現在のアルファ値</summary>
    //private Color nowAlphaVal = Color.white;
    /// <summary>経過した秒</summary>
    private float elapsedSec = 0;

    void Awake()
    {
        // 全シーンで使用する機能のため永続オブジェクトにする
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    /// <summary>
    /// フェード画像表示を開始する
    /// </summary>
    [ContextMenu("FadeDisplayStart()")]
    public void FadeDisplayStart()
    {
        // フェード画像を全て取得
        Image[] images = this.GetComponentsInChildren<Image>(true);

        // 一つの画像のみフェード表示したいので配下の画像を一旦全てDisableに設定
        AllFadeImagesDisable(images);

        // ランダム表示の場合は配下の画像から一つを選択
        if (isRandom)
        {
            fadeImage = GetFadeImageRandomly(images);
        }

        // フェード画像のみ表示を有効にするためEnableに設定
        if (null != fadeImage)
        {
            Debug.Log("フェード画像 = " + fadeImage.name);
            fadeImage.enabled = true;
            //　TODO: ★設定の手間を省くなら、ここでfadeImageのRectTransformの設定を変更するのも有り
        }

        elapsedSec = 0;
        this.enabled = true;
    }

    /// <summary>
    /// フェード時に複数の画像が表示されないように全てのフェード画像を一旦Disableに設定
    /// </summary>
    /// <param name="images">フェード画像のImage型配列</param>
    private void AllFadeImagesDisable(Image[] images)
    {
        // 引数が不正
        if (null == images)
        {
            return;
        }

        // 全てのフェード画像オブジェクトをDisableに設定
        foreach (Image image in images)
        {
            image.enabled = false;
        }
    }

    /// <summary>
    /// フェード画像のランダム表示時に、対象となる画像を無作為に選択して返す。
    /// ただし、引数が不正な場合はnullを返す
    /// </summary>
    /// <param name="images">フェード画像のImage型配列</param>
    /// <returns>無作為に選択されたフェード画像。引数が不正の場合はnullを返す</returns>
    private Image GetFadeImageRandomly(Image[] images)
    {
        // 引数が不正
        if (null == images)
        {
            return null;
        }

        return images[Random.Range(0, images.Length - 1)];
    }

    void Update()
    {
        if (null == fadeImage)
        {
            // Imageをアタッチしていない場合はフェード不可のため本スクリプトを停止する
            Debug.LogWarning("フェード画像が存在しません。[null == fadeImage]");
            this.enabled = false;
        }

        if (0 >= fadeinSec)
        {
            // フェードインを行う秒の設定が不正な場合はフェード不可のため本スクリプトを停止
            Debug.LogWarning("フェードイン時間の設定が不正。[0 >= fadeinSec]");
            this.enabled = false;
        }

        if (0 >= fadeoutSec)
        {
            // フェードアウトを行う秒の設定が不正な場合はフェード不可のため本スクリプトを停止
            Debug.LogWarning("フェードアウト時間の設定が不正。[0 >= fadeoutSec]");
            this.enabled = false;
        }

        // 経過時間を求める
        elapsedSec += Time.deltaTime;

        if (elapsedSec < fadeinSec)
        {
            // フェードイン時間中はフェード画像を徐々に表示する
            fadeImage.color = Color.Lerp(initialAlphaVal, reachingAlphaVal, elapsedSec / fadeinSec);
        }
        else if (elapsedSec < fadeinSec + fadeinKeepSec)
        {
            // フェードイン維持時間中はフェード画像を表示する
            if (fadeImage.color != reachingAlphaVal)
            {
                fadeImage.color = reachingAlphaVal;
            }
        }
        else if (elapsedSec < fadeinSec + fadeinKeepSec + fadeoutSec)
        {
            // フェードアウト時間中はフェード画像を徐々に消す
            fadeImage.color = Color.Lerp(reachingAlphaVal, initialAlphaVal, (elapsedSec - fadeinSec - fadeinKeepSec) / fadeoutSec);
        }
        else if (elapsedSec < fadeinSec + fadeinKeepSec + fadeoutSec + fadeoutKeepSec)
        {
            // フェードアウト維持時間中はフェード画像を消す
            if (fadeImage.color != initialAlphaVal)
            {
                fadeImage.color = initialAlphaVal;
            }
        }
        else
        {
            // 画面のフェード処理終了
            this.enabled = false;
        }
    }
}
